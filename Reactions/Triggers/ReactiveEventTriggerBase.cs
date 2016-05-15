using System;
using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;
using Core.Helpers.DependencyHelpers;
using Reactions.Helpers;
using Reactions.Recursive;

namespace Reactions.Triggers
{
	public abstract class ReactiveEventTriggerBase : ReactiveTriggerBase
	{
		public static readonly DependencyProperty SourceObjectProperty = DP.Register(
			new Meta<ReactiveEventTriggerBase, object>(null, OnSourceObjectChanged));

		public static readonly DependencyProperty SourceNameProperty = DP.Register(
			new Meta<ReactiveEventTriggerBase, string>(null, OnSourceNameChanged));

		private bool isSourceChangedRegistered;
		private NameResolver sourceNameResolver;
		private MethodInfo eventHandlerMethodInfo;

		public object SourceObject
		{
			get { return GetValue(SourceObjectProperty); }
			set { SetValue(SourceObjectProperty, value); }
		}

		public string SourceName
		{
			get { return (string)GetValue(SourceNameProperty); }
			set { SetValue(SourceNameProperty, value); }
		}

		public object Source
		{
			get
			{
				var obj = (object)AssociatedObject;
				if (SourceObject != null)
					obj = SourceObject;
				else if (IsSourceNameSet)
					obj = SourceNameResolver.Object;
				return obj;
			}
		}

		private NameResolver SourceNameResolver
		{
			get
			{
				return sourceNameResolver;
			}
		}

		protected bool IsSourceChangeRegistered
		{
			get
			{
				return isSourceChangedRegistered;
			}
			set
			{
				isSourceChangedRegistered = value;
			}
		}

		protected bool IsSourceNameSet
		{
			get
			{
				if (string.IsNullOrEmpty(SourceName))
					return ReadLocalValue(SourceNameProperty) != DependencyProperty.UnsetValue;
				return true;
			}
		}

		protected bool IsLoadedRegistered { get; set; }

		protected ReactiveEventTriggerBase()
		{
			sourceNameResolver = new NameResolver();
			RegisterSourceChanged();
		}

		////[TraceAspect]
		protected abstract string GetEventName();

		//[TraceAspect]
		protected virtual void OnEvent(EventArgs e)
		{
			Execute();
		}

		//[TraceAspect]
		private void OnSourceChanged(object oldSource, object newSource)
		{
			if (AssociatedObject == null)
				return;
			OnSourceChangedImpl(oldSource, newSource);
		}

		//[TraceAspect]
		internal virtual void OnSourceChangedImpl(object oldSource, object newSource)
		{
			var eventName = GetEventName();
			if (string.IsNullOrEmpty(eventName) || string.Compare(eventName, "Loaded", StringComparison.Ordinal) == 0)
				return;
			if (oldSource != null)
				UnregisterEvent(oldSource, eventName);
		}

		//[TraceAspect]
		protected override void OnAttached()
		{
			base.OnAttached();
			var obj1 = AssociatedObject;
			var dynamicStoryboard = obj1 as ReactiveStoryboard;
			var frameworkElement = obj1 as FrameworkElement;
			if (dynamicStoryboard != null)
			{
				dynamicStoryboard.AssociatedObjectChanged += OnStoryboardHostChanged;
			}
			else
			{
				if (SourceObject != null)
				{
					SourceNameResolver.NameScopeReferenceElement = frameworkElement;
					goto l7;
				}
				try
				{
					OnSourceChanged(null, Source);
				}
				catch (InvalidOperationException ex)
				{
				}
			}
			l7:
			if (string.Compare(GetEventName(), "Loaded", StringComparison.Ordinal) != 0 || frameworkElement == null ||
					React.IsElementLoaded(frameworkElement))
				return;
			RegisterLoaded(frameworkElement);
		}

		//[TraceAspect]
		protected override void OnDetaching()
		{
			base.OnDetaching();
			var dynamicStoryboard = AssociatedObject as ReactiveStoryboard;
			var associatedElement = AssociatedObject as FrameworkElement;
			try
			{
				OnSourceChanged(null, Source);
			}
			catch (InvalidOperationException ex)
			{
			}
			UnregisterSourceChanged();
			if (dynamicStoryboard != null)
			{
				dynamicStoryboard.AssociatedObjectChanged -= OnStoryboardHostChanged;
			}
			SourceNameResolver.NameScopeReferenceElement = null;
			if (string.Compare(GetEventName(), "Loaded", StringComparison.Ordinal) != 0 || associatedElement == null)
				return;
			UnregisterLoaded(associatedElement);
		}

		//[TraceAspect]
		private void OnStoryboardHostChanged(object obj, EventArgs e)
		{
			SourceNameResolver.NameScopeReferenceElement = ((IAttachedObject)obj).AssociatedObject as FrameworkElement;
		}

