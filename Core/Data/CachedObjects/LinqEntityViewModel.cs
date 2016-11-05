using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Caliburn.Micro;

namespace Core.Data.CachedObjects
{
	public class LinqEntityViewModel : Screen, ILinqEntity
	{
		private bool isInView;
		public bool IsInView
		{
			get { return isInView; }
			set
			{
				isInView = value;
				NotifyOfPropertyChange(() => IsInView);
			}
		}
		////TODO why doesnt this work? does this class need to be abstract?
		//private static readonly DependencyObject DesignerModeDO = new DependencyObject();
		//public bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(DesignerModeDO);


		private static bool? _isInDesignMode;


		/// <summary>
		/// Gets a value indicating whether the control is in design mode (running in Blend
		/// or Visual Studio).
		/// </summary>
		public static bool IsInDesignModeStatic
		{
			get
			{
				if (_isInDesignMode.HasValue)
					return _isInDesignMode.Value;
				var prop = DesignerProperties.IsInDesignModeProperty;
				_isInDesignMode
					= (bool)DependencyPropertyDescriptor
						.FromProperty(prop, typeof(FrameworkElement))
						.Metadata.DefaultValue;

				return _isInDesignMode.Value;
			}
		}

		internal Dictionary<int, EffectiveValueEntry> EffectiveValues = new Dictionary<int, EffectiveValueEntry>();

		protected override void OnInitialize()
		{
			base.OnInitialize();
			((ILinqEntity)this).OnLoaded();
		}

		void ILinqEntity.OnLoaded()
		{
			((INotifyPropertyChanged)this).PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
			InvalidateAllDependentProperties();
		}

		void ILinqEntity.SendPropertyChanged(string propertyName)
		{
			NotifyOfPropertyChange(propertyName);
		}

		//TODO recursion protection with property invalidation, ignore/filter out OnPropertyChanged calls from LinqProperties?
		public void OnPropertyChanged(string propertyName)
		{
			var dependencyChainOwnerType = GetType();
			if (!LinqEntity.LinqPropertyDependencyChainMap.ContainsKey(dependencyChainOwnerType))
			{
				return;
			}
			var linqPropertyDependencyChain = LinqEntity.LinqPropertyDependencyChainMap[dependencyChainOwnerType];
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
