using System.Windows;

namespace Core.Helpers.EventHelpers
{
	public class ParameterizedRoutedEventArgs<T> : RoutedEventArgs
	{
		public T Param { get; }
		public ParameterizedRoutedEventArgs(RoutedEvent routedEvent, T param) : base(routedEvent)
		{
			Param = param;
		}
		public ParameterizedRoutedEventArgs(RoutedEvent routedEvent, object source, T param) : base(routedEvent, source)
		{
			Param = param;
		}
	}
}