		//[TraceAspect]
		private static void OnSourceNameChanged(ReactiveEventTriggerBase obj, DPChangedEventArgs<string> e)
		{
			obj.SourceNameResolver.Name = e.NewValue;
		}

		//[TraceAspect]
		private static void OnSourceObjectChanged(ReactiveEventTriggerBase obj, DPChangedEventArgs<object> e)
		{
			var newSource = obj.SourceNameResolver.Object;
			if (e.NewValue == null)
			{
				obj.OnSourceChanged(e.OldValue, newSource);
			}
			else
			{
				if (e.OldValue == null && newSource != null)
					obj.UnregisterEvent(newSource, obj.GetEventName());
				obj.OnSourceChanged(e.OldValue, e.NewValue);
			}
		}

		//[TraceAspect]
		private void RegisterSourceChanged()
		{
			if (IsSourceChangeRegistered)
				return;
			SourceNameResolver.ResolvedElementChanged += OnSourceNameResolverElementChanged;
			IsSourceChangeRegistered = true;
		}

		//[TraceAspect]
		private void UnregisterSourceChanged()
		{
			if (!IsSourceChangeRegistered)
				return;
			SourceNameResolver.ResolvedElementChanged -= OnSourceNameResolverElementChanged;
			IsSourceChangeRegistered = false;
		}

		//[TraceAspect]
		private void OnSourceNameResolverElementChanged(object s, NameResolvedEventArgs e)
		{
			if (SourceObject != null)
				return;
			OnSourceChanged(e.OldObject, e.NewObject);
		}
		
		//[TraceAspect]
    private void RegisterLoaded(FrameworkElement associatedElement)
    {
      if (IsLoadedRegistered || associatedElement == null)
        return;
      associatedElement.Loaded += OnEventImpl;
      IsLoadedRegistered = true;
    }

		//[TraceAspect]
    private void UnregisterLoaded(FrameworkElement associatedElement)
    {
      if (!IsLoadedRegistered || associatedElement == null)
        return;
      associatedElement.Loaded -= OnEventImpl;
      IsLoadedRegistered = false;
    }
		
		//[TraceAspect]
    private void RegisterEvent(object obj, string eventName)
    {
      var @event = obj.GetType().GetEvent(eventName);
      if (@event == null)
      {
        if (SourceObject != null)
					throw new ArgumentException($"EventTriggerCannotFindEventNameExceptionMessage {eventName}, {obj.GetType().Name}");
      }
      else if (!IsValidEvent(@event))
      {
        if (SourceObject != null)
          throw new ArgumentException($"EventTriggerBaseInvalidEventExceptionMessage {eventName}, {obj.GetType().Name}");
      }
      else
      {
        eventHandlerMethodInfo = typeof (ReactiveEventTriggerBase).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
        @event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, eventHandlerMethodInfo));
      }
    }

		//[TraceAspect]
    private static bool IsValidEvent(EventInfo eventInfo)
    {
      var eventHandlerType = eventInfo.EventHandlerType;
      if (!typeof (Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
        return false;
      var parameters = eventHandlerType.GetMethod("Invoke").GetParameters();
      if (parameters.Length == 2 && typeof (object).IsAssignableFrom(parameters[0].ParameterType))
        return typeof (EventArgs).IsAssignableFrom(parameters[1].ParameterType);
      return false;
    }

		//[TraceAspect]
    private void UnregisterEvent(object obj, string eventName)
    {
      if (string.Compare(eventName, "Loaded", StringComparison.Ordinal) == 0)
      {
        var associatedElement = obj as FrameworkElement;
        if (associatedElement == null)
          return;
        UnregisterLoaded(associatedElement);
      }
      else
        UnregisterEventImpl(obj, eventName);
    }

		//[TraceAspect]
    private void UnregisterEventImpl(object obj, string eventName)
    {
      var type = obj.GetType();
      if (eventHandlerMethodInfo == null)
        return;
      var @event = type.GetEvent(eventName);
      @event.RemoveEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, eventHandlerMethodInfo));
      eventHandlerMethodInfo = null;
    }

		//[TraceAspect]
    private void OnEventImpl(object s, EventArgs e)
    {
      OnEvent(e);
    }

		//[TraceAspect]
    internal void OnEventNameChanged(string oldEventName, string newEventName)
    {
      if (AssociatedObject == null)
        return;
      var associatedElement = Source as FrameworkElement;
      if (associatedElement != null && string.Compare(oldEventName, "Loaded", StringComparison.Ordinal) == 0)
        UnregisterLoaded(associatedElement);
      else if (!string.IsNullOrEmpty(oldEventName))
        UnregisterEvent(Source, oldEventName);
      if (associatedElement != null && string.Compare(newEventName, "Loaded", StringComparison.Ordinal) == 0)
      {
        RegisterLoaded(associatedElement);
      }
      else
      {
        if (string.IsNullOrEmpty(newEventName))
          return;
        RegisterEvent(Source, newEventName);
      }
    }
	}
}
