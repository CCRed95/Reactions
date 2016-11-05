using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.CachedCollections.Sorting
{
	public enum OrderByDirection
	{
		Ascending,
		Descending
	}
	public class OrderByInsertionStrategy<TElement, TKey> : SortedInsertionStrategy<TElement>
	{
		internal Func<TElement, TKey> Selector;
		internal IComparer<TKey> Comparer;
		internal OrderByDirection Direction;


		public OrderByInsertionStrategy(Func<TElement, TKey> selector, IComparer<TKey> comparer, OrderByDirection direction)
		{
			if (selector == null)
				throw new ArgumentNullException(nameof(selector));
			Selector = selector;

			Comparer = comparer ?? Comparer<TKey>.Default;
			Direction = direction;
		}

		public override int DetermineInsertionIndex(IEnumerable<TElement> source, TElement item)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			if (Direction == OrderByDirection.Ascending)
			{
				var insertionKey = Selector(item);

				var index = 0;
				foreach (var sortKey in source.Select(Selector))
				{
					if (sortKey == null)
						throw new NullReferenceException("could not select key of sorted value");

					var difference = Comparer.Compare(sortKey, insertionKey);

					if (difference > 0)
					{
						return index;
					}
					index++;
				}
				return index;
			}
			else
			{
				var insertionKey = Selector(item);

				var index = 0;
				foreach (var sortKey in source.Select(Selector))
				{
					if (sortKey == null)
						throw new NullReferenceException("could not select key of sorted value");

					var difference = Comparer.Compare(sortKey, insertionKey);

					if (difference < 0)
					{
						return index;
					}
					index++;
				}
				return index;
			}
		}
	}
}