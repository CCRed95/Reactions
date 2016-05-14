using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Reactions.Markup.Extensions
{
	public class EaseExtension : MarkupExtension
	{
		private EasingType EasingType { get; }
		private EasingMode EasingMode { get; }
		private double? Param1 { get; }
		private int? Param2 { get; }

		public EaseExtension(CustomEasingMode easingMode, EasingType easingType)
		{
			EasingType = easingType;
			EasingMode = easingMode.ToStdEasingMode();
		}
		public EaseExtension(CustomEasingMode easingMode, EasingType easingType, double? param1 = null)
			: this(easingMode, easingType)
		{
			Param1 = param1;
		}
		public EaseExtension(CustomEasingMode easingMode, EasingType easingType, double? param1 = null, int? param2 = null)
			: this(easingMode, easingType, param1)
		{
			Param2 = param2;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			EasingFunctionBase easingFunctionBase = null;
			switch (EasingType)
			{
				case EasingType.Sine:
					easingFunctionBase = new SineEase();
					break;
				case EasingType.Cubic:
					easingFunctionBase = new CubicEase();
					break;
				case EasingType.Quint:
					easingFunctionBase = new QuinticEase();
					break;
				case EasingType.Circ:
					easingFunctionBase = new CircleEase();
					break;
				case EasingType.Quad:
					easingFunctionBase = new QuadraticEase();
					break;
				case EasingType.Quart:
					easingFunctionBase = new QuarticEase();
					break;
				case EasingType.Elastic:
					easingFunctionBase = new ElasticEase
					{
						Springiness = Param1.GetValueOrDefault(3.0),
						Oscillations = Param2.GetValueOrDefault(3)
					};
					break;
				case EasingType.Expo:
					easingFunctionBase = new ExponentialEase
					{
						Exponent = Param1.GetValueOrDefault(2.0)
					};
					break;
				case EasingType.Back:
					easingFunctionBase = new BackEase
					{
						Amplitude = Param1.GetValueOrDefault(1.0)
					};
					break;
				case EasingType.Bounce:
					easingFunctionBase = new BounceEase
					{
						Bounciness = Param1.GetValueOrDefault(2.0),
						Bounces = Param2.GetValueOrDefault(3)
					};
					break;
			}
			if (easingFunctionBase != null) easingFunctionBase.EasingMode = EasingMode;
			return easingFunctionBase;
		}
	}
}
