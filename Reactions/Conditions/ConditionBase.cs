using Reactions.Core;

namespace Reactions.Conditions
{
	public abstract class ConditionBase : AttachableBase
	{
		public abstract bool Evaluate(object leftValue);
	}
}
