﻿using System;
using System.Runtime.CompilerServices;
using System.Windows;
using Core.Exceptions;

namespace Core.Helpers.EventHelpers
{
	public static class EM
	{
		public const RoutingStrategy BUBBLE = RoutingStrategy.Bubble;
		public const RoutingStrategy DIRECT = RoutingStrategy.Direct;
		public const RoutingStrategy TUNNEL = RoutingStrategy.Tunnel;

		public static RoutedEvent Register<D, H>(RoutingStrategy rs, [CallerMemberName] string afn = null)
			where H : class where D : UIElement
		{
			if (!typeof(H).IsSubclassOf(typeof(Delegate)))
				throw new InvalidOperationException($"{typeof(H).Name} is not a delegate type.");
			return EventManager.RegisterRoutedEvent(GetEventName(afn), rs, typeof(H), typeof(D));
		}

		internal static string GetEventName(string autoFieldName)
		{
			if (autoFieldName == null)
				throw new Exception(FSR.DP.AutoCallerNameNotAssigned());
			if (!autoFieldName.EndsWith("Event"))
				throw new Exception(FSR.DP.AutoCallerNameNotValid(autoFieldName));
			return autoFieldName.Replace("Event", string.Empty);
		}
	}
}
