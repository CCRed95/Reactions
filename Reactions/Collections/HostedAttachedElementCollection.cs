using System;
using Reactions.Core;

namespace Reactions.Collections
{
	//TODO helper function for creating exceptions to throw passing () => Property expressions? to add to Data property
	public abstract class HostedAttachedElementCollection<T> : AttachedElementCollection<T>, IHostedObject where T : HostedAttachableBase
	{
		public bool IsHosted => HostObject != null;

		public IReactionHostTarget HostObject { get; protected set; }

		IReactionHostTarget IHostedObject.HostObject => HostObject;

		public event EventHandler HostObjectChanged;

		public void RegisterHost(IReactionHostTarget dependencyObject)
		{
			if (dependencyObject == HostObject)
				return;
			if (IsHosted)
				throw new InvalidOperationException("HostedAttachedElementCollection cannot register multiple hosts.")
				{
					Data = {
						["FailedObjectHosting"] = dependencyObject.ToString(),
						["ExistingHostObject"] = HostObject.ToString() 
					}
				};
			HostObject = dependencyObject;
			HostObjectChanged?.Invoke(this, new EventArgs());
			OnHostRegistered();
		}

		public void UnregisterHost()
		{
			OnHostUnregistering();
			HostObject = null;
		}

		public virtual void OnHostRegistered()
		{
			foreach (var item in this)
				item.RegisterHost(HostObject);
		}

		public virtual void OnHostUnregistering()
		{
			foreach (var item in this)
				item.UnregisterHost();
		}

		internal override void ItemAdded(T item)
		{
			if (IsHosted)
				item.RegisterHost(HostObject);
			base.ItemAdded(item);
		}

		internal override void ItemRemoved(T item)
		{
			if (item.IsHosted)
				item.UnregisterHost();
			base.ItemRemoved(item);
		}
	}
}
