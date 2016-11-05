using System;

namespace Reactions.Core
{
	public sealed class NameResolvedEventArgs : EventArgs
	{
		public object OldObject { get; }

		public object NewObject { get; }

		public NameResolvedEventArgs(object oldObject, object newObject)
		{
			OldObject = oldObject;
			NewObject = newObject;
		}
	}
}
