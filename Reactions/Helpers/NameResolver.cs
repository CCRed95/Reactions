﻿using System;
using System.Windows;
//using Reactions.Recursive;

namespace Reactions.Helpers
{
	//public class NameResolver
	//{
	//	private string name;
 //   private FrameworkElement nameScopeReferenceElement;

 //   public string Name
 //   {
 //     get
 //     {
 //       return name;
 //     }
 //     set
 //     {
 //       var @object = Object;
	//			name = value;
	//			UpdateObjectFromName(@object);
 //     }
 //   }

 //   public DependencyObject Object
 //   {
 //     get
 //     {
 //       if (string.IsNullOrEmpty(Name) && HasAttempedResolve)
 //         return NameScopeReferenceElement;
 //       return ResolvedObject;
 //     }
 //   }

 //   public FrameworkElement NameScopeReferenceElement
 //   {
 //     get
 //     {
 //       return nameScopeReferenceElement;
 //     }
 //     set
 //     {
 //       var referenceElement = NameScopeReferenceElement;
	//			nameScopeReferenceElement = value;
	//			OnNameScopeReferenceElementChanged(referenceElement);
 //     }
 //   }

 //   private FrameworkElement ActualNameScopeReferenceElement
 //   {
 //     get
 //     {
 //       if (NameScopeReferenceElement == null || !React.IsElementLoaded(NameScopeReferenceElement))
 //         return null;
 //       return GetActualNameScopeReference(NameScopeReferenceElement);
 //     }
 //   }

 //   private DependencyObject ResolvedObject { get; set; }

 //   private bool PendingReferenceElementLoad { get; set; }

 //   private bool HasAttempedResolve { get; set; }


 //   public event EventHandler<NameResolvedEventArgs> ResolvedElementChanged;

	//	//[TraceAspect]
 //   private void OnNameScopeReferenceElementChanged(FrameworkElement oldNameScopeReference)
 //   {
 //     if (PendingReferenceElementLoad)
 //     {
 //       oldNameScopeReference.Loaded -= OnNameScopeReferenceLoaded;
	//			PendingReferenceElementLoad = false;
 //     }
	//		HasAttempedResolve = false;
	//		UpdateObjectFromName(Object);
 //   }

	//	//[TraceAspect]
 //   private void UpdateObjectFromName(DependencyObject oldObject)
 //   {
 //     var dependencyObject = (DependencyObject) null;
	//		ResolvedObject = null;
 //     if (NameScopeReferenceElement != null)
 //     {
 //       if (!React.IsElementLoaded(NameScopeReferenceElement))
 //       {
	//				NameScopeReferenceElement.Loaded += OnNameScopeReferenceLoaded;
	//				PendingReferenceElementLoad = true;
 //         return;
 //       }
 //       if (!string.IsNullOrEmpty(Name))
 //       {
 //         var referenceElement = ActualNameScopeReferenceElement;
 //         if (referenceElement != null)
 //           dependencyObject = referenceElement.FindName(Name) as DependencyObject;
 //       }
 //     }
	//		HasAttempedResolve = true;
	//		ResolvedObject = dependencyObject;
 //     if (oldObject == Object)
 //       return;
	//		OnObjectChanged(oldObject, Object);
 //   }

	//	//[TraceAspect]
 //   private void OnObjectChanged(DependencyObject oldTarget, DependencyObject newTarget)
 //   {
	//    ResolvedElementChanged?.Invoke(this, new NameResolvedEventArgs(oldTarget, newTarget));
 //   }

	//	//[TraceAspect]
 //   private FrameworkElement GetActualNameScopeReference(FrameworkElement initialReferenceElement)
 //   {
 //     var frameworkElement = initialReferenceElement;
 //     if (IsNameScope(initialReferenceElement))
 //       frameworkElement = initialReferenceElement.Parent as FrameworkElement ?? frameworkElement;
 //     return frameworkElement;
 //   }

	//	//[TraceAspect]
 //   private bool IsNameScope(FrameworkElement frameworkElement)
 //   {
 //     var frameworkElement1 = frameworkElement.Parent as FrameworkElement;
	//    return frameworkElement1?.FindName(Name) != null;
 //   }

	//	//[TraceAspect]
 //   private void OnNameScopeReferenceLoaded(object sender, RoutedEventArgs e)
 //   {
	//		PendingReferenceElementLoad = false;
	//		NameScopeReferenceElement.Loaded -= OnNameScopeReferenceLoaded;
	//		UpdateObjectFromName(Object);
 //   }
	//}
}
