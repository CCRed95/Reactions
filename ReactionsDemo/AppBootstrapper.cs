using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using ReactionsDemo.ViewModels;

namespace ReactionsDemo
{
	public class AppBootstrapper : BootstrapperBase
	{
		public AppBootstrapper()
		{
			Initialize();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			var settings = new Dictionary<string, object>
			{
				//{ "SizeToContent", SizeToContent.Manual },
				//{ "WindowState" , WindowState.Maximized }
				{ "Height", 700 },
				{ "Width" , 500 }
			};

			DisplayRootViewFor<MainViewModel>(settings);
		}

	}
}
