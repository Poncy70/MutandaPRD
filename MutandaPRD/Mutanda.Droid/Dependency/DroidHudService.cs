using AndroidHUD;
using Android.Views;
using Xamarin.Forms;
using Mutanda.Droid.Dependency;
using Mutanda.Services;

[assembly: Dependency(typeof(DroidHudService))]

namespace Mutanda.Droid.Dependency
{
    public class DroidHudService : IHudService
    {
        Android.Views.View _load;

        #region IHudManager implementation

        bool isHudVisible;

        public void ShowHud(string ProgressText = "Loading...")
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndHUD.Shared.Show(Forms.Context, ProgressText, maskType: MaskType.Black);
                isHudVisible = true;

            });
        }

        public void HideHud()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AndHUD.Shared.Dismiss(Forms.Context);
                if (_load != null)
                    _load.Visibility = ViewStates.Gone;
                isHudVisible = false;
            });
        }

        public void SetProgress(double Progress, string ProgressText = "")
        {
            if (!isHudVisible)
                return;
            Device.BeginInvokeOnMainThread(() =>
            {
                int progress = (int)(Progress * 100);
                AndHUD.Shared.Show(Forms.Context, ProgressText + progress + "%", progress, MaskType.Black);
            });
        }
        public void SetText(string Text)
        {
            if (!isHudVisible)
                return;
            Device.BeginInvokeOnMainThread(() =>
            {
                AndHUD.Shared.Show(Forms.Context, Text, maskType: MaskType.Black);
            });
        }

        Android.Views.View CustomLoadingView(string ProgressText)
        {
            Android.Views.View loadingView = new Android.Views.View(Forms.Context);

            return loadingView;
        }

        #endregion
    }
}