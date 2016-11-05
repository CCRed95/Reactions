using System;
using System.Windows.Data;
using System.Windows.Markup;
using Material.Design;

namespace Material.Static
{
	public class ThemeExtension : MarkupExtension
	{
		private const string rootPath = "(ThemePrimitive.DocumentTheme).";
		private DocumentThemeBindingItem Element { get; }
		public string SubPath { get; }= "";
		public ThemeExtension(DocumentThemeBindingItem element)
		{
			Element = element;
		}
		public ThemeExtension(DocumentThemeBindingItem element, string subPath)
		{
			Element = element;
			SubPath = $".{subPath}";
		}
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var e = Element.ToString();
			return new Binding(rootPath + e + SubPath) {RelativeSource = new RelativeSource(RelativeSourceMode.Self)}.ProvideValue(serviceProvider);
		}
	}
}
