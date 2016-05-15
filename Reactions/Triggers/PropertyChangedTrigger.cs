using System.Windows;
using Core.Helpers.DependencyHelpers;

namespace Reactions.Triggers
{
	public class PropertyChangedTrigger : ReactiveTriggerBase
	{
		public static readonly DependencyProperty TargetProperty = DP.Register(
			new Meta<PropertyChangedTrigger, object>(null, onTargetChanged));

		private static void onTargetChanged(PropertyChangedTrigger i, DPChangedEventArgs<object> e)
		{
			i.Execute();
		}

		public object Target
 		{
			get { return GetValue(TargetProperty); }
			set { SetValue(TargetProperty, value); }
		}

	}
}
