using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
	public static class IntrinsicExtensions
	{
		public static byte ScaleDown(this byte i, double percent)
		{
			var value = i - (i * (percent / 1));
			if (value > byte.MaxValue) value = byte.MaxValue;
			if (value < byte.MinValue) value = byte.MinValue;
			return Convert.ToByte(value);
		}
		public static byte ScaleUp(this byte i, double percent)
		{
			var value = i + (i * (percent / 1));
			if (value > byte.MaxValue) value = byte.MaxValue;
			if (value < byte.MinValue) value = byte.MinValue;
			return Convert.ToByte(value);
		}
		public static Stream ToStream(this string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		//public static string After(this string source, int startIndex)
		//{
		//	return source.Substring(startIndex, source.Length - 1);
		//}

		public static bool IsNullOrWhiteSpace(this string i) => string.IsNullOrWhiteSpace(i);

		public static bool IsNullOrEmpty(this string i) => string.IsNullOrEmpty(i);


		public static bool IsWhiteSpace(this char i) => char.IsWhiteSpace(i);
		
		public static bool IsDigit(this char i) => char.IsDigit(i);

		public static bool IsLetter(this char i) => char.IsLetter(i);

		public static bool IsLetterOrDigit(this char i) => char.IsLetterOrDigit(i);


		//TODO is this used anywhere? This doesnt do anything. bad copy/paste
		public static string[] SplitOutsideParenthesis(this string i, char c)
		{
			return i.Split(c);
		}

		public static string ToTitleCase(this string i)
		{
			var textInfo = new CultureInfo("en-US", false).TextInfo;
			return textInfo.ToTitleCase(i.ToLower());
		}
		public static bool ContainsCaseInsensitive(this string source, string toCheck)
		{
			return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public static bool IsExclusivelyHex(this string source)
		{
			return Regex.IsMatch(source, @"\A\b[0-9a-fA-F]+\b\Z");
		}
		public static bool IsCStyleCompilerHexLiteral(this string source)
		{
			return Regex.IsMatch(source, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
		}

	}
}
