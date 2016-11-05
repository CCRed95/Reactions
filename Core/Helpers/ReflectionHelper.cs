using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Core.Helpers
{
	public static class ReflectionHelper
	{
		//TODO static refl members should not use this! it includes instance members! static only!
		const BindingFlags InstanceNonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

		public static DependencyProperty GetDependencyProperty(string name, DependencyObject parent, bool caseSensitive = false)
		{
			const string prop = "Property";
			var propertyName = name;
			if (!propertyName.ToLower().EndsWith(prop.ToLower()))
			{
				propertyName += prop;
			}
			var fieldInfoArray = parent.GetType().GetFields();
			foreach (var fieldInfo in fieldInfoArray)
			{
				if (caseSensitive)
				{
					if (fieldInfo.Name != propertyName) continue;
					var dependencyPropertyField = (DependencyProperty)fieldInfo.GetValue(parent);
					return dependencyPropertyField;
				}
				else
				{
					if (!string.Equals(fieldInfo.Name, propertyName, StringComparison.CurrentCultureIgnoreCase)) continue;
					var dependencyPropertyField = (DependencyProperty)fieldInfo.GetValue(parent);
					return dependencyPropertyField;
				}
			}
			throw new Exception("FSR.Reflection.CannotFindDependencyProperty(parent, name, caseSensitive)");
		}

		public static DependencyProperty GetDependencyProperty(string name, Type parent, bool caseSensitive = false)
		{
			const string prop = "Property";
			var propertyName = name;
			if (!propertyName.ToLower().EndsWith(prop.ToLower()))
			{
				propertyName += prop;
			}
			var fieldInfoArray = parent.GetFields();
			foreach (var fieldInfo in fieldInfoArray)
			{
				if (caseSensitive)
				{
					if (fieldInfo.Name != propertyName) continue;
					var dependencyPropertyField = (DependencyProperty)fieldInfo.GetValue(null);
					return dependencyPropertyField;
				}
				else
				{
					if (!string.Equals(fieldInfo.Name, propertyName, StringComparison.CurrentCultureIgnoreCase)) continue;
					var dependencyPropertyField = (DependencyProperty)fieldInfo.GetValue(null);
					return dependencyPropertyField;
				}
			}
			throw new Exception("FSR.Reflection.CannotFindDependencyProperty(null, name, caseSensitive)");
		}

		public static FieldInfo GetFieldInfoByName(FieldInfo[] fieldArray, string name, bool caseSensitive = false)
		{
			foreach (var field in fieldArray)
			{
				if (caseSensitive)
				{
					if (field.Name == name) return field;
				}
				else
				{
					if (string.Equals(field.Name, name, StringComparison.CurrentCultureIgnoreCase)) return field;
				}
			}
			throw new Exception("FSR.Reflection.CannotFindFieldInfoWithName(name, caseSensitive)");
		}

		public static bool TryGetObjectFromRuntimeField(FieldInfo runtimeFieldInfo, DependencyObject parent, out DependencyObject runtimeObject)
		{
			try
			{
				var runtimeFieldValue = runtimeFieldInfo.GetValue(parent);
				runtimeObject = (DependencyObject)runtimeFieldValue;
				return true;
			}
			catch
			{
				runtimeObject = default(DependencyObject);
				return false;
			}
		}

		public static bool TryReflectPropertyValue<T>(this object i, string propertyName, out T propertyValue, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
		{
			var propertyInfo = i.GetType().GetProperty(propertyName, bindingFlags);
			var propertyVal = propertyInfo?.GetValue(i);
			if (propertyVal is T)
			{
				propertyValue = (T)propertyVal;
				return true;
			}
			propertyValue = default(T);
			return false;
		}

		public static bool TryInvokeInternalMethod<T1, TResult>(this object i, string methodName, out TResult @returnValue, T1 arg1)
		{
			var methodInfo = i.GetType().GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1) }, null);
			var retVal = methodInfo?.Invoke(i, new object[] { arg1 });
			if (retVal is TResult)
			{
				@returnValue = (TResult)retVal;
				return true;
			}
			@returnValue = default(TResult);
			return false;
		}

		public static bool TryInvokeInternalMethod<T1, T2, T3, T4>(this object i, string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			var methodInfo = i.GetType().GetMethod(methodName, InstanceNonPublic, null, new[]
			{
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4)
			}, null);
			methodInfo?.Invoke(i, new object[] { arg1, arg2, arg3, arg4 });
			return true;
		}

		public static bool TryInvokeInternalMethod<T1>(this object i, string methodName, T1 arg1)
		{
			try
			{
				var methodInfo = i.GetType().GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1) }, null);
				if (methodInfo == null)
					return false;
				methodInfo.Invoke(i, new object[] { arg1 });
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool TryInvokeInternalMethod<T1, T2>(this object i, string methodName, T1 arg1, T2 arg2)
		{
			try
			{
				var methodInfo = i.GetType().GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1), typeof(T2) }, null);
				if (methodInfo == null)
					return false;
				methodInfo.Invoke(i, new object[] { arg1, arg2 });
				return true;
			}
			catch
			{
				return false;
			}
		}
		public static bool TryInvokeInternalStaticMethod<T1, TResult>(this Type type, string methodName, out TResult @returnValue, T1 arg1)
		{
			var methodInfo = type.GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1) }, null);
			if (methodInfo == null)
			{
				@returnValue = default(TResult);
				return false;
			}

			var retVal = methodInfo.Invoke(null, new object[] { arg1 });
			if (retVal is TResult)
			{
				@returnValue = (TResult)retVal;
				return true;
			}
			@returnValue = default(TResult);
			return false;

		}
		public static bool TryInvokeInternalStaticMethod<T1, T2, T3, TResult>(this Type type, string methodName, out TResult @returnValue, T1 arg1, T2 arg2, T3 arg3)
		{
			var methodInfo = type.GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1), typeof(T2), typeof(T3) }, null);
			if (methodInfo == null)
			{
				@returnValue = default(TResult);
				return false;
			}

			var retVal = methodInfo.Invoke(null, new object[] { arg1, arg2, arg3 });
			if (retVal is TResult)
			{
				@returnValue = (TResult)retVal;
				return true;
			}
			@returnValue = default(TResult);
			return false;

		}
		public static bool TryInvokeInternalStaticMethod<T1, T2, T3, T4, TResult>(this Type type, string methodName, out TResult @returnValue, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			var methodInfo = type.GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, null);
			if (methodInfo == null)
			{
				@returnValue = default(TResult);
				return false;
			}

			var retVal = methodInfo.Invoke(null, new object[] { arg1, arg2, arg3, arg4 });
			if (retVal is TResult)
			{
				@returnValue = (TResult)retVal;
				return true;
			}
			@returnValue = default(TResult);
			return false;

		}
		public static TResult InvokeInternalStaticMethod<TResult>(this Type type, string methodName, params object[] args)
		{
			var methodInfo = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, args.Select(arg => arg.GetType()).ToArray(), null);

			//var mis = type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
			//var s = type.GetRuntimeMethods();
			if (methodInfo == null)
			{
				throw new MissingMethodException(type.Name, methodName);
			}
			var returnVal = methodInfo.Invoke(null, args);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast return value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}
		public static TResult InvokeInternalStaticMethodWithOutParameter<TResult, TOut>(this Type type, string methodName, out TOut outParameter, params object[] args)
		{
			object[] parameters = args.Concat(new object[] { null }).ToArray();
			var methodInfo = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic, null, parameters.Select(arg => arg.GetType()).ToArray(), null);

			if (methodInfo == null)
			{
				throw new MissingMethodException(type.Name, methodName);
			}

			var returnVal = methodInfo.Invoke(null, args);
			var returnedOutVal = parameters.Last();
			if (returnVal is TResult)
			{
				if (!(returnedOutVal is TOut))
				{
					throw new InvalidCastException($"Cannot cast returned out parameter value of type \'{returnedOutVal.GetType().Name}\' to \'{typeof(TOut).Name}\'.");
				}
				outParameter = (TOut)returnedOutVal;
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast return value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}
		public static void CallInternalStaticMethod(this Type type, string methodName, params object[] args)
		{
			var methodInfo = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic, null, args.Select(arg => arg.GetType()).ToArray(), null);

			if (methodInfo == null)
			{
				throw new MissingMethodException(type.Name, methodName);
			}
			methodInfo.Invoke(null, args);
		}
		public static TResult InvokeInternalMethod<TResult>(this object instance, string methodName, params object[] args)
		{
			var type = instance.GetType();
			var methodInfo = type.GetMethod(methodName, InstanceNonPublic, null, args.Select(arg => arg.GetType()).ToArray(), null);
			if (methodInfo == null)
			{
				throw new MissingMethodException(type.Name, methodName);
			}
			var returnVal = methodInfo.Invoke(instance, args);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast return value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}

		public static TResult InvokeInternalStaticMethodGeneric<T1, T2, T3, TResult>(this Type type, string methodName, T1 arg1, T2 arg2, T3 arg3)
		{
			var methodInfo = type.GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1), typeof(T2), typeof(T3) }, null);
			if (methodInfo == null)
			{
				throw new MissingMethodException(type.Name, methodName);
			}
			var returnVal = methodInfo.Invoke(null, new object[] { arg1, arg2, arg3 });
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast return value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}
		public static TResult InvokeInternalStaticMethodGeneric<T1, T2, T3, T4, TResult>(this Type type, string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			var methodInfo = type.GetMethod(methodName, InstanceNonPublic, null, new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, null);
			if (methodInfo == null)
			{
				throw new MissingMethodException(type.Name, methodName);
			}
			var returnVal = methodInfo.Invoke(null, new object[] { arg1, arg2, arg3, arg4 });
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast return value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}
		public static TResult GetInternalStaticPropertyValue<TResult>(this Type type, string propertyName)
		{
			var propertyInfo = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.NonPublic);

			if (propertyInfo == null)
			{
				throw new MissingMemberException(type.Name, propertyName);
			}
			var returnVal = propertyInfo.GetValue(null);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast property value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}
		public static TResult GetInternalPropertyValue<TResult>(this object instance, string propertyName)
		{
			var type = instance.GetType();
			var propertyInfo = type.GetProperty(propertyName, InstanceNonPublic);

			if (propertyInfo == null)
			{
				throw new MissingMemberException(type.Name, propertyName);
			}
			var returnVal = propertyInfo.GetValue(instance);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast property value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}

		public static TResult GetPropertyValue<TResult>(this object instance, string propertyName)
		{
			if (instance == null)
				throw new NullReferenceException($"Could not get property name \'{propertyName}\' in null object");

			var type = instance.GetType();
			var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

			if (propertyInfo == null)
			{
				throw new MissingMemberException(type.Name, propertyName);
			}
			var returnVal = propertyInfo.GetValue(instance);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast property value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}

		public static void SetPropertyValue(this object instance, string propertyName, object value)
		{
			var type = instance.GetType();
			var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

			if (propertyInfo == null)
			{
				throw new MissingMemberException(type.Name, propertyName);
			}
			propertyInfo.SetValue(instance, value);
		}
		public static void SetInternalPropertyValue(this object instance, string propertyName, object value)
		{
			var type = instance.GetType();
			var propertyInfo = type.GetProperty(propertyName, InstanceNonPublic);

			if (propertyInfo == null)
			{
				throw new MissingMemberException(type.Name, propertyName);
			}
			propertyInfo.SetValue(instance, value);
		}


		public static TResult InvokeMethod<TResult>(this object instance, string methodName, params object[] args)
		{
			var type = instance.GetType();
			var methodInfo = type.GetMethod(methodName, InstanceNonPublic, null, args.Select(arg => arg.GetType()).ToArray(), null);
			if (methodInfo == null)
			{
				throw new MissingMethodException(type.Name, methodName);
			}
			var returnVal = methodInfo.Invoke(instance, args);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast return value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}

		public static TResult GetStaticFieldValue<TResult>(this Type type, string fieldName)
		{
			var fieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
			if (fieldInfo == null)
			{
				throw new MissingMemberException(type.Name, fieldName);
			}
			var returnVal = fieldInfo.GetValue(null);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast property value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}

		public static TResult GetFieldValue<TResult>(this object @this, string fieldName)
		{
			if (@this == null)
				throw new NullReferenceException($"Could not get property name \'{fieldName}\' in null object");

			var type = @this.GetType();
			var fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

			if (fieldInfo == null)
			{
				throw new MissingMemberException(type.Name, fieldName);
			}
			//TODO was this before:	var returnVal = fieldInfo.GetValue(fieldName);
			var returnVal = fieldInfo.GetValue(@this);
			if (returnVal is TResult)
			{
				return (TResult)returnVal;
			}
			throw new InvalidCastException($"Cannot cast property value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}
		public static void SetFieldValue(this object @this, string fieldName, object value)
		{
			if (@this == null)
				throw new NullReferenceException($"Could not get property name \'{fieldName}\' in null object");

			var type = @this.GetType();
			var fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

			if (fieldInfo == null)
			{
				throw new MissingMemberException(type.Name, fieldName);
			}
			//TODO was this before:	var returnVal = fieldInfo.GetValue(fieldName);
			fieldInfo.SetValue(@this, value);

			//throw new InvalidCastException($"Cannot cast property value \'{returnVal.GetType().Name}\' to {typeof(TResult).Name}");
		}
	}
}
