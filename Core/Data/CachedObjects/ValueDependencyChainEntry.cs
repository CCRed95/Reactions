using System.Collections.Generic;

namespace Core.Data.CachedObjects
{
	public class ValueDependencyChainEntry
	{
		public string TriggerPropertyName { get; }

		public List<LinqPropertyReference> dependentPropertyReferences;
		public IEnumerable<LinqPropertyReference> DependentPropertyReferences => dependentPropertyReferences;


		public ValueDependencyChainEntry(string triggerPropertyName, LinqPropertyReference dependentPropertyReference)
			: this(triggerPropertyName, new[] { dependentPropertyReference })
		{
		}

		public ValueDependencyChainEntry(string triggerPropertyName, IEnumerable<LinqPropertyReference> _dependentPropertyReferences)
		{
			TriggerPropertyName = triggerPropertyName;
			dependentPropertyReferences = new List<LinqPropertyReference>(_dependentPropertyReferences);
		}

		public void AddDependentProperty(LinqPropertyReference dependentProperty)
		{
			dependentPropertyReferences.Add(dependentProperty);
		}
	}
}
