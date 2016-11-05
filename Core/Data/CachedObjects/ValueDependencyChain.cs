using System.Collections.Generic;
using System.Linq;

namespace Core.Data.CachedObjects
{
	public class ValueDependencyChain
	{
		protected List<ValueDependencyChainEntry> ValueDependencyChainEntries = new List<ValueDependencyChainEntry>();

		//TODO singleordefault, like below method
		public void AddValueDependency(string triggerPropertyName, LinqPropertyReference dependentPropertyReference)
		{
			if (ValueDependencyChainEntries.Any(e => e.TriggerPropertyName == triggerPropertyName))
			{
				var valueDependencyChainEntry = ValueDependencyChainEntries.Single(e => e.TriggerPropertyName == triggerPropertyName);
				valueDependencyChainEntry.AddDependentProperty(dependentPropertyReference);
				return;
			}
			//else
			//{
			//	var d = new ValueDependencyChainEntry(triggerPropertyName, dependentPropertyReference);
			//	ValueDependencyChainEntries.Add()
			//}
			ValueDependencyChainEntries.Add(new ValueDependencyChainEntry(triggerPropertyName, dependentPropertyReference));
		}

		public IEnumerable<LinqPropertyReference> GetDependentPropertyReferences(string triggerPropertyName)
		{
			var valueDependencyChainEntry = ValueDependencyChainEntries.SingleOrDefault(e => e.TriggerPropertyName == triggerPropertyName);
			if (valueDependencyChainEntry == default(ValueDependencyChainEntry))
			{
				return Enumerable.Empty<LinqPropertyReference>();
			}
			return valueDependencyChainEntry.DependentPropertyReferences;
		}
	}
}
