using System;
using Material.Static;

namespace Material.Design
{
	public enum MaterialSetEnum
	{
		Red,
		Pink,
		Purple,
		DeepPurple,
		Indigo,
		Blue,
		LightBlue,
		Cyan,
		Teal,
		Green,
		LightGreen,
		Lime,
		Yellow,
		Amber,
		Orange,
		DeepOrange,
		Brown,
		Grey,
		BlueGrey,
		Transparent
	}

	internal static class MaterialSetMap
	{
		public static MaterialSet AsSet(this MaterialSetEnum i)
		{
			return GetMaterial(i);
		}

		public static MaterialSet GetMaterial(MaterialSetEnum materialSetEnum)
		{
			switch (materialSetEnum)
			{
				case MaterialSetEnum.Red:
					return Palette.Red;

				case MaterialSetEnum.Pink:
					return Palette.Pink;

				case MaterialSetEnum.Purple:
					return Palette.Purple;

				case MaterialSetEnum.DeepPurple:
					return Palette.DeepPurple;

				case MaterialSetEnum.Indigo:
					return Palette.Indigo;

				case MaterialSetEnum.Blue:
					return Palette.Blue;

				case MaterialSetEnum.LightBlue:
					return Palette.LightBlue;

				case MaterialSetEnum.Cyan:
					return Palette.Cyan;

				case MaterialSetEnum.Teal:
					return Palette.Teal;

				case MaterialSetEnum.Green:
					return Palette.Green;

				case MaterialSetEnum.LightGreen:
					return Palette.LightGreen;

				case MaterialSetEnum.Lime:
					return Palette.Lime;

				case MaterialSetEnum.Yellow:
					return Palette.Yellow;

				case MaterialSetEnum.Amber:
					return Palette.Amber;

				case MaterialSetEnum.Orange:
					return Palette.Orange;

				case MaterialSetEnum.DeepOrange:
					return Palette.DeepOrange;

				case MaterialSetEnum.Brown:
					return Palette.Brown;

				case MaterialSetEnum.Grey:
					return Palette.Grey;

				case MaterialSetEnum.BlueGrey:
					return Palette.BlueGrey;

				case MaterialSetEnum.Transparent:
					return Palette.Transparent;

				default:
					throw new ArgumentOutOfRangeException(nameof(materialSetEnum));
			}
		}
	}
}
