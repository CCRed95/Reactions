using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Core.Helpers.DependencyHelpers;

namespace Reactions.Triggers
{
	public class SimplePropertyTrigger2 : ReactiveTriggerBase
	{
		//TODO DependencyObject
		private object source;
		public object Source
		{
			get { return source; }
			protected set
			{
				var oldSource = source;
				source = value;
				OnSourceChanged(oldSource, source);
			}
		}

		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<SimplePropertyTrigger2, DependencyObject>(null, onSourceObjectChanged));

		public DependencyObject SourceObject
		{
			get { return (DependencyObject)GetValue(SourceObjectProperty); }
			set { SetValue(SourceObjectProperty, value); }
		}

		private DependencyProperty property;
		public DependencyProperty Property
		{
			get { return property; }
			set
			{
				var oldProperty = property;
				property = value;
				OnPropertyChanged(oldProperty, property);
				if (deferValueConversion)
					AttemptInterpretValue();
			}
		}

		private TypeConverter valueTypeConverter;
		protected TypeConverter ValueTypeConverter
		{
			get
			{
				if (valueTypeConverter == null)
					valueTypeConverter = TypeDescriptor.GetConverter(Property.PropertyType);
				return valueTypeConverter;
			}
			set { valueTypeConverter = value; }
		}

		private bool deferValueConversion;
		private object unresolvedValue;
		private object actualValue;

		public object Value
		{
			get
			{
				if (actualValue == null)
					AttemptInterpretValue();
				return actualValue;
			}
			set
			{
				var oldValue = unresolvedValue;
				unresolvedValue = value;
				OnValueSet(oldValue, unresolvedValue);
				AttemptInterpretValue();
			}
		}

		public static readonly DependencyProperty BoundValueProperty = DP.Register(
			new Meta<SimplePropertyTrigger2, object>(null, onBoundValueChanged));

		public object BoundValue
		{
			get { return GetValue(BoundValueProperty); }
			set { SetValue(BoundValueProperty, value); }
		}

		private static void onSourceObjectChanged(SimplePropertyTrigger2 i, DPChangedEventArgs<DependencyObject> e)
		{
			i.Source = e.NewValue;
		}

		private static void onBoundValueChanged(SimplePropertyTrigger2 i, DPChangedEventArgs<object> e)
		{
			i.UpdateTriggerState();
		}

		protected void AttemptInterpretValue()
		{
			deferValueConversion = false;
			if (Property == null)
			{
				deferValueConversion = true;
				return;
			}
			try
			{
				actualValue = ValueTypeConverter.ConvertFrom(unresolvedValue); 
			}
			catch
			{
				actualValue = unresolvedValue; 
			}
			//if (Property.PropertyType.IsInstanceOfType(unresolvedValue))
			//{
			//	actualValue = ValueTypeConverter.ConvertFrom(unresolvedValue); 
			//}
			//else
			//{
			//	actualValue = unresolvedValue; 
			//}
			
			OnValueInterpreted();
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			Source = AssociatedObject;
			if (SourceObject == null)
			{
				Source = AssociatedObject;
			}
			else
			{
				Source = SourceObject;
			}
		}

		protected void OnSourceChanged(object oldSource, object newSource)
		{
			if (newSource != null && Property != null)
			{
				SetBinding();
			}
		}

		protected void OnPropertyChanged(DependencyProperty oldProperty, DependencyProperty newProperty)
		{
			valueTypeConverter = null;
			if (Source != null && newProperty != null)
			{
				SetBinding();
			}
		}

		protected void OnValueSet(object oldValue, object newValue)
		{
		}

		protected void OnValueInterpreted()
		{
			if (Source != null && actualValue != null && Property != null)
			{
				UpdateTriggerState();
			}
		}

		protected void SetBinding()
		{
			BindingOperations.SetBinding(this, BoundValueProperty, new Binding(Property.Name) { Source = Source });
		}

