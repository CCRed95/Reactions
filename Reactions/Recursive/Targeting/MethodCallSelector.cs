using System;
using System.Windows.Markup;

namespace Reactions.Recursive.Targeting
{
	[ContentProperty("NextSelector")]
	public class MethodCallSelector : ElementSelectorBase
	{
		public string MethodName { get; set; }

		public MethodParameterList Parameters { get; set; }

		public MethodCallSelector()
		{
			Parameters = new MethodParameterList();
		}
		internal override object Resolve(object root, object param)
		{
			throw new NotImplementedException();
		}
	}
}
