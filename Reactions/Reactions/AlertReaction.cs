using System;
using System.Windows;
using Reactions.Core;

namespace Reactions.Reactions
{
	public class AlertReaction : AttachableReactionBase
	{
		public string Message { get; set; } = "Alert!";
		
		protected override void ReactImpl()
		{
			Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(Message, "Alert Reaction")));
		}
	}
}
