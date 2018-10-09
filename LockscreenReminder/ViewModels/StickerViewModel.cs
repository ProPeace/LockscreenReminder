using System;
namespace LockscreenReminder.ViewModels
{
	public class StickerViewModel : BaseViewModel
    {
		private string _stickerText;

		public string StickerText
		{
			get { return _stickerText; }
			set { SetProperty(ref _stickerText, value); }
		}

        public StickerViewModel(string stickerText)
        {
			StickerText = stickerText;
        }
    }
}
