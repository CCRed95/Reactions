namespace Reactions.Triggers
{
	public abstract class DynamicEventTriggerBase<T> : ReactiveEventTriggerBase where T : class
	{
		public T Source
		{
			get
			{
				return (T) base.Source;
			}
		}

		//protected DynamicEventTriggerBase() : base(typeof(T))
		//{
		//}
		
    internal sealed override void OnSourceChangedImpl(object oldSource, object newSource)
    {
      base.OnSourceChangedImpl(oldSource, newSource);
      OnSourceChanged(oldSource as T, newSource as T);
    }

    protected virtual void OnSourceChanged(T oldSource, T newSource)
    {
    }
	}
}
