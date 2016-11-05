using System.Windows;
using System.Windows.Media;
using Core.Require;
using Material.Design.Text;

namespace Material.Static
{
	public static class Text
	{
		public static class Fonts
		{
			private static readonly FontFamilyConverter ffc = new FontFamilyConverter();
			internal static FontFamily TimesNewRoman => (FontFamily)ffc.ConvertFromString("Times New Roman");
			public static FontFamily Roboto => (FontFamily)ffc.ConvertFromString("Roboto");
		}
		//TODO make all this stuff FlexEnum<T>
		//TODO should be using DESKTOP font sizes in md specs instead of device?
		public class FontSizes
		{
			private static readonly FontSizeConverter fsc = new FontSizeConverter();

			public static double SP12 => fsc.ConvertFromString("12pt").RequireType<double>();
			public static double SP14 => fsc.ConvertFromString("14pt").RequireType<double>();
			public static double SP16 => fsc.ConvertFromString("16pt").RequireType<double>();
			public static double SP20 => fsc.ConvertFromString("20pt").RequireType<double>();
			public static double SP24 => fsc.ConvertFromString("24pt").RequireType<double>();
			public static double SP34 => fsc.ConvertFromString("34pt").RequireType<double>();
			public static double SP45 => fsc.ConvertFromString("45pt").RequireType<double>();
			public static double SP56 => fsc.ConvertFromString("56pt").RequireType<double>();
			public static double SP112 => fsc.ConvertFromString("112pt").RequireType<double>();
		}

		public class FontWeights
		{
			private static readonly FontWeightConverter fwc = new FontWeightConverter();
			public static FontWeight Thin => fwc.ConvertFromString("Thin").RequireType<FontWeight>();
			public static FontWeight Light => fwc.ConvertFromString("Light").RequireType<FontWeight>();
			public static FontWeight Regular => fwc.ConvertFromString("Regular").RequireType<FontWeight>();
			public static FontWeight Medium => fwc.ConvertFromString("Medium").RequireType<FontWeight>();
			public static FontWeight Bold => fwc.ConvertFromString("Bold").RequireType<FontWeight>();
			public static FontWeight Black => fwc.ConvertFromString("Black").RequireType<FontWeight>();
		}
		//TODO auto fontattr creators using [CallerMemberName]. generic T Create<T, TC>([CMN] auto) where TC:Typeconverter
		public class FontStyles
		{
			private static readonly FontStyleConverter fsc = new FontStyleConverter();

			public static FontStyle Normal = fsc.ConvertFromString("Normal").RequireType<FontStyle>();
			public static FontStyle Italic = fsc.ConvertFromString("Italic").RequireType<FontStyle>();
			// TODO does roboto have oblique?
			public static FontStyle Oblique = fsc.ConvertFromString("Oblique").RequireType<FontStyle>();
		}
		//TODO does roboto support these stretches
		public class FontStretches
		{
			private static readonly FontStretchConverter fsc = new FontStretchConverter();

			public static FontStretch ExtraCondensed = fsc.ConvertFromString("ExtraCondensed").RequireType<FontStretch>();
			public static FontStretch Condensed = fsc.ConvertFromString("Condensed").RequireType<FontStretch>();
			public static FontStretch SemiCondensed = fsc.ConvertFromString("SemiCondensed").RequireType<FontStretch>();
			public static FontStretch Normal = fsc.ConvertFromString("Normal").RequireType<FontStretch>();
			public static FontStretch Medium = fsc.ConvertFromString("Medium").RequireType<FontStretch>();
			public static FontStretch SemiExpanded = fsc.ConvertFromString("SemiExpanded").RequireType<FontStretch>();
			public static FontStretch Expanded = fsc.ConvertFromString("Expanded").RequireType<FontStretch>();
			public static FontStretch ExtraExpanded = fsc.ConvertFromString("ExtraExpanded").RequireType<FontStretch>();
			public static FontStretch UltraExpanded = fsc.ConvertFromString("UltraExpanded").RequireType<FontStretch>();
		}

		public class Typesets
		{
			public static FlexTypeface Button => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Medium, FontStretches.Normal, FontSizes.SP14);

			public static FlexTypeface Caption => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Regular, FontStretches.Normal, FontSizes.SP12);

			public static FlexTypeface Body1 => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Regular, FontStretches.Normal, FontSizes.SP14);

			public static FlexTypeface Body2 => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Medium, FontStretches.Normal, FontSizes.SP14);

			public static FlexTypeface Subhead => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Regular, FontStretches.Normal, FontSizes.SP16);

			public static FlexTypeface Title => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Medium, FontStretches.Normal, FontSizes.SP20);

			public static FlexTypeface Headline => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Regular, FontStretches.Normal, FontSizes.SP24);

			public static FlexTypeface Display1 => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Regular, FontStretches.Normal, FontSizes.SP34);

			public static FlexTypeface Display2 => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Regular, FontStretches.Normal, FontSizes.SP45);

			public static FlexTypeface Display3 => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Regular, FontStretches.Normal, FontSizes.SP56);

			public static FlexTypeface Display4 => new FlexTypeface(Fonts.Roboto, FontStyles.Normal,
				FontWeights.Light, FontStretches.Normal, FontSizes.SP112);
		}
	}
}
