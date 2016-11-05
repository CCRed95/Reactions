namespace Core.Data.CachedObjects
{
	public class LinqPropertyReference
	{
		private LinqPropertyBase resolvedProperty;

		public int GlobalIndex { get; }

		public LinqPropertyReference(int globalIndex)
		{
			GlobalIndex = globalIndex;
		}

		public LinqPropertyBase ResolvePropertyReference()
		{
			return resolvedProperty ?? (resolvedProperty = LinqPropertyBase.GetPropertyFromIndex(GlobalIndex));
		}
	}
}