		protected void UpdateTriggerState()
		{
			if (Value == BoundValue)
			{
				Execute();
			}
			else
			{

			}
		}

	}
	//public class SimplePropertyTrigger : ReactiveTriggerBase
	//{
	//	protected static readonly DependencyPropertyKey SourcePropertyKey = DP.RegisterReadOnly(
	//		new Meta<SimplePropertyTrigger, object>(null, onSourceChanged));

	//	public static readonly DependencyProperty SourceProperty = SourcePropertyKey.DependencyProperty;

	//	public static readonly DependencyProperty SourceObjectProperty = DP.Register(
	//		new Meta<SimplePropertyTrigger, object>(null, onSourceObjectChanged));

	//	public static readonly DependencyProperty PropertyProperty = DP.Register(
	//		new Meta<SimplePropertyTrigger, DependencyProperty>(UIElement.IsMouseOverProperty, onPropertyPropertyChanged));

	//	public static readonly DependencyProperty BoundValueProperty = DP.Register(
	//		new Meta<SimplePropertyTrigger, object>(null, onBoundValueChanged));

	//	public static readonly DependencyProperty ValueProperty = DP.Register(
	//		new Meta<SimplePropertyTrigger, object>(true, onValueChanged));



	//	public object Value
	//	{
	//		get { return  GetValue(ValueProperty); }
	//		set { SetValue(ValueProperty, value); }
	//	}


	//	public object BoundValue
	//	{
	//		get { return GetValue(BoundValueProperty); }
	//		set { SetValue(BoundValueProperty, value); }
	//	}


	//	public object Source
	//	{
	//		get { return GetValue(SourceProperty); }
	//		protected set { SetValue(SourcePropertyKey, value); }
	//	}
	//	public object SourceObject
	//	{
	//		get { return GetValue(SourceObjectProperty); }
	//		set { SetValue(SourceObjectProperty, value); }
	//	}
	//	public DependencyProperty Property
	//	{
	//		get { return (DependencyProperty)GetValue(PropertyProperty); }
	//		set { SetValue(PropertyProperty, value); }
	//	}

	//	protected override void OnAttached()
	//	{
	//		base.OnAttached();
	//		if (SourceObject != null)
	//		{
	//			Source = SourceObject;
	//		}
	//		else
	//		{
	//			Source = AssociatedObject;
	//		}
	//	}

	//	protected override void OnDetaching()
	//	{
	//		base.OnDetaching();
	//	}
	//	public override void OnHostUnregistering()
	//	{
	//		base.OnHostUnregistering();
	//	}


	//	private static void onSourceChanged(SimplePropertyTrigger i, DPChangedEventArgs<object> e)
	//	{
	//		if (e.OldValue != null)
	//		{
	//			//unreg
	//		}
	//		if (e.NewValue != null && i.Property != null)
	//		{
	//			i.SetBinding();
	//		}
	//	}
	//	private static void onSourceObjectChanged(SimplePropertyTrigger i, DPChangedEventArgs<object> e)
	//	{
	//		if (e.NewValue != null)
	//		{
	//			i.Source = e.NewValue;
	//		}
	//	}
	//	private static void onPropertyPropertyChanged(SimplePropertyTrigger i, DPChangedEventArgs<DependencyProperty> e)
	//	{
	//		i.SetBinding();
	//		//BindingOperations.SetBinding(i, BoundValueProperty, new Binding(i.Property.Name) { Source = i.Source });
	//	}
	//	private static void onValueChanged(SimplePropertyTrigger i, DPChangedEventArgs<object> e)
	//	{
	//		i.UpdateTriggerState();
	//	}
	//	private static void onBoundValueChanged(SimplePropertyTrigger i, DPChangedEventArgs<object> e)
	//	{
	//		i.UpdateTriggerState();
	//	}

	//	private void UpdateTriggerState()
	//	{
	//		if (Value == BoundValue)
	//		{
	//			Execute();
	//		}
	//	}

	//	static SimplePropertyTrigger()
	//	{

	//	}
	//	public SimplePropertyTrigger()
	//	{

	//	}
	//	private void SetBinding()
	//	{
	//		BindingOperations.SetBinding(this, BoundValueProperty, new Binding(Property.Name) { Source = Source });
	//	}

	//}
}