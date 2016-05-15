using System;
using System.Windows;
using System.Windows.Media.Animation;
using Reactions.Core;
using Reactions.Recursive.Targeting;

namespace Reactions.Recursive
{
	//TODO property paths, to/from type fix, should not be : Animatable? 
	//TODO enable/disable animation/shouldexecute?
	//TODO better durationmode solution... shouldnt be able to set Offsettimes with EntireDuration?
	//TODO conditional animation based on current collection item condition
	//TODO divided animation offset calc based on collection size with Min/Max timespan limits
	//TODO how to handle objects that have no visual yet due to virtualization?
	//TODO bindable animation targets!!!!
	//TODO how to handle failed element selector resoultion
	//TODO scoping for element selector, Linq group selector similar?
	//TODO better easing and advanced state control
	//TODO revert back to original state, calculate inverse for return
	//TODO object interpolator system per-type
	public abstract class IterativeAnimationReactionBase : AttachableReactionBase
	{
		public DependencyProperty TargetProperty { get; set; }

		public string TargetName { get; set; }

		public PropertyPath TargetPath { get; set; }

		public double? From { get; set; }

		public double? To { get; set; }

		public Duration Duration { get; set; }

		public DurationMode DurationMode { get; set; } = DurationMode.StepDuration;

		public TimeSpan BeginTime { get; set; }

		public TimeSpan OffsetTime { get; set; }

		public IEasingFunction EasingFunction { get; set; }

		public bool ApplyFromPreOffset { get; set; }

		public ElementSelectorBase ElementSelector { get; set; }
	}

	//public abstract class IterativeAnimationReactionBase<T> : IterativeAnimationReactionBase
	//{

	//}
}
