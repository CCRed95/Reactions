using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Material.Extensions;
using Material.Static;

namespace Material.Design.Providers
{
	public class GradientMaterialProvider : IMaterialProvider
	{
		private int currentIndex;

		public List<GradientMaterialStop> GradientStepData { get; }
		public void Reset()
		{
			currentIndex = 0;
		}

		public void Reset(ProviderContext context)
		{
			currentIndex = 0;
		}

		public GradientMaterialProvider(List<GradientMaterialStop> gradientStepData)
		{
			GradientStepData = gradientStepData;
		}

		public MaterialSet ProvideNext(ProviderContext context)
		{

			if (currentIndex > context.CycleLength) currentIndex = 0;

			var position = 1.0 / context.CycleLength * currentIndex;
			var gradientStepSpan = GradientStepData.Sum(step => step.Offset);
			var additivestepPercentage = 0d;
			MaterialSet interpolatedMaterialSet = Palette.Brown;
			if (position >= 1)
			{
				currentIndex++;
				return GradientStepData.Last().MaterialSet;
			}
			for (var x = 0; x <= GradientStepData.Count - 2; x++)
			{
				var stepPercentage = GradientStepData[x].Offset / gradientStepSpan;
				var stepProgression = position - additivestepPercentage;
				var stepProgressionPercentage = stepProgression / stepPercentage;

				if (position >= additivestepPercentage && position < (additivestepPercentage + stepPercentage))
				{
					interpolatedMaterialSet = MediaExtensions.Interpolate(GradientStepData[x].MaterialSet,
						GradientStepData[x + 1].MaterialSet, stepProgressionPercentage);
					break;
				}
				additivestepPercentage += stepPercentage;
			}
			currentIndex++;
			return interpolatedMaterialSet;
		}

		public static GradientMaterialProvider MaterialRYG => new GradientMaterialProvider(
			new List<GradientMaterialStop>
			{
				new GradientMaterialStop(Palette.Red),
				new GradientMaterialStop(Palette.Yellow),
				new GradientMaterialStop(Palette.Green, 0)
			});
		public static GradientMaterialProvider MaterialHLbB => new GradientMaterialProvider(
			new List<GradientMaterialStop>
			{
				new GradientMaterialStop(Palette.Pink),
				new GradientMaterialStop(Palette.Purple ),
				new GradientMaterialStop(Palette.DeepPurple),
				new GradientMaterialStop(Palette.Blue),
				new GradientMaterialStop(Palette.LightBlue),
				new GradientMaterialStop(Palette.Teal, 0),
			});
		public static GradientMaterialProvider RainbowPaletteOrder => new GradientMaterialProvider(
			new List<GradientMaterialStop>
			{
				new GradientMaterialStop(Palette.Red),
				new GradientMaterialStop(Palette.Pink ),
				new GradientMaterialStop(Palette.Purple),
				new GradientMaterialStop(Palette.DeepPurple),
				new GradientMaterialStop(Palette.Indigo),
				new GradientMaterialStop(Palette.Blue),
				new GradientMaterialStop(Palette.LightBlue),
				new GradientMaterialStop(Palette.Cyan),
				new GradientMaterialStop(Palette.Teal),
				new GradientMaterialStop(Palette.Green),
				new GradientMaterialStop(Palette.LightGreen),
				new GradientMaterialStop(Palette.Lime),
				new GradientMaterialStop(Palette.Yellow),
				new GradientMaterialStop(Palette.Amber),
				new GradientMaterialStop(Palette.Orange),
				new GradientMaterialStop(Palette.DeepOrange, 0)
			});
		public static GradientMaterialProvider RainbowRefractionOrder => new GradientMaterialProvider(
			new List<GradientMaterialStop>
			{
				new GradientMaterialStop(Palette.Red),
				new GradientMaterialStop(Palette.DeepOrange),
				new GradientMaterialStop(Palette.Orange),
				new GradientMaterialStop(Palette.Amber),
				new GradientMaterialStop(Palette.Lime),
				new GradientMaterialStop(Palette.Yellow),
				new GradientMaterialStop(Palette.LightGreen),
				new GradientMaterialStop(Palette.Green),
				new GradientMaterialStop(Palette.Teal),
				new GradientMaterialStop(Palette.Cyan),
				new GradientMaterialStop(Palette.LightBlue),
				new GradientMaterialStop(Palette.Blue),
				new GradientMaterialStop(Palette.Indigo),
				new GradientMaterialStop(Palette.DeepPurple),
				new GradientMaterialStop(Palette.Purple),
				new GradientMaterialStop(Palette.Pink, 0)
			});
		
