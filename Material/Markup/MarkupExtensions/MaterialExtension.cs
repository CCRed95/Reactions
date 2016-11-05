using System;
using System.Reflection;
using System.Windows.Markup;
using System.Windows.Media;
using Material.Controls.Primitives;
using Material.Design;
using Material.Extensions;
using Material.Static;

namespace Material.Markup.MarkupExtensions
{
	//		//TODO what is AcceptedMarkupExtensionExpressionTypeAttribute?
	//public class MPExtension : MarkupExtension
	//{
	//	private readonly MaterialSetEnum? _materialSet;
	//	private readonly Luminosity _luminosity;

	//	private readonly MDBrushEnum? _materialDesignBrush;

	//	private Brush _resolvedBrush;

	//	public MPExtension()
	//	{
	//	}
	//	public MPExtension(MaterialSetEnum materialSet, Luminosity luminosity)
	//		: this()
	//	{
	//		_materialSet = materialSet;
	//		_luminosity = luminosity;
	//	}
	//	public MPExtension(MaterialSetEnum materialSet, string luminosity)
	//		: this(materialSet, Luminosity.Parse(luminosity))
	//	{
	//	}


	//	public MPExtension(MDBrushEnum materialDesignBrush) : this()
	//	{
	//		_materialDesignBrush = materialDesignBrush;
	//	}
	//	//TODO what is AcceptedMarkupExtensionExpressionTypeAttribute?
	//TODO ray animations and vectors, controlling transitioning objects enter/leave angles
	//TODO geometry based animation offset triggers
	//	public override object ProvideValue(IServiceProvider serviceProvider)
	//	{
	//		if (_resolvedBrush != null)
	//			return _resolvedBrush;

	//		if (_materialDesignBrush.HasValue)
	//		{
	//			var reflectedStaticFieldInfo = typeof(LegacyPalette).GetField(_materialDesignBrush.ToString(),
	//				BindingFlags.Public | BindingFlags.Static);

	//			if (reflectedStaticFieldInfo == null)
	//			{
	//				throw new NotSupportedException($"Could not find a static field for the enum identifier " +
	//																				$"\'{_materialDesignBrush}\' in \'LegacyPalette\' class. " +
	//																				$"Must be declared as a public static field.");
	//			}
	//			var reflectedValue = reflectedStaticFieldInfo.GetValue(null);
	//			var reflectedBrush = reflectedValue as Brush;
	//			if (reflectedBrush == null)
	//				throw new NotSupportedException($"The value reflected from LegacyPalette.{_materialDesignBrush}" +
	//																				$"must be not null and derive from \'System.Windows.Media.Brush\'");

	//			_resolvedBrush = reflectedBrush;
	//			return _resolvedBrush;
	//		}
	//		if (_materialSet.HasValue && _luminosity != null)
	//		{
	//			_resolvedBrush = _materialSet.Value.AsSet().FromLuminosity(_luminosity);
	//			return _resolvedBrush;
	//		}

	//		throw new NotSupportedException("args specified are not valid");
	//	}
	//}

	public class MDBlendExtension : MarkupExtension
	{
		private readonly MDBrushEnum _mdInitialBrushReference;
		private readonly MDBrushEnum _mdMixedBrushReference;

		private SolidColorBrush _resolvedInitialBrush;
		private SolidColorBrush _resolvedMixedBrush;

		private readonly double _mixinOpacity;


		public MDBlendExtension(MDBrushEnum mdInitialBrushReference, double mixinOpacity, MDBrushEnum mdMixedBrushReference)// : this()
		{
			_mdInitialBrushReference = mdInitialBrushReference;

			if (mixinOpacity <= 0 || mixinOpacity >= 1)
				throw new ArgumentOutOfRangeException(nameof(mixinOpacity), mixinOpacity,
					@"The mixin opacity value must be >= 0 and <= 1");

			_mixinOpacity = mixinOpacity;

			_mdMixedBrushReference = mdMixedBrushReference;
		}


		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			verifyBrushResolvedFromEnumReference(_mdInitialBrushReference, ref _resolvedInitialBrush);

			verifyBrushResolvedFromEnumReference(_mdMixedBrushReference, ref _resolvedMixedBrush);

			var blendedBrush = _resolvedInitialBrush.Blend(_resolvedMixedBrush, _mixinOpacity);

			return blendedBrush;
		}

