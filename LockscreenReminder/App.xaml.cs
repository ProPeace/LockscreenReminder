using System;
using System.Collections.Generic;
using LockscreenReminder.Helpers;
using LockscreenReminder.Services.Database;
using LockscreenReminder.Views;
using Xamarin.Forms;

namespace LockscreenReminder
{
	public partial class App : Application
    {
		static App()
		{
			Locator.Instance.Build();
		}

        public App()
        {
            InitializeComponent();
			CreateDataBase();
			MainPage = new RootPage();
        }

		void CreateDataBase()
		{
			var localDbService = Locator.Instance.Resolve<ILocalDbService>();
		}
    }
}
