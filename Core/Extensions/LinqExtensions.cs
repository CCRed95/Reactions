using System;
using System.Collections.Generic;
using System.Linq;
using Core.Collections;

namespace Core.Extensions
{
	public static class LinqExtensions
	{
		public static bool None<TSource>(this IEnumerable<TSource> source)
		{
			return !source.Any();
			//if (source == null) throw Error.ArgumentNull("source");
			//using (IEnumerator<TSource> e = source.GetEnumerator())
			//{
			//	if (e.MoveNext()) return true;
			//}
			//return false;
		}

		public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return !source.Any(predicate);
			//if (source == null)
			//	throw new ArgumentNullException(nameof(source));
			//if (predicate == null)
			//	throw new ArgumentNullException(nameof(predicate));

			//foreach (TSource element in source)
			//{
			//	if (predicate(element)) return true;
			//}
			//return false;
		}

		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
		{
			while (source.Any())
			{
				yield return source.Take(chunksize);
				source = source.Skip(chunksize);
			}
		}
		public static IEnumerable<IEnumerable<T>> ChunkTrivial<T>(this IEnumerable<T> source, int chunksize)
		{
			var pos = 0;
			while (source.Skip(pos).Any())
			{
				yield return source.Skip(pos).Take(chunksize);
				pos += chunksize;
			}
		}
		public static IEnumerable<IGrouping<TKey, TElement>> GroupBySequence<TSource, TKey, TElement>
		(this TSource[] source,
		 Func<TSource, TKey> keySelector,
		 Func<TSource, TElement> elementSelector,
		 IEqualityComparer<TKey> comparer)
		{
			var newElement = source.Select(keySelector).ToArray().MakeSequentialKey(comparer).Zip(
					source.Select(elementSelector),
					(x, y) => new Tuple<int, TElement>(x, y));

			var groupElement = newElement.GroupBy(t => t.Item1, t => t.Item2);

			var newKey = source.Select(keySelector).ToArray().MakeSequentialKey(comparer).Zip(
					source.Select(keySelector),
					(x, y) => new Tuple<int, TKey>(x, y));

			var groupKey = newKey.GroupBy(t => t.Item1, t => t.Item2);

			return groupKey.Zip(groupElement,
					(key, element) => new Grouping<TKey, TElement>(key.First(), element));
		}

		public static IEnumerable<TResult> GroupBySequence<TSource, TKey, TElement, TResult>
				(this TSource[] source,
				 Func<TSource, TKey> keySelector,
				 Func<TSource, TElement> elementSelector,
				 Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
				 IEqualityComparer<TKey> comparer)
		{
			return source.GroupBySequence(keySelector,
					elementSelector, comparer).Select(x => resultSelector(x.Key, x));
		}
		//Performs an operation over each consecutive item. Here used for determining equality.
		public static IEnumerable<TResult> WithNext<T, TResult>
				(this T[] source, Func<T, T, TResult> operation)
		{
			return source.Zip(source.Skip(1), operation);
		}

		//Makes the unique key
		public static IEnumerable<int> MakeSequentialKey<T>
				(this T[] source, IEqualityComparer<T> comparer)
		{

			if (source.Length == 0)
				return Enumerable.Empty<int>();

			return (new[] { 0 })
					.Concat(source.ToArray().WithNext<T, int>((x, y) => comparer.Equals(x, y) ? 0 : 1))
					.ToArray()
					.RunningSum();
		}

		//Sum of all previous elements up to each item of an array
		public static IEnumerable<int> RunningSum(this int[] source)
		{
			int cumul = 0;
			foreach (int i in source)
				yield return cumul += i;
		}
		public static bool AnyAreNull<TSource>(this IEnumerable<TSource> source)
		{
			return source.Any(t => t == null);
			//using (IEnumerator<TSource> e = source.GetEnumerator())
			//{
			//	e.MoveNext();
			//	if (e.Current == null)
			//	{
			//		return true;
			//	}
			//}
			//return false;
		}


	}
	public class IntComparator : IEqualityComparer<int>
	{
		public bool Equals(int x, int y)
		{
			return x == y;
		}

		public int GetHashCode(int obj)
		{
			return obj.GetHashCode();
		}
	}
}