		protected static void verifyBrushResolvedFromEnumReference(MDBrushEnum wdBrushReference, ref SolidColorBrush _resolvedBrushInstance)
		{
			if (_resolvedBrushInstance != null)
				return;

			var reflectedStaticFieldInfo = typeof(LegacyPalette).GetField(wdBrushReference.ToString(),
				BindingFlags.Public | BindingFlags.Static);

			if (reflectedStaticFieldInfo == null)
				throw new NotSupportedException($"Could not find a matching static field for the enum identifier " +
																				$"\'{wdBrushReference}\' in \'LegacyPalette\' class. " +
																				$"It must be declared as a public static field.");

			var reflectedValue = reflectedStaticFieldInfo.GetValue(null);
			var reflectedBrush = reflectedValue as SolidColorBrush;
			if (reflectedBrush == null)
				throw new NotSupportedException($"The value reflected from LegacyPalette.{wdBrushReference}" +
																				$"must be not null and must derive from \'System.Windows.Media.SolidColorBrush\'.");

			_resolvedBrushInstance = reflectedBrush;
		}
	}

	public class MDLightenExtension : MarkupExtension
	{
		private readonly MDBrushEnum _mdInitialBrushReference;

		private SolidColorBrush _resolvedInitialBrush;

		private readonly double _lightenOpacity;


		public MDLightenExtension(MDBrushEnum mdInitialBrushReference, double lightenOpacity)
		{
			_mdInitialBrushReference = mdInitialBrushReference;

			if (lightenOpacity <= 0 || lightenOpacity >= 1)
				throw new ArgumentOutOfRangeException(nameof(lightenOpacity), lightenOpacity,
					@"The ligthen opacity value must be >= 0 and <= 1");

			_lightenOpacity = lightenOpacity;
		}


		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			verifyBrushResolvedFromEnumReference(_mdInitialBrushReference, ref _resolvedInitialBrush);

			return _resolvedInitialBrush.Lighten(_lightenOpacity);
		}

		protected static void verifyBrushResolvedFromEnumReference(MDBrushEnum wdBrushReference, ref SolidColorBrush _resolvedBrushInstance)
		{
			if (_resolvedBrushInstance != null)
				return;

			var reflectedStaticFieldInfo = typeof(LegacyPalette).GetField(wdBrushReference.ToString(),
				BindingFlags.Public | BindingFlags.Static);

			if (reflectedStaticFieldInfo == null)
				throw new NotSupportedException($"Could not find a matching static field for the enum identifier " +
																				$"\'{wdBrushReference}\' in \'LegacyPalette\' class. " +
																				$"It must be declared as a public static field.");

			var reflectedValue = reflectedStaticFieldInfo.GetValue(null);
			var reflectedBrush = reflectedValue as SolidColorBrush;
			if (reflectedBrush == null)
				throw new NotSupportedException($"The value reflected from LegacyPalette.{wdBrushReference}" +
																				$"must be not null and must derive from \'System.Windows.Media.SolidColorBrush\'.");

			_resolvedBrushInstance = reflectedBrush;
		}
	}

	public class MDDarkenExtension : MarkupExtension
	{
		private readonly MDBrushEnum _mdInitialBrushReference;

		private SolidColorBrush _resolvedInitialBrush;

		private readonly double _darkenOpacity;


		public MDDarkenExtension(MDBrushEnum mdInitialBrushReference, double darkenOpacity)
		{
			_mdInitialBrushReference = mdInitialBrushReference;

			if (darkenOpacity <= 0 || darkenOpacity >= 1)
				throw new ArgumentOutOfRangeException(nameof(darkenOpacity), darkenOpacity,
					@"The darken opacity value must be >= 0 and <= 1");

			_darkenOpacity = darkenOpacity;
		}


		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			verifyBrushResolvedFromEnumReference(_mdInitialBrushReference, ref _resolvedInitialBrush);

			return _resolvedInitialBrush.Darken(_darkenOpacity);
		}

		protected static void verifyBrushResolvedFromEnumReference(MDBrushEnum wdBrushReference, ref SolidColorBrush _resolvedBrushInstance)
		{
			if (_resolvedBrushInstance != null)
				return;

			var reflectedStaticFieldInfo = typeof(LegacyPalette).GetField(wdBrushReference.ToString(),
				BindingFlags.Public | BindingFlags.Static);

			if (reflectedStaticFieldInfo == null)
				throw new NotSupportedException($"Could not find a matching static field for the enum identifier " +
																				$"\'{wdBrushReference}\' in \'LegacyPalette\' class. " +
																				$"It must be declared as a public static field.");

			var reflectedValue = reflectedStaticFieldInfo.GetValue(null);
			var reflectedBrush = reflectedValue as SolidColorBrush;
			if (reflectedBrush == null)
				throw new NotSupportedException($"The value reflected from LegacyPalette.{wdBrushReference}" +
																				$"must be not null and must derive from \'System.Windows.Media.SolidColorBrush\'.");

			_resolvedBrushInstance = reflectedBrush;
		}
	}
}
