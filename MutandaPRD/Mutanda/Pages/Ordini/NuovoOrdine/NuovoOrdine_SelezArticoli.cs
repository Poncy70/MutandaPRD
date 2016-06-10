using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.ViewModels;
using Mutanda.Extensions;
using Mutanda.CustomControl;

namespace Mutanda.Pages
{


    public class NuovoOrdine_SelezArticoli : ContentPage
    {
		//public List<WrappedSelection> FamiglieItems = new List<WrappedSelection>();
  //      public List<WrappedSelection> ClassiItems = new List<WrappedSelection>();
        protected ListView listViewSelezioneArticoli;
        public NuovoOrdine_SelezArticoli()
        {
            #region Ricerca FullText
            SearchBar txtRicercaArticoli = new SearchBar();
            txtRicercaArticoli.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtRicercaArticoli.VerticalOptions = LayoutOptions.Start;
            txtRicercaArticoli.SetBinding<NuovoOrdineViewModel>(SearchBar.TextProperty, x => x.SelezArticoli_TestoRicerca, BindingMode.OneWayToSource);
            txtRicercaArticoli.Placeholder = "Ricerca Articoli";
            #endregion

            #region Bottoni Seleziona / Deseleziona Tutti
            Button btnAggiungiAdOrdine = new Button();
            btnAggiungiAdOrdine.HorizontalOptions = LayoutOptions.End;
            btnAggiungiAdOrdine.VerticalOptions = LayoutOptions.Start;
            btnAggiungiAdOrdine.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty,x=>x.SelezionaArticoli_AggiungiRigheAdOrdineCommand, BindingMode.OneWay);
            btnAggiungiAdOrdine.Text = "Aggiungi";
            btnAggiungiAdOrdine.BackgroundColor = Color.FromHex("f99643");
            btnAggiungiAdOrdine.TextColor = Color.White;

            Button btnAzzeraFiltri = new Button();
            btnAzzeraFiltri.Text = "Deselez. Tutti";
            btnAzzeraFiltri.Font = Font.SystemFontOfSize(NamedSize.Micro);
            btnAzzeraFiltri.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.SelezionaArticoli_AnnullaFiltriFamigliaClasseCommand, BindingMode.OneWay);
            btnAzzeraFiltri.BackgroundColor = Color.FromHex("f99643");
            btnAzzeraFiltri.TextColor = Color.White;

            Button btnSelezionaTutti = new Button();
            btnSelezionaTutti.Text = "Selez. Tutti";
            btnSelezionaTutti.Font = Font.SystemFontOfSize(NamedSize.Micro);
            btnSelezionaTutti.SetBinding<NuovoOrdineViewModel>(Button.CommandProperty, x => x.SelezionaArticoli_ReimpostaFiltriFamigliaClasseCommand, BindingMode.OneWay);
            btnSelezionaTutti.BackgroundColor = Color.FromHex("f99643");
            btnSelezionaTutti.TextColor = Color.White;

