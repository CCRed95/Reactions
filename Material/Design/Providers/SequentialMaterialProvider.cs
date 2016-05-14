using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Material.Static;

namespace Material.Design.Providers
{
	public class SequentialMaterialProvider : IMaterialProvider
	{
		#region Properties
		public ReadOnlyCollection<MaterialSet> MaterialSetSequence { get; }

		public CyclicalBehavior CyclicalMode { get; }

		public bool Reverse { get; set; }
		#endregion

		#region Fields
		private MirrorDirection mirrorDirection = MirrorDirection.Forward;
		private int currentIndex;
		#endregion

		#region Constructors
		public SequentialMaterialProvider(params MaterialSet[] materialSetSequence)
			: this(CyclicalBehavior.Repeat, materialSetSequence)
		{ }

		public SequentialMaterialProvider(IList<MaterialSet> materialSetSequence)
			: this(CyclicalBehavior.Repeat, materialSetSequence)
		{ }

		public SequentialMaterialProvider(CyclicalBehavior mode = CyclicalBehavior.Repeat,
			params MaterialSet[] materialSetSequence) : this(mode, materialSetSequence.ToList())
		{ }

		public SequentialMaterialProvider(CyclicalBehavior mode, IList<MaterialSet> materialSetSequence)
		{
			MaterialSetSequence = new ReadOnlyCollection<MaterialSet>(materialSetSequence);
			CyclicalMode = mode;
		}
		#endregion

		#region Methods
		public MaterialSet ProvideNext(ProviderContext context)
		{
			switch (CyclicalMode)
			{
				case CyclicalBehavior.Repeat:
					{
						if (currentIndex > MaterialSetSequence.Count - 1)
							currentIndex = 0;
						if (currentIndex < 0)
							currentIndex = MaterialSetSequence.Count - 1;

						var currentSet = MaterialSetSequence[currentIndex];
						if (Reverse)
							currentIndex--;
						else
							currentIndex++;
						return currentSet;
					}
				case CyclicalBehavior.Mirror:
					{
						if (currentIndex > MaterialSetSequence.Count - 1)
						{
							currentIndex = MaterialSetSequence.Count - 2;
							mirrorDirection = MirrorDirection.Backward;
						}
						else if (currentIndex < 0)
						{
							currentIndex = 1;
							mirrorDirection = MirrorDirection.Forward;
						}
						var currentSet = MaterialSetSequence[currentIndex];

						switch (mirrorDirection)
						{
							case MirrorDirection.Forward:
								currentIndex++;
								break;
							case MirrorDirection.Backward:
								currentIndex--;
								break;
						}
						return currentSet;
					}
				default:
					throw new NotSupportedException($"Cyclical Mode {CyclicalMode} is not supported.");
			}
		}

		public void Reset(ProviderContext context)
		{
			if (Reverse)
				currentIndex = context.CycleLength - 1;
			else
				currentIndex = 0;
		}
		#endregion

		#region Static Sequences
		public static SequentialMaterialProvider RainbowPaletteOrder = new SequentialMaterialProvider
		(
			Palette.Red,
			Palette.Pink,
			Palette.Purple,
			Palette.DeepPurple,
			Palette.Indigo,
			Palette.Blue,
			Palette.LightBlue,
			Palette.Cyan,
			Palette.Teal,
			Palette.Green,
			Palette.LightGreen,
			Palette.Lime,
			Palette.Yellow,
			Palette.Amber,
			Palette.Orange,
			Palette.DeepOrange
		);
		public static SequentialMaterialProvider RainbowRefractionOrder = new SequentialMaterialProvider
		(
			Palette.Red,
			Palette.DeepOrange,
			Palette.Orange,
			Palette.Amber,
			Palette.Lime,
			Palette.Yellow,
			Palette.LightGreen,
			Palette.Green,
			Palette.Teal,
			Palette.Cyan,
			Palette.LightBlue,
			Palette.Blue,
			Palette.Indigo,
			Palette.DeepPurple,
			Palette.Purple,
			Palette.Pink
		);
		public static SequentialMaterialProvider CoolColors = new SequentialMaterialProvider
		(
			Palette.Purple,
			Palette.DeepPurple,
			Palette.Indigo,
			Palette.Blue,
			Palette.LightBlue,
			Palette.Cyan,
			Palette.Teal,
			Palette.Green
		);
		public static SequentialMaterialProvider CoolColorsReverse = new SequentialMaterialProvider
		(
			Palette.Purple,
			Palette.DeepPurple,
			Palette.Indigo,
			Palette.Blue,
			Palette.LightBlue,
			Palette.Cyan,
			Palette.Teal,
			Palette.Green
		) {Reverse = true};
		public static SequentialMaterialProvider RBSFO = new SequentialMaterialProvider
		(
			Palette.Green,
			Palette.Blue,
			Palette.Purple,
			Palette.Red,
			Palette.DeepOrange
		);
		public static SequentialMaterialProvider RBSFORev = new SequentialMaterialProvider
		(
			Palette.Green,
			Palette.Blue,
			Palette.Purple,
			Palette.Red,
			Palette.DeepOrange
		) {Reverse = true};

