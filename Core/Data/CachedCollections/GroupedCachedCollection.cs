using System;
using System.Collections.Generic;
using Core.Data.CachedCollections.Sorting;

namespace Core.Data.CachedCollections
{
	public class GroupedCachedCollection<TCachedGrouping, TKey, TElement> : CachedCollection<TCachedGrouping> 
		where TCachedGrouping : ICachedGrouping<TKey, TElement>
	{
		protected Func<CachedCollection<TCachedGrouping>, TElement, CachedCollection<TElement>> SubInsertionGroupSelector { get; }

		public GroupedCachedCollection(IEnumerable<TCachedGrouping> source, 
			ISortedInsertionStrategy<TCachedGrouping> insertionStrategy, 
			Func<CachedCollection<TCachedGrouping>, TElement, CachedCollection<TElement>> subInsertionGroupSelector) : base(source, insertionStrategy)
		{
			SubInsertionGroupSelector = subInsertionGroupSelector;
		}
		public void LogicalSortedSubInsertion(TElement item)
		{
			var cachedSubCollectionTarget = SubInsertionGroupSelector(this, item);
			cachedSubCollectionTarget.LogicalSortedInsertion(item);
		}
	}
}
