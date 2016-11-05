using System.Windows;
using Core.Helpers.DependencyHelpers;
using Material.Design;
using Material.Static;

namespace Material.Controls.Primitives
{
	//TODO get rid of this
	public static class Theme
	{
		//public static readonly DependencyProperty ThemeProperty = DependencyProperty.RegisterAttached(
		//	"Theme",
		//	typeof(MaterialSet),
		//	typeof(Theme),
		//	new FrameworkPropertyMetadata(Palette.Blue, FrameworkPropertyMetadataOptions.Inherits)
		//	);
		//public static MaterialSet GetTheme(DependencyObject i) => i.Get<MaterialSet>(ThemeProperty);
		//public static void SetTheme(DependencyObject i, MaterialSet v) => i.Set(ThemeProperty, v);
		//public static readonly DependencyProperty ThemeProperty = DependencyProperty.RegisterAttached(
		//	"Theme",
		//	typeof(MaterialSet),
		//	typeof(Theme),
		//	new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits)
		//	);
		//public static MaterialSet GetTheme(DependencyObject i) => i.Get<MaterialSet>(ThemeProperty);
		//public static void SetTheme(DependencyObject i, MaterialSet v) => i.Set(ThemeProperty, v);



		public static readonly DependencyProperty ThemeProperty =
			DP.Attach<MaterialSet>(typeof (Theme), new FrameworkPropertyMetadata(Palette.Blue, FrameworkPropertyMetadataOptions.Inherits));

		public static MaterialSet GetTheme(DependencyObject i) => i.Get<MaterialSet>(ThemeProperty);
		public static void SetTheme(DependencyObject i, MaterialSet v) => i.Set(ThemeProperty, v);

	}
}
