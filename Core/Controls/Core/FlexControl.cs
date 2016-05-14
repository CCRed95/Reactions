using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Core.Controls.Core
{
	public class FlexControl : Control
	{
		#region Fields
		protected const FrameworkPropertyMetadataOptions FXR = FrameworkPropertyMetadataOptions.AffectsRender;
		protected const FrameworkPropertyMetadataOptions FXM = FrameworkPropertyMetadataOptions.AffectsMeasure;
		protected const FrameworkPropertyMetadataOptions FXA = FrameworkPropertyMetadataOptions.AffectsArrange;
		protected const FrameworkPropertyMetadataOptions INH = FrameworkPropertyMetadataOptions.Inherits;
		#endregion

		protected T GetTemplateChild<T>(string name) where T : DependencyObject
		{
			var templateChild = GetTemplateChild(name) as T;
			if (templateChild == null)
				throw new NullReferenceException($"TemplateChild {name} as {typeof(T)}");
			return templateChild;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			var type = GetType();
			var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (var field in fields)
			{
				var d = field.Name;
				var fieldType = field.FieldType;
				if (field.GetCustomAttribute<AutoTemplatePartAttribute>(true) != null)
				{
					var templatePart = GetTemplateChild(d);
					var convertedPart = Convert.ChangeType(templatePart, fieldType);
					
					field.SetValue(this, convertedPart);
				}
			}
		}

		protected static void OverrideDefaultStyleKey<T>()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(typeof(T)));
		}
	}
}
