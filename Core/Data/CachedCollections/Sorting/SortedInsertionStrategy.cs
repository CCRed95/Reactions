using System.Collections.Generic;

namespace Core.Data.CachedCollections.Sorting
{
	public abstract class SortedInsertionStrategy<TElement> : ISortedInsertionStrategy<TElement>
	{
		public abstract int DetermineInsertionIndex(IEnumerable<TElement> source, TElement item);
	}
}
