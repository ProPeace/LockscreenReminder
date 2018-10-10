using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LockscreenReminder.Events;
using LockscreenReminder.Models.Touch;
using LockscreenReminder.Transforms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace LockscreenReminder.Views
{
    public partial class StickerPositioningPage : ContentPage
    {
		SKBitmap bitmap;
		SKMatrix matrix = SKMatrix.MakeIdentity();
		// Touch information
		long touchId = -1;
		SKPoint previousPoint;
		Dictionary<long, SKPoint> touchDictionary = new Dictionary<long, SKPoint>();

        public StickerPositioningPage()
        {
            InitializeComponent();

			string resourceID = "LockscreenReminder.Media.icon_sticky_yellow.png";
			Assembly assembly = GetType().GetTypeInfo().Assembly;

			using (Stream stream = assembly.GetManifestResourceStream(resourceID))
			{
				bitmap = SKBitmap.Decode(stream);
			}
        }

		void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
		{
			SKImageInfo info = args.Info;
			SKSurface surface = args.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			// Display the bitmap
			canvas.SetMatrix(matrix);
			canvas.DrawBitmap(bitmap, new SKPoint());
		}

		void OnTouchEffectAction(object sender, TouchActionEventArgs args)
		{
			// Convert Xamarin.Forms point to pixels
			Point pt = args.Location;
			SKPoint point =
				new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
							(float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));

			switch (args.Type)
			{
				case TouchActionType.Pressed:
					// Find transformed bitmap rectangle
					SKRect rect = new SKRect(0, 0, bitmap.Width, bitmap.Height);
					rect = matrix.MapRect(rect);

					// Determine if the touch was within that rectangle
					if (rect.Contains(point))
					{
						touchId = args.Id;
						previousPoint = point;
					}
					break;

				case TouchActionType.Moved:
					if (touchId == args.Id)
					{
						if (touchDictionary.Count == 1)
						{
							// Adjust the matrix for the new position
							matrix.TransX += point.X - previousPoint.X;
							matrix.TransY += point.Y - previousPoint.Y;
							previousPoint = point;
							canvasView.InvalidateSurface();
						}
						// Double-finger scale and drag
						else if (touchDictionary.Count >= 2)
						{
							// Copy two dictionary keys into array
							long[] keys = new long[touchDictionary.Count];
							touchDictionary.Keys.CopyTo(keys, 0);

							// Find index of non-moving (pivot) finger
							int pivotIndex = (keys[0] == args.Id) ? 1 : 0;

							// Get the three points involved in the transform
							SKPoint pivotPoint = touchDictionary[keys[pivotIndex]];
							SKPoint prevPoint = touchDictionary[args.Id];
							SKPoint newPoint = point;

							// Calculate two vectors
							SKPoint oldVector = prevPoint - pivotPoint;
							SKPoint newVector = newPoint - pivotPoint;
							if ((newVector.X > 100 || newVector.X < -100) && (newVector.Y < -100 || newVector.Y > 100))
							{

								// Scaling factors are ratios of those
								float scaleX = newVector.X / oldVector.X;
								float scaleY = newVector.Y / oldVector.Y;

								if (!float.IsNaN(scaleX) && !float.IsInfinity(scaleX) && !float.IsNaN(scaleY) && !float.IsInfinity(scaleY))
								{
									// If smething bad hasn't happened, calculate a scale and translation matrix
									SKMatrix scaleMatrix =
										SKMatrix.MakeScale(scaleX, scaleY, pivotPoint.X, pivotPoint.Y);

									SKMatrix.PostConcat(ref matrix, scaleMatrix);
									canvasView.InvalidateSurface();
								}
							}
						}

						// Store the new point in the dictionary
						touchDictionary[args.Id] = point;
					}
					break;

				case TouchActionType.Released:
				case TouchActionType.Cancelled:
					if (touchDictionary.ContainsKey(args.Id))
					{
						touchDictionary.Remove(args.Id);
					}
					touchId = -1;
					break;
			}
		}
    }
}
