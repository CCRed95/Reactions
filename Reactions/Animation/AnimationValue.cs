using System.ComponentModel;

namespace Reactions.Animation
{
	public interface IAnimationValueCore
	{
		object Value { get; }

		object GetEffectiveValue(object currentValue);

		bool IsUnset { get; }
	}

	[TypeConverter(typeof(AnimationValueConverter))]
	public abstract class AnimationValue<T> : IAnimationValueCore where T : struct
	{
		object IAnimationValueCore.Value => Value;
		object IAnimationValueCore.GetEffectiveValue(object currentValue)
		{
			return GetEffectiveValue((T?)currentValue);
		}

		protected T? Value { get; set; }

		public abstract T? GetEffectiveValue(T? currentValue);

		public bool IsUnset => Value == null;
	}
}
