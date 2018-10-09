using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LockscreenReminder.Services;
using LockscreenReminder.Views;
using Xamarin.Forms;

namespace LockscreenReminder.ViewModels
{
    public class RootPageViewModel
    {
		private ICommand ValidateCommand { get; }

        public RootPageViewModel()
        {
			ValidateCommand = new Command(() => ValidateSticker());
        }

		private void ValidateSticker()
		{
			//DependencyService.Get<IWallpaperService>().ChangeLockWallpaper(new StickerContentView(StickerEntry.Text));
		}
    }
}
