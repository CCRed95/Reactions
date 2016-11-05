using System;
using System.Collections.Generic;

namespace Core.Data.CachedCollections.Chunking
{
	public class FixedBufferChunkingStrategy<T> : IChunkingStrategy<T>
	{
		protected int currentSkipOffset;

		public int FixedBufferLength { get; set; } = 5;

		protected Func<int, int, IEnumerable<T>> FetchNextDataChunk;

		public FixedBufferChunkingStrategy(Func<int, int, IEnumerable<T>> fetchNextDataChunk)
		{
			FetchNextDataChunk = fetchNextDataChunk;
		}

		public IEnumerable<T> GetNextDataChunk()
		{
			var dataChunk = FetchNextDataChunk(currentSkipOffset, FixedBufferLength);
			FixedBufferLength += currentSkipOffset;
			return dataChunk;
		}
	}
}
