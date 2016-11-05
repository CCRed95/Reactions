using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Core.Data.CachedObjects
{
	public abstract class LinqEntity : ILinqEntity
	{
		//TODO should this be in the linqpropertybase class? all static data storage in one spot?
		internal static Dictionary<Type, ValueDependencyChain> LinqPropertyDependencyChainMap = new Dictionary<Type, ValueDependencyChain>();

		internal Dictionary<int, EffectiveValueEntry> EffectiveValues = new Dictionary<int, EffectiveValueEntry>();

		void ILinqEntity.OnLoaded()
		{
			((INotifyPropertyChanged)this).PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
			InvalidateAllDependentProperties();
		}

		void ILinqEntity.SendPropertyChanged(string propertyName)
		{
			var t = this.GetType();
			t.InvokeMember("SendPropertyChanged",
				BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
				null, this, new object[] { propertyName });
		}
		//TODO recursion protection with property invalidation, ignore/filter out OnPropertyChanged calls from LinqProperties?
		public void OnPropertyChanged(string propertyName)
		{
			var dependencyChainOwnerType = GetType();
			if (!LinqPropertyDependencyChainMap.ContainsKey(dependencyChainOwnerType))
			{
				return;
			}
			var linqPropertyDependencyChain = LinqPropertyDependencyChainMap[dependencyChainOwnerType];
			var dependentPropertyReferences = linqPropertyDependencyChain.GetDependentPropertyReferences(propertyName);
			foreach (var dependentPropertyReference in dependentPropertyReferences)
			{
				var resolvedLinqProperty = dependentPropertyReference.ResolvePropertyReference();
				InvalidateProperty(resolvedLinqProperty);
			}
		}

		public void InvalidateAllDependentProperties()
		{
			var linqEntityType = GetType();
			foreach (var linqProperty in LinqPropertyBase.RegisteredPropertiesForOwnerType(linqEntityType))
			{
				InvalidateProperty(linqProperty);
			}
			
		}

		public object GetValueBase(LinqPropertyBase linqProperty)
		{
			var effectiveValueEntry = GetEffectiveValue(linqProperty);
			return effectiveValueEntry.GetValue();
		}

		//TODO Generic GetValueBase

		public void InvalidateProperty(LinqPropertyBase linqProperty)
		{
			var effectiveValueEntry = GetEffectiveValue(linqProperty);
			effectiveValueEntry.InvalidateCache();
		}

		internal EffectiveValueEntry GetEffectiveValue(LinqPropertyBase linqProperty)
		{
			if (EffectiveValues.ContainsKey(linqProperty.GlobalIndex))
			{
				var effectiveValue = EffectiveValues[linqProperty.GlobalIndex];
				return effectiveValue;
			}
			if (linqProperty.OwnerType == GetType())
			{
				EffectiveValues.Add(linqProperty.GlobalIndex, new EffectiveValueEntry(this, linqProperty));
				var effectiveValue = EffectiveValues[linqProperty.GlobalIndex];
				return effectiveValue;
			}
			throw new NotSupportedException("only valid on the LinqProperty's Owner type.");
		}

	}
}
