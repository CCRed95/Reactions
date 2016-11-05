using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Reactions.Core;

namespace Reactions.Implementations
{
	public class TestRecursiveZIndexApplicatorReaction : AttachableReactionBase
	{
		protected ItemsControl AssociatedItemsControl
		{
			get
			{
				var associatedItemsControl = AssociatedObject as ItemsControl;

				if (associatedItemsControl == null)
					throw new NullReferenceException("Associated object is null");

				return associatedItemsControl;
			}
		}
		private int currentZIndex = 10000;

		
		protected override void ReactImpl()
		{
			ensureCollectionChangeHook();
			execute(AssociatedItemsControl.Items);
		}

		private void onItemsCurrentChanging(object s, CurrentChangingEventArgs e)
		{

		}

		private void onSourceUpdated(object s, DataTransferEventArgs e)
		{

		}
		private void onItemsCurrentChanged(object s, EventArgs e)
		{
			execute(AssociatedItemsControl.Items);
		}

		//TODO this sucks, fix it
		protected void ensureCollectionChangeHook()
		{
			var sourceCollection = AssociatedItemsControl.Items;
			var notifyCollection = (INotifyCollectionChanged)sourceCollection;

			AssociatedItemsControl.SourceUpdated -= onSourceUpdated;
			AssociatedItemsControl.SourceUpdated += onSourceUpdated;

			AssociatedItemsControl.Items.CurrentChanged -= onItemsCurrentChanged;
			AssociatedItemsControl.Items.CurrentChanged += onItemsCurrentChanged;

			AssociatedItemsControl.Items.CurrentChanging -= onItemsCurrentChanging;
			AssociatedItemsControl.Items.CurrentChanging += onItemsCurrentChanging;

			notifyCollection.CollectionChanged -= onCollectionChanged;
			notifyCollection.CollectionChanged += onCollectionChanged;
		}

		private void onCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
		{
			try
			{
				if (e.Action == NotifyCollectionChangedAction.Reset)
				{
					ensureCollectionChangeHook();
				}
				if (e.NewItems != null)
				{
					execute(e.NewItems);
				}
				if (e.OldItems != null)
				{
					foreach (var i in e.OldItems)
					{

					}
				}
			}
			catch
			{

			}

		}

		protected void execute(IEnumerable itemsAdded)
		{
			foreach (var dataObject in itemsAdded)
			{
				UIElement targetVisualElement;

				if (tryGetTargetVisualElement(dataObject, out targetVisualElement))
				{
					Panel.SetZIndex(targetVisualElement, currentZIndex);
					currentZIndex = currentZIndex - 1;
				}
			}
		}

		//[TraceAspect]	
		protected bool tryGetTargetVisualElement(object dataObject, out UIElement targetVisualElement)
		{
			var container = AssociatedItemsControl.ItemContainerGenerator.ContainerFromItem(dataObject) as ContentPresenter;
			if (container == null)
			{
				targetVisualElement = null;
				return false;
			}

			container.ApplyTemplate();

			targetVisualElement = container;
			return true;

			//var boxedtarget = AssociatedItemsControl.ItemTemplate.FindName(TargetName, container);

			//var targetObject = boxedtarget as IAnimatable;
			//if (targetObject == null)
			//{
			//	targetVisualElement = null;
			//	return false;
			//}
			//targetVisualElement = targetObject;
			//return true;
		}
	}
}
