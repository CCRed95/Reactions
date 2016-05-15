using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Reactions.Recursive
{
	public class IterativeListViewAnimationReaction : IterativeAnimationReactionBase//<ListView>
	{
		//TODO call base.React?
		//[TraceAspect]
		public override void React()
		{
			var currentOffset = BeginTime;

			if (AssociatedObject == null)
				throw new NullReferenceException("Associated object is null");

			foreach (var dataObject in ((ListView)AssociatedObject).Items)
			{
				IAnimatable targetVisualElement;

				if (tryGetTargetVisualElement(dataObject, out targetVisualElement))
				{
					if (ApplyFromPreOffset)
					{
						((DependencyObject)targetVisualElement).SetCurrentValue(TargetProperty, From);
					}
					targetVisualElement.BeginAnimation(TargetProperty,
						new DoubleAnimation
						{
							Duration = Duration,
							From = From,
							To = To,
							BeginTime = currentOffset,
							EasingFunction = EasingFunction
						});
					currentOffset += OffsetTime;
				}
				else
				{

				}
			}
		}
		//[TraceAspect]
		protected bool tryGetTargetVisualElement(object dataObject, out IAnimatable targetVisualElement)
		{
			var container = ((ListView)AssociatedObject).ItemContainerGenerator.ContainerFromItem(dataObject) as ListViewItem;
			if (container == null)
			{
				targetVisualElement = null;
				return false;
			}

			container.ApplyTemplate();

			var currentChild = VisualTreeHelper.GetChild(container, 0) as ContentPresenter;
			if (currentChild == null)
			{
				targetVisualElement = null;
				return false;
			}

			var targetObject = currentChild.FindName(TargetName) as IAnimatable;
			if (targetObject == null)
			{
				targetVisualElement = null;
				return false;
			}
			targetVisualElement = targetObject;
			return true;
		}
	}
}
