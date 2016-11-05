using System;
using Reactions.Core;

namespace Reactions.Triggers
{
	public abstract class ReactiveTriggerBase : HostedAttachableBase
	{
		public void Execute()
		{
			if (IsHosted)
			{
				HostObject.Execute(this);
				ExecuteImpl();
			}
			else
			{
				throw new NotSupportedException($"Cannot execute trigger type \'{GetType().Name}\' because it is not hosted.");
			}
		}

		protected virtual void ExecuteImpl()
		{
		}
	}
}
