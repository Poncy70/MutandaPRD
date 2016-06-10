using Xamarin.Forms;
using Mutanda.Services;
using Mutanda.ViewModels;
using Mutanda.Converters;
using Mutanda.Models;

namespace Mutanda.Pages
{
    public class NuovoOrdine_DatiOrdine: ContentPage
	{
		public NuovoOrdine_DatiOrdine ()
		{
            Label lblClienteOrdine = new Label();
            lblClienteOrdine.SetBinding<NuovoOrdineViewModel>(Label.TextProperty, x => x.DatiOrdine_DescrizioneClienteSelezionato, BindingMode.OneWay);
            lblClienteOrdine.HorizontalOptions = LayoutOptions.Start;
            lblClienteOrdine.VerticalOptions = LayoutOptions.Center;
            lblClienteOrdine.HorizontalTextAlignment = TextAlignment.Start;
            lblClienteOrdine.VerticalTextAlignment = TextAlignment.Center;
            lblClienteOrdine.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            lblClienteOrdine.TextColor = Color.FromHex("f99643");
            lblClienteOrdine.FontAttributes = FontAttributes.Bold;

            ListView listViewArticoliInOrdineRagguppataPerClasse = new ListView
            {
                Header= this,
                HeaderTemplate = new DataTemplate(() =>
                {
                    Grid gridHeaderLayout = new Grid();
                    gridHeaderLayout.ColumnSpacing = 3;
                    gridHeaderLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                    gridHeaderLayout.VerticalOptions = LayoutOptions.Fill;
                    gridHeaderLayout.Padding = new Thickness(5, 0);

                    gridHeaderLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //lblCodArt
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });    //lblDescrizione
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //txtQtaDaOrdinare
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //txtQtaDaOrdinareScontoMerce
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //lblSc1
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //lblSc2
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //lblValUnit
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //lblTotaleRiga
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });    //lblDataConsegna

                    //COLONNA 0
                    gridHeaderLayout.Children.Add(new Label() { Text = "Cod Art", HorizontalTextAlignment = TextAlignment.Start, HorizontalOptions = LayoutOptions.FillAndExpand }, 0, 0);
                    //COLONNA 1
                    gridHeaderLayout.Children.Add(new Label() { Text = "Descrizione", HorizontalTextAlignment = TextAlignment.Start, HorizontalOptions = LayoutOptions.FillAndExpand }, 1, 0);
                    //COLONNA 2
                    gridHeaderLayout.Children.Add(new Label() { Text = "Qta", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 2, 0);
                    //COLONNA 3
                    gridHeaderLayout.Children.Add(new Label() { Text = "Sc. Merce", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 3, 0);

                    gridHeaderLayout.Children.Add(new Label() { Text = "Sconto 1", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 4, 0);

                    gridHeaderLayout.Children.Add(new Label() { Text = "Sconto 2", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 5, 0);
                    
                    gridHeaderLayout.Children.Add(new Label() { Text = "Prezzo", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.FillAndExpand }, 6, 0);
                    
                    gridHeaderLayout.Children.Add(new Label() { Text = "Totale", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.FillAndExpand }, 7, 0);

					gridHeaderLayout.Children.Add(new Label() { Text = "Data", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 8, 0);

                    return gridHeaderLayout;
                }),
                ItemTemplate = new DataTemplate(() => new NuovoOrdine_DatiOrdineItemCell((NuovoOrdineViewModel)this.BindingContext))
            };

            listViewArticoliInOrdineRagguppataPerClasse.SetBinding<NuovoOrdineViewModel>(ListView.ItemsSourceProperty, x => x.ListaArticoliInOrdineRaggruppatoPerClasse,BindingMode.OneWay);
            listViewArticoliInOrdineRagguppataPerClasse.IsGroupingEnabled = true;
            listViewArticoliInOrdineRagguppataPerClasse.GroupDisplayBinding = new Binding("Key");
            listViewArticoliInOrdineRagguppataPerClasse.GroupHeaderTemplate = new DataTemplate(typeof(HeaderCell));
            listViewArticoliInOrdineRagguppataPerClasse.HasUnevenRows = true;
            listViewArticoliInOrdineRagguppataPerClasse.SeparatorColor = Color.FromHex("f99643");
            listViewArticoliInOrdineRagguppataPerClasse.HorizontalOptions = LayoutOptions.FillAndExpand;
            listViewArticoliInOrdineRagguppataPerClasse.VerticalOptions = LayoutOptions.StartAndExpand;
            #region Button
            Button btnCancellaOrdine = new Button()
            {
                Text = "Cancella",
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.End
            };
            btnCancellaOrdine.SetBinding<NuovoOrdineViewModel>(Button.IsVisibleProperty, x => x.IsInEdit);
            btnCancellaOrdine.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.CancellaOrdineInCorsoCommand, BindingMode.OneWay);

            Button btnSalvaOrdine = new Button()
            {
                Text = "Salva",
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            btnSalvaOrdine.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.SalvaOrdineInCorsoCommand, BindingMode.OneWay);
            btnSalvaOrdine.SetBinding<NuovoOrdineViewModel>(Button.IsVisibleProperty, x => x.IsSoloLettura, BindingMode.OneWay, converter: new CellNegationConverter()) ;
            #endregion

            Label lblTotaleOrdineDescr = new Label();
            lblTotaleOrdineDescr.Text = "Totale Ordine";
            lblTotaleOrdineDescr.HorizontalOptions = LayoutOptions.End;
            lblTotaleOrdineDescr.VerticalOptions = LayoutOptions.Center;
            lblTotaleOrdineDescr.HorizontalTextAlignment = TextAlignment.End;
            lblTotaleOrdineDescr.VerticalTextAlignment = TextAlignment.Center;
            lblTotaleOrdineDescr.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            lblTotaleOrdineDescr.FontAttributes = FontAttributes.Bold;
            lblTotaleOrdineDescr.TextColor = Color.FromHex("f99643");

            Label lblTotaleOrdine = new Label();
            lblTotaleOrdine.SetBinding<NuovoOrdineViewModel>(Label.TextProperty, x => x.DatiOrdine_TotaleOrdine, BindingMode.OneWay, null, "{0:C2}");
            lblTotaleOrdine.HorizontalOptions = LayoutOptions.End;
            lblTotaleOrdine.VerticalOptions = LayoutOptions.Center;
            lblTotaleOrdine.HorizontalTextAlignment = TextAlignment.End;
            lblTotaleOrdine.VerticalTextAlignment = TextAlignment.Center;
            lblTotaleOrdine.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            lblTotaleOrdine.FontAttributes = FontAttributes.Bold;

            Content = new StackLayout()
            {
                Padding = new Thickness(10),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new ContentView() { Padding= new Thickness(10), Content = lblClienteOrdine },
                    listViewArticoliInOrdineRagguppataPerClasse,
                    new StackLayout() {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Padding = new Thickness(0, 0, 0, 25),
                        Children =
                        {
                            new ContentView() { Padding = new Thickness(10, 0), Content = lblTotaleOrdineDescr },lblTotaleOrdine
                        }
                    },
                    new StackLayout()
                    {
                        Padding = new Thickness(10, 0),
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        Children = { btnCancellaOrdine, btnSalvaOrdine }
                    }
                }
            };
            
		}


        public class HeaderCell : ViewCell
        {
            public HeaderCell()
            {
                Label title = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry)),
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    
                };
                title.SetBinding(Label.TextProperty, "Key");

                View = new ContentView() { BackgroundColor = Color.FromHex("f99643"), Content = title };
            }
        }


        


    }
}

