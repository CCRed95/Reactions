using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Core.Controls.Core;
using Core.Helpers.DependencyHelpers;

namespace Material.Controls
{
	/// <summary>
	/// This is a modified version of ButchersBoy's Card control from the Material Design in XAML Toolkit
	/// https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/blob/master/MaterialDesignThemes.Wpf/Card.cs
	/// </summary>
	public class Card : FlexContentControl
	{
		public static readonly DependencyProperty UniformCornerRadiusProperty = DP.Register(
			new Meta<Card, double>(2.0) {Flags = FXR});

		public static readonly DependencyPropertyKey ContentClipPropertyKey = DP.RegisterReadOnly(
			new Meta<Card, Geometry>());
		public static readonly DependencyProperty ContentClipProperty = ContentClipPropertyKey.DependencyProperty;

		public double UniformCornerRadius
		{
			get { return (double) GetValue(UniformCornerRadiusProperty); }
			set { SetValue(UniformCornerRadiusProperty, value); }
		}
		public Geometry ContentClip
		{
			get { return (Geometry) GetValue(ContentClipProperty); }
			private set { SetValue(ContentClipPropertyKey, value); }
		}
		
		[AutoTemplatePart] protected Border PART_ClipBorder;

		static Card()
		{
			OverrideDefaultStyleKey<Card>();
		}

		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			base.OnRenderSizeChanged(sizeInfo);

			if (PART_ClipBorder == null) return;

			var farPoint = new Point(
					Math.Max(0, PART_ClipBorder.ActualWidth),
					Math.Max(0, PART_ClipBorder.ActualHeight));

			var clipRect = new Rect(
					new Point(),
					new Point(farPoint.X, farPoint.Y));

			ContentClip = new RectangleGeometry(clipRect, UniformCornerRadius, UniformCornerRadius);
		}
	}
}
