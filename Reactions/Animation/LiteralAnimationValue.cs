namespace Reactions.Animation
{
	public class LiteralAnimationValue<T> : AnimationValue<T> where T : struct
	{
		public LiteralAnimationValue()
		{
		}

		public LiteralAnimationValue(T? literalValue)
		{
			Value = literalValue;
		} 

		public override T? GetEffectiveValue(T? currentValue)
		{
			return Value;
		}
	}
	public class LiteralDoubleAnimationValue : LiteralAnimationValue<double>
	{
		public LiteralDoubleAnimationValue()
		{
		}
		public LiteralDoubleAnimationValue(double? literalValue) : base(literalValue)
		{
		} 
	}
}
