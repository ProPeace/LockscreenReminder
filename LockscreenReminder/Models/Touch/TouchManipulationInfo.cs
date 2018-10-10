using System;
using SkiaSharp;

namespace LockscreenReminder.Models.Touch
{
    public class TouchManipulationInfo
    {
		public SKPoint PreviousPoint { set; get; }

		public SKPoint NewPoint { set; get; }
    }
}
