using Reactions.Core;

namespace Reactions.Triggers
{
	//TODO tracking/assert/handle trigger execution failures for hosting target issues, etc
	public abstract class ReactiveTriggerBase : HostedAttachableBase
	{
		//[TraceAspect]
		internal virtual void Execute()
		{
			if (IsHosted)
				HostObject.Execute(this);
			else
			{
				
			}
		}
	}
}
