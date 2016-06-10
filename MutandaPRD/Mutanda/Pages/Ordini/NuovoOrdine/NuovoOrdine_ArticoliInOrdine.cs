using Mutanda.Extensions;
using Mutanda.ViewModels;
using Xamarin.Forms;

namespace Mutanda.Pages
{
    public class NuovoOrdine_ArticoliInOrdine : ContentPage
    {
        private ListView listViewArticoliInOrdine;
        
        //Per forza nel costruttore poichè posso instanziare questa maschera da sola e se lo passo dopo ho errori causali
        public NuovoOrdine_ArticoliInOrdine()
        {
            var txtSearch = new SearchBar();
            txtSearch.SetBinding<NuovoOrdineViewModel>(SearchBar.TextProperty, x => x.ArticoliInOrdine_TestoRicerca, BindingMode.OneWayToSource);
            txtSearch.Placeholder = "Ricerca Articoli";

            FormattedString fsmassiveLabel = new FormattedString();
            fsmassiveLabel.Spans.Add(new Span { Text = "Modifiche Massive", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });
            Label massiveLabel = new Label()
            {
                FormattedText = fsmassiveLabel,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            
            #region Data di consegna
            var NuovaDataConsegna = new DatePicker()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Format = "d"

            };



            NuovaDataConsegna.SetBinding<NuovoOrdineViewModel>(DatePicker.DateProperty, x => x.ArticoliInOrdine_NuovaDataConsegna,BindingMode.OneWayToSource);
            NuovaDataConsegna.SetBinding<NuovoOrdineViewModel>(DatePicker.IsEnabledProperty, x => x.ArticoliInOrdine_ConsideraNuovaDataConsegna);

            Switch swdata = new Switch();
            swdata.SetBinding<NuovoOrdineViewModel>(Switch.IsToggledProperty, x => x.ArticoliInOrdine_ConsideraNuovaDataConsegna);
            swdata.HorizontalOptions = LayoutOptions.EndAndExpand;

            StackLayout stackperdataconsegna = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { NuovaDataConsegna,swdata }
            };
            #endregion

