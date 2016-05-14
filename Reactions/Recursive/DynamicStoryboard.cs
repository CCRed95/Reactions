using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Markup;
using Reactions.Collections;
using Reactions.Core;
using Reactions.Triggers;

namespace Reactions.Recursive
{
	//TODO is reinheriting IAttachedObject necessary if already inheriting attachablebase?
	[ContentProperty("Reactions")]
	[RuntimeNameProperty("Name")]
	public class DynamicStoryboard : AttachableBase, IAttachedObject, IReactionHostTarget
	{
		private static readonly DependencyPropertyKey DynamicTriggersPropertyKey = DependencyProperty.RegisterReadOnly("DynamicTriggers",
			typeof(DynamicTriggerCollection), typeof(DynamicStoryboard), new FrameworkPropertyMetadata());
		public static readonly DependencyProperty DynamicTriggersProperty = DynamicTriggersPropertyKey.DependencyProperty;

		private static readonly DependencyPropertyKey ReactionsPropertyKey = DependencyProperty.RegisterReadOnly("Reactions",
			typeof(ReactionCollection), typeof(DynamicStoryboard), new FrameworkPropertyMetadata());
		public static readonly DependencyProperty ReactionsProperty = ReactionsPropertyKey.DependencyProperty;

		public DynamicTriggerCollection DynamicTriggers => (DynamicTriggerCollection)GetValue(DynamicTriggersProperty);

		public ReactionCollection Reactions => (ReactionCollection)GetValue(ReactionsProperty);

		public string Name { get; set; }

		public DynamicStoryboard()
		{
			SetValue(DynamicTriggersPropertyKey, new DynamicTriggerCollection());
			SetValue(ReactionsPropertyKey, new ReactionCollection());
		}

		//[TraceAspect]
		protected override void OnAttached()
		{
			base.OnAttached();
			DynamicTriggers.RegisterHost(this);
			DynamicTriggers.Attach(AssociatedObject);
			Reactions.Attach(AssociatedObject);
		}

		//[TraceAspect]
		protected override void OnDetaching()
		{
			base.OnDetaching();
			DynamicTriggers.UnregisterHost();
			DynamicTriggers.Detach();
			Reactions.Detach();
		}

		//[TraceAspect]
		void IReactionHostTarget.Execute(HostedAttachableBase source)
		{
			var sourceTrigger = source as DynamicTriggerBase;
			if (sourceTrigger != null)
				OnDynamicTriggerFired(sourceTrigger);
		}

		//[TraceAspect]
		protected virtual void OnDynamicTriggerFired(DynamicTriggerBase sourceTrigger)
		{
			Reactions.React();
		}
	}
}