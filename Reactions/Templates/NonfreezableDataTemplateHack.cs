using System.Windows;
using Core.Helpers.EventHelpers;

namespace Reactions.Templates
{
	internal class NonfreezableDataTemplateHack : UIElement
	{
		internal static readonly RoutedEvent FakeEvent = EM.Register<NonfreezableDataTemplateHack, RoutedEventHandler>(EM.DIRECT);

		internal event RoutedEventHandler Fake
		{
			add { AddHandler(FakeEvent, value); }
			remove { RemoveHandler(FakeEvent, value); }
		}
	}
}