		public static GradientMaterialProvider CoolColors => new GradientMaterialProvider(
			new List<GradientMaterialStop>
			{
				new GradientMaterialStop(Palette.Purple),
				new GradientMaterialStop(Palette.DeepPurple),
				new GradientMaterialStop(Palette.Indigo),
				new GradientMaterialStop(Palette.Blue),
				new GradientMaterialStop(Palette.LightBlue),
				new GradientMaterialStop(Palette.Cyan),
				new GradientMaterialStop(Palette.Teal),
				new GradientMaterialStop(Palette.Green, 0),
			});

		//public static ComplexGradientBrushProvider MaterialPBDark => new ComplexGradientBrushProvider(
		//	new List<ComplexGradientStep>
		//	{
		//		new ComplexGradientStep(Material.Pink.Darken(.4)),
		//		new ComplexGradientStep(Material.Teal.Lighten(.05), 0)
		//	})
		//{ ProviderFunction = new DarkenBrushFunction { DarkenPercentage = 0 } };
		//public static ComplexGradientBrushProvider MaterialBPDark => new ComplexGradientBrushProvider(
		//	new List<ComplexGradientStep>
		//	{
		//		new ComplexGradientStep(Material.Blue.Darken(.2)),
		//		new ComplexGradientStep(Material.Pink, 0)
		//	})
		//{ ProviderFunction = new DarkenBrushFunction { DarkenPercentage = .1 } };
		//public static ComplexGradientBrushProvider MaterialGB => new ComplexGradientBrushProvider(
		//	new List<ComplexGradientStep>
		//	{
		//		new ComplexGradientStep(Material.Green),
		//		new ComplexGradientStep(Material.Teal),
		//		new ComplexGradientStep(Material.LightBlue),
		//		new ComplexGradientStep(Material.Indigo, 0)
		//	})
		//{ ProviderFunction = new LightenBrushFunction { LightenPercentage = .3 } };
		//public static ComplexGradientBrushProvider MaterialBG => new ComplexGradientBrushProvider(
		//	new List<ComplexGradientStep>
		//	{
		//		new ComplexGradientStep(Material.Indigo),
		//		new ComplexGradientStep(Material.LightBlue),
		//		new ComplexGradientStep(Material.Teal),
		//		new ComplexGradientStep(Material.Green, 0)
		//	})
		//{ ProviderFunction = new LightenBrushFunction { LightenPercentage = .3 } };

		//public static ComplexGradientBrushProvider MaterialRainbow => new ComplexGradientBrushProvider(
		//	new List<ComplexGradientStep>
		//	{
		//		new ComplexGradientStep(Material.Red),
		//		new ComplexGradientStep(Material.DeepOrange),
		//		new ComplexGradientStep(Material.Orange),
		//		new ComplexGradientStep(Material.Amber),
		//		new ComplexGradientStep(Material.Yellow),
		//		new ComplexGradientStep(Material.Lime),
		//		new ComplexGradientStep(Material.LightGreen),
		//		new ComplexGradientStep(Material.Green),
		//		new ComplexGradientStep(Material.Teal),
		//		new ComplexGradientStep(Material.Cyan),
		//		new ComplexGradientStep(Material.LightBlue),
		//		new ComplexGradientStep(Material.Blue),
		//		new ComplexGradientStep(Material.Indigo),
		//		new ComplexGradientStep(Material.DeepPurple),
		//		new ComplexGradientStep(Material.Purple),
		//		new ComplexGradientStep(Material.Pink),
		//		new ComplexGradientStep(Material.Red, 0)
		//	});
		//public static ComplexGradientBrushProvider MaterialCoolColors => new ComplexGradientBrushProvider(
		//	new List<ComplexGradientStep>
		//	{
		//		new ComplexGradientStep(Material.Blue),
		//		new ComplexGradientStep(Material.Indigo),
		//		new ComplexGradientStep(Material.DeepPurple),
		//		new ComplexGradientStep(Material.Purple),
		//		new ComplexGradientStep(Material.Pink.Darken(.15), 0)
		//	});
	}
}
