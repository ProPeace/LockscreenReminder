using System;
using System.Collections.Generic;
using LockscreenReminder.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LockscreenReminder.Views
{
    public partial class StickerContentView : ContentView
    {
        public StickerContentView(string stickerText)
        {
			BindingContext = new StickerViewModel(stickerText);
            InitializeComponent();
        }
    }
}
