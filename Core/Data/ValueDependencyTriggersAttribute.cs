using System;
namespace Core.Data
{
	public class ValueDependencyTriggersAttribute : Attribute
	{
		public string[] TriggerPropertyNames { get; }

		public ValueDependencyTriggersAttribute(params string[] triggerPropertyNames)
		{
			TriggerPropertyNames = triggerPropertyNames;
		}
	}
}
