using System;
using System.Collections;
using System.Windows;

namespace Core.Data.CachedObjects.Collections
{
	internal sealed class LargeSortedObjectMap : FrugalMapBase
	{
		private int _lastKey = int.MaxValue;
		private const int MINSIZE = 2;
		internal int _count;
		private FrugalMapBase.Entry[] _entries;

		public override int Count
		{
			get
			{
				return this._count;
			}
		}

		public override FrugalMapStoreState InsertEntry(int key, object value)
		{
			bool found;
			int insertIndex = this.FindInsertIndex(key, out found);
			if (found)
			{
				this._entries[insertIndex].Value = value;
				return FrugalMapStoreState.Success;
			}
			if (this._entries != null)
			{
				if (this._entries.Length <= this._count)
				{
					int length = this._entries.Length;
					int num1 = 1;
					int num2 = length >> num1;
					FrugalMapBase.Entry[] entryArray = new FrugalMapBase.Entry[length + num2];
					Array.Copy((Array)this._entries, 0, (Array)entryArray, 0, this._entries.Length);
					this._entries = entryArray;
				}
			}
			else
				this._entries = new FrugalMapBase.Entry[2];
			if (insertIndex < this._count)
				Array.Copy((Array)this._entries, insertIndex, (Array)this._entries, insertIndex + 1, this._count - insertIndex);
			else
				this._lastKey = key;
			this._entries[insertIndex].Key = key;
			this._entries[insertIndex].Value = value;
			this._count = this._count + 1;
			return FrugalMapStoreState.Success;
		}

		public override void RemoveEntry(int key)
		{
			bool found;
			int insertIndex = this.FindInsertIndex(key, out found);
			if (!found)
				return;
			int length = this._count - insertIndex - 1;
			if (length > 0)
				Array.Copy((Array)this._entries, insertIndex + 1, (Array)this._entries, insertIndex, length);
			else
				this._lastKey = this._count <= 1 ? int.MaxValue : this._entries[this._count - 2].Key;
			this._entries[this._count - 1].Key = int.MaxValue;
			this._entries[this._count - 1].Value = DependencyProperty.UnsetValue;
			this._count = this._count - 1;
		}

		public override object Search(int key)
		{
			bool found;
			int insertIndex = this.FindInsertIndex(key, out found);
			if (found)
				return this._entries[insertIndex].Value;
			return DependencyProperty.UnsetValue;
		}

		public override void Sort()
		{
		}

		public override void GetKeyValuePair(int index, out int key, out object value)
		{
			if (index < this._count)
			{
				value = this._entries[index].Value;
				key = this._entries[index].Key;
			}
			else
			{
				value = DependencyProperty.UnsetValue;
				key = int.MaxValue;
				throw new ArgumentOutOfRangeException(nameof(index));
			}
		}

		public override void Iterate(ArrayList list, FrugalMapIterationCallback callback)
		{
			if (this._count <= 0)
				return;
			for (int index = 0; index < this._count; ++index)
				callback(list, this._entries[index].Key, this._entries[index].Value);
		}

		public override void Promote(FrugalMapBase newMap)
		{
			for (int index = 0; index < this._entries.Length; ++index)
			{
				if (newMap.InsertEntry(this._entries[index].Key, this._entries[index].Value) != FrugalMapStoreState.Success)
					throw new ArgumentException("FrugalMap_TargetMapCannotHoldAllData -- newMap");
			}
		}

		private int FindInsertIndex(int key, out bool found)
		{
			int index1 = 0;
			if (this._count > 0 && key <= this._lastKey)
			{
				int num = this._count - 1;
				do
				{
					int index2 = (num + index1) / 2;
					if (key <= this._entries[index2].Key)
						num = index2;
					else
						index1 = index2 + 1;
				}
				while (index1 < num);
				found = key == this._entries[index1].Key;
			}
			else
			{
				index1 = this._count;
				found = false;
			}
			return index1;
		}
	}
}


