using System;
using System.Reflection;
using System.Windows;

namespace Core.Helpers.ControlHelpers
{
	public static class Ctrl
	{
		public static void OverrideDefaultStyleKey<T>()
		{
			var dskFieldInfo = typeof (FrameworkElement).GetField("DefaultStyleKeyProperty", BindingFlags.Static | BindingFlags.NonPublic);
			if(dskFieldInfo == null)
				throw new InvalidOperationException($"Could not find DefaultStyleKeyProperty in {typeof(T)}.");
			var dskValue = dskFieldInfo.GetValue(null) as DependencyProperty;
			if(dskValue == null)
				throw new InvalidOperationException("Could not get DefaultStyleKeyProperty value.");
			dskValue.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(typeof(T)));
		}
	}
}
