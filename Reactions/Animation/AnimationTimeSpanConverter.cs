using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Reactions.Animation
{
	//public class AnimationTimeSpanConverter : TypeConverter
	//{
	//	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	//	{
	//		if (sourceType == typeof(string))
	//		{
	//			return true;
	//		}
	//		return base.CanConvertFrom(context, sourceType);
	//	}

	//	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	//	{
	//		if (destinationType == typeof(InstanceDescriptor))
	//		{
	//			return true;
	//		}
	//		return base.CanConvertTo(context, destinationType);
	//	}

	//	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	//	{
	//		if (value is string)
	//		{
	//			string text = ((string)value).Trim();
	//			try
	//			{
	//				return AnimationTimeSpan.Parse(text, culture);
	//			}
	//			catch (FormatException e)
	//			{
	//				throw new FormatException("AnimationTimeSpan format exception", e);
	//			}
	//		}
	//		return base.ConvertFrom(context, culture, value);
	//	}

	//	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	//	{
	//		if (destinationType == null)
	//		{
	//			throw new ArgumentNullException(nameof(destinationType));
	//		}

	//		if (destinationType == typeof(InstanceDescriptor) && value is AnimationTimeSpan)
	//		{
	//			var method = typeof(AnimationTimeSpan).GetMethod("Parse", new[] { typeof(string) });
	//			if (method != null)
	//			{
	//				return new InstanceDescriptor(method, new object[] { value.ToString() });
	//			}
	//		}

	//		return base.ConvertTo(context, culture, value, destinationType);
	//	}
	//}
}
