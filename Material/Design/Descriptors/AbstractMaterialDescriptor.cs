using System.ComponentModel;
using System.Windows.Media;
using Material.Markup.Converters;

namespace Material.Design.Descriptors
{
	[TypeConverter(typeof(DescriptorConverter))]
	public abstract class AbstractMaterialDescriptor
	{
		public double Opacity { get; set; } = 1.0;
		public abstract SolidColorBrush GetMaterial(MaterialSet materialSet);
	}
}
