using System.Collections.Generic;
using Caliburn.Micro;
using Core.Data.CachedCollections.Sorting;

namespace Core.Data.CachedCollections
{
	public class CachedCollection<T> : BindableCollection<T>
	{
		protected readonly ISortedInsertionStrategy<T> InsertionStrategy;

		//protected readonly IChunkingStrategy<T> ChunkingStrategy;

		public CachedCollection(IEnumerable<T> source, ISortedInsertionStrategy<T> insertionStrategy) : base(source)
		//IChunkingStrategy<T> chunkingStrategy)
		{
			InsertionStrategy = insertionStrategy;
			//ChunkingStrategy = chunkingStrategy;
			//AppendDataChunk();
		}

		public void LogicalSortedInsertion(T item)
		{
			var insertionIndex = InsertionStrategy.DetermineInsertionIndex(this, item);
			InsertItem(insertionIndex, item);
		}

		//public void AppendDataChunk()
		//{
		//	var dataChunk = ChunkingStrategy.GetNextDataChunk();
		//	AddRange(dataChunk);
		//}
	}

}
