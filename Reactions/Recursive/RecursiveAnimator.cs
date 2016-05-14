using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace Reactions.Recursive
{
	public enum DurationMode
	{
		StepDuration,
		EntireDuration
	}
	public class RecursiveAnimator : Behavior<ItemsControl>
	{
		public double? From { get; set; }
		public double? To { get; set; }
		public Duration Duration { get; set; }
		public DurationMode DurationMode { get; set; } = DurationMode.StepDuration;
		public TimeSpan BeginTime { get; set; }
		public TimeSpan OffsetTime { get; set; }
		public RoutedEvent RoutedEvent { get; set; }
		public IEasingFunction EasingFunction { get; set; }
		public DependencyProperty TargetProperty { get; set; }
		public string TargetName { get; set; }

		protected override void OnAttached()
		{
			AssociatedObject.AddHandler(RoutedEvent, new RoutedEventHandler(onRoutedEvent));
		}

		private void onRoutedEvent(object s, RoutedEventArgs e)
		{
			var currentOffset = TimeSpan.Zero;
			var stepDuration = Duration;
			if (DurationMode == DurationMode.EntireDuration && AssociatedObject.Items.Count > 0)
			{
				stepDuration = new TimeSpan(Duration.TimeSpan.Ticks / AssociatedObject.Items.Count);
			}
			foreach (var i in AssociatedObject.Items)
			{
				var container = (ContentControl)AssociatedObject.ItemContainerGenerator.ContainerFromItem(i);
				container.ApplyTemplate();
				var targetObject = container.ContentTemplate.FindName(TargetName, container) as IAnimatable;
				if (targetObject != null)
				{
					targetObject.BeginAnimation(TargetProperty,
						new DoubleAnimation
						{
							Duration = stepDuration,
							To = To,
							From = From,
							BeginTime = currentOffset + BeginTime,
							EasingFunction = EasingFunction
						});
					currentOffset += OffsetTime;
				}
				else
				{
					throw new Exception("Couldnt find" + TargetName);
				}
			}
		}

	}

}