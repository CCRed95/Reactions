using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Core.Helpers.DependencyHelpers;
using Core.Extensions;

namespace Reactions.Triggers
{
	public class SimpleEventTrigger : ReactiveTriggerBase
	{
		protected static readonly DependencyPropertyKey SourcePropertyKey = DP.RegisterReadOnly(
			new Meta<SimpleEventTrigger, object>(null, onSourceChanged));
		public static readonly DependencyProperty SourceProperty = SourcePropertyKey.DependencyProperty;
		
		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<SimpleEventTrigger, object>(null, onSourceObjectChanged));

		public static readonly DependencyProperty EventNameProperty = DP.Register(
			new Meta<SimpleEventTrigger, string>(null, onEventNameChanged));


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
			if (SourceObject != null)
			{
				Source = SourceObject;
			}
			else
			{
				Source = AssociatedObject;
			}
		}

		protected override void OnDetaching()
		{
			
			base.OnDetaching();
		}
		public override void OnHostUnregistering()
		{
			base.OnHostUnregistering();
		}

		private MethodInfo eventHandlerMethodInfo;
		private bool IsEventHooked = false;

		private static void onSourceChanged(SimpleEventTrigger i, DPChangedEventArgs<object> e)
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
		private static void onSourceObjectChanged(SimpleEventTrigger i, DPChangedEventArgs<object> e)
		{
			if (e.NewValue != null)
			{
				i.Source = e.NewValue;
			}
		}

		private static void onEventNameChanged(SimpleEventTrigger i, DPChangedEventArgs<string> e)
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
					throw new ArgumentException($"EventTriggerCannotFindEventNameExceptionMessage {eventName}, {obj.GetType().Name}");
			}
			else if (!IsValidEvent(@event))
			{
				if (SourceObject != null)
					throw new ArgumentException($"EventTriggerBaseInvalidEventExceptionMessage {eventName}, {obj.GetType().Name}");
			}
			else
			{
				eventHandlerMethodInfo = typeof(SimpleEventTrigger).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
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
	}
}