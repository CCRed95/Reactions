using Reactions.Core;

namespace Reactions.Reactions
{
	public class VisualStateSetter : AttachableReactionBase
	{
		public string State { get; set; }

		public bool UseTransitions { get; set; }

		
		protected override void ReactImpl()
		{

		}
	}
}