            var gridBottoni = new Grid()
            {
                RowSpacing = 0,
                ColumnSpacing = 2,
                RowDefinitions = { new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) } },

                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            gridBottoni.Children.Add(btnAzzeraFiltri, 0, 0);
            gridBottoni.Children.Add(btnSelezionaTutti, 1, 0);
            #endregion Bottoni Seleziona / Deseleziona Tutti


            var fsGroupFamiglie = new FormattedString();
            fsGroupFamiglie.Spans.Add(new Span { Text = "Famiglie", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });
            
            var lblGroupFamiglie = new ContentView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5),

                Content = new Label()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    FormattedText = fsGroupFamiglie
                }
            };
            lblGroupFamiglie.SetBinding<NuovoOrdineViewModel>(Label.IsVisibleProperty, x => x.FamiglieVisibilePerSelezioneArticoli, BindingMode.OneWay);

            var fsGroupClassi = new FormattedString();
            fsGroupClassi.Spans.Add(new Span { Text = "Classi", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });
            var lblGroupClassi = new ContentView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5),
                Content = new Label()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    FormattedText = fsGroupClassi
                }
            };
            lblGroupClassi.SetBinding<NuovoOrdineViewModel>(Label.IsVisibleProperty, x => x.ClassiVisibilePerSelezioneArticoli, BindingMode.OneWay);
            
            var fsGroupNature = new FormattedString();
            fsGroupNature.Spans.Add(new Span { Text = "Nature", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });
            var lblGroupNature = new ContentView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5),

                Content = new Label()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    FormattedText = fsGroupNature
                }
            };
            lblGroupNature.SetBinding<NuovoOrdineViewModel>(Label.IsVisibleProperty, x => x.NatureVisibilePerSelezioneArticoli, BindingMode.OneWay);

            #region Listri Filtri
            //FAMIGLIE
            ListView FamiglieList = new ListView();
            FamiglieList.HorizontalOptions = LayoutOptions.FillAndExpand;
            FamiglieList.HasUnevenRows = true;
            FamiglieList.SeparatorVisibility = SeparatorVisibility.None;
            FamiglieList.ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate));
			FamiglieList.SetBinding<NuovoOrdineViewModel>(ListView.ItemsSourceProperty,x=>x.ListaFamigliePerSelezioneArticoli, BindingMode.OneWay);
            FamiglieList.SetBinding<NuovoOrdineViewModel>(ListView.IsVisibleProperty, x => x.FamiglieVisibilePerSelezioneArticoli, BindingMode.OneWay);

            FamiglieList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var o = (SelezioneListView)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null; //de-select
            };
            
            //CLASSI
            ListView ClassiList = new ListView();
            ClassiList.HorizontalOptions = LayoutOptions.FillAndExpand;
            ClassiList.HasUnevenRows = true;
            ClassiList.SeparatorVisibility = SeparatorVisibility.None;
            ClassiList.ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate));
            ClassiList.SetBinding<NuovoOrdineViewModel>(ListView.ItemsSourceProperty, x=>x.ListaClassiPerSelezioneArticoli, BindingMode.OneWay);
            ClassiList.SetBinding<NuovoOrdineViewModel>(ListView.IsVisibleProperty, x => x.ClassiVisibilePerSelezioneArticoli, BindingMode.OneWay);

            ClassiList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var o = (SelezioneListView)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null;
            };

            //NATURE
            ListView NatureList = new ListView();
            NatureList.HorizontalOptions = LayoutOptions.FillAndExpand;
            NatureList.HasUnevenRows = true;
            NatureList.SeparatorVisibility = SeparatorVisibility.None;
            NatureList.ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate));
            NatureList.SetBinding<NuovoOrdineViewModel>(ListView.ItemsSourceProperty, x=>x.ListaNaturePerSelezioneArticoli, BindingMode.OneWay);
            NatureList.SetBinding<NuovoOrdineViewModel>(ListView.IsVisibleProperty, x => x.NatureVisibilePerSelezioneArticoli, BindingMode.OneWay);
            NatureList.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                var o = (SelezioneListView)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null; //de-select
            };
            #endregion

            var stackLayoutSX = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(5),
                Children = { gridBottoni, lblGroupFamiglie, FamiglieList, lblGroupClassi, ClassiList, lblGroupNature, NatureList }
            };

            listViewSelezioneArticoli = new ListViewAlternateColorCustom(ListViewCachingStrategy.RecycleElement)
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

                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblCodArt
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });      //lblDescrizione
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //txtQtaDaOrdinare
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //txtQtaDaOrdinareScontoMerce
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblValUnit
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //txtSconto1
                    gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblTotaleRiga

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
                    gridHeaderLayout.Children.Add(new Label() { Text = "Sconto 1", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.FillAndExpand }, 5, 0);
                    // COLONNA 6
                    gridHeaderLayout.Children.Add(new Label() { Text = "Totale", HorizontalTextAlignment = TextAlignment.End, HorizontalOptions = LayoutOptions.FillAndExpand }, 6, 0);

                    return gridHeaderLayout;
                }),
                ItemTemplate = new DataTemplate(() => new NuovoOrdine_SelezArticoliItemCell_Lamande((NuovoOrdineViewModel)this.BindingContext)),
                HasUnevenRows=true,
                SeparatorColor = Color.FromHex("f99643"),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Fill
        };
            
            listViewSelezioneArticoli.SetBinding<NuovoOrdineViewModel>(ListViewExt.ItemsSourceProperty, x=>x.ListaArticoli,BindingMode.OneWay);

            //listViewSelezioneArticoli.IsGroupingEnabled = true;
            //listViewSelezioneArticoli.GroupDisplayBinding=new Binding("CodClasse");
            


            //Per evitare la selezione della cella che qui non serve a nulla
            listViewSelezioneArticoli.ItemSelected += (sender, e) => listViewSelezioneArticoli.SelectedItem = null;


            var leadListActivityIndicator = new ActivityIndicator()
            {
                //				HeightRequest = RowSizes.MediumRowHeightDouble
            };
            leadListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");


            #region Layout principale pagina
            Grid gridLayout = new Grid();
            gridLayout.Padding = new Thickness(8);
            gridLayout.ColumnSpacing = 3;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Fill;
            
            StackLayout TestoeActivity = new StackLayout() { Children = { leadListActivityIndicator, txtRicercaArticoli } };
            
            //gridLayout.Children.Add(TestoeActivity, 0, 0);
			gridLayout.Children.Add(leadListActivityIndicator, 0,0);
			Grid.SetColumnSpan(leadListActivityIndicator, 2);
			gridLayout.Children.Add(txtRicercaArticoli, 0,1);
            Grid.SetColumnSpan(txtRicercaArticoli, 2);

            gridLayout.Children.Add(new ContentView() { Padding = new Thickness(8), Content = stackLayoutSX }, 0, 2);
            gridLayout.Children.Add(listViewSelezioneArticoli, 1, 2);
            gridLayout.Children.Add(btnAggiungiAdOrdine, 1, 3);




            
			gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });




			gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(160) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            #endregion

            this.Content = gridLayout;
            
        }

        protected override void OnAppearing()
        {
            
        }

        protected override void OnDisappearing()
        {

        }

       



}

	//public class WrappedItemSelectionTemplate : ViewCell
	//{
	//	public WrappedItemSelectionTemplate() : base ()
	//	{
	//		Label name = new Label();
	//		name.SetBinding(Label.TextProperty, new Binding("Item"));
 //           name.Font = Font.SystemFontOfSize(NamedSize.Micro);

 //           Switch mainSwitch = new Switch();
 //           mainSwitch.HorizontalOptions = LayoutOptions.EndAndExpand;
 //           mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));

 //           StackLayout stackFiltriInvio = new StackLayout()
 //           {
 //               Orientation = StackOrientation.Horizontal,
 //               VerticalOptions = LayoutOptions.Center,
 //               Padding = new Thickness(0,3),
 //               Children = { name, mainSwitch }
 //           };
	//		View = stackFiltriInvio;
	//	}
	//}


}
