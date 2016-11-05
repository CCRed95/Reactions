using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Core.Collections;
using Core.Data.CachedObjects.Collections;

namespace Core.Data.CachedObjects
{
	public abstract class LinqPropertyBase
	{
		public static readonly object UnsetValue = new NamedObject("BetaCachedPropertyBase.UnsetValue");

		protected static ItemStructList<LinqPropertyBase> RegisteredPropertyList = new ItemStructList<LinqPropertyBase>(768);

		internal static IEnumerable<IGrouping<Type, LinqPropertyBase>> RegisteredPropertiesGroupedByOwnerType
		{
			get
			{
				return RegisteredProperties.GroupBy(
					key => key.OwnerType,
					element => element,
					(o, e) => new Grouping<Type, LinqPropertyBase>(o, e));
			}
		}

		internal static IEnumerable<LinqPropertyBase> RegisteredPropertiesForOwnerType(Type ownerType) =>
			RegisteredProperties.Where(p => p.OwnerType == ownerType);

		internal static int RegisteredPropertyCount
		{
			get
			{
				return RegisteredPropertyList.Count;
			}
		}

		internal static IEnumerable<LinqPropertyBase> RegisteredProperties
		{
			get
			{
				var cachedPropertyArray = RegisteredPropertyList.List;
				return cachedPropertyArray.Where(cachedProperty => cachedProperty != null);
			}
		}

		internal static LinqPropertyBase GetPropertyFromIndex(int globalIndex)
		{
			return RegisteredProperties.Single(linqProperty => linqProperty.GlobalIndex == globalIndex);
		}

		public static LinqProperty<TOwner, TValue> Register<TOwner, TValue>(Func<TOwner, TValue> evaluator,
			[CallerMemberName] string cachedPropertyName = null)
			where TOwner : ILinqEntity
		{
			var linqProperty = new LinqProperty<TOwner, TValue>(evaluator, cachedPropertyName);
			RegisterInternal(linqProperty);
			return linqProperty;
		}

		public string CachedPropertyName { get; }

		public string PropertyAccessorName { get; }

		public Type PropertyType { get; }

		public Type OwnerType { get; }

		private LinqPropertyReference propertyReference;
		public LinqPropertyReference PropertyReference => 
			propertyReference ?? (propertyReference = new LinqPropertyReference(GlobalIndex));


		private static int globalLinqPropertyCount = 0;

		internal int globalIndex = globalLinqPropertyCount++;

		public int GlobalIndex => globalIndex;

		public abstract Func<ILinqEntity, object> EvaluatorBase { get; }


		internal static void RegisterInternal(LinqPropertyBase linqProperty)
		{
			RegisteredPropertyList.Add(linqProperty);
			linqProperty.BuildDependentPropertyChain();
		}

		protected void BuildDependentPropertyChain()
		{
			if (!LinqEntity.LinqPropertyDependencyChainMap.ContainsKey(OwnerType))
			{
				LinqEntity.LinqPropertyDependencyChainMap.Add(OwnerType, new ValueDependencyChain());
			}
			var dependentPropertyChain = LinqEntity.LinqPropertyDependencyChainMap[OwnerType];

			foreach (var valueDependencyTrigger in GetValueDependencyTriggers())
			{
				dependentPropertyChain.AddValueDependency(valueDependencyTrigger, PropertyReference);
			}
		}

		protected IEnumerable<string> GetValueDependencyTriggers()
		{
			var propertyFieldInfo = OwnerType.GetField(CachedPropertyName, BindingFlags.Static | BindingFlags.Public);
			var valueDependencyTriggersAttribute = propertyFieldInfo.GetCustomAttribute<ValueDependencyTriggersAttribute>();
			if (valueDependencyTriggersAttribute != null)
			{
				return valueDependencyTriggersAttribute.TriggerPropertyNames;
			}
			return Enumerable.Empty<string>();
		}

		protected LinqPropertyBase(string cachedPropertyName, Type propertyType, Type ownerType)
		{
			CachedPropertyName = cachedPropertyName;
			PropertyAccessorName = getPropertyAccessorName(cachedPropertyName);
			PropertyType = propertyType;
			OwnerType = ownerType;
		}

		protected static string getPropertyAccessorName(string cachedPropertyName)
		{
			if (!cachedPropertyName.EndsWith("Property"))
				throw new FormatException($"LinqProperty name \'{cachedPropertyName}\' invalid. It must end with \'Property\'.");
			return cachedPropertyName.Replace("Property", "");
		}
	}
}





