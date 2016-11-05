using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace Reactions.Animation
{
	//TODO IComparable<TimeSpan>, IEquatable<TimeSpan>,
	//TODO implicit/explicit convert operator to TimeSpan
	//[Serializable]
	//[DebuggerDisplay("{TotalMilliseconds} ms")]
	//[TypeConverter(typeof(AnimationTimeSpanConverter))]
	//public struct AnimationTimeSpan : IComparable, IComparable<AnimationTimeSpan>, IEquatable<AnimationTimeSpan>, IFormattable
	//{
	//	public TimeSpan TimeSpan { get; }

	//	public static readonly AnimationTimeSpan Zero = new AnimationTimeSpan(TimeSpan.Zero);

	//	public static readonly AnimationTimeSpan MaxValue = new AnimationTimeSpan(TimeSpan.MaxValue);
	//	public static readonly AnimationTimeSpan MinValue = new AnimationTimeSpan(TimeSpan.MinValue);

	//	public AnimationTimeSpan(TimeSpan timeSpan)
	//	{
	//		TimeSpan = timeSpan;
	//	}

	//	public AnimationTimeSpan(long ticks)
	//		: this(TimeSpan.FromTicks(ticks))
	//	{ }

	//	public AnimationTimeSpan(int hours, int minutes, int seconds)
	//		: this(new TimeSpan(hours, minutes, seconds))
	//	{ }

	//	public AnimationTimeSpan(int days, int hours, int minutes, int seconds)
	//		: this(new TimeSpan(days, hours, minutes, seconds))
	//	{ }

	//	public AnimationTimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
	//		: this(new TimeSpan(days, hours, minutes, seconds, milliseconds))
	//	{ }


	//	public long Ticks => TimeSpan.Ticks;

	//	public int Days => TimeSpan.Days;

	//	public int Hours => TimeSpan.Hours;

	//	public int Milliseconds => TimeSpan.Milliseconds;

	//	public int Minutes => TimeSpan.Minutes;

	//	public int Seconds => TimeSpan.Seconds;

	//	public double TotalDays => TimeSpan.TotalDays;

	//	public double TotalHours => TimeSpan.TotalHours;

	//	public double TotalMilliseconds => TimeSpan.TotalMilliseconds;

	//	public double TotalMinutes => TimeSpan.TotalMinutes;

	//	public double TotalSeconds => TimeSpan.TotalSeconds;


	//	public AnimationTimeSpan Add(AnimationTimeSpan ts)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.Add(ts.TimeSpan));
	//	}

	//	public AnimationTimeSpan MultipliedBy(int x)
	//	{
	//		return FromTicks(Ticks * x);
	//	}
	//	public AnimationTimeSpan DividedBy(int x)
	//	{
	//		return FromTicks(Ticks / x);
	//	}

	//	public AnimationTimeSpan MultipliedBy(AnimationTimeSpan ts)
	//	{
	//		return FromTicks(Ticks * ts.Ticks);
	//	}
	//	public AnimationTimeSpan DividedBy(AnimationTimeSpan ts)
	//	{
	//		return FromTicks(Ticks / ts.Ticks);
	//	}

	//	public static int Compare(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return TimeSpan.Compare(t1.TimeSpan, t2.TimeSpan);
	//	}
	//	//TODO fix this, comparing to parent
	//	public int CompareTo(object value)
	//	{
	//		return TimeSpan.CompareTo(value);
	//	}

	//	public int CompareTo(AnimationTimeSpan value)
	//	{
	//		return TimeSpan.CompareTo(value.TimeSpan);
	//	}


	//	public static AnimationTimeSpan FromDays(double value)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.FromDays(value));
	//	}

	//	public AnimationTimeSpan Duration()
	//	{
	//		return new AnimationTimeSpan(TimeSpan.Duration());
	//	}

	//	public override bool Equals(object value)
	//	{
	//		if (value is AnimationTimeSpan)
	//		{
	//			return ((AnimationTimeSpan)value).TimeSpan.Equals(TimeSpan);
	//		}
	//		return false;
	//	}

	//	public bool Equals(AnimationTimeSpan obj)
	//	{
	//		return TimeSpan.Equals(obj.TimeSpan);
	//	}

	//	public static bool Equals(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return TimeSpan.Equals(t1.TimeSpan, t2.TimeSpan);
	//	}

	//	public override int GetHashCode()
	//	{
	//		return TimeSpan.GetHashCode();
	//	}

	//	public static AnimationTimeSpan FromHours(double value)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.FromHours(value));
	//	}
	//	public static AnimationTimeSpan FromMilliseconds(double value)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.FromMilliseconds(value));
	//	}
	//	public static AnimationTimeSpan FromMinutes(double value)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.FromMinutes(value));
	//	}
	//	public static AnimationTimeSpan FromSeconds(double value)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.FromSeconds(value));
	//	}
	//	public AnimationTimeSpan Subtract(AnimationTimeSpan ts)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.Subtract(ts.TimeSpan));
	//	}
	//	public static AnimationTimeSpan FromTicks(long value)
	//	{
	//		return new AnimationTimeSpan(TimeSpan.FromTicks(value));
	//	}

	//	public AnimationTimeSpan Negate()
	//	{
	//		return new AnimationTimeSpan(TimeSpan.Negate());
	//	}

	//	#region ParseAndFormat
	//	public static AnimationTimeSpan Parse(string s)
	//	{
	//		return Parse(s, null);
	//	}
	//	public static AnimationTimeSpan Parse(string input, IFormatProvider formatProvider)
	//	{
	//		return AnimationTimeSpanParse.Parse(input, formatProvider);
	//	}
	//	public static AnimationTimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
	//	{
	//		return AnimationTimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
	//	}
	//	public static AnimationTimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
	//	{
	//		return AnimationTimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
	//	}
	//	public static AnimationTimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
	//	{
	//		AnimationTimeSpanParse.ValidateStyles(styles, "styles");
	//		return AnimationTimeSpanParse.ParseExact(input, format, formatProvider, styles);
	//	}
	//	public static AnimationTimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
	//	{
	//		AnimationTimeSpanParse.ValidateStyles(styles, "styles");
	//		return AnimationTimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
	//	}
	//	public static bool TryParse(string s, out AnimationTimeSpan result)
	//	{
	//		return AnimationTimeSpanParse.TryParse(s, null, out result);
	//	}
	//	public static bool TryParse(string input, IFormatProvider formatProvider, out AnimationTimeSpan result)
	//	{
	//		return AnimationTimeSpanParse.TryParse(input, formatProvider, out result);
	//	}
	//	public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out AnimationTimeSpan result)
	//	{
	//		return AnimationTimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
	//	}
	//	public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out AnimationTimeSpan result)
	//	{
	//		return AnimationTimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
	//	}
	//	public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out AnimationTimeSpan result)
	//	{
	//		AnimationTimeSpanParse.ValidateStyles(styles, "styles");
	//		return AnimationTimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
	//	}
	//	public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out AnimationTimeSpan result)
	//	{
	//		AnimationTimeSpanParse.ValidateStyles(styles, "styles");
	//		return AnimationTimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
	//	}

	//	//TODO implement these properly
	//	public override string ToString()
	//	{
	//		return TimeSpan.ToString();
	//		//return TimeSpanFormat.Format(this, null, null);
	//	}
	//	public string ToString(string format)
	//	{
	//		return TimeSpan.ToString(format);
	//		//return TimeSpanFormat.Format(this, format, null);
	//	}
	//	public string ToString(string format, IFormatProvider formatProvider)
	//	{
	//		return TimeSpan.ToString(format, formatProvider);
	//		//return TimeSpanFormat.Format(this, format, formatProvider);
	//	}
	//	#endregion

	//	public static AnimationTimeSpan operator -(AnimationTimeSpan t)
	//	{
	//		return new AnimationTimeSpan(-t.TimeSpan);
	//	}

	//	public static AnimationTimeSpan operator -(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.Subtract(t2);
	//	}

	//	public static AnimationTimeSpan operator +(AnimationTimeSpan t)
	//	{
	//		return t;
	//	}

	//	public static AnimationTimeSpan operator +(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.Add(t2);
	//	}

	//	public static bool operator ==(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.TimeSpan == t2.TimeSpan;
	//	}

	//	public static bool operator !=(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.TimeSpan != t2.TimeSpan;
	//	}

	//	public static bool operator <(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.TimeSpan < t2.TimeSpan;
	//	}

	//	public static bool operator <=(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.TimeSpan <= t2.TimeSpan;
	//	}

	//	public static bool operator >(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.TimeSpan > t2.TimeSpan;
	//	}

	//	public static bool operator >=(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.TimeSpan >= t2.TimeSpan;
	//	}

	//	public static AnimationTimeSpan operator *(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.MultipliedBy(t2);
	//	}

	//	public static AnimationTimeSpan operator /(AnimationTimeSpan t1, AnimationTimeSpan t2)
	//	{
	//		return t1.DividedBy(t2);
	//	}
	//}
	
}
