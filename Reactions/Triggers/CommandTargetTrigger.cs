using System;
using System.Windows.Input;

namespace Reactions.Triggers
{
	public class CommandTargetTrigger : DynamicTriggerBase, ICommand
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}

		public bool CanExecute(object parameter)
		{
			return IsHosted;
		}

		void ICommand.Execute(object parameter)
		{
			Execute();
		}

		public event EventHandler CanExecuteChanged;
	}
}
