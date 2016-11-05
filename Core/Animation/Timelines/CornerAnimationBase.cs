using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Core.Animation.Timelines
{
	public abstract class CornerAnimationBase : AnimationTimeline
	{
		public sealed override Type TargetPropertyType
		{
			get
			{
				ReadPreamble();
				return typeof(CornerRadius);
			}
		}

		public CornerAnimationBase Clone()
		{
			return (CornerAnimationBase)base.Clone();
		}

		public sealed override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
		{
			if (defaultOriginValue == null)
				throw new ArgumentNullException(nameof(defaultOriginValue));
			if (defaultDestinationValue == null)
				throw new ArgumentNullException(nameof(defaultDestinationValue));
			return GetCurrentValue((CornerRadius)defaultOriginValue, (CornerRadius)defaultDestinationValue, animationClock);
		}

		public CornerRadius GetCurrentValue(CornerRadius defaultOriginValue, CornerRadius defaultDestinationValue, AnimationClock animationClock)
		{
			ReadPreamble();
			if (animationClock == null)
				throw new ArgumentNullException(nameof(animationClock));
			if (animationClock.CurrentState == ClockState.Stopped)
				return defaultDestinationValue;
			return GetCurrentValueCore(defaultOriginValue, defaultDestinationValue, animationClock);
		}

		protected abstract CornerRadius GetCurrentValueCore(CornerRadius defaultOriginValue, CornerRadius defaultDestinationValue, AnimationClock animationClock);
	}
}
