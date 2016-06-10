using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mutanda.CustomControl
{
    public class WrappedItemSelectionTemplate : ViewCell
    {
        public WrappedItemSelectionTemplate() : base()
        {
            

            Label name = new Label();
            name.SetBinding(Label.TextProperty, new Binding("Item"));
            //name.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Entry));


            Label lblDescrizione = new Label();
            lblDescrizione.HorizontalOptions = LayoutOptions.StartAndExpand;
            lblDescrizione.SetBinding(Label.TextProperty, new Binding("Descrizione"));
            lblDescrizione.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Entry));
            //lblDescrizione.LineBreakMode = LineBreakMode.TailTruncation;

            Switch mainSwitch = new Switch();
            mainSwitch.HorizontalOptions = LayoutOptions.End;
            mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));

            StackLayout stackFiltriInvio = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 3),
                Children = { lblDescrizione, mainSwitch }
            };
            View = stackFiltriInvio;
        }
    }
}
