using System.Windows;
using System.Windows.Input;
using Core.Helpers.DependencyHelpers;

namespace Material.Controls.Behaviors
{
	public static class CustomWindowCommandBinder
	{
		public static readonly DependencyProperty AttachProperty =
			DP.Attach<bool>(typeof(CustomWindowCommandBinder), new FrameworkPropertyMetadata(false, onAttachChanged));

		private static void onAttachChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var window = o as Window;
			var attach = (bool)e.NewValue;
			if (attach && window != null)
			{
				window.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
				window.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
				window.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
				window.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
			}
		}

		public static bool GetAttach(DependencyObject i) => i.Get<bool>(AttachProperty);
		public static void SetAttach(DependencyObject i, bool v) => i.Set(AttachProperty, v);


		public static void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
		{
			var window = (Window) sender;
			e.CanExecute = window.ResizeMode == ResizeMode.CanResize || window.ResizeMode == ResizeMode.CanResizeWithGrip;
		}

		public static void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
		{
			var window = (Window) sender;
			e.CanExecute = window.ResizeMode != ResizeMode.NoResize;
		}

		public static void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
		{
			var window = (Window) target;
			SystemCommands.CloseWindow(window);
		}

		public static void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
		{
			var window = (Window) target;
			SystemCommands.MaximizeWindow(window);
		}

		public static void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
		{
			var window = (Window) target;
			SystemCommands.MinimizeWindow(window);
		}

		public static void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
		{
			var window = (Window) target;
			SystemCommands.RestoreWindow(window);
		}

	}
}
