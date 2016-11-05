using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xaml;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.DependencyHelpers;
using Reactions.Conditions;

namespace Reactions.Triggers
{
	[XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
	public class ReactiveComplexDataTrigger : ReactiveTriggerBase
	{
		private bool _deferMarkupExtensionResolve;
		private object _targetObject;
		private XamlSetMarkupExtensionEventArgs _eventArgs;


		public static readonly DependencyProperty BindingProperty = DP.Register(
			new Meta<ReactiveComplexDataTrigger, object>(null, onBindingChanged));

		public static readonly DependencyProperty ConditionProperty = DP.Register(
			new Meta<ReactiveComplexDataTrigger, ConditionBase>());
		public ConditionBase Condition
		{
			get { return (ConditionBase)GetValue(ConditionProperty); }
			set { SetValue(ConditionProperty, value); }
		}
		//public static readonly DependencyProperty ConditionProperty = DP.Register(
		//	new Meta<ReactiveDataTrigger, ConditionBase>());

		//public static readonly DependencyProperty ValueProperty = DP.Register(
		//	new Meta<ReactiveComplexDataTrigger, object>(null, onValueChanged));


		//public object Value
		//{
		//	get { return GetValue(ValueProperty); }
		//	set { SetValue(ValueProperty, value); }
		//}
		////public ConditionBase Condition
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


		private static void onBindingChanged(ReactiveComplexDataTrigger i, DPChangedEventArgs<object> e)
		{
			onValueChanged(i, e);
		}

		private static void onValueChanged(ReactiveComplexDataTrigger i, DPChangedEventArgs<object> e)
		{
			if (!i.IsAssociated)
				return;

			//if (i.Binding == null)
			//{
			if (i.Condition.Evaluate(i.Binding))
			{
				i.Execute();
			}
			else
			{

			}

			//}

			//DataBindingHelper.RefreshDataBindingOnReactions(i.);

		}



		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			var reactiveDataTrigger = targetObject as ReactiveComplexDataTrigger;
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

						var ancestorLevel = relativeSource.AncestorLevel;
						DependencyObject resolvedVisualAncestor;

						if (frameworkElement.GetType() == relativeSource.AncestorType && ancestorLevel == 1)
						{
							resolvedVisualAncestor = frameworkElement;
						}
						else
						{
							resolvedVisualAncestor = frameworkElement.FindParent(relativeSource.AncestorType, ancestorLevel);
						}

						var adjustedBinding = new Binding
						{
							Source = resolvedVisualAncestor,
							Path = binding.Path
						};
						BindingOperations.SetBinding(reactiveDataTrigger, BindingProperty, adjustedBinding);
						eventArgs.Handled = true;
					}
				}
				else if (!binding.ElementName.IsNullOrWhiteSpace())
				{
					var xamlNameResolver = (IXamlNameResolver)eventArgs.ServiceProvider
						.GetService(typeof(IXamlNameResolver));

					var resolvedElement = xamlNameResolver.Resolve(binding.ElementName);
					if (resolvedElement == null)
						throw new NullReferenceException(
							$"Cannot find name \'{binding.ElementName}\' in the current context.");

					var adjustedBinding = new Binding
					{
						Source = resolvedElement,
						Path = binding.Path
					};
					BindingOperations.SetBinding(reactiveDataTrigger, BindingProperty, adjustedBinding);
					eventArgs.Handled = true;
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
/*				
var xnp = eventArgs.ServiceProvider.GetService(typeof (IXamlNameProvider));
var xowf = eventArgs.ServiceProvider.GetService(typeof (IXamlObjectWriterFactory));
					var xtr = eventArgs.ServiceProvider.GetService(typeof (IXamlTypeResolver));

					var all = ((IXamlNameResolver) xnr).GetAllNamesAndValuesInScope();*/
