using System.ComponentModel;
using System.Windows;
using Caliburn.Micro;

namespace ReactionsDemo.ViewModels
{
	public abstract class ViewModelBase : Screen, IShell
	{
		private static DependencyObject DesignerModeDO => new DependencyObject();
		public bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(DesignerModeDO);
	}
}
