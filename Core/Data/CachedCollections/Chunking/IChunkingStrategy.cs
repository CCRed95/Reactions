using System.Collections.Generic;

namespace Core.Data.CachedCollections.Chunking
{
	public interface IChunkingStrategy<out T>
	{
		IEnumerable<T> GetNextDataChunk();
	}
}
