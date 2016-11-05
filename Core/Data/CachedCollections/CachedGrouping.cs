using System;
using System.Collections;
using System.Collections.Generic;
using Core.Data.CachedCollections.Sorting;

namespace Core.Data.CachedCollections
{
	public interface ICachedGrouping<out TKey, TElement> : IEnumerable<TElement>
	{
		ISortedInsertionStrategy<TElement> SubInsertionStrategy { get; }
		TKey Key { get; }
	}

	public class CachedGrouping<TKey, TElement> : ICachedGrouping<TKey, TElement>
	{
		private readonly CachedCollection<TElement> Elements;

		public CachedCollection<TElement> __CACHEDCOLLECTION => Elements; 

		public ISortedInsertionStrategy<TElement> SubInsertionStrategy { get; }

		public TKey Key { get; }

		public CachedGrouping(TKey key, IEnumerable<TElement> elements, ISortedInsertionStrategy<TElement> subInsertionStrategy)
		{
			Key = key;
			SubInsertionStrategy = subInsertionStrategy;
			if (elements is CachedCollection<TElement>)
			{
				throw new NotSupportedException();
			}
			Elements = new CachedCollection<TElement>(elements, subInsertionStrategy);
		}

		public IEnumerator<TElement> GetEnumerator()
		{
			return Elements.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Elements.GetEnumerator();
		}
	}
}
/*	public class CachedGrouping<TKey, TElement> : ICachedGrouping<TKey, TElement>
	{
		private readonly CachedCollection<TElement> Elements;

		public CachedCollection<TElement> __CACHEDCOLLECTION => Elements; 

		public ISortedInsertionStrategy<TElement> SubInsertionStrategy { get; }

		public TKey Key { get; }

		public CachedGrouping(TKey key, CachedCollection<TElement> cachedCollection, ISortedInsertionStrategy<TElement> subInsertionStrategy)
		{
			Key = key;
			SubInsertionStrategy = subInsertionStrategy;
			Elements = cachedCollection;
			//Elements = new CachedCollection<TElement>(elements, subInsertionStrategy);
		}

		public IEnumerator<TElement> GetEnumerator()
		{
			return Elements.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Elements.GetEnumerator();
		}
	}
}
*/