using System;
using System.Windows;
using System.Windows.Media.Effects;
using Core.Helpers.DependencyHelpers;

namespace Material.Controls.Shadows
{
	public enum ShadowDirection
	{
		Down,
		Up
	}
	public static class Shadow
	{
		public static readonly DependencyProperty LevelProperty =
			DP.Attach<double>(typeof(Shadow), new FrameworkPropertyMetadata(0d, onLevelChanged));

		public static double GetLevel(DependencyObject i) => i.Get<double>(LevelProperty);
		public static void SetLevel(DependencyObject i, double v) => i.Set(LevelProperty, v);


		public static readonly DependencyProperty DirectionProperty =
			DP.Attach<ShadowDirection>(typeof(Shadow), new FrameworkPropertyMetadata(ShadowDirection.Down, onDirectionChanged));

		public static ShadowDirection GetDirection(DependencyObject i) => i.Get<ShadowDirection>(DirectionProperty);
		public static void SetDirection(DependencyObject i, ShadowDirection v) => i.Set(DirectionProperty, v);



		public static readonly DependencyProperty ForceAllowShadowProperty =
			DP.Attach<bool>(typeof(Shadow), new FrameworkPropertyMetadata(false, onForceAllowShadowPropertyChanged));

		public static bool GetForceAllowShadow(DependencyObject i) => i.Get<bool>(ForceAllowShadowProperty);
		public static void SetForceAllowShadow(DependencyObject i, bool v) => i.Set(ForceAllowShadowProperty, v);



		private static void onDirectionChanged(DependencyObject i, DependencyPropertyChangedEventArgs e)
		{
			var level = GetLevel(i);
			var direction = GetDirection(i);
			var forceAllowShadow = GetForceAllowShadow(i);

			initializeShadow(i, level, direction, forceAllowShadow);
			//var uiElement = i as UIElement;
			//var value = e.NewValue as ShadowDirection?;
			//if (uiElement != null && value.HasValue)
			//{
			//	var shadowEffect = uiElement.Effect as DropShadowEffect;
			//	if (shadowEffect != null)
			//	{
			//		shadowEffect.Direction = value == ShadowDirection.Down ? 270 : 90; 
			//	}
			//}
		}

		private static void onLevelChanged(DependencyObject i, DependencyPropertyChangedEventArgs e)
		{
			var level = GetLevel(i);
			var direction = GetDirection(i);
			var forceAllowShadow = GetForceAllowShadow(i);

			initializeShadow(i, level, direction, forceAllowShadow);
		}


		private static void onForceAllowShadowPropertyChanged(DependencyObject i, DependencyPropertyChangedEventArgs e)
		{
			var level = GetLevel(i);
			var direction = GetDirection(i);
			var forceAllowShadow = GetForceAllowShadow(i);

			initializeShadow(i, level, direction, forceAllowShadow);
		}

		private static void initializeShadow(DependencyObject i, double? value, ShadowDirection direction, bool allowShadows)
		{
			var uiElement = i as UIElement;
			if (uiElement != null && value.HasValue)
			{
				if (!allowShadows)
				{
					uiElement.Effect = null;
					return;
				}

				if (value.Value >= 0)
				{
					if (value == 0)
					{
						uiElement.Effect = null;
					}
					else
					{
						var shadow = ShadowInterpolator.Interpolate(value.Value, direction);
						uiElement.Effect = shadow;
					}
				}
			}
		}
	}
}
