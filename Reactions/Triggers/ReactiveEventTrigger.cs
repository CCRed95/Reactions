using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.DependencyHelpers;
using Reactions.Core;

namespace Reactions.Triggers
{
	/// <summary>
	/// FINAL EVENT TRIGGER CLASS
	/// </summary>
	[XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
	public partial class ReactiveEventTrigger : ReactiveTriggerBase
	{
		private MethodInfo eventHandlerMethodInfo;

		private bool _deferMarkupExtensionResolve;
		private object _targetObject;
		private XamlSetMarkupExtensionEventArgs _eventArgs;


		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<ReactiveEventTrigger, object>(null, onSourceObjectChanged));

		public static readonly DependencyProperty EventNameProperty = DP.Register(
			new Meta<ReactiveEventTrigger, string>(null, onEventNameChanged));


		public object SourceObject
		{
			get { return GetValue(SourceObjectProperty); }
			set { SetValue(SourceObjectProperty, value); }
		}
		public string EventName
		{
			get { return (string)GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}


		public object Source
		{
			get
			{
				var obj = (object)AssociatedObject;
				if (SourceObject != null)
					obj = SourceObject;
				else if (IsSourceNameSet)
					obj = SourceNameResolver.Object;
				return obj;
			}
		}

		protected bool IsSourceNameSet
		{
			get
			{
				if (string.IsNullOrEmpty(SourceName))
					return ReadLocalValue(SourceNameProperty) != DependencyProperty.UnsetValue;
				return true;
			}
		}



		private void onEffectiveSourceChanged(object oldSource)
		{
			if (oldSource != null)
			{
				//unreg
			}
			if (!EventName.IsNullOrWhiteSpace())
			{
				if (Source != null)
				{
					registerEventHandler(Source, EventName);
				}
				else
				{
					SourceNameResolver.ResolvedElementChanged += onResolvedElementChanged;
				}
			}
		}

		private void onResolvedElementChanged(object s, NameResolvedEventArgs e)
		{
			SourceNameResolver.ResolvedElementChanged -= onResolvedElementChanged;
			if (EventName == "Loaded")
			{
				Execute();
			}
			else
			{
				onEffectiveSourceChanged(null);
			}
		}

		private static void onSourceObjectChanged(ReactiveEventTrigger i, DPChangedEventArgs<object> e)
		{
			i.onEffectiveSourceChanged(e.OldValue);
		}

		private static void onEventNameChanged(ReactiveEventTrigger i, DPChangedEventArgs<string> e)
		{
			i.onEffectiveSourceChanged(e.OldValue);
		}


		private void registerEventHandler(object obj, string eventName)
		{
			var @event = obj.GetType().GetEvent(eventName);
			if (@event == null)
			{
				//	if (SourceObject != null)
				if (Source != null)
					throw new ArgumentException($"ReactiveEventTrigger EventName \'{eventName}\' not found in \'{obj.GetType().Name}\'");
			}

			else if (!IsValidEvent(@event))
			{
					//if (SourceObject != null)
				if (Source != null)
					throw new ArgumentException($"ReactiveEventTrigger EventName \'{eventName}\' in \'{obj.GetType().Name}\' is not valid.");
			}
			else
			{
				eventHandlerMethodInfo = typeof(ReactiveEventTrigger).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
				@event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, eventHandlerMethodInfo));
			}
		}

