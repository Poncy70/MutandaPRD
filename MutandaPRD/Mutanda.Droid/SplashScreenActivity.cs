using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Xamarin.Forms.Platform.Android;

namespace Mutanda.Droid
{
    [Activity(Theme = "@style/Theme.Splash", Label = "OrderEntry", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Landscape, WindowSoftInputMode = SoftInput.AdjustPan)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set Fullscreen
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            this.SetContentView(new SplashLayout(this));

            new Handler().PostDelayed(new Java.Lang.Runnable(() =>
            {
                StartActivity(typeof(MainActivity));
				OverridePendingTransition(0, 0);
			}), 100);
        }

        private class SplashLayout : LinearLayout
        {
            public SplashLayout(Context context)  : base(context)
            {
                Inflate(Context, Resource.Layout.splash_screen, this);
            }
        }
    }
}