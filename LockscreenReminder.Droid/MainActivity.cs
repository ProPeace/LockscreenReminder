using Android.App;
using Android.Widget;
using Android.OS;

namespace LockscreenReminder.Droid
{
	[Activity(Label = "LockscreenReminder.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new App());
        }
    }
}