		private static bool IsValidEvent(EventInfo eventInfo)
		{
			var eventHandlerType = eventInfo.EventHandlerType;
			if (!typeof(Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
				return false;
			var parameters = eventHandlerType.GetMethod("Invoke").GetParameters();
			if (parameters.Length == 2 && typeof(object).IsAssignableFrom(parameters[0].ParameterType))
				return typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType);
			return false;
		}

		

		protected void OnEventImpl(object s, EventArgs e)
		{
			Execute();
		}


		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			var reactiveEventTrigger = targetObject as ReactiveEventTrigger;
			if (reactiveEventTrigger == null || eventArgs.Member.Name != nameof(SourceObject))
				return;

			if (!reactiveEventTrigger.IsHosted)
			{
				reactiveEventTrigger._targetObject = targetObject;
				reactiveEventTrigger._eventArgs = eventArgs;
				reactiveEventTrigger._deferMarkupExtensionResolve = true;
				return;
			}
			reactiveEventTrigger._targetObject = null;
			reactiveEventTrigger._eventArgs = null;
			reactiveEventTrigger._deferMarkupExtensionResolve = false;

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
						var frameworkElement = reactiveEventTrigger.AssociatedObject as FrameworkElement;
						if (frameworkElement == null)
							throw new NotSupportedException();

						var resolvedVisualAncestor = frameworkElement.FindParent(relativeSource.AncestorType, relativeSource.AncestorLevel);

						var adjustedBinding = new Binding
						{
							Source = resolvedVisualAncestor,
							Path = binding.Path
						};
						BindingOperations.SetBinding(reactiveEventTrigger, SourceObjectProperty, adjustedBinding);
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

	public partial class ReactiveEventTrigger
	{
		private NameResolver SourceNameResolver { get; }
		protected bool IsSourceChangeRegistered { get; set; }
		//private bool _deferTargetNameResolve;


		public static readonly DependencyProperty SourceNameProperty = DP.Register(
			new Meta<ReactiveEventTrigger, string>(null, onSourceNameChanged));

		[DefaultValue(null)]
		[Ambient]
		public string SourceName
		{
			get { return (string)GetValue(SourceNameProperty); }
			set { SetValue(SourceNameProperty, value); }
		}

		public ReactiveEventTrigger()
		{
			SourceNameResolver = new NameResolver();
		}


		protected override void OnAttached()
		{
			base.OnAttached();
			SourceNameResolver.NameScopeReferenceElement = (FrameworkElement)AssociatedObject;
			if (_deferMarkupExtensionResolve)
			{
				ReceiveMarkupExtension(_targetObject, _eventArgs);
			}
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			SourceNameResolver.NameScopeReferenceElement = null;
		}

		private static void onSourceNameChanged(ReactiveEventTrigger i, DPChangedEventArgs<string> e)
		{
			i.SourceNameResolver.Name = e.NewValue;
			i.onEffectiveSourceChanged(e.OldValue);
		}

		//private void resolveEffectiveTarget()
		//{
		//	if (_deferMarkupExtensionResolve)
		//	{
		//		ReceiveMarkupExtension(_targetObject, _eventArgs);
		//	}
		//}

	}
}
/*	[XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
	public class ReactiveEventTrigger : ReactiveTriggerBase
	{
		private MethodInfo eventHandlerMethodInfo;

		private bool _deferMarkupExtensionResolve;
		private object _targetObject;
		private XamlSetMarkupExtensionEventArgs _eventArgs;

		protected static readonly DependencyPropertyKey SourcePropertyKey = DP.RegisterReadOnly(
			new Meta<ReactiveEventTrigger, object>(null, onSourceChanged));
		public static readonly DependencyProperty SourceProperty = SourcePropertyKey.DependencyProperty;

		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<ReactiveEventTrigger, object>(null, onSourceObjectChanged));

		public static readonly DependencyProperty EventNameProperty = DP.Register(
			new Meta<ReactiveEventTrigger, string>(null, onEventNameChanged));


		public object Source
		{
			get { return GetValue(SourceProperty); }
			protected set { SetValue(SourcePropertyKey, value); }
		}
		public object SourceObject
		{
			get { return GetValue(SourceObjectProperty); }
			set { SetValue(SourceObjectProperty, value); }
		}
		public string EventName
		{
			get { return (string)GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}


		protected override void OnAttached()
		{
			base.OnAttached();
			Source = SourceObject ?? AssociatedObject;

			if (_deferMarkupExtensionResolve)
			{
				ReceiveMarkupExtension(_targetObject, _eventArgs);
			}
		}

		private static void onSourceChanged(ReactiveEventTrigger i, DPChangedEventArgs<object> e)
		{
			if (e.OldValue != null)
			{
				//unreg
			}
			if (e.NewValue != null && !i.EventName.IsNullOrWhiteSpace())
			{
				i.registerEventHandler(i.Source, i.EventName);
			}
		}

		private static void onSourceObjectChanged(ReactiveEventTrigger i, DPChangedEventArgs<object> e)
		{
			if (e.NewValue != null)
				i.Source = e.NewValue;
		}

		private static void onEventNameChanged(ReactiveEventTrigger i, DPChangedEventArgs<string> e)
		{
			if (e.OldValue != null)
			{
				//unreg
			}
			if (i.Source != null && !i.EventName.IsNullOrWhiteSpace())
			{
				i.registerEventHandler(i.Source, i.EventName);
			}
		}


		private void registerEventHandler(object obj, string eventName)
		{
			var @event = obj.GetType().GetEvent(eventName);
			if (@event == null)
			{
				if (SourceObject != null)
					throw new ArgumentException($"ReactiveEventTrigger EventName \'{eventName}\' not found in \'{obj.GetType().Name}\'");
			}
			else if (!IsValidEvent(@event))
			{
				if (SourceObject != null)
					throw new ArgumentException($"ReactiveEventTrigger EventName \'{eventName}\' in \'{obj.GetType().Name}\' is not valid.");
			}
			else
			{
				eventHandlerMethodInfo = typeof(ReactiveEventTrigger).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
				@event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, eventHandlerMethodInfo));
			}
		}

		private static bool IsValidEvent(EventInfo eventInfo)
		{
			var eventHandlerType = eventInfo.EventHandlerType;
			if (!typeof(Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
				return false;
			var parameters = eventHandlerType.GetMethod("Invoke").GetParameters();
			if (parameters.Length == 2 && typeof(object).IsAssignableFrom(parameters[0].ParameterType))
				return typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType);
			return false;
		}

		protected void OnEventImpl(object s, EventArgs e)
		{
			Execute();
		}

		
		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			var reactiveEventTrigger = targetObject as ReactiveEventTrigger;
			if (reactiveEventTrigger == null || eventArgs.Member.Name != nameof(SourceObject))
				return;

			if (!reactiveEventTrigger.IsHosted)
			{
				reactiveEventTrigger._targetObject = targetObject;
				reactiveEventTrigger._eventArgs = eventArgs;
				reactiveEventTrigger._deferMarkupExtensionResolve = true;
				return;
			}
			reactiveEventTrigger._targetObject = null;
			reactiveEventTrigger._eventArgs = null;
			reactiveEventTrigger._deferMarkupExtensionResolve = false;

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
						var frameworkElement = reactiveEventTrigger.AssociatedObject as FrameworkElement;
						if (frameworkElement == null)
							throw new NotSupportedException();

						var resolvedVisualAncestor = frameworkElement.FindParent(relativeSource.AncestorType, relativeSource.AncestorLevel);

						var adjustedBinding = new Binding
						{
							Source = resolvedVisualAncestor,
							Path = binding.Path
						};
						BindingOperations.SetBinding(reactiveEventTrigger, SourceObjectProperty, adjustedBinding);
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
	}*/
/*	[XamlSetMarkupExtension(nameof(ReceiveMarkupExtension))]
	public class ReactiveEventTrigger : ReactiveTriggerBase
	{
		private MethodInfo eventHandlerMethodInfo;

		private bool _deferMarkupExtensionResolve;
		private object _targetObject;
		private XamlSetMarkupExtensionEventArgs _eventArgs;

		protected static readonly DependencyPropertyKey SourcePropertyKey = DP.RegisterReadOnly(
			new Meta<ReactiveEventTrigger, object>(null, onSourceChanged));
		public static readonly DependencyProperty SourceProperty = SourcePropertyKey.DependencyProperty;

		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<ReactiveEventTrigger, object>(null, onSourceObjectChanged));

		public static readonly DependencyProperty EventNameProperty = DP.Register(
			new Meta<ReactiveEventTrigger, string>(null, onEventNameChanged));


		public object Source
		{
			get { return GetValue(SourceProperty); }
			protected set { SetValue(SourcePropertyKey, value); }
		}
		public object SourceObject
		{
			get { return GetValue(SourceObjectProperty); }
			set { SetValue(SourceObjectProperty, value); }
		}
		public string EventName
		{
			get { return (string)GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}


		protected override void OnAttached()
		{
			base.OnAttached();
			Source = SourceObject ?? AssociatedObject;

			if (_deferMarkupExtensionResolve)
			{
				ReceiveMarkupExtension(_targetObject, _eventArgs);
			}
		}

		private static void onSourceChanged(ReactiveEventTrigger i, DPChangedEventArgs<object> e)
		{
			if (e.OldValue != null)
			{
				//unreg
			}
			if (e.NewValue != null && !i.EventName.IsNullOrWhiteSpace())
			{
				i.registerEventHandler(i.Source, i.EventName);
			}
		}

		private static void onSourceObjectChanged(ReactiveEventTrigger i, DPChangedEventArgs<object> e)
		{
			if (e.NewValue != null)
				i.Source = e.NewValue;
		}

		private static void onEventNameChanged(ReactiveEventTrigger i, DPChangedEventArgs<string> e)
		{
			if (e.OldValue != null)
			{
				//unreg
			}
			if (i.Source != null && !i.EventName.IsNullOrWhiteSpace())
			{
				i.registerEventHandler(i.Source, i.EventName);
			}
		}


		private void registerEventHandler(object obj, string eventName)
		{
			var @event = obj.GetType().GetEvent(eventName);
			if (@event == null)
			{
				if (SourceObject != null)
					throw new ArgumentException($"ReactiveEventTrigger EventName \'{eventName}\' not found in \'{obj.GetType().Name}\'");
			}
			else if (!IsValidEvent(@event))
			{
				if (SourceObject != null)
					throw new ArgumentException($"ReactiveEventTrigger EventName \'{eventName}\' in \'{obj.GetType().Name}\' is not valid.");
			}
			else
			{
				eventHandlerMethodInfo = typeof(ReactiveEventTrigger).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
				@event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, eventHandlerMethodInfo));
			}
		}

		private static bool IsValidEvent(EventInfo eventInfo)
		{
			var eventHandlerType = eventInfo.EventHandlerType;
			if (!typeof(Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
				return false;
			var parameters = eventHandlerType.GetMethod("Invoke").GetParameters();
			if (parameters.Length == 2 && typeof(object).IsAssignableFrom(parameters[0].ParameterType))
				return typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType);
			return false;
		}

		protected void OnEventImpl(object s, EventArgs e)
		{
			Execute();
		}

		
		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			var reactiveEventTrigger = targetObject as ReactiveEventTrigger;
			if (reactiveEventTrigger == null || eventArgs.Member.Name != nameof(SourceObject))
				return;

			if (!reactiveEventTrigger.IsHosted)
			{
				reactiveEventTrigger._targetObject = targetObject;
				reactiveEventTrigger._eventArgs = eventArgs;
				reactiveEventTrigger._deferMarkupExtensionResolve = true;
				return;
			}
			reactiveEventTrigger._targetObject = null;
			reactiveEventTrigger._eventArgs = null;
			reactiveEventTrigger._deferMarkupExtensionResolve = false;

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
						var frameworkElement = reactiveEventTrigger.AssociatedObject as FrameworkElement;
						if (frameworkElement == null)
							throw new NotSupportedException();

						var resolvedVisualAncestor = frameworkElement.FindParent(relativeSource.AncestorType, relativeSource.AncestorLevel);

						var adjustedBinding = new Binding
						{
							Source = resolvedVisualAncestor,
							Path = binding.Path
						};
						BindingOperations.SetBinding(reactiveEventTrigger, SourceObjectProperty, adjustedBinding);
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
	}*/
