using System.Collections.Generic;
using System.Linq;

namespace Core.Data.CachedCollections.Sorting
{
	public class LastInsertionStrategy<TElement> : SortedInsertionStrategy<TElement>
	{
		public override int DetermineInsertionIndex(IEnumerable<TElement> source, TElement item)
		{
			return source.Count();
		}
	}
}
