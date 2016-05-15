using System;
using System.Windows;
using System.Windows.Interactivity;
using Reactions.Collections;

namespace Reactions.Recursive
{
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
	}
}