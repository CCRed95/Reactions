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
			"ShadowStoryboards", typeof(DynamicStoryboardCollection), typeof(React),
			new FrameworkPropertyMetadata(OnStoryboardsChanged));

		public static DynamicStoryboardCollection GetStoryboards(DependencyObject obj)
		{
			var storyboardCollection = (DynamicStoryboardCollection)obj.GetValue(StoryboardsProperty);
			if (storyboardCollection == null)
			{
				storyboardCollection = new DynamicStoryboardCollection();
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
//private static readonly DependencyPropertyKey VisualStateGroupsPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
//	"VisualStateGroups", typeof(IList), typeof(VisualStateManager), 
//	new FrameworkPropertyMetadata(new ObservableCollectionDefaultValueFactory<VisualStateGroup>()));

//public static readonly DependencyProperty VisualStateGroupsProperty = VisualStateManager.VisualStateGroupsPropertyKey.DependencyProperty;

/*		private static readonly DependencyProperty StoryboardsProperty = DependencyProperty.RegisterAttached(
			"ShadowStoryboards", typeof (RecursiveAnimationCollection), typeof (DynamicAnimator),
			new FrameworkPropertyMetadata(OnStoryboardsChanged));
    
		public static RecursiveAnimationCollection GetStoryboards(DependencyObject obj)
		{
			Interaction.
		}
		private static void OnStoryboardsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
      var behaviorCollection1 = (RecursiveAnimationCollection) args.OldValue;
      var behaviorCollection2 = (RecursiveAnimationCollection) args.NewValue;
      if (behaviorCollection1 == behaviorCollection2)
        return;
      if (behaviorCollection1 != null && behaviorCollection1.AssociatedObject != null)
        behaviorCollection1.Detach();
      if (behaviorCollection2 == null || obj == null)
        return;
      if (behaviorCollection2.AssociatedObject != null)
        throw new InvalidOperationException(ExceptionStringTable.CannotHostBehaviorCollectionMultipleTimesExceptionMessage);
      behaviorCollection2.Attach(obj);
    }*/
