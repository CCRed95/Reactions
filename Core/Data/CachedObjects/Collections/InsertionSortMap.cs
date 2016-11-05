using System;
using System.Collections;

namespace Core.Data.CachedObjects.Collections
{
	internal struct InsertionSortMap
	{
		internal LargeSortedObjectMap _mapStore;

		public object this[int key]
		{
			get
			{
				return _mapStore != null ? _mapStore.Search(key) : LinqPropertyBase.UnsetValue;
			}
			set
			{
				if (value != LinqPropertyBase.UnsetValue)
				{
					if (_mapStore == null)
						_mapStore = new LargeSortedObjectMap();
					var frugalMapStoreState = _mapStore.InsertEntry(key, value);
					if (frugalMapStoreState == FrugalMapStoreState.Success)
						return;
					if (FrugalMapStoreState.SortedArray != frugalMapStoreState)
						throw new InvalidOperationException("MS.Internal.WindowsBase.SR.Get(FrugalMap_CannotPromoteBeyondHashtable)");
					var largeSortedObjectMap = new LargeSortedObjectMap();
					_mapStore.Promote(largeSortedObjectMap);
					_mapStore = largeSortedObjectMap;
					var num = (int)_mapStore.InsertEntry(key, value);
				}
				else
				{
					if (_mapStore == null)
						return;
					_mapStore.RemoveEntry(key);
					if (_mapStore.Count != 0)
						return;
					_mapStore = null;
				}
			}
		}

		public int Count => _mapStore?.Count ?? 0;

		public void Sort()
		{
			_mapStore?.Sort();
		}

		public void GetKeyValuePair(int index, out int key, out object value)
		{
			if (_mapStore == null)
				throw new ArgumentOutOfRangeException(nameof(index));
			_mapStore.GetKeyValuePair(index, out key, out value);
		}

		public void Iterate(ArrayList list, FrugalMapIterationCallback callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));
			if (list == null)
				throw new ArgumentNullException(nameof(list));
			_mapStore?.Iterate(list, callback);
		}
	}
}
