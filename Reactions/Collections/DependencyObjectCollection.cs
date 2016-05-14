using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace Reactions.Collections
{
	public class DependencyObjectCollection<T> : DependencyObject, IList, ICollection, IEnumerable, IList<T>, ICollection<T>, IEnumerable<T>,  INotifyCollectionChanged where T : DependencyObject
	{
		private static readonly DependencyProperty CollectionProperty = DependencyProperty.Register("DOCollection", 
			typeof(ObservableCollection<T>), typeof(DependencyObjectCollection<T>), null);

		private ObservableCollection<T> items;

		public DependencyObjectCollection()
		{
			items = new ObservableCollection<T>();
			SetValue(CollectionProperty, items);
		}

		public void Add(T item) => items.Add(item);

		int IList.Add(object value)
		{
			return ((IList) items).Add(value);
		}

		bool IList.Contains(object value)
		{
			return ((IList) items).Contains(value);
		}

		public void Clear() => items.Clear();
		int IList.IndexOf(object value)
		{
			return ((IList) items).IndexOf(value);
		}

		void IList.Insert(int index, object value)
		{
			((IList) items).Insert(index, value);
		}

		void IList.Remove(object value)
		{
			((IList) items).Remove(value);
		}

		public bool Contains(T item) => items.Contains(item);

		public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection) items).CopyTo(array, index);
		}

		public int Count => items.Count;
		object ICollection.SyncRoot => ((ICollection)items).SyncRoot;
		bool ICollection.IsSynchronized => ((ICollection)items).IsSynchronized;

		public bool IsReadOnly => false;
		bool IList.IsFixedSize => ((IList)items).IsFixedSize;

		public bool Remove(T item) => items.Remove(item);

		public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

		public int IndexOf(T item) => items.IndexOf(item);

		public void Insert(int index, T item) => items.Insert(index, item);

		public void RemoveAt(int index) => items.RemoveAt(index);
		object IList.this[int index]
		{
			get { return items[index]; }
			set { items[index] = (T) value; }
		}

		public T this[int index]
		{
			get { return items[index]; }
			set { items[index] = value; }
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add { items.CollectionChanged += value; }
			remove { items.CollectionChanged -= value; }
		}
	}
}
