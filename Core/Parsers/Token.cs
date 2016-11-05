using System;
using System.Diagnostics;
namespace Core.Parsers
{
	//rootBorder.Opacity .4s [.2->.9] .2s EaseIn Cubic
	public class Token
	{
		public string Text { get; protected set; }
	}
	[DebuggerDisplay("IDT: {Text}")]
	internal class IdentiferToken : Token
	{
		public IdentiferToken(string text)
		{
			Text = text;
		}
	}
	[DebuggerDisplay("NLT: {Text}")]
	public class NumberLiteralToken : Token
	{
		public NumberLiteralToken(string text)
		{
			Text = text;
		}
	}

	//[DebuggerDisplay("TSLT: {Text} {Suffix.ToString()}")]
	//internal class TimeSpanLiteralToken : Token
	//{
	//	public CustomTimeSpanSuffix Suffix { get; }

	//	public TimeSpanLiteralToken(string text, CustomTimeSpanSuffix suffix)
	//	{
	//		Text = text;
	//		Suffix = suffix;
	//	}

	//	public TimeSpan ToTimeSpan()
	//	{
	//		var value = Convert.ToDouble(Text);
	//		switch (Suffix)
	//		{
	//			case CustomTimeSpanSuffix.Milliseconds:
	//				return TimeSpan.FromMilliseconds(value);
	//			case CustomTimeSpanSuffix.Seconds:
	//				return TimeSpan.FromSeconds(value);
	//			case CustomTimeSpanSuffix.Minutes:
	//				return TimeSpan.FromMinutes(value);
	//		}
	//		throw new FormatException($"Unsupported timespan suffix {Suffix}.");
	//	}
	//}
	[DebuggerDisplay("ULT: {Text}")]
	internal class UnknownLiteralToken : Token
	{
		public UnknownLiteralToken(string text)
		{
			Text = text;
		}

		public override string ToString()
		{
			return Text ?? "null";
		}
	}
	[DebuggerDisplay("PBSU: From {From}, By {By}, To {To}")]
	internal class ProgressionBlockSyntacticUnit
	{
		public UnknownLiteralToken From { get; set; }
		public UnknownLiteralToken To { get; set; }
		public UnknownLiteralToken By { get; set; }
	}

}
