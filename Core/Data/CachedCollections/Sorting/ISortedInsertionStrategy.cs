using System.Collections.Generic;

namespace Core.Data.CachedCollections.Sorting
{
	public interface ISortedInsertionStrategy<in TElement>
	{
		int DetermineInsertionIndex(IEnumerable<TElement> source, TElement item);
	}
}
