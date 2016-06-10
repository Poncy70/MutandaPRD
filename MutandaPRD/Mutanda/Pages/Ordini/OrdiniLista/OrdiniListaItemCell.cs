using Xamarin.Forms;
using Mutanda.Converters;
using Mutanda.Models;

namespace Mutanda.Pages
{
    public class OrdiniListaItemCell : ViewCell
    {
        
        public OrdiniListaItemCell()
        {
            Label lblData = new Label();
            lblData.SetBinding(Label.TextProperty, new Binding("DataDocumento", stringFormat: "{0:dd/MM/yyyy}"));
            lblData.HorizontalOptions = LayoutOptions.Start;
            
            Label RagioneSociale = new Label() { FontAttributes = FontAttributes.Bold };
            //RagioneSociale.SetBinding(Label.TextProperty, "RagioneSociale");
			RagioneSociale.SetBinding<GEST_Ordini_Teste>(Label.TextProperty, x=>x.RagioneSociale);
            RagioneSociale.LineBreakMode = LineBreakMode.TailTruncation;
            
            Label lblNumeroOrdine = new Label();
            lblNumeroOrdine.SetBinding(Label.TextProperty, "NumeroOrdineDevice", BindingMode.OneWay, new NumeroOrdineConverter());
            lblNumeroOrdine.HorizontalTextAlignment = TextAlignment.Start;

            Label lblTotaleConsegna = new Label();
            lblTotaleConsegna.SetBinding(Label.TextProperty, new Binding("TotaleConsegna", stringFormat: "{0:C}"));
            
            Label lblInviato = new Label();
            lblInviato.SetBinding<GEST_Ordini_Teste>(Label.TextProperty,  x=>x.CloudState, BindingMode.OneWay, new CloutStateToString());
            

            Grid gridLayout = new Grid();
            gridLayout.ColumnSpacing = 12;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Center;

            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblData
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });      //RagioneSociale
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblNumeroOrdine
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblTotaleConsegna
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblInviato
            
            gridLayout.Children.Add(lblData, 0, 0);
            gridLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.StartAndExpand,  Content = RagioneSociale }, 1, 0);
            gridLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.FillAndExpand, Content = lblNumeroOrdine }, 2, 0);
            gridLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.EndAndExpand, Content = lblTotaleConsegna }, 3, 0);
            gridLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.End, Content = lblInviato}, 4, 0);
            
            View = gridLayout;

        }
    }
}
