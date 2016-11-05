using System;
using System.ComponentModel;
using Material.Markup.TypeConverters;

namespace Material.Design
{
	public enum LengthMode
	{
		Pixels,
		Percent
	}
	[TypeConverter(typeof(LengthDescriptorConverter))]
	public class LengthDescriptor
	{
		private double _value;
		public double Value
		{
			get { return _value; }
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException(nameof(value), value,
						@"Value must be greater than or equal to 0.");
				_value = value;
			}
		}
		public LengthMode LengthMode { get; set; }

		public LengthDescriptor() { }

		public LengthDescriptor(double value, LengthMode lengthMode)
		{
			Value = value;
			LengthMode = lengthMode;
		}

		public double ResolvePixelLengthWithinContainer(double containerLength)
		{
			if (LengthMode == LengthMode.Pixels)
				return Value;

			if (containerLength <= 0)
				return 0;

			return containerLength * (Value / 100d);
		}
	}
}
