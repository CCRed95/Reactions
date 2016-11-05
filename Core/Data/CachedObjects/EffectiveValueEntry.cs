namespace Core.Data.CachedObjects
{
	internal class EffectiveValueEntry
	{
		protected ILinqEntity Owner { get; }

		protected LinqPropertyBase LinqProperty { get; }

		protected object CachedValue;

		protected bool IsCacheValid;


		internal EffectiveValueEntry(ILinqEntity owner, LinqPropertyBase linqProperty)
		{ 
			Owner = owner;
			LinqProperty = linqProperty;
		}

		internal object GetValue()
		{
			if(IsCacheValid)
				return CachedValue;

			CachedValue = LinqProperty.EvaluatorBase(Owner);
			IsCacheValid = true;
			return CachedValue;

		}

		internal void InvalidateCache()
		{
			IsCacheValid = false;
			Owner.SendPropertyChanged(LinqProperty.PropertyAccessorName);
		}
	}
}