		public static SequentialMaterialProvider TOR = new SequentialMaterialProvider
		(
			Palette.Teal,
			Palette.Orange,
			Palette.Red
		);
		public static SequentialMaterialProvider TORRev = new SequentialMaterialProvider
		(
			Palette.Teal,
			Palette.Orange,
			Palette.Red
		) {Reverse = true};

		#endregion
	}
	public class BETASequentialMaterialProvider : IBETAMaterialProvider
	{
		#region Properties
		public ReadOnlyCollection<MaterialSet> MaterialSetSequence { get; }

		public CyclicalBehavior CyclicalMode { get; }

		public bool Reverse { get; set; }
		#endregion

		#region Fields
		private MirrorDirection mirrorDirection = MirrorDirection.Forward;
		#endregion

		#region Constructors
		public BETASequentialMaterialProvider(params MaterialSet[] materialSetSequence)
			: this(CyclicalBehavior.Repeat, materialSetSequence)
		{ }

		public BETASequentialMaterialProvider(IList<MaterialSet> materialSetSequence)
			: this(CyclicalBehavior.Repeat, materialSetSequence)
		{ }

		public BETASequentialMaterialProvider(CyclicalBehavior mode = CyclicalBehavior.Repeat,
			params MaterialSet[] materialSetSequence) : this(mode, materialSetSequence.ToList())
		{ }

		public BETASequentialMaterialProvider(CyclicalBehavior mode, IList<MaterialSet> materialSetSequence)
		{
			MaterialSetSequence = new ReadOnlyCollection<MaterialSet>(materialSetSequence);
			CyclicalMode = mode;
		}
		#endregion

		#region Methods
		public MaterialSet ProvideNext(ref ProviderContext context)
		{
			switch (CyclicalMode)
			{
				case CyclicalBehavior.Repeat:
					{
						if (context.ProviderIndex > MaterialSetSequence.Count - 1)
							context.ProviderIndex = 0;
						if (context.ProviderIndex < 0)
							context.ProviderIndex = MaterialSetSequence.Count - 1;

						var currentSet = MaterialSetSequence[context.ProviderIndex];
						if (Reverse)
							context.ProviderIndex = context.ProviderIndex - 1;
						else
							context.ProviderIndex = context.ProviderIndex + 1;
						return currentSet;
					}
				case CyclicalBehavior.Mirror:
					{
						if (context.ProviderIndex > MaterialSetSequence.Count - 1)
						{
							context.ProviderIndex = MaterialSetSequence.Count - 2;
							mirrorDirection = MirrorDirection.Backward;
						}
						else if (context.ProviderIndex < 0)
						{
							context.ProviderIndex = 1;
							mirrorDirection = MirrorDirection.Forward;
						}
						var currentSet = MaterialSetSequence[context.ProviderIndex];

						switch (mirrorDirection)
						{
							case MirrorDirection.Forward:
								context.ProviderIndex = context.ProviderIndex + 1;
								break;
							case MirrorDirection.Backward:
								context.ProviderIndex = context.ProviderIndex - 1;
								break;
						}
						return currentSet;
					}
				default:
					throw new NotSupportedException($"Cyclical Mode {CyclicalMode} is not supported.");
			}
		}

		public void Reset(ref ProviderContext context)
		{
			if (Reverse)
				context.ProviderIndex = context.CycleLength - 1;
			else
				context.ProviderIndex = 0;
		}
		#endregion

		#region Static Sequences
		public static BETASequentialMaterialProvider BETARainbowPaletteOrder = new BETASequentialMaterialProvider
		(
			Palette.Red,
			Palette.Pink,
			Palette.Purple,
			Palette.DeepPurple,
			Palette.Indigo,
			Palette.Blue,
			Palette.LightBlue,
			Palette.Cyan,
			Palette.Teal,
			Palette.Green,
			Palette.LightGreen,
			Palette.Lime,
			Palette.Yellow,
			Palette.Amber,
			Palette.Orange,
			Palette.DeepOrange
		);
		#endregion
	}
}
