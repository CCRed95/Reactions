using System.Collections;

namespace Core.Data.CachedObjects.Collections
{
	internal abstract class FrugalMapBase
	{
		protected const int INVALIDKEY = 2147483647;

		public abstract int Count { get; }

		public abstract FrugalMapStoreState InsertEntry(int key, object value);

		public abstract void RemoveEntry(int key);

		public abstract object Search(int key);

		public abstract void Sort();

		public abstract void GetKeyValuePair(int index, out int key, out object value);

		public abstract void Iterate(ArrayList list, FrugalMapIterationCallback callback);

		public abstract void Promote(FrugalMapBase newMap);

		internal struct Entry
		{
			public int Key;
			public object Value;
		}
	}
}
