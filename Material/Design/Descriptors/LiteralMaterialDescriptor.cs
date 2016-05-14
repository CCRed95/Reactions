using System.Windows.Media;
using Material.Extensions;

namespace Material.Design.Descriptors
{
	public class LiteralMaterialDescriptor : AbstractMaterialDescriptor
	{
		public SolidColorBrush LiteralMaterial { get; set; }

		public override SolidColorBrush GetMaterial(MaterialSet materialSet) => LiteralMaterial.WithOpacity(Opacity);

		public LiteralMaterialDescriptor(SolidColorBrush literalMaterial, double opacity = 1.0)
		{
			LiteralMaterial = literalMaterial;
			Opacity = opacity;
		}
	}
}
