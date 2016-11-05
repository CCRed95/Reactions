using System.Windows;
using Core.Extensions;
using Core.Markup;
using Core.Markup.Converters;

namespace Material.Controls.Ripples
{
	public class DynamicRippleDiameterConverter : XAMLConverter<double, double, Point, double, double>
	{
		public override double Convert(double width, double height, Point cursorPosition, double scale)
		{
			double effectiveRippleCoverWidth;
			double effectiveRippleCoverHeight;

			if (cursorPosition.X < width / 2)
				effectiveRippleCoverWidth = width - cursorPosition.X;

			else
				effectiveRippleCoverWidth = cursorPosition.X;

			if (cursorPosition.Y < height / 2)
				effectiveRippleCoverHeight = height - cursorPosition.Y;

			else
				effectiveRippleCoverHeight = cursorPosition.Y;

			var radius = (effectiveRippleCoverWidth.Squared() + effectiveRippleCoverHeight.Squared()).Root();

			return radius * scale;
		}
	}
}
