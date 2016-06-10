using Xamarin.Forms;
using Mutanda.ViewModels;
using Mutanda.Services;

namespace Mutanda.Pages.Ordini.NuovoOrdine
{
    public class NuovoOrdine_Note : ContentPage
    {
        public NuovoOrdine_Note()
        {
            FormattedString fsNote = new FormattedString();
            fsNote.Spans.Add(new Span { Text = "Note Ordine", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });
            Label lblNote = new Label()
            {
                FormattedText = fsNote,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start
            };

            ExpandableEditor txtNote = new ExpandableEditor();
            txtNote.SetBinding<NuovoOrdineViewModel>(Editor.TextProperty, x => x.DatiOrdine_Note);
            txtNote.VerticalOptions = LayoutOptions.FillAndExpand;
            //txtNote.HeightRequest = 350;

            var stackLayout = new StackLayout()
            {
                Padding = new Thickness(10),
                Orientation = StackOrientation.Vertical,
                Children = { lblNote, txtNote }
            };

            Content = stackLayout;
        }
    }
}
