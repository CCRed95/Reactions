using System.Windows;
using System.Windows.Media;
using Core.Extensions;
using Core.Markup;

namespace Material.Controls.Ripples
{
	public class TransitionRippleSizeConverter : XAMLConverter<double, double, NULLPARAM, EllipseGeometry>
	{
		public override EllipseGeometry Convert(double a, double b, NULLPARAM param)
		{
			var c = (a.Squared() + b.Squared()).Root();
			return new EllipseGeometry(new Point(32, 32), c, c, new ScaleTransform(.5, .5, .5, .5));
		}
	}
}
