﻿using System;

namespace Core.Markup.TypeConverterInjection
{
	[AttributeUsage(AttributeTargets.Property)]
	public class InjectTypeConverterAttribute : Attribute
	{
		public string AssemblyQualifiedTypeName { get; }

		public InjectTypeConverterAttribute(Type alternateTypeConverter)
		{
			AssemblyQualifiedTypeName = alternateTypeConverter.AssemblyQualifiedName;
		}
	}
}