//public abstract object GetValueBase(ILinqEntity owner);

//public void InvalidateProperty(ILinqEntity owner)
//{
//	//IsCacheValid = false;
//	owner.SendPropertyChanged(PropertyAccessorName);
//}

//internal static int currentUniqueID;

//private int? globallyUniqueID;
//public int GloballyUniqueID
//{
//	get
//	{
//		if (globallyUniqueID.HasValue)
//			return globallyUniqueID.Value;
//		currentUniqueID++;
//		globallyUniqueID = currentUniqueID;
//		return globallyUniqueID.Value;
//	}
//}

// protected bool IsCacheValid;
/*
		public static readonly object UnsetValue = new NamedObject("BetaCachedPropertyBase.UnsetValue");

		internal static ItemStructList<BetaCachedPropertyBase> RegisteredPropertyList = new ItemStructList<BetaCachedPropertyBase>(768);

			internal static int RegisteredPropertyCount
		{
			get
			{
				return RegisteredPropertyList.Count;
			}
		}

		internal static IEnumerable RegisteredProperties
		{
			get
			{
				BetaCachedPropertyBase[] dependencyPropertyArray = RegisteredPropertyList.List;
				for (var index = 0; index < dependencyPropertyArray.Length; ++index)
				{
					BetaCachedPropertyBase dependencyProperty = dependencyPropertyArray[index];
					if (dependencyProperty != null)
						yield return (object)dependencyProperty;
				}
				dependencyPropertyArray = null;
			}
		}

	*/

/*

	
		private static Hashtable PropertyFromName = new Hashtable();
		internal static object Synchronized = new object();
		private static Type NullableType = typeof(Nullable<>);
		internal InsertionSortMap _metadataMap;


			//private class FromNameKey
		//{
		//	private string _name;
		//	private Type _ownerType;
		//	private int _hashCode;

		//	public FromNameKey(string name, Type ownerType)
		//	{
		//		this._name = name;
		//		this._ownerType = ownerType;
		//		this._hashCode = this._name.GetHashCode() ^ this._ownerType.GetHashCode();
		//	}

		//	public void UpdateNameKey(Type ownerType)
		//	{
		//		this._ownerType = ownerType;
		//		this._hashCode = this._name.GetHashCode() ^ this._ownerType.GetHashCode();
		//	}

		//	public override int GetHashCode()
		//	{
		//		return this._hashCode;
		//	}

		//	public override bool Equals(object o)
		//	{
		//		if (o != null && o is BetaCachedPropertyBase.FromNameKey)
		//			return this.Equals((BetaCachedPropertyBase.FromNameKey)o);
		//		return false;
		//	}

		//	public bool Equals(BetaCachedPropertyBase.FromNameKey key)
		//	{
		//		if (this._name.Equals(key._name))
		//			return this._ownerType == key._ownerType;
		//		return false;
		//	}
		//}


				BetaCachedPropertyBase.FromNameKey fromNameKey = new BetaCachedPropertyBase.FromNameKey(name, typeof(TOwner));
			object obj1 = BetaCachedPropertyBase.Synchronized;
			bool lockTaken1 = false;
			try
			{
				Monitor.Enter(obj1, ref lockTaken1);
				if (BetaCachedPropertyBase.PropertyFromName.Contains((object)fromNameKey))
					throw new ArgumentException($"PropertyAlreadyRegistered \'{(object)typeof(TOwner).Name}\' \'{(object)name}\'");
			}
			finally
			{
				if (lockTaken1)
					Monitor.Exit(obj1);
			}
			BetaCachedPropertyBase dp = new BetaCachedProperty<TOwner, TValue>(name, );
			object obj2 = BetaCachedPropertyBase.Synchronized;
			bool lockTaken2 = false;
			try
			{
				Monitor.Enter(obj2, ref lockTaken2);
				DependencyProperty.PropertyFromName[(object)fromNameKey] = (object)dp;
			}
			finally
			{
				if (lockTaken2)
					Monitor.Exit(obj2);
			}
			if (TraceDependencyProperty.IsEnabled)
				TraceDependencyProperty.TraceActivityItem(TraceDependencyProperty.Register, (object)dp, (object)dp.OwnerType);
			return dp;*/
