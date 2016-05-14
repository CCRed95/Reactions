namespace Reactions.Core
{
	public interface IReactionHostTarget
	{
		void Execute(HostedAttachableBase source);
	}
}
