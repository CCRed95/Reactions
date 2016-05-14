using Reactions.Core;

namespace Reactions.Collections
{
	public class ReactionCollection : AttachedElementCollection<AttachableReactionBase>, IReaction
	{
		//[TraceAspect]
		public virtual void React()
		{
			foreach (var item in this)
				item.React();
		}
	}
}
