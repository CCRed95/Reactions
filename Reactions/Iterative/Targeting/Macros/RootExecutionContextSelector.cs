using System.Windows.Markup;
using Reactions.Iterative.Targeting.Core;

namespace Reactions.Iterative.Targeting.Macros
{
	[ContentProperty("NextSelector")]
	public class RootExecutionContextSelector : ElementSelectorBase
	{
		protected override object ResolveImpl(object parent, ref SelectorTreeResolutionContext context)
		{
			return context.RootExecutionContext;
		}
	}
}
