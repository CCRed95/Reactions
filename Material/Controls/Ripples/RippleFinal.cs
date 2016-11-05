using System.Windows;
using System.Windows.Media;
using Core.Helpers.DependencyHelpers;

namespace Material.Controls.Ripples
{
	public static class RippleFinal
	{
		public static readonly DependencyProperty MousePositionProperty =
			DP.Attach<Point>(typeof(RippleFinal), new FrameworkPropertyMetadata(new Point(0, 0)));

		public static Point GetMousePosition(DependencyObject i) => i.Get<Point>(MousePositionProperty);
		public static void SetMousePosition(DependencyObject i, Point v) => i.Set(MousePositionProperty, v);


		public static readonly DependencyProperty PlacementProperty =
			DP.Attach<Point>(typeof(RippleFinal), new FrameworkPropertyMetadata(new Point(0, 0), onPlacementChanged));

		public static Point GetPlacement(DependencyObject i) => i.Get<Point>(PlacementProperty);
		public static void SetPlacement(DependencyObject i, Point v) => i.Set(PlacementProperty, v);



		private static void onPlacementChanged(DependencyObject i, DependencyPropertyChangedEventArgs e)
		{
			var translateTransform = i as TranslateTransform;
			var value = e.NewValue as Point?;

			if (translateTransform != null && value.HasValue)
			{
				translateTransform.X = value.Value.X;
				translateTransform.Y = value.Value.Y;
			}
		}
	}
}
/*		public static readonly DependencyProperty TrackMousePositionProperty =
			DP.Attach<bool>(typeof(RippleFinal), new FrameworkPropertyMetadata(false, onTrackMousePositionChanged));

		public static bool GetTrackMousePosition(DependencyObject i) => i.Get<bool>(TrackMousePositionProperty);
		public static void SetTrackMousePosition(DependencyObject i, bool v) => i.Set(TrackMousePositionProperty, v);

			private static void onTrackMousePositionChanged(DependencyObject i, DependencyPropertyChangedEventArgs e)
		{
			var uiElement = i as UIElement;
			var value = e.NewValue as bool?;

			if (uiElement != null && value.HasValue && value.Value)
			{
				uiElement.MouseUp += (s, args) =>
				{
					var frameworkElement = args.Source as FrameworkElement;
					var mousePosition = Mouse.GetPosition(frameworkElement);
					SetMousePosition(frameworkElement, mousePosition);
				};
			}

		}

	*/