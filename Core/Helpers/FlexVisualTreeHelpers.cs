using System;
using System.Windows;
using System.Windows.Media;

namespace Core.Helpers
{
	public static class FlexVisualTreeHelpers
	{
		public static T FindParent<T>(this FrameworkElement i) where T : DependencyObject
		{
			var c = VisualTreeHelper.GetParent(i);
			while (!(c is T))
			{
				if (c == null)
					throw new ArgumentNullException(nameof(c));
				c = VisualTreeHelper.GetParent(c);
			}
			return (T)c;
		}
		public static DependencyObject FindParent(this FrameworkElement i, Type ofType, int level = 0)
		{
			var isTypeUnspecified = ofType == null;
			if (level < 0)
				throw new ArgumentOutOfRangeException(nameof(level), "Level parameter must be greater than or equal to 0.");

			var currentNode = VisualTreeHelper.GetParent(i);
			while (level >= 0)
			{
				while (isTypeUnspecified || !ofType.IsInstanceOfType(currentNode))
				{
					if (currentNode == null)
					{
						throw new ArgumentNullException(nameof(currentNode));
					}
					var lastNode = currentNode;
					currentNode = VisualTreeHelper.GetParent(currentNode);
					if (currentNode == null)
					{
						currentNode = LogicalTreeHelper.GetParent(lastNode);
					}
				}
				level--;
			}
			return currentNode;
		}
	}
}
