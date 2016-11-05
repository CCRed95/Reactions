using System;
using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Core.Collections;
using Core.Helpers;
using Core.Helpers.DependencyHelpers;
using Reactions.Core;
using Reactions.Iterative.Targeting.Core;

namespace Reactions.Implementations
{
	[XamlSetMarkupExtension("ReceiveMarkupExtension")]
	//[XamlSetTypeConverter("ReceiveTypeConverter")]
	public class IterativeIndexAwareReaction : AttachableReactionBase
	{
		public const string _defaultIterationTargetPropertyName = "Items";

		private string _iterationTargetPropertyName = _defaultIterationTargetPropertyName;
		private PropertyInfo _propertyInfo;
		private IList _resolvedIterationTarget;
		private bool _deferIterationTargetReflect;

		public string IterationTargetPropertyName
		{
			get { return _iterationTargetPropertyName; }
			set
			{
				if (_deferIterationTargetReflect)
				{
					AttemptResolveIterationTargetPropertyInfo();
				}

				var oldProperty = _iterationTargetPropertyName;
				_iterationTargetPropertyName = value;
				OnIterationTargetPropertyNameChanged(oldProperty, _iterationTargetPropertyName);
			}
		}

		public IList ResolvedIterationTarget
		{
			get { return _resolvedIterationTarget; }
			protected set
			{
				_resolvedIterationTarget = value;
				OnResolvedIterationTarget();
			}
		}
		public static readonly DependencyProperty SelectorExpressionProperty = DP.Register(
			new Meta<IterativeIndexAwareReaction, SelectorExpressionTree>());
		public SelectorExpressionTree SelectorExpression
		{
			get { return (SelectorExpressionTree)GetValue(SelectorExpressionProperty); }
			set { SetValue(SelectorExpressionProperty, value); }
		}

		public static readonly DependencyProperty IsDataAwareTrackingProperty = DP.Register(
			new Meta<IterativeIndexAwareReaction, bool>());
		public bool IsDataAwareTracking
		{
			get { return (bool)GetValue(IsDataAwareTrackingProperty); }
			set { SetValue(IsDataAwareTrackingProperty, value); }
		}


		protected void OnResolvedIterationTarget()
		{

		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AttemptResolveIterationTargetPropertyInfo();
		}


		protected void OnIterationTargetPropertyNameChanged(string oldName, string newName)
		{
			AttemptResolveIterationTargetPropertyInfo();
		}

		protected void AttemptResolveIterationTargetPropertyInfo()
		{
			_deferIterationTargetReflect = false;
			if (AssociatedObject == null || IterationTargetPropertyName == null)
			{
				_deferIterationTargetReflect = true;
				return;
			}
			var associatedObjectType = AssociatedObject.GetType();
			var propertyInfo = associatedObjectType.GetProperty(IterationTargetPropertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			if (propertyInfo == null)
			{
				throw new MissingMemberException(associatedObjectType.Name, IterationTargetPropertyName);
			}
			_propertyInfo = propertyInfo;
		}

		protected void AttemptResolveIterationTarget()
		{
			var reflectedValue = _propertyInfo.GetValue(AssociatedObject);

			var enumerableTarget = reflectedValue as IList;
			if (enumerableTarget == null)
				throw new NotSupportedException($"Target reflected value must be IList. Type \'{reflectedValue.GetType()}\' is not valid.");

			ResolvedIterationTarget = enumerableTarget;
		}

		protected override void ReactImpl()
		{
			if (_propertyInfo == null)
				AttemptResolveIterationTargetPropertyInfo();

			if (ResolvedIterationTarget == null)
				AttemptResolveIterationTarget();

			if (ResolvedIterationTarget == null)
				throw new NotSupportedException("Target Enumerable IList could not be resolved.");

			var itemIndex = 0;
			var count = ResolvedIterationTarget.Count;

			foreach (var item in ResolvedIterationTarget)
			{
				//TODO use selectors for this to use on other types instead of itemscontrol
				var associatedItemsControl = AssociatedObject as ItemsControl;
				if (associatedItemsControl == null)
					throw new NotSupportedException(
							$"IterativeIndexAwareReactionBase Associated Item \'{AssociatedObject.GetType().Name}\' must be of type \'ItemsControl\'");

				var container = associatedItemsControl.ItemContainerGenerator.ContainerFromItem(item);

				var targetElement = SelectorExpression.ResolveSelectorTree(container, associatedItemsControl);

				var indexAwareData = new IndexAwareData
				{
					Index = itemIndex,
					IsFirst = itemIndex == 0,
					IsInner = itemIndex > 0 && itemIndex < count - 1 && count > 2,
					IsLast = itemIndex == count - 1,
					IsSingle = count == 1
				};
				IndexAwareAssist.SetIndexAwareData((DependencyObject)targetElement, indexAwareData);
				//TODO make this a generic solution, move to whole seperate tracking class.use selectors?
				if (IsDataAwareTracking)
				{
					var currentDataPointValue = item.GetPropertyValue<object>("DataPoint").GetPropertyValue<double>("Value");
					var lastDataPointValue = 0d;
					if (itemIndex - 1 > 0)
					{
						lastDataPointValue = ResolvedIterationTarget[itemIndex - 1]
							.GetPropertyValue<object>("DataPoint").GetPropertyValue<double>("Value");
					}
					var nextDataPointValue = 0d;
					if (itemIndex + 1 < ResolvedIterationTarget.Count)
					{
						nextDataPointValue = ResolvedIterationTarget[itemIndex + 1]
							.GetPropertyValue<object>("DataPoint").GetPropertyValue<double>("Value");
					}
					var dataAwareData = new DataAwareData
					{
						Index = itemIndex,
						IsAdjacentToLargerLeftBar = currentDataPointValue < lastDataPointValue,
						IsAdjacentToLargerRightBar = currentDataPointValue < nextDataPointValue,
						IsAdjacentToSimilarLeftBar = Math.Abs(currentDataPointValue - lastDataPointValue) < 3,
						IsAdjacentToSimilarRightBar = Math.Abs(currentDataPointValue - nextDataPointValue) < 3
					};
					DataAwareAssist.SetDataAwareData((DependencyObject)targetElement, dataAwareData);
				}


				//if (AnimationTemplate.Emitter.ApplyFromBeforeOffset)
				//{
				//	var targetDependendencyObject = targetAnimatable as DependencyObject;
				//	if (targetDependendencyObject == null)
				//		throw new NotSupportedException(
				//			$"Element targeted by the AnimationSelector type \'{targetAnimatable.GetType().Name}\' must be \'DependencyObject\'");

				//	targetDependendencyObject.SetCurrentValue(AnimationTemplate.Emitter.Property, AnimationTemplate.Emitter.From);
				//}

				//var animationTimeline = AnimationTemplate.Emitter.Emit(itemIndex);
				//targetAnimatable.BeginAnimation(AnimationTemplate.Emitter.Property, animationTimeline);

				itemIndex++;
			}



		}

		public static void ReceiveMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			if (targetObject == null)
				throw new ArgumentNullException(nameof(targetObject));
			if (eventArgs == null)
				throw new ArgumentNullException(nameof(eventArgs));
			var setter = targetObject as IterativeIndexAwareReaction;
			if (setter == null || eventArgs.Member.Name != nameof(IterationTargetPropertyName))
				return;
			var markupExtension = eventArgs.MarkupExtension;
			if (markupExtension is StaticResourceExtension)
			{
				var resourceExtension = markupExtension as StaticResourceExtension;
				setter.IterationTargetPropertyName = resourceExtension.InvokeInternalMethod<string>(
					"ProvideValueInternal", eventArgs.ServiceProvider, true);
				eventArgs.Handled = true;
			}
			else
			{
				if (!(markupExtension is DynamicResourceExtension) && !(markupExtension is BindingBase))
					return;
				throw new NotImplementedException();
				//setter.IterationTargetPropertyName = markupExtension;
				//eventArgs.Handled = true;
			}
		}

