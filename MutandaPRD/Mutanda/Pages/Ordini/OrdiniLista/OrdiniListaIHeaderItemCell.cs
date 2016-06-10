using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mutanda.Pages.Ordini.OrdiniLista
{
    public class OrdiniListaIHeaderItemCell : ViewCell
    {
        public OrdiniListaIHeaderItemCell()
        {
            var view = new StackLayout()
            {
                Spacing = 5,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(5, 0),
                Children =
                {
                    new Label() { Text="Data" },
                    new Label() { Text="Ragione Sociale" },
                    new StackLayout()
                    {
                        Spacing = 5,
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Padding = new Thickness(0, 0, 10, 0),
                        Children =
                        {
                            new Label() { Text="N° Ordine:"},
                        }
                    },

                    new ContentView()
                    {
                        HorizontalOptions = LayoutOptions.End,
                        Content = new Label() { Text="Stato"}
                    }

                }
            };

            View = view;

        }
    }
}