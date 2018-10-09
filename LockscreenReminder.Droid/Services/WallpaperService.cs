using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using LockscreenReminder.Droid.Services;
using LockscreenReminder.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(WallpaperService))]
namespace LockscreenReminder.Droid.Services
{
	public class WallpaperService : IWallpaperService
    {
		public Android.Views.ViewGroup AndroidView { get; set; }
		private WallpaperManager wallpaperManager;
		private DisplayMetrics metrics = Android.App.Application.Context.Resources.DisplayMetrics;
		private int width;
		private int height;

		public WallpaperService()
		{
			wallpaperManager = WallpaperManager.GetInstance(Android.App.Application.Context);
			width = metrics.WidthPixels;
			height = metrics.HeightPixels;
		}

		public async Task ChangeLockWallpaper(Xamarin.Forms.View view)
		{
			AndroidView = ConvertFormsToNative(view, new Rectangle(0, 0, width/6, height/10));
			var bitmap = ConvertViewToBitMap(AndroidView);

			var info = wallpaperManager.WallpaperInfo;
			var wallpaper = wallpaperManager.GetWallpaperFile(WallpaperManagerFlags.Lock);
			var fileDescriptor = wallpaper.FileDescriptor;
			var bitmapWallpaper = BitmapFactory.DecodeFileDescriptor(fileDescriptor);
			wallpaper.Close();

			Bitmap bmOverlay = Bitmap.CreateBitmap(bitmapWallpaper.Width, bitmapWallpaper.Height, bitmapWallpaper.GetConfig());
			Canvas canvas = new Canvas(bmOverlay);
			canvas.DrawBitmap(bitmapWallpaper, new Matrix(), null);
			canvas.DrawBitmap(bitmap, 15, 15, null); // Changer la position du post-it

			await Task.Run(() =>
			{
				wallpaperManager.SetBitmap(bmOverlay, null, true, WallpaperManagerFlags.Lock);
			});
		}

		private Bitmap ConvertViewToBitMap(Android.Views.ViewGroup view)
		{
			Bitmap bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
			Canvas canvas = new Canvas(bitmap);
			view.Draw(canvas);
			return bitmap;
		}

		public static ViewGroup ConvertFormsToNative(Xamarin.Forms.View view, Rectangle size)
		{
			var vRenderer = Platform.CreateRenderer(view);
			var viewGroup = vRenderer.ViewGroup;
			vRenderer.Tracker.UpdateLayout();
			var layoutParams = new ViewGroup.LayoutParams((int)size.Width, (int)size.Height);
			viewGroup.LayoutParameters = layoutParams;
			view.Layout(size);
			viewGroup.Layout(0, 0, (int)view.WidthRequest, (int)view.HeightRequest);
			return viewGroup;
		}
    }
}
