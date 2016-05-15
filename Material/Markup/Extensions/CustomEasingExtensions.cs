using System;
using System.Windows.Media.Animation;

namespace Material.Markup.Extensions
{
	public static class CustomEasingExtensions
	{
		public static EasingMode ToStdEasingMode(this CustomEasingMode i)
		{
			switch (i)
			{
				case CustomEasingMode.In:
					return EasingMode.EaseIn;
				case CustomEasingMode.Out:
					return EasingMode.EaseOut;
				case CustomEasingMode.InOut:
					return EasingMode.EaseInOut;
				default:
					throw new NotSupportedException("unsupported easingmode");
			}
		}
	}
}