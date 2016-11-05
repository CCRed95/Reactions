using System.Windows;
using System.Windows.Media;
using Core.Extensions;
using Core.Markup;
using Core.Markup.Converters;

namespace Material.Controls.Ripples
{
	public class DynamicEllipseGeometryMaskConverter : XAMLConverter<double, double, double, double, double, double, NULLPARAM, EllipseGeometry>
	{
		public override EllipseGeometry Convert(double actualWidth, double actualHeight, double ellipseWidth, double ellipseHeight, double placementX, double placementY, NULLPARAM param)
		{
			var radiusX = ellipseWidth / 2;
			var radiusY = ellipseHeight / 2;

			var theoreticalCenter = new Point(radiusX, radiusY);
			//var actualCenter = theoreticalCenter.Push(placementX, placementY);

			return new EllipseGeometry
			{
				Center = theoreticalCenter,
				RadiusX = radiusX,
				RadiusY = radiusY,
				//Transform = new TranslateTransform(placementX, placementY)
			};
		}
	}
}
