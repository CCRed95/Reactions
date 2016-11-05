using System.Windows;
using System.Windows.Media;
using Core.Helpers.DependencyHelpers;
using Material.Design;
using Material.Extensions;

namespace Material.Controls.Primitives
{
	public static class VisualFeedback
	{
		public static readonly DependencyProperty ClickFeedbackProperty =
			DP.Attach<Luminosity>(typeof (VisualFeedback), new FrameworkPropertyMetadata(Luminosity.P100, FrameworkPropertyMetadataOptions.Inherits));

		public static Luminosity GetClickFeedback(DependencyObject i) => i.Get<Luminosity>(ClickFeedbackProperty);
		public static void SetClickFeedback(DependencyObject i, Luminosity v) => i.Set(ClickFeedbackProperty, v);


		public static readonly DependencyProperty BrushProperty =
			DP.Attach<Brush>(typeof (VisualFeedback), new FrameworkPropertyMetadata(Brushes.White.WithOpacity(.3), FrameworkPropertyMetadataOptions.Inherits));

		public static Brush GetBrush(DependencyObject i) => i.Get<Brush>(BrushProperty);
		public static void SetBrush(DependencyObject i, Brush v) => i.Set(BrushProperty, v);
	}
}