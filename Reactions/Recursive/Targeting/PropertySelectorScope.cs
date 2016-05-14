using System;
using System.Windows.Markup;

namespace Reactions.Recursive.Targeting
{
	[ContentProperty("NextSelector")]
	public class PropertySelectorScope : ElementSelectorBase
	{
		public string Property { get; set; }

		internal override object Resolve(object root, object param)
		{
			throw new NotImplementedException();
		}
	}
}
