using System;
using System.Windows;

namespace Core.Markup.Converters
{
	public static class XAMLHelpers
	{
		public static T Convert<T>(object i, object caller)
		{
			if (i is IConvertible)
			{
				var converted = System.Convert.ChangeType(i, typeof(T));
				return (T)converted;
			}
			if (i == DependencyProperty.UnsetValue)
			{
				return default(T);
			}
			try
			{
				return (T)i;
			}
			catch
			{
				var callertype = caller.GetType();
				throw new InvalidCastException($"{callertype.FullName} : Invalid cast from {i.GetType()} to {typeof(T)}");
			}

		}
	}
}