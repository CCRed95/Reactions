using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Core.Markup.Converters
{
	public abstract class OPTIONALPARAM
	{
		public bool IsProvided { get; protected set; }
	}
	public class OPTIONALPARAM<T> : OPTIONALPARAM
	{
		private T _value;
		public T Value
		{
			get { return _value; }
			set
			{
				_value = value;
				IsProvided = true;
			}
		}

		public OPTIONALPARAM()
		{
			IsProvided = false;
		}
		public OPTIONALPARAM(T value)
		{
			IsProvided = true;
			Value = value;
		}
	}
	public abstract class CONVERSIONPARAM
	{
		public Type TargetType { get; }
		public CultureInfo CultureInfo { get; }

		protected CONVERSIONPARAM(Type targetType, CultureInfo cultureInfo)
		{
			TargetType = targetType;
			CultureInfo = cultureInfo;
		}
	}
	public class NULLPARAM//TODO  : CONVERSIONPARAM
	{

	}
	public static class XAMLHelpers
	{
		public static T Convert<T>(object i, object caller)
		{
			if (i is IConvertible)
			{
				var converted = System.Convert.ChangeType(i, typeof(T));
				return (T)converted;
			}
			if (i == DependencyProperty.UnsetValue)
			{
				return default(T);
			}
			try
			{
				return (T)i;
			}
			catch
			{
				var callertype = caller.GetType();
				throw new InvalidCastException($"{callertype.FullName} : Invalid cast from {i.GetType()} to {typeof(T)}");
			}

		}
	}
	//TODO remove all the try catches! not necessary
	public abstract class XAMLConverter<T1, TParam, TResult> : MarkupSingleton, IValueConverter// TODO where TParam : CONVERSIONPARAM
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//try
			//{
				var arg1 = ConvertArg1(value);
				var param = (typeof(TParam) == typeof(NULLPARAM)) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, param);
			//}
			//catch (Exception ex)
			//{
			//	throw new Exception($"Conversiton error in {GetType()}", ex);
			//}

		}

		public abstract TResult Convert(T1 arg1, TParam param);

		object IValueConverter.ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			//try
			//{
			var arg1 = ConvertArg1(values[0]);
			var arg2 = ConvertArg2(values[1]);
			var param = (typeof(TParam) == typeof(NULLPARAM)) ? default(TParam) : ConvertParam(parameter);
			return Convert(arg1, arg2, param);
			//}
			//catch (Exception ex)
			//{
			//	throw new Exception($"Conversiton error in {GetType()}", ex);
			//}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			//try
			//{
			var arg1 = ConvertArg1(values[0]);
			var arg2 = ConvertArg2(values[1]);
			var arg3 = ConvertArg3(values[2]);
			var param = (typeof(TParam) == typeof(NULLPARAM)) ? default(TParam) : ConvertParam(parameter);
			return Convert(arg1, arg2, arg3, param);
			//}
			//catch (Exception ex)
			//{
			//	throw new Exception($"Conversiton error in {GetType()}", ex);
			//}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{

			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var param = (typeof(TParam) == typeof(NULLPARAM)) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			//try
			//{
			var arg1 = ConvertArg1(values[0]);
			var arg2 = ConvertArg2(values[1]);
			var arg3 = ConvertArg3(values[2]);
			var arg4 = ConvertArg4(values[3]);
			var arg5 = ConvertArg5(values[4]);
			var param = (typeof(TParam) == typeof(NULLPARAM)) ? default(TParam) : ConvertParam(parameter);
			return Convert(arg1, arg2, arg3, arg4, arg5, param);
			//}
			//catch (Exception ex)
			//{
			//	throw new Exception($"Conversiton error in {GetType()}", ex);
			//}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);
		protected virtual T9 ConvertArg9(object arg) => XAMLHelpers.Convert<T9>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var arg9 = ConvertArg9(values[8]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);
		protected virtual T9 ConvertArg9(object arg) => XAMLHelpers.Convert<T9>(arg, this);
		protected virtual T10 ConvertArg10(object arg) => XAMLHelpers.Convert<T10>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var arg9 = ConvertArg9(values[8]);
				var arg10 = ConvertArg10(values[9]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}


	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);
		protected virtual T9 ConvertArg9(object arg) => XAMLHelpers.Convert<T9>(arg, this);
		protected virtual T10 ConvertArg10(object arg) => XAMLHelpers.Convert<T10>(arg, this);
		protected virtual T11 ConvertArg11(object arg) => XAMLHelpers.Convert<T11>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var arg9 = ConvertArg9(values[8]);
				var arg10 = ConvertArg10(values[9]);
				var arg11 = ConvertArg11(values[10]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);
		protected virtual T9 ConvertArg9(object arg) => XAMLHelpers.Convert<T9>(arg, this);
		protected virtual T10 ConvertArg10(object arg) => XAMLHelpers.Convert<T10>(arg, this);
		protected virtual T11 ConvertArg11(object arg) => XAMLHelpers.Convert<T11>(arg, this);
		protected virtual T12 ConvertArg12(object arg) => XAMLHelpers.Convert<T12>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var arg9 = ConvertArg9(values[8]);
				var arg10 = ConvertArg10(values[9]);
				var arg11 = ConvertArg11(values[10]);
				var arg12 = ConvertArg12(values[11]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);
		protected virtual T9 ConvertArg9(object arg) => XAMLHelpers.Convert<T9>(arg, this);
		protected virtual T10 ConvertArg10(object arg) => XAMLHelpers.Convert<T10>(arg, this);
		protected virtual T11 ConvertArg11(object arg) => XAMLHelpers.Convert<T11>(arg, this);
		protected virtual T12 ConvertArg12(object arg) => XAMLHelpers.Convert<T12>(arg, this);
		protected virtual T13 ConvertArg13(object arg) => XAMLHelpers.Convert<T13>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var arg9 = ConvertArg9(values[8]);
				var arg10 = ConvertArg10(values[9]);
				var arg11 = ConvertArg11(values[10]);
				var arg12 = ConvertArg12(values[11]);
				var arg13 = ConvertArg13(values[12]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);
		protected virtual T9 ConvertArg9(object arg) => XAMLHelpers.Convert<T9>(arg, this);
		protected virtual T10 ConvertArg10(object arg) => XAMLHelpers.Convert<T10>(arg, this);
		protected virtual T11 ConvertArg11(object arg) => XAMLHelpers.Convert<T11>(arg, this);
		protected virtual T12 ConvertArg12(object arg) => XAMLHelpers.Convert<T12>(arg, this);
		protected virtual T13 ConvertArg13(object arg) => XAMLHelpers.Convert<T13>(arg, this);
		protected virtual T14 ConvertArg14(object arg) => XAMLHelpers.Convert<T14>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var arg9 = ConvertArg9(values[8]);
				var arg10 = ConvertArg10(values[9]);
				var arg11 = ConvertArg11(values[10]);
				var arg12 = ConvertArg12(values[11]);
				var arg13 = ConvertArg13(values[12]);
				var arg14 = ConvertArg14(values[13]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

		public abstract class XAMLConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TParam, TResult> : MarkupSingleton, IMultiValueConverter
	{
		protected virtual T1 ConvertArg1(object arg) => XAMLHelpers.Convert<T1>(arg, this);
		protected virtual T2 ConvertArg2(object arg) => XAMLHelpers.Convert<T2>(arg, this);
		protected virtual T3 ConvertArg3(object arg) => XAMLHelpers.Convert<T3>(arg, this);
		protected virtual T4 ConvertArg4(object arg) => XAMLHelpers.Convert<T4>(arg, this);
		protected virtual T5 ConvertArg5(object arg) => XAMLHelpers.Convert<T5>(arg, this);
		protected virtual T6 ConvertArg6(object arg) => XAMLHelpers.Convert<T6>(arg, this);
		protected virtual T7 ConvertArg7(object arg) => XAMLHelpers.Convert<T7>(arg, this);
		protected virtual T8 ConvertArg8(object arg) => XAMLHelpers.Convert<T8>(arg, this);
		protected virtual T9 ConvertArg9(object arg) => XAMLHelpers.Convert<T9>(arg, this);
		protected virtual T10 ConvertArg10(object arg) => XAMLHelpers.Convert<T10>(arg, this);
		protected virtual T11 ConvertArg11(object arg) => XAMLHelpers.Convert<T11>(arg, this);
		protected virtual T12 ConvertArg12(object arg) => XAMLHelpers.Convert<T12>(arg, this);
		protected virtual T13 ConvertArg13(object arg) => XAMLHelpers.Convert<T13>(arg, this);
		protected virtual T14 ConvertArg14(object arg) => XAMLHelpers.Convert<T14>(arg, this);
		protected virtual T15 ConvertArg15(object arg) => XAMLHelpers.Convert<T15>(arg, this);

		protected virtual TParam ConvertParam(object param) => XAMLHelpers.Convert<TParam>(param, this);

		object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				var arg1 = ConvertArg1(values[0]);
				var arg2 = ConvertArg2(values[1]);
				var arg3 = ConvertArg3(values[2]);
				var arg4 = ConvertArg4(values[3]);
				var arg5 = ConvertArg5(values[4]);
				var arg6 = ConvertArg6(values[5]);
				var arg7 = ConvertArg7(values[6]);
				var arg8 = ConvertArg8(values[7]);
				var arg9 = ConvertArg9(values[8]);
				var arg10 = ConvertArg10(values[9]);
				var arg11 = ConvertArg11(values[10]);
				var arg12 = ConvertArg12(values[11]);
				var arg13 = ConvertArg13(values[12]);
				var arg14 = ConvertArg14(values[13]);
				var arg15 = ConvertArg15(values[14]);
				var param = typeof(TParam) == typeof(NULLPARAM) ? default(TParam) : ConvertParam(parameter);
				return Convert(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, param);
			}
			catch (Exception ex)
			{
				throw new Exception($"Conversiton error in {GetType()}", ex);
			}
		}

		public abstract TResult Convert(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, TParam param);

		object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
