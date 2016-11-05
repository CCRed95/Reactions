using System.Windows.Media;
using System.Windows.Media.Effects;
using Core.Extensions;

namespace Material.Controls.Shadows
{
	public static class ShadowInterpolator
	{
		private const double opacity = .42;
		private static readonly Color shadowColor = Colors.Black;

		public static DropShadowEffect Interpolate(double level, ShadowDirection direction)
		{
			var blur = calculateBlur(level);
			var depth = calculateDepth(level);

			return new DropShadowEffect
			{
				BlurRadius = blur,
				ShadowDepth = depth,
				Direction = direction == ShadowDirection.Down ? 270 : 90,
				Color = shadowColor,
				Opacity = opacity
			};
		}

		public static double calculateDepth(double x)
		{
			const double a = .124;
			const double b = -1.5833333333;
			const double c = 7.625;
			const double d = -13.166666667;
			const double e = 8;

			return a * x.Power(4) + b * x.Power(3) + c * x.Power(2) + d * x + e;
		}

		public static double calculateBlur(double x)
		{
			const double a = -.333333333;
			const double b = 4.357142857;
			const double c = -8.30952381;
			const double d = 9.4;

			return a * x.Power(3) + b * x.Power(2) + c * x + d;
		}

	}
}
