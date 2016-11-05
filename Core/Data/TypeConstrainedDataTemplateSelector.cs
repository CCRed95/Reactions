using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Core.Data
{
	//[ContentProperty("TemplateMappings")]
	//public class TypeConstrainedDataTemplateSelector : DataTemplateSelector
	//{
	//	public Collection<DataTemplateSelectorMapping> TemplateMappings { get; }

	//	public override DataTemplate SelectTemplate(object item, DependencyObject container)
	//	{
	//		if (item == null)
	//			return null;
	//		var s = TemplateMappings.Where(t => t.Type == item.GetType()).ToArray();
	//		if (s.Any())
	//			return s.First().Template;
	//		return base.SelectTemplate(item, container);
	//	}

	//	public TypeConstrainedDataTemplateSelector()
	//	{
	//		TemplateMappings = new Collection<DataTemplateSelectorMapping>();
	//	}
	//}
}