		//public static void ReceiveTypeConverter(object targetObject, XamlSetTypeConverterEventArgs eventArgs)
		//{
		//	var setter = targetObject as IterativeIndexAwareReaction;
		//	if (setter == null)
		//		throw new ArgumentNullException(nameof(targetObject));
		//	if (eventArgs == null)
		//		throw new ArgumentNullException(nameof(eventArgs));
		//	if (eventArgs.Member.Name == nameof(IterationTargetPropertyName))
		//	{
		//		//setter._unresolvedProperty = eventArgs.Value;
		//		//setter._serviceProvider = eventArgs.ServiceProvider;
		//		//setter._typeConverterCultureInfo = eventArgs.CultureInfo;
		//		eventArgs.Handled = true;
		//	}
		//	else
		//	{
		//		throw new NotImplementedException();
		//		//if (eventArgs.Member.Name != nameof(Value))
		//		//	return;
		//		//setter._unresolvedValue = eventArgs.Value;
		//		//setter._serviceProvider = eventArgs.ServiceProvider;
		//		//setter._typeConverterCultureInfo = eventArgs.CultureInfo;
		//		//eventArgs.Handled = true;
		//	}
		//}

	}
}
/*	public class TestIterativeItemsControlIndexAwareReaction : AttachableReactionBase
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


		public override void React()
		{
			ensureCollectionChangeHook();
			executeAnimation(AssociatedItemsControl.Items);
		}

		private void onItemsCurrentChanging(object s, CurrentChangingEventArgs e)
		{

		}

		private void onSourceUpdated(object s, DataTransferEventArgs e)
		{

		}
		private void onItemsCurrentChanged(object s, EventArgs e)
		{
			executeAnimation(AssociatedItemsControl.Items);
		}


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
					executeAnimation(e.NewItems);
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

		protected void executeAnimation(IEnumerable itemsAdded)
		{
			foreach (var dataObject in itemsAdded)
			{
				IAnimatable targetVisualElement;

				if (tryGetTargetVisualElement(dataObject, out targetVisualElement))
				{
					if (ApplyFromPreOffset)
					{
						//((DependencyObject) targetVisualElement).unregisterAnimationUnsafe(TargetProperty);

						((DependencyObject)targetVisualElement).SetValue(TargetProperty, From);
					}
					targetVisualElement.BeginAnimation(TargetProperty,
						new DoubleAnimation
						{
							Duration = Duration,
							From = From,
							To = To,
							SpeedRatio = SpeedRatio,
							BeginTime = currentOffset,
							EasingFunction = EasingFunction
						});
					currentOffset += scaledOffsetTime;
				}
				else
				{

				}
			}
		}

		//[TraceAspect]
		protected bool tryGetTargetVisualElement(object dataObject, out IAnimatable targetVisualElement)
		{
			var container = AssociatedItemsControl.ItemContainerGenerator.ContainerFromItem(dataObject) as ContentPresenter;
			if (container == null)
			{
				targetVisualElement = null;
				return false;
			}

			container.ApplyTemplate();

			var boxedtarget = AssociatedItemsControl.ItemTemplate.FindName(TargetName, container);

			var targetObject = boxedtarget as IAnimatable;
			if (targetObject == null)
			{
				targetVisualElement = null;
				return false;
			}
			targetVisualElement = targetObject;
			return true;
		}
	}*/
