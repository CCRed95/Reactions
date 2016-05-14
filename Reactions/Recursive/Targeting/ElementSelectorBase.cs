using System.Windows.Markup;

namespace Reactions.Recursive.Targeting
{
	[ContentProperty("NextSelector")]
	public abstract class ElementSelectorBase
	{
		public ElementSelectorBase NextSelector { get; set; }
		
		internal abstract object Resolve(object root, object param);
	}
}
