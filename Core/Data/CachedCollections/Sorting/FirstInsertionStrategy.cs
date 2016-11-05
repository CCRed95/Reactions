using System.Collections.Generic;

namespace Core.Data.CachedCollections.Sorting
{
	public class FirstInsertionStrategy<TElement> : SortedInsertionStrategy<TElement>
	{
		public override int DetermineInsertionIndex(IEnumerable<TElement> source, TElement item)
		{
			return 0;
		}
	}
}
