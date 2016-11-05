using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Core.Helpers;
using Reactions.Core;

namespace Reactions.Triggers
{
	//TODO does this work...? looks like it doesnt
	[XamlSetMarkupExtension("ReceiveMarkupExtension")]
	public class DataTrigger4 : ReactiveTriggerBase
	{
		private object _localValue;
		private IServiceProvider _serviceProvider;

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
				if (_localValue == null)
					return null;
				//TODO what is going on here
				if (valueTypeConverter == null)
					valueTypeConverter = TypeDescriptor.GetConverter("OLD OLD OLD OLD".GetType());
				return valueTypeConverter;
			}
			set { valueTypeConverter = value; }
		}

		private void onBindingChanged()
		{
			if (_serviceProvider == null)
				throw new NotSupportedException("service provider null");
			//return (object)this;



		}

		protected void AttemptInterpretValue()
		{
			deferValueConversion = false;
			if (_localValue == null)
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
				//UpdateTriggerState();
			}
		}


		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			if (targetObject == null)
				throw new ArgumentNullException(nameof(targetObject));
			if (eventArgs == null)
				throw new ArgumentNullException(nameof(eventArgs));
			var dataTrigger = targetObject as DataTrigger4;
			if (dataTrigger != null && eventArgs.Member.Name == "Binding" && eventArgs.MarkupExtension is BindingBase)
			{
				dataTrigger._serviceProvider = eventArgs.ServiceProvider;
				dataTrigger.Binding = (BindingBase)eventArgs.MarkupExtension;
				dataTrigger.ResolveBinding();
				eventArgs.Handled = true;
			}
			else
				eventArgs.CallBase();
		}

		protected void ResolveBinding()
		{
			

			//__Helper.CheckCanReceiveMarkupExtension(Binding, _serviceProvider, out targetDependencyObject, out targetDependencyProperty);
			//if (targetDependencyObject == null || targetDependencyProperty == null)
			//	throw new NotSupportedException("target dp/obj null");
			// return (object) this;
			//   return (object) this.CreateBindingExpression(targetDependencyObject, targetDependencyProperty);
			
			DependencyObject targetDependencyObject;
			DependencyProperty targetDependencyProperty;

			__Helper.GetBindingElementsFromBinding(Binding, _serviceProvider, out targetDependencyObject, out targetDependencyProperty);
			if (targetDependencyObject == null || targetDependencyProperty == null)
				throw new NotSupportedException("target dp/obj null");
			
			var expr = Binding.InvokeInternalMethod<BindingExpressionBase>("CreateBindingExpression",
				targetDependencyObject, targetDependencyProperty);

		}
	}
}
