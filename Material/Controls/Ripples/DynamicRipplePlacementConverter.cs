using System.Windows;
using Core.Extensions;
using Core.Markup;
using Core.Markup.Converters;

namespace Material.Controls.Ripples
{
	public class DynamicRipplePlacementConverter : XAMLConverter<double, double, double, Point, NULLPARAM, Point>
	{
		public override Point Convert(double width, double height, double rippleHeight, Point mouse, NULLPARAM param)
		{
			var upperRightOriginOffset = new Point(-rippleHeight / 2, -rippleHeight / 2);
			var rippleOffset = upperRightOriginOffset.Push(mouse.X, mouse.Y);
			return rippleOffset;
		}
	}
}
