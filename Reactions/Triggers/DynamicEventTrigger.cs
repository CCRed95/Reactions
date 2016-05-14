using System.Windows;
using Core.Helpers.DependencyHelpers;

namespace Reactions.Triggers
{
	public class DynamicEventTrigger : DynamicEventTriggerBase<object>
	{
		public static readonly DependencyProperty EventNameProperty = DP.Register(
			new Meta<DynamicEventTrigger, string>("Loaded", OnEventNameChanged));

		public string EventName
		{
			get { return (string) GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}

		public DynamicEventTrigger()
		{
			
		}

		public DynamicEventTrigger(string eventName)
    {
      EventName = eventName;
    }

		//[TraceAspect]
    protected override string GetEventName()
    {
      return EventName;
    }

		//[TraceAspect]
    private static void OnEventNameChanged(DynamicEventTrigger obj, DPChangedEventArgs<string> e)
		{
			obj.OnEventNameChanged(e.OldValue, e.NewValue);
		}
	}
}
 