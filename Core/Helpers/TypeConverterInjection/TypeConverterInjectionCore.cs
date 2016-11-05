using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using Core.Helpers;

namespace Core.Markup.TypeConverterInjection
{
	public static class TypeConverterInjectionCore
	{
		public static void HandleMarkupExtensionTypeConverterInject(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			if (targetObject == null)
				throw new ArgumentNullException(nameof(targetObject));
			if (eventArgs == null)
				throw new ArgumentNullException(nameof(eventArgs));


			var injectedTypeConverterAttributes = eventArgs.Member.UnderlyingMember
				.GetCustomAttributes(typeof(InjectTypeConverterAttribute), true)
				.OfType<InjectTypeConverterAttribute>()
				.ToArray();

			if (injectedTypeConverterAttributes.Any())
			{
				if (injectedTypeConverterAttributes.Length > 1)
					throw new NotSupportedException("Multiple \'InjectTypeConverterAttribute\' attributes discovered.");

				var injectedTypeConverterAttribute = injectedTypeConverterAttributes.Single();

				var converterType = Type.GetType(injectedTypeConverterAttribute.AssemblyQualifiedTypeName);
				if (converterType == null)
					throw new TypeLoadException("Injected type converter type cannot be resolved.");

				var constructor = converterType.GetConstructor(new Type[] { });
				if (constructor == null)
					throw new NotSupportedException("Injected type converter constructor must have a parameterless constructor.");

				var converterTypeInstance = constructor.Invoke(new object[] { });

				var converter = converterTypeInstance as TypeConverter;
				if (converter == null)
					throw new Exception($"Type \'{converterTypeInstance.GetType().Name}\' not valid. Must be of type \'TypeConverter\'.");

				if (eventArgs.Value.GetType().Name == "TypeConverterMarkupExtension")
				{
					eventArgs.Value.SetFieldValue("_converter", converter);
					eventArgs.Handled = true;
				}
				//var unresolvedValue = eventArgs.Value;
				//if (eventArgs.Value.GetType().Name == "TypeConverterMarkupExtension")
				//{
				//	unresolvedValue = eventArgs.Value.GetFieldValue<object>("_value");
				//}

				//var convertedValue = converter.ConvertFrom(unresolvedValue);

				//if (convertedValue == null)
				//	throw new NullReferenceException(nameof(convertedValue));

				//var inv = eventArgs.Member.Invoker;
				//inv.SetValue(targetObject, convertedValue);

				//eventArgs.Handled = true;
			}

			//if (eventArgs.MarkupExtension is )
		}

		public static void HandlePropertySetFromMarkupExtension(object targetObject, XamlSetMarkupExtensionEventArgs eventArgs)
		{
			if (targetObject == null)
				throw new ArgumentNullException(nameof(targetObject));
			if (eventArgs == null)
				throw new ArgumentNullException(nameof(eventArgs));


			var injectedTypeConverterAttributes = eventArgs.Member.UnderlyingMember
				.GetCustomAttributes(typeof(InjectTypeConverterAttribute), true)
				.OfType<InjectTypeConverterAttribute>()
				.ToArray();

			if (injectedTypeConverterAttributes.Any())
			{
				if (injectedTypeConverterAttributes.Length > 1)
					throw new NotSupportedException("Multiple \'InjectTypeConverterAttribute\' attributes discovered.");

				var injectedTypeConverterAttribute = injectedTypeConverterAttributes.Single();

				var converterType = Type.GetType(injectedTypeConverterAttribute.AssemblyQualifiedTypeName);
				if (converterType == null)
					throw new TypeLoadException("Injected type converter type cannot be resolved.");

				var constructor = converterType.GetConstructor(new Type[] { });
				if (constructor == null)
					throw new NotSupportedException("Injected type converter constructor must have a parameterless constructor.");

				var converterTypeInstance = constructor.Invoke(new object[] { });

				var converter = converterTypeInstance as TypeConverter;
				if (converter == null)
					throw new Exception($"Type \'{converterTypeInstance.GetType().Name}\' not valid. Must be of type \'TypeConverter\'.");

				var unresolvedValue = eventArgs.Value;
				if (eventArgs.Value.GetType().Name == "TypeConverterMarkupExtension")
				{
					unresolvedValue = eventArgs.Value.GetFieldValue<object>("_value");
				}

				var convertedValue = converter.ConvertFrom(unresolvedValue);

				if (convertedValue == null)
					throw new NullReferenceException(nameof(convertedValue));

				var inv = eventArgs.Member.Invoker;
				inv.SetValue(targetObject, convertedValue);

				eventArgs.Handled = true;
			}

			//if (eventArgs.MarkupExtension is )
		}

		public static void HandlePropertySet(object targetObject, XamlSetTypeConverterEventArgs eventArgs)
		{
			if (targetObject == null)
				throw new ArgumentNullException(nameof(targetObject));
			if (eventArgs == null)
				throw new ArgumentNullException(nameof(eventArgs));

			var injectedTypeConverterAttributes = eventArgs.Member.UnderlyingMember
				.GetCustomAttributes(typeof(InjectTypeConverterAttribute), true)
				.OfType<InjectTypeConverterAttribute>()
				.ToArray();

			if (injectedTypeConverterAttributes.Any())
			{
				if (injectedTypeConverterAttributes.Length > 1)
					throw new NotSupportedException("Multiple \'InjectTypeConverterAttribute\' attributes discovered.");

				var injectedTypeConverterAttribute = injectedTypeConverterAttributes.Single();

				var converterType = Type.GetType(injectedTypeConverterAttribute.AssemblyQualifiedTypeName);
				if (converterType == null)
					throw new TypeLoadException("Injected type converter type cannot be resolved.");

				var constructor = converterType.GetConstructor(new Type[] { });
				if (constructor == null)
					throw new NotSupportedException("Injected type converter constructor must have a parameterless constructor.");

				var converterTypeInstance = constructor.Invoke(new object[] { });

				var converter = converterTypeInstance as TypeConverter;
				if (converter == null)
					throw new Exception($"Type \'{converterTypeInstance.GetType().Name}\' not valid. Must be of type \'TypeConverter\'.");

				var convertedValue = converter.ConvertFrom(eventArgs.Value);

				if (convertedValue == null)
					throw new NullReferenceException(nameof(convertedValue));

				var inv = eventArgs.Member.Invoker;
				inv.SetValue(targetObject, convertedValue);

				eventArgs.Handled = true;
			}
		}
	}
}
