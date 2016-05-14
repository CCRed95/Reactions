using System.Windows.Media;
using Material.Extensions;

namespace Material.Design.Descriptors
{
	public class LuminosityMaterialDescriptor : AbstractMaterialDescriptor
	{
		public Luminosity Luminosity { get; set; }

		public override SolidColorBrush GetMaterial(MaterialSet materialSet)
		{
			return materialSet.FromLuminosity(Luminosity).WithOpacity(Opacity);
		}

		public LuminosityMaterialDescriptor(Luminosity luminosity, double opacity = 1.0)
		{
			Luminosity = luminosity;
			Opacity = opacity;
		}
	}
}