using Mutanda.Models;
using Plugin.DeviceInfo;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mutanda.Pages
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            var spalshImage = new Image { Aspect = Aspect.AspectFit };
            spalshImage.Source = ImageSource.FromFile("splash.png");

            StackLayout sl = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                    new ContentView() { HorizontalOptions=LayoutOptions.EndAndExpand,VerticalOptions=LayoutOptions.EndAndExpand, Margin = new Thickness(180), Content = spalshImage },
                    new Label() { HorizontalOptions=LayoutOptions.EndAndExpand, Text = CrossDeviceInfo.Current.Id, FontSize =  Device.GetNamedSize(NamedSize.Micro, typeof(Label)) }
                }
            };


            this.Content = sl;

        }
    }
}
