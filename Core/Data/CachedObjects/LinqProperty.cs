using System;

namespace Core.Data.CachedObjects
{
	public class LinqProperty<TOwner, TValue> : LinqPropertyBase
		where TOwner : ILinqEntity
	{
		public override Func<ILinqEntity, object> EvaluatorBase
		{
			get { return owner => Evaluator.Invoke((TOwner)owner); }
		}

		public readonly Func<TOwner, TValue> Evaluator;
		
		internal LinqProperty(Func<TOwner, TValue> evaluator, string cachedPropertyName)
			: base(cachedPropertyName, typeof(TValue), typeof(TOwner))
		{
			Evaluator = evaluator;
		}

		public TValue GetValue(TOwner owner)
		{
			return (TValue)owner.GetValueBase(this);
		}
	}
}