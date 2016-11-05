using System.Windows.Markup;
using Reactions.Iterative.Targeting.Core;

namespace Reactions.Iterative.Targeting.Selectors
{
	[ContentProperty("NextSelector")]
	public class MethodParameterSelector : ElementSelectorBase
	{
		public object ParameterValue { get; set; }
		
		public MethodParameterSelector()
		{

		}
		protected override object ResolveImpl(object parent, ref SelectorTreeResolutionContext context)
		{
			return ParameterValue;
		}
	}
}


