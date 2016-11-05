using System.Windows;

namespace Reactions.Templates
{
	/// <summary>
	/// TODO GET RID OF THIS. Not necessary anymore
	/// </summary>
	public class ReactiveDataTemplate : DataTemplate
	{
		public ReactiveDataTemplate()
		{
			Triggers.Add(new EventTrigger {RoutedEvent = NonfreezableDataTemplateHack.FakeEvent});
		}
	}
}
