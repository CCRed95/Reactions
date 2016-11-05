using System;
using System.Windows;
using Core.Helpers.DependencyHelpers;
using Reactions.Collections;
using Reactions.Core;

namespace Reactions
{
	//TODO IDEA: system to bind something to a value wrapper that allows value changed transitioning/animation using emitters etc
	//TODO support "styles" for animation properties on emitters
	//TODO support different types of animations for AnimationTimelineEmmiters
	//TODO support keyframe animations/spline emitters
	//TODO IDEA: targeting system could support cached mappers to resolve a selector tree faster after the first iteration. avoid reflection/slow stuff.
	//TODO disable animations: still should update animated values with a setter reaction
	public static class React
	{
		internal static bool ShouldRunInDesignMode { get; set; }

		private static readonly DependencyProperty StoryboardsProperty = DependencyProperty.RegisterAttached(
			"ShadowStoryboards", typeof(ReactiveStoryboardCollection), typeof(React),
			new FrameworkPropertyMetadata(OnStoryboardsChanged));

		public static ReactiveStoryboardCollection GetStoryboards(DependencyObject obj)
		{
			var storyboardCollection = (ReactiveStoryboardCollection)obj.GetValue(StoryboardsProperty);
			if (storyboardCollection == null)
			{
				storyboardCollection = new ReactiveStoryboardCollection();
				obj.SetValue(StoryboardsProperty, storyboardCollection);
			}
			return storyboardCollection;
		}

		private static void OnStoryboardsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			var oldStoryboardCollection = (IAttachedObject)args.OldValue;
			var newStoryboardCollection = (IAttachedObject)args.NewValue;
			if (oldStoryboardCollection == newStoryboardCollection)
				return;
			if (oldStoryboardCollection != null && oldStoryboardCollection.AssociatedObject != null)
				oldStoryboardCollection.Detach();
			if (newStoryboardCollection == null || obj == null)
				return;
			if (newStoryboardCollection.AssociatedObject != null)
				throw new InvalidOperationException("ExceptionStringTable.CannotHostBehaviorCollectionMultipleTimesExceptionMessage");
			newStoryboardCollection.Attach(obj);
		}

		public static bool IsElementLoaded(FrameworkElement element)
		{
			return element.IsLoaded;
		}


		public static readonly DependencyProperty IsExternallyAnimatedProperty =
			DP.Attach<bool>(typeof(React), new FrameworkPropertyMetadata(false, onIsExternallyAnimatedChanged));

		private static void onIsExternallyAnimatedChanged(DependencyObject i, DependencyPropertyChangedEventArgs e)
		{


		}

		public static bool GetIsExternallyAnimated(DependencyObject i) => i.Get<bool>(IsExternallyAnimatedProperty);
		public static void SetIsExternallyAnimated(DependencyObject i, bool v) => i.Set(IsExternallyAnimatedProperty, v);

		//TODO make this better handled.
		public static readonly DependencyProperty DisableProperty =
			DP.Attach<bool>(typeof (React), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

		public static bool GetDisable(DependencyObject i) => i.Get<bool>(DisableProperty);
		public static void SetDisable(DependencyObject i, bool v) => i.Set(DisableProperty, v);


	}
}