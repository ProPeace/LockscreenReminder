using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LockscreenReminder.Services
{
    public interface IWallpaperService
    {
		Task ChangeLockWallpaper(View view);
    }
}
