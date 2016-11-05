using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Core.Extensions;
using Core.Helpers.DependencyHelpers;

namespace Material.Controls.Ripples
{
	public class RippleMouseTracker : Behavior<FrameworkElement>
	{
		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<RippleMouseTracker, object>(null, onSourceObjectChanged ));
		
		public static readonly DependencyProperty EventNameProperty = DP.Register(
			new Meta<RippleMouseTracker, string>(null, onEventNameChanged));


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


		private static void onSourceObjectChanged(RippleMouseTracker e, DPChangedEventArgs<object> args)
		{
			if (e.SourceObject != null && !e.EventName.IsNullOrWhiteSpace())
			{
				e.registerEventHandler(e.SourceObject, e.EventName);
			}
		}
		private static void onEventNameChanged(RippleMouseTracker e, DPChangedEventArgs<string> args)
		{
			if (e.SourceObject != null && !e.EventName.IsNullOrWhiteSpace())
			{
				e.registerEventHandler(e.SourceObject, e.EventName);
			}
		}

		private MethodInfo eventHandlerMethodInfo;

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
				eventHandlerMethodInfo = typeof(RippleMouseTracker).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
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
			var mousePos = Mouse.GetPosition(AssociatedObject);
			RippleFinal.SetMousePosition(AssociatedObject, mousePos);
		}
	}
}
/*		private void UnregisterEventImpl(object obj, string eventName)
		{
			var type = obj.GetType();
			if (eventHandlerMethodInfo == null)
				return;
			var @event = type.GetEvent(eventName);
			@event.RemoveEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, eventHandlerMethodInfo));
			eventHandlerMethodInfo = null;
		}*/