using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LockscreenReminder.Models;
using LockscreenReminder.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LockscreenReminder.Views
{
    public partial class RootPage : ContentPage
    {
        public RootPage()
        {
            InitializeComponent();
        }

		async void OnButtonClicked(object sender, EventArgs args)
		{
			ActivityTemplate.ActivityText = "Création du sticker";
			ActivityTemplate.IsVisible = true;
			ActivityTemplate.IsActivate = true;
			var sticker = new Sticker()
			{
				Text = StickerEditor.Text
			};

			var stickerView = new StickerContentView(sticker.Text);
			try
			{
				await DependencyService.Get<IWallpaperService>().ChangeLockWallpaper(stickerView);
				await Application.Current.MainPage.DisplayAlert("Terminé", "Sticker créé", "OK");
			}
			catch (Exception e)
			{
				await Application.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
			}
			finally
			{
				ActivityTemplate.IsActivate = false;
			}
		}
    }
}