            var QtaLessButton = new Button()
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "minus.png"
            };
            QtaLessButton.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_NuovaQtaBtnDownCommand,BindingMode.OneWay);
            
            var NuovaQta = new Entry()
            {
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
			NuovaQta.SetBinding<NuovoOrdineViewModel>(Entry.TextProperty, x => x.ArticoliInOrdine_NuovaQta,BindingMode.TwoWay);

            var QtaMoreButton = new Button()
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "plus.png"
            };
			QtaMoreButton.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_NuovaQtaBtnUpCommand,BindingMode.OneWay);
            
            var QtaScontoMerceLessButton = new Button()
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "minus.png"
            };
            QtaScontoMerceLessButton.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_NuovaQtaScontoMerceBtnDownCommand,BindingMode.OneWay);

            var qtaScontoMerce = new Entry()
            {
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
			qtaScontoMerce.SetBinding<NuovoOrdineViewModel>(Entry.TextProperty, x => x.ArticoliInOrdine_NuovaQtaScontoMerce,BindingMode.TwoWay);
            
            var QtaScontoMerceMoreButton = new Button()
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Image = "plus.png"
            };
            QtaScontoMerceMoreButton.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_NuovaQtaScontoMerceBtnUpCommand,BindingMode.OneWay);
            
            Entry txtNuovaPercSc1 = new Entry();
			txtNuovaPercSc1.Keyboard = Keyboard.Numeric;
			txtNuovaPercSc1.SetBinding<NuovoOrdineViewModel>(Entry.TextProperty, x => x.ArticoliInOrdine_NuovoPercSconto1,BindingMode.TwoWay);
            txtNuovaPercSc1.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtNuovaPercSc1.HorizontalTextAlignment = TextAlignment.Center;

            Entry txtNuovaPercSc2 = new Entry();
			txtNuovaPercSc2.Keyboard = Keyboard.Numeric;
			txtNuovaPercSc2.SetBinding<NuovoOrdineViewModel>(Entry.TextProperty, x => x.ArticoliInOrdine_NuovoPercSconto2,BindingMode.TwoWay);
            txtNuovaPercSc2.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtNuovaPercSc2.HorizontalTextAlignment = TextAlignment.Center;

            Grid gridQta = new Grid();
            gridQta.Padding = new Thickness(2);
            gridQta.ColumnSpacing = 0;
            gridQta.HorizontalOptions = LayoutOptions.FillAndExpand;

            gridQta.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //0 - BtnQtaDown
            gridQta.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //1 - txtQtaDaOrdinare
            gridQta.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //2 - BtnQtaUp

            gridQta.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridQta.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridQta.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridQta.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridQta.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridQta.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

            Label lblQta = new Label() { Text = "Qta:", FontSize = 16, FontAttributes = FontAttributes.Bold };
            gridQta.Children.Add(lblQta, 0, 0);
            Grid.SetColumnSpan(lblQta, 3);

            gridQta.Children.Add(QtaLessButton, 0, 1);
            gridQta.Children.Add(NuovaQta, 1, 1);
            gridQta.Children.Add(QtaMoreButton, 2, 1);

            Label lblQtaScontoMerce = new Label() { Text = "Qta Sconto Merce:", FontSize = 16, FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            gridQta.Children.Add(lblQtaScontoMerce, 0, 2);
            Grid.SetColumnSpan(lblQtaScontoMerce, 3);

            gridQta.Children.Add(QtaScontoMerceLessButton, 0, 3);
            gridQta.Children.Add(qtaScontoMerce, 1, 3);
            gridQta.Children.Add(QtaScontoMerceMoreButton, 2, 3);

            Label lblPercSc1 = new Label() { Text = "Sconto 1:", FontSize = 16, FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            gridQta.Children.Add(lblPercSc1, 0, 4);
            Grid.SetColumnSpan(lblPercSc1, 2);
            gridQta.Children.Add(txtNuovaPercSc1, 2, 4);

            Label lblPercSc2 = new Label() { Text = "Sconto 2:", FontSize = 16, FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            gridQta.Children.Add(lblPercSc2, 0, 5);
            Grid.SetColumnSpan(lblPercSc2, 2);
            gridQta.Children.Add(txtNuovaPercSc2, 2, 5);


            var BtnFiltri = new Button()
            {
                Text = "Filtri",
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                
            };
            BtnFiltri.Clicked += BtnFiltri_Clicked;

            var BtnSelezionaTuttiGliArticoliVisualizzati = new Button()
            {
                Text = "Selez. Tutti Art.",
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,

            };
            BtnSelezionaTuttiGliArticoliVisualizzati.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_SelezionaTuttiGliarticoliVisualizzatiCommand, BindingMode.OneWay);

            var BtnDeSelezionaTuttiGliArticoliVisualizzati = new Button()
            {
                Text = "DeSelez. Tutti Art.",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White
            };
            BtnDeSelezionaTuttiGliArticoliVisualizzati.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_DeSelezionaTuttiGliarticoliVisualizzatiCommand, BindingMode.OneWay);
            
            StackLayout slFiltriSinistra = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    massiveLabel,
                    new Label() { Text = "Data Consegna:", FontSize=16, FontAttributes = FontAttributes.Bold },
                    stackperdataconsegna,
                    gridQta,BtnFiltri,BtnSelezionaTuttiGliArticoliVisualizzati,BtnDeSelezionaTuttiGliArticoliVisualizzati
                }
            };

            #region Lista Articoli in Ordine
            listViewArticoliInOrdine = new ListViewExt()
            {
                Header = this,
                HeaderTemplate = new DataTemplate(() =>
                {
                    Grid gridHeaderLayout = new Grid();
                    gridHeaderLayout.ColumnSpacing = 3;
                    gridHeaderLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                    gridHeaderLayout.VerticalOptions = LayoutOptions.Fill;
                    gridHeaderLayout.Padding = new Thickness(5, 0);

                    gridHeaderLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //0 - lblCodArt
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });      //1 - lblDescrizione
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //2 - txtQtaDaOrdinare
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //3 - txtQtaDaOrdinareScontoMerce
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //4 - lblValUnit
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //5 - txtSconto1
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //6 - txtSconto2
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //8 - lblTotaleRiga
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });      //9 - BtnDelete

                    //COLONNA 0
                    gridHeaderLayout.Children.Add(new Label() { Text = "Cod.Art", HorizontalTextAlignment = TextAlignment.Start, HorizontalOptions = LayoutOptions.FillAndExpand }, 0, 0);
                    //COLONNA 1
                    gridHeaderLayout.Children.Add(new Label() { Text = "Descrizione", HorizontalTextAlignment = TextAlignment.Start, HorizontalOptions = LayoutOptions.FillAndExpand }, 1, 0);
                    //COLONNA 2
                    gridHeaderLayout.Children.Add(new Label() { Text = "Qta", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 2, 0);
                    //COLONNA 3
                    gridHeaderLayout.Children.Add(new Label() { Text = "Sc. Merce", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 3, 0);
                    //COLONNA 4
                    gridHeaderLayout.Children.Add(new Label() { Text = "Prezzo", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.FillAndExpand }, 4, 0);
                    //COLONNA 5
                    gridHeaderLayout.Children.Add(new Label() { Text = "Sc. 1", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 5, 0);
                    //COLONNA 6
                    gridHeaderLayout.Children.Add(new Label() { Text = "Sc. 2", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.FillAndExpand }, 6, 0);
                    //COLONNA 7
                    gridHeaderLayout.Children.Add(new Label() { Text = "Totale", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.FillAndExpand }, 7, 0);
                    //COLONNA (
                    gridHeaderLayout.Children.Add(new Label() { Text = "Data", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 8, 0);


                    return gridHeaderLayout;
                }),
                ItemTemplate = new DataTemplate(() => new NuovoOrdine_ArticoliInOrdineItemCell_Lamande((NuovoOrdineViewModel)this.BindingContext)),
                HasUnevenRows = true,
                SeparatorColor = Color.FromHex("f99643"),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Fill
            };
            listViewArticoliInOrdine.SetBinding<NuovoOrdineViewModel>(ListViewExt.ItemsSourceProperty, x => x.ListaArticoliInOrdineFiltrato);
			listViewArticoliInOrdine.SetBinding<NuovoOrdineViewModel>(ListViewExt.ItemSelezionatoCommandProperty, x => x.ArticoliInOrdine_ArticoloSelezionatoCommand,BindingMode.OneWay);
            #endregion

            #region Bottoni conferma azioni
            Button BtnCancellaSelezionati = new Button()
            {
                Text = "Cancella Selez.",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White,
                Margin =new Thickness(10,0)
            };
            BtnCancellaSelezionati.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_CancellaSelezionatiCommand, BindingMode.OneWay);


            Button confirmMassive = new Button()
            {
                Text = "Modifica Massiva",
                BackgroundColor = Color.FromHex("f99643"),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.End
            };
			confirmMassive.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.ArticoliInOrdine_ConfermaModificaMassivaCommand,BindingMode.OneWay);
            
            #endregion

            #region Layout principale pagina
            Grid gridLayout = new Grid();
            gridLayout.Padding = new Thickness(8);
            gridLayout.ColumnSpacing = 3;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.FillAndExpand;

            gridLayout.Children.Add(txtSearch, 0, 0);
            Grid.SetColumnSpan(txtSearch, 2);
            gridLayout.Children.Add(slFiltriSinistra, 0, 1);
            gridLayout.Children.Add(listViewArticoliInOrdine, 1, 1);
            gridLayout.Children.Add(
                new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    Children =
                    {
                       BtnCancellaSelezionati,confirmMassive
                    }
                }
                , 1, 2);

            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
            #endregion
            
            Content = gridLayout;

        }

        private void BtnFiltri_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new NuovoOrdine_ArticoliInOrdineFiltri((NuovoOrdineViewModel)this.BindingContext));
        }

        protected override void OnAppearing()
        {

        }
        
        protected override void OnDisappearing()
        {
        }

    }
}
