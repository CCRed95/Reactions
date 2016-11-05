using System;
using System.Collections.Specialized;
using System.Windows;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.DependencyHelpers;

namespace Reactions.Triggers
{
	public class CollectionChangedTrigger : ReactiveTriggerBase
	{
		private INotifyCollectionChanged _resolvedCollection;

		private object source;
		public object Source
		{
			get { return source; }
			protected set
			{
				var oldSource = source;
				source = value;
				OnSourceChanged(oldSource, source);
			}
		}

		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<CollectionChangedTrigger, DependencyObject>(null, onSourceObjectChanged));

		public DependencyObject SourceObject
		{
			get { return (DependencyObject)GetValue(SourceObjectProperty); }
			set { SetValue(SourceObjectProperty, value); }
		}

		public static readonly DependencyProperty PropertyNameProperty = DP.Register(
			new Meta<CollectionChangedTrigger, string>(null, onPropertyNameChanged));

		public string PropertyName
		{
			get { return (string)GetValue(PropertyNameProperty); }
			set { SetValue(PropertyNameProperty, value); }
		}

		private static void onSourceObjectChanged(CollectionChangedTrigger i, DPChangedEventArgs<DependencyObject> e)
		{
			i.Source = e.NewValue;
		}

		private static void onPropertyNameChanged(CollectionChangedTrigger i, DPChangedEventArgs<string> e)
		{
			i.resolveTarget();
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			Source = AssociatedObject;
			if (SourceObject == null)
			{
				Source = AssociatedObject;
			}
			else
			{
				Source = SourceObject;
			}
		}

		protected void OnSourceChanged(object oldSource, object newSource)
		{
			//((ListView) newSource).PreviewMouseWheel += (s, e) =>
			//{
			//	resolveTarget();
			//};
			//((ListView) newSource).MouseWheel += (s, e) =>
			//{
			//	resolveTarget();
			//};
			resolveTarget();
		}

		protected void resolveTarget()
		{
			if (Source != null && !PropertyName.IsNullOrWhiteSpace())
			{
				_resolvedCollection = Source.GetPropertyValue<INotifyCollectionChanged>(PropertyName);
				ensureCollectionChangeHook();
			}
		}

		protected void ensureCollectionChangeHook()
		{
			_resolvedCollection.CollectionChanged -= onCollectionChanged;
			_resolvedCollection.CollectionChanged += onCollectionChanged;
		}

		private void onCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
		{
			try
			{
				if (e.Action == NotifyCollectionChangedAction.Reset)
				{
					ensureCollectionChangeHook();
					Execute();
				}
				if (e.NewItems != null)
				{
					Execute();
					//executeAnimation(e.NewItems);
				}
				if (e.OldItems != null)
				{
					foreach (var i in e.OldItems)
					{

					}
				}
			}
			catch (Exception ex)
			{

			}

		}
	}

	//public class CollectionChangedTrigger : ReactiveTriggerBase
	//{
	//	//TODO DependencyObject
	//	private object source;
	//	public object Source
	//	{
	//		get { return source; }
	//		protected set
	//		{
	//			var oldSource = source;
	//			source = value;
	//			OnSourceChanged(oldSource, source);
	//		}
	//	}

	//	public static readonly DependencyProperty SourceObjectProperty = DP.Register(
	//		new Meta<CollectionChangedTrigger, DependencyObject>(null, onSourceObjectChanged));

	//	public DependencyObject SourceObject
	//	{
	//		get { return (DependencyObject) GetValue(SourceObjectProperty); }
	//		set { SetValue(SourceObjectProperty, value); }
	//	}

	//	private DependencyProperty property;
	//	public DependencyProperty Property
	//	{
	//		get { return property; }
	//		set
	//		{
	//			var oldProperty = property;
	//			property = value;
	//			OnPropertyChanged(oldProperty, property);
	//		}
	//	}

	//	public static readonly DependencyProperty BoundValueProperty = DP.Register(
	//		new Meta<CollectionChangedTrigger, object>(null, onBoundValueChanged));

	//	public object BoundValue
	//	{
	//		get { return GetValue(BoundValueProperty); }
	//		set { SetValue(BoundValueProperty, value); }
	//	}

	//	private static void onSourceObjectChanged(CollectionChangedTrigger i, DPChangedEventArgs<DependencyObject> e)
	//	{
	//		i.Source = e.NewValue;
	//	}

	//	private static void onBoundValueChanged(CollectionChangedTrigger i, DPChangedEventArgs<object> e)
	//	{
	//		i.ensureCollectionChangeHook();
	//	}

	//	protected override void OnAttached()
	//	{
	//		base.OnAttached();
	//		Source = AssociatedObject;
	//		if (SourceObject == null)
	//		{
	//			Source = AssociatedObject;
	//		}
	//		else
	//		{
	//			Source = SourceObject;
	//		}
	//	}

	//	protected void OnSourceChanged(object oldSource, object newSource)
	//	{
	//		if (newSource != null && Property != null)
	//		{
	//			SetBinding();
	//		}
	//	}

	//	protected void OnPropertyChanged(DependencyProperty oldProperty, DependencyProperty newProperty)
	//	{
	//		if (Source != null && newProperty != null)
	//		{
	//			SetBinding();
	//		}
	//	}

	//	protected void OnValueSet(object oldValue, object newValue)
	//	{
	//	}

	//	protected void SetBinding()
	//	{
	//		BindingOperations.SetBinding(this, BoundValueProperty, new Binding(Property.Name) {Source = Source});
	//	}

	//	//private void onItemsCurrentChanging(object s, CurrentChangingEventArgs e)
	//	//{

	//	//}

	//	//	private void onSourceUpdated(object s, DataTransferEventArgs e)
	//	//{

	//	//}
	//	private void onItemsCurrentChanged(object s, EventArgs e)
	//	{
	//		Execute();
	//	}


	//	protected void ensureCollectionChangeHook()
	//	{
	//		var sourceCollection = BoundValue;
	//		var notifyCollection = (INotifyCollectionChanged) sourceCollection;

	//		//AssociatedItemsControl.SourceUpdated -= onSourceUpdated;
	//		//AssociatedItemsControl.SourceUpdated += onSourceUpdated;

	//		//AssociatedItemsControl.Items.CurrentChanged -= onItemsCurrentChanged;
	//		//AssociatedItemsControl.Items.CurrentChanged += onItemsCurrentChanged;

	//		//AssociatedItemsControl.Items.CurrentChanging -= onItemsCurrentChanging;
	//		//AssociatedItemsControl.Items.CurrentChanging += onItemsCurrentChanging;

	//		notifyCollection.CollectionChanged -= onCollectionChanged;
	//		notifyCollection.CollectionChanged += onCollectionChanged;
	//	}

	//	private void onCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
	//	{
	//		try
	//		{
	//			if (e.Action == NotifyCollectionChangedAction.Reset)
	//			{
	//				ensureCollectionChangeHook();
	//			}
	//			if (e.NewItems != null)
	//			{
	//				Execute();
	//				//executeAnimation(e.NewItems);
	//			}
	//			if (e.OldItems != null)
	//			{
	//				foreach (var i in e.OldItems)
	//				{

	//				}
	//			}
	//		}
	//		catch
	//		{

	//		}

	//	}
	//}
}
