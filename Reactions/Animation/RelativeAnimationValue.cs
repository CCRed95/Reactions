
using System.ComponentModel;

namespace Reactions.Animation
{
	public enum RelativeAnimationOperation
	{
		Add,
		Subtract,
		Multiply,
		Divide
	}
	public class RelativeAnimationValue<T> : AnimationValue<T> where T : struct
	{
		public RelativeAnimationOperation Operation { get; set; }
		
		public override T? GetEffectiveValue(T? currentValue)
		{
			switch (Operation)
			{
				case RelativeAnimationOperation.Add:
					return addDynamic(currentValue, Value);

				case RelativeAnimationOperation.Subtract:
					return subtractDynamic(currentValue, Value);

				case RelativeAnimationOperation.Multiply:
					return multiplyDynamic(currentValue, Value);

				case RelativeAnimationOperation.Divide:
					return divideDynamic(currentValue, Value);

				default:
					throw new InvalidEnumArgumentException();
			}
		}

		public RelativeAnimationValue()
		{
		}
		public RelativeAnimationValue(RelativeAnimationOperation operation, T? value)
		{
			Operation = operation;
			Value = value;
		} 

		private static TValue addDynamic<TValue>(TValue left, TValue right)
		{
			return (dynamic)left + (dynamic)right;
		}
		private static TValue subtractDynamic<TValue>(TValue left, TValue right)
		{
			return (dynamic)left + (dynamic)right;
		}
		private static TValue multiplyDynamic<TValue>(TValue left, TValue right)
		{
			return (dynamic)left * (dynamic)right;
		}
		private static TValue divideDynamic<TValue>(TValue left, TValue right)
		{
			return (dynamic)left / (dynamic)right;
		}
	}

	public class RelativeDoubleAnimationValue : RelativeAnimationValue<double>
	{
		public RelativeDoubleAnimationValue()
		{
		}
		public RelativeDoubleAnimationValue(RelativeAnimationOperation operation, double? value) : base(operation, value)
		{
		} 
	}
}
