using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Core.Helpers;
using Core.Helpers.DependencyHelpers;

namespace Reactions.Triggers
{
	/// <summary>
	/// FINAL DATA TRIGGER CLASS
	/// </summary>
	[XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
	public class ReactiveDataTrigger : ReactiveTriggerBase
	{
		private bool _deferMarkupExtensionResolve;
		private object _targetObject;
		private XamlSetMarkupExtensionEventArgs _eventArgs;


		public static readonly DependencyProperty BindingProperty = DP.Register(
			new Meta<ReactiveDataTrigger, object>(null, onBindingChanged));

		//public static readonly DependencyProperty ConditionProperty = DP.Register(
		//	new Meta<ReactiveDataTrigger, ConditionBase>());

		public static readonly DependencyProperty ValueProperty = DP.Register(
			new Meta<ReactiveDataTrigger, object>(null, onValueChanged));


		public object Value
		{
			get { return GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		//public ConditionBase Condition
		//{
		//	get { return (ConditionBase) GetValue(ConditionProperty); }
		//	set { SetValue(ConditionProperty, value); }
		//}
		public object Binding
		{
			get { return GetValue(BindingProperty); }
			set { SetValue(BindingProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			if (_deferMarkupExtensionResolve)
			{
				ReceiveMarkupExtension(_targetObject, _eventArgs);
			}
		}


		private static void onBindingChanged(ReactiveDataTrigger i, DPChangedEventArgs<object> e)
		{
			onValueChanged(i, e);
		}

		private static void onValueChanged(ReactiveDataTrigger i, DPChangedEventArgs<object> e)
		{
			if (!i.IsAssociated)
				return;

			if (Compare(i.Binding, i.Value))
			{
				i.Execute();
			}
			else
			{
				
			}

			//DataBindingHelper.RefreshDataBindingOnReactions(i.);

		}

		private static bool Compare(object left, object right)
		{
			var targetType = left.GetType();
			var converter = TypeDescriptor.GetConverter(targetType);
			var convertedRight = converter.ConvertFrom(right);
			return left.Equals(convertedRight);
		}


		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			var reactiveDataTrigger = targetObject as ReactiveDataTrigger;
			if (reactiveDataTrigger == null || eventArgs.Member.Name != nameof(Binding))
				return;

			if (!reactiveDataTrigger.IsHosted)
			{
				reactiveDataTrigger._targetObject = targetObject;
				reactiveDataTrigger._eventArgs = eventArgs;
				reactiveDataTrigger._deferMarkupExtensionResolve = true;
				return;
			}
			reactiveDataTrigger._targetObject = null;
			reactiveDataTrigger._eventArgs = null;
			reactiveDataTrigger._deferMarkupExtensionResolve = false;

			if (targetObject == null)
				throw new ArgumentNullException(nameof(targetObject));
			if (eventArgs == null)
				throw new ArgumentNullException(nameof(eventArgs));


			var markupExtension = eventArgs.MarkupExtension;
			if (markupExtension is Binding)
			{
				var binding = markupExtension as Binding;
				var relativeSource = binding.RelativeSource;
				if (relativeSource != null)
				{
					if (relativeSource.Mode == RelativeSourceMode.FindAncestor)
					{
						var frameworkElement = reactiveDataTrigger.AssociatedObject as FrameworkElement;
						if (frameworkElement == null)
							throw new NotSupportedException();

						var resolvedVisualAncestor = frameworkElement.FindParent(relativeSource.AncestorType, relativeSource.AncestorLevel);

						var adjustedBinding = new Binding
						{
							Source = resolvedVisualAncestor,
							Path = binding.Path
						};
						BindingOperations.SetBinding(reactiveDataTrigger, BindingProperty, adjustedBinding);
						eventArgs.Handled = true;
					}
				}
			}
			else
			{
				//if (!(markupExtension is DynamicResourceExtension) && !(markupExtension is BindingBase))
				//	return;
				throw new NotImplementedException();
			}
		}
	}
}
