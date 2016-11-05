using System.Windows;
using Core.Helpers.DependencyHelpers;
using Material.Design;
using Material.Static;

namespace Material.Controls.Primitives
{
	public class MD
	{
		public static readonly DependencyProperty SetProperty =
			DP.Attach<MaterialSetEnum>(typeof(MD), new FrameworkPropertyMetadata(
				MaterialSetEnum.Pink, FrameworkPropertyMetadataOptions.Inherits, onSetChanged));
		
		public static MaterialSetEnum GetSet(DependencyObject i) => i.Get<MaterialSetEnum>(SetProperty);
		public static void SetSet(DependencyObject i, MaterialSetEnum v) => i.Set(SetProperty, v);


		private static void onSetChanged(DependencyObject i, DependencyPropertyChangedEventArgs e)
		{
			var newValue = (MaterialSetEnum)e.NewValue;
			var theme = MaterialSetMap.GetMaterial(newValue);

			SetTheme(i, theme);
		}

		 
		public static readonly DependencyProperty ThemeProperty =
			DP.Attach<MaterialSet>(typeof(MD), new FrameworkPropertyMetadata(Palette.Pink));

		public static AccentedMaterialSet GetTheme(DependencyObject i) => i.Get<AccentedMaterialSet>(ThemeProperty);
		public static void SetTheme(DependencyObject i, MaterialSet v) => i.Set(ThemeProperty, v);
	}
}
