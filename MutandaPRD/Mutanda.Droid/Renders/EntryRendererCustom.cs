using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Mutanda.CustomControl;
using Mutanda.Droid.Renders;

[assembly: ExportRenderer(typeof(EntryCustom), typeof(EntryRendererCustom))]
namespace Mutanda.Droid.Renders
{
    public class EntryRendererCustom : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
            }
        }
    }
}