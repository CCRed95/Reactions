using System;
using System.Globalization;

namespace Core.Markup.Converters
{
	public abstract class CONVERSIONPARAM
	{
		public Type TargetType { get; }
		public CultureInfo CultureInfo { get; }

		protected CONVERSIONPARAM(Type targetType, CultureInfo cultureInfo)
		{
			TargetType = targetType;
			CultureInfo = cultureInfo;
		}
	}
}