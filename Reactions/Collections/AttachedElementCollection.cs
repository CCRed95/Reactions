using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;
using Reactions.Core;
using Reactions.Recursive;

namespace Reactions.Collections
{
	//TODO make a IAttachedObject replacement interface locally and remove Sys.Win.Interactivity Behavior project reference 
	public abstract class AttachedElementCollection<T> : DependencyObjectCollection<T>, IAttachedObject where T : AttachableBase
	{
		private Collection<T> snapshot;

		protected DependencyObject AssociatedObject { get; private set; }

		DependencyObject IAttachedObject.AssociatedObject => AssociatedObject;

		protected AttachedElementCollection()
		{
			((INotifyCollectionChanged)this).CollectionChanged += OnCollectionChanged;
			snapshot = new Collection<T>();
		}

		//[TraceAspect]
		public void Attach(DependencyObject dependencyObject)
		{
			if (dependencyObject == AssociatedObject)
				return;
			if (AssociatedObject != null)
				throw new InvalidOperationException("AttachedElementCollection cannot host objects multiple times.")
				{
					Data = {
						["FailedObjectAssociation"] = dependencyObject.ToString(),
						["ExistingAssociatedObject"] = AssociatedObject.ToString()
					}
				};
			if (React.ShouldRunInDesignMode || !(bool)GetValue(DesignerProperties.IsInDesignModeProperty))
			{
				AssociatedObject = dependencyObject;
			}
			OnAttached();
		}

		//[TraceAspect]
		public void Detach()
		{
			OnDetaching();
			AssociatedObject = null;
		}

		//[TraceAspect]
		protected virtual void OnAttached()
		{
			foreach (var item in this)
				item.Attach(AssociatedObject);
		}

		//[TraceAspect]
		protected virtual void OnDetaching()
		{
			foreach (var item in this)
				item.Detach();
		}

		//[TraceAspect]
		internal virtual void ItemAdded(T item)
		{
			if (AssociatedObject == null)
				return;
			item.Attach(AssociatedObject);
		}

		//[TraceAspect]
		internal virtual void ItemRemoved(T item)
		{
			if (item.AssociatedObject == null)
				return;
			item.Detach();
		}

		//[TraceAspect]
		private void VerifyAdd(T item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));
			if (snapshot.Contains(item))
				throw new InvalidOperationException("Duplicate item in collection.")
				{
					Data =
					{
						["DuplicateItem"] = item.ToString(),
						["CollectionMemberType"] = typeof(T).Name,
						["CollectionType"] = GetType().Name
					}
				};
		}

		//[TraceAspect]
		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					var enumerator1 = e.NewItems.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							var obj = (T)enumerator1.Current;
							try
							{
								VerifyAdd(obj);
								ItemAdded(obj);
							}
							finally
							{
								snapshot.Insert(IndexOf(obj), obj);
							}
						}
						break;
					}
					finally
					{
						var disposable = enumerator1 as IDisposable;
						disposable?.Dispose();
					}
				case NotifyCollectionChangedAction.Remove:
					var enumerator2 = e.OldItems.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							var obj = (T)enumerator2.Current;
							ItemRemoved(obj);
							snapshot.Remove(obj);
						}
						break;
					}
					finally
					{
						var disposable = enumerator2 as IDisposable;
						disposable?.Dispose();
					}
				case NotifyCollectionChangedAction.Replace:
					foreach (T obj in e.OldItems)
					{
						ItemRemoved(obj);
						snapshot.Remove(obj);
					}
					var enumerator3 = e.NewItems.GetEnumerator();
					try
					{
						while (enumerator3.MoveNext())
						{
							var obj = (T)enumerator3.Current;
							try
							{
								VerifyAdd(obj);
								ItemAdded(obj);
							}
							finally
							{
								snapshot.Insert(IndexOf(obj), obj);
							}
						}
						break;
					}
					finally
					{
						var disposable = enumerator3 as IDisposable;
						disposable?.Dispose();
					}
				case NotifyCollectionChangedAction.Reset:
					foreach (var obj in snapshot)
						ItemRemoved(obj);
					snapshot = new Collection<T>();
					using (var enumerator4 = GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							var current = enumerator4.Current;
							VerifyAdd(current);
							ItemAdded(current);
						}
						break;
					}
			}
		}
	}
}
