using System.ComponentModel;

namespace Reactions.Conditions
{
	[TypeConverter(typeof(ComparisonTypeConverter))]
	public enum ComparisonType
	{
		Equals,
		NotEquals,
		LessThan,
		GreaterThan,
		LessThanOrEqual,
		GreaterThanOrEqual
	}
}
