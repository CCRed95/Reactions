namespace Reactions.Core
{
	public abstract class AttachableReactionBase : AttachableBase, IReaction
	{
		public abstract void React();

		void IReaction.React() => React();
	}
}
