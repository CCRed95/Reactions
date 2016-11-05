using System.Windows;
using Core.Helpers.CLREventHelpers;
using Core.Helpers.DependencyHelpers;

namespace Core.Extensions
{
	public static class InvocationExtensions
	{
		public static void TryInvoke<D, T>(this PropertyChange<D, T> i, DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) where D : DependencyObject
		{
			i?.Invoke((D)dependencyObject, new DPChangedEventArgs<T>(e));
		}
		public static object TryInvoke<D, T>(this PropertyCoerce<D, T> i, DependencyObject dependencyObject, object baseValue) where D : DependencyObject
		{
			if (i == null) return (T)baseValue;
			return i.Invoke((D)dependencyObject, (T)baseValue);
		}
		public static bool TryInvoke<T>(this PropertyValidate<T> i, object value)
		{
			return i == null || i.Invoke((T)value);
		}
		public static void Raise<T1>(this ParameterizedEventHandler<T1> i, T1 p1)
		{
			i?.Invoke(p1);
		}
		public static void Raise<T1, T2>(this ParameterizedEventHandler<T1, T2> i, T1 p1, T2 p2)
		{
			i?.Invoke(p1, p2);
		}
	}
}
