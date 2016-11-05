using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Core.Helpers.DependencyHelpers;

namespace Reactions.Triggers
{
	/*
	DO tHIS... in stylehelper
    internal static object GetDataTriggerValue(UncommonField<HybridDictionary[]> dataField, DependencyObject container, BindingBase binding)
    {
      dataField.GetValue(container);
      HybridDictionary hybridDictionary = StyleHelper.EnsureInstanceData(dataField, container, InstanceStyleData.InstanceValues);
      BindingExpressionBase bindingExpressionBase = (BindingExpressionBase) hybridDictionary[(object) binding];
      if (bindingExpressionBase == null)
      {
        bindingExpressionBase = BindingExpressionBase.CreateUntargetedBindingExpression(container, binding);
        hybridDictionary[(object) binding] = (object) bindingExpressionBase;
        if (dataField == StyleHelper.StyleDataField)
          bindingExpressionBase.ValueChanged += new EventHandler<BindingValueChangedEventArgs>(StyleHelper.OnBindingValueInStyleChanged);
        else if (dataField == StyleHelper.TemplateDataField)
        {
          bindingExpressionBase.ResolveNamesInTemplate = true;
          bindingExpressionBase.ValueChanged += new EventHandler<BindingValueChangedEventArgs>(StyleHelper.OnBindingValueInTemplateChanged);
        }
        else
          bindingExpressionBase.ValueChanged += new EventHandler<BindingValueChangedEventArgs>(StyleHelper.OnBindingValueInThemeStyleChanged);
        bindingExpressionBase.Attach(container);
      }
      return bindingExpressionBase.Value;
    }
*/
//TODO look at internal StyleHelper
	[XamlSetMarkupExtension("ReceiveMarkupExtension")]
	public class SimpleDataTrigger : ReactiveTriggerBase
	{
		private BindingBase binding;
		public BindingBase Binding
		{
			get { return binding; }
			set
			{
				binding = value;
				onBindingChanged();
				if (deferValueConversion)
					AttemptInterpretValue();
			}
		}

		private bool deferValueConversion;
		private object unresolvedValue;
		private object actualValue;

		public object Value
		{
			get
			{
				if (actualValue == null)
					AttemptInterpretValue();
				return actualValue;
			}
			set
			{
				var oldValue = unresolvedValue;
				unresolvedValue = value;
				OnValueSet(oldValue, unresolvedValue);
				AttemptInterpretValue();
			}
		}

		private TypeConverter valueTypeConverter;
		protected TypeConverter ValueTypeConverter
		{
			get
			{
				if (BoundValue == null)
					return null;
				if (valueTypeConverter == null)
					valueTypeConverter = TypeDescriptor.GetConverter(BoundValue.GetType());
				return valueTypeConverter;
			}
			set { valueTypeConverter = value; }
		}


		public static readonly DependencyProperty BoundValueProperty = DP.Register(
			new Meta<SimpleDataTrigger, object>(null, onBoundValueChanged));

		public object BoundValue
		{
			get { return GetValue(BoundValueProperty); }
			set { SetValue(BoundValueProperty, value); }
		}

		private void onBindingChanged()
		{
			SetBinding();
		}

		private static void onBoundValueChanged(SimpleDataTrigger i, DPChangedEventArgs<object> e)
		{
			i.UpdateTriggerState();
		}

		protected void AttemptInterpretValue()
		{
			deferValueConversion = false;
			if (BoundValue == null)
			{
				deferValueConversion = true;
				return;
			}
			try
			{
				actualValue = ValueTypeConverter.ConvertFrom(unresolvedValue);
			}
			catch
			{
				actualValue = unresolvedValue;
			}
			OnValueInterpreted();
		}

		protected void OnValueSet(object oldValue, object newValue)
		{
		}

		protected void OnValueInterpreted()
		{
			if (Binding != null && actualValue != null)
			{
				UpdateTriggerState();
			}
		}

		protected void SetBinding()
		{
			 if (Binding != null)
				BindingOperations.SetBinding(this, BoundValueProperty, binding);
		}

		protected void UpdateTriggerState()
		{
			if (Value == BoundValue)
			{
				Execute();
			}
			else
			{

			}
		}
		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			if (targetObject == null)
				throw new ArgumentNullException(nameof(targetObject));
			if (eventArgs == null)
				throw new ArgumentNullException(nameof(eventArgs));
			var dataTrigger = targetObject as SimpleDataTrigger;
			if (dataTrigger != null && eventArgs.Member.Name == "Binding" && eventArgs.MarkupExtension is BindingBase)
			{
				dataTrigger.Binding = eventArgs.MarkupExtension as BindingBase;
				eventArgs.Handled = true;
			}
			else
				eventArgs.CallBase();
		}
	}
}
