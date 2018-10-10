using System;
using LockscreenReminder.Events;
using Xamarin.Forms;

namespace LockscreenReminder.Effects
{
	public class TouchEffect : RoutingEffect
    {
		public event TouchActionEventHandler TouchAction;

		public TouchEffect() : base("LockscreenReminder.Effects.TouchEffect")
		{
		}

		public bool Capture { set; get; }

		public void OnTouchAction(Element element, TouchActionEventArgs args)
		{
			TouchAction?.Invoke(element, args);
		}
    }
}
