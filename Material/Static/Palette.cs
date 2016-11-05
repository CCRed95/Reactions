using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Material.Design;

namespace Material.Static
{
	public static class Palette
	{
		public static readonly SolidColorBrush White000 = Brushes.White;
		public static readonly SolidColorBrush Black000 = Brushes.Black;
		public static readonly SolidColorBrush Transparent000 = Brushes.Transparent;

		public static AccentedMaterialSet Red => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Pink => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Purple => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet DeepPurple => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Indigo => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Blue => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet LightBlue => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Cyan => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Teal => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Green => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet LightGreen => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Lime => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Yellow => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Amber => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Orange => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet DeepOrange => ResourceProvider.Extract<AccentedMaterialSet>();
		public static MaterialSet Brown => ResourceProvider.Extract<MaterialSet>();
		public static MaterialSet Grey => ResourceProvider.Extract<MaterialSet>();
		public static AccentedMaterialSet BlueGrey => ResourceProvider.Extract<AccentedMaterialSet>();
		public static AccentedMaterialSet Transparent => ResourceProvider.Extract<AccentedMaterialSet>();
		public static ReadOnlyCollection<AccentedMaterialSet> RecommendedThemeSets = new ReadOnlyCollection<AccentedMaterialSet>(
			new List<AccentedMaterialSet>
			{
				Red, Pink, Purple, DeepPurple, Indigo, Blue, LightBlue, BlueGrey, Teal, Green, Orange, DeepOrange
			});

	}
	//Red, Pink, Purple, DeepPurple, Indigo, Blue, LightBlue, Cyan, Teal, Green, Orange, DeepOrange


}
