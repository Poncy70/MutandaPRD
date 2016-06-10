using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.ViewModels;
using Mutanda.Views;
using Mutanda.Extensions;
using Mutanda.Pages.Ordini.OrdiniLista;

namespace Mutanda.Pages
{
    public class OrdiniLista : ContentPage
    {
        protected ListViewExt listView;

        OrdiniListaViewModel _OrdiniListaViewModel;
        public OrdiniLista()
        {
            BindingContext = _OrdiniListaViewModel = new OrdiniListaViewModel() { Navigation = this.Navigation };
            _OrdiniListaViewModel.Navigation = Navigation;
                        
            ToolbarItem tbi = new ToolbarItem() { Text = "NuovoOrdine", Icon = "ic_action_new" };
            tbi.SetBinding<OrdiniListaViewModel>(ToolbarItem.CommandProperty, x=>x.VaiNuovoOrdineCommand, BindingMode.OneWay);
            this.ToolbarItems.Add(tbi);
            
            var txtRicercaOrdine = new SearchBar();
            txtRicercaOrdine.HorizontalOptions = LayoutOptions.FillAndExpand;
            txtRicercaOrdine.VerticalOptions = LayoutOptions.Start;
			txtRicercaOrdine.SetBinding<OrdiniListaViewModel>(SearchBar.TextProperty, x => x.OrdiniLista_TestoRicerca);
            txtRicercaOrdine.Placeholder = "Ricerca Ordini";
            
            listView = new ListViewExt() {  };
            listView.Header = this;
            listView.HeaderTemplate = new DataTemplate(() =>
            {
                Grid gridHeaderLayout = new Grid();
                gridHeaderLayout.ColumnSpacing = 12;
                gridHeaderLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                gridHeaderLayout.VerticalOptions = LayoutOptions.Center;

                gridHeaderLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

                gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblData
                gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });      //RagioneSociale
                gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblNumeroOrdine
                gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblTotaleConsegna
                gridHeaderLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });      //lblInviato

                gridHeaderLayout.Children.Add(new Label() { Text = "Data Doc." }, 0, 0);
                gridHeaderLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.StartAndExpand, Content = new Label() { Text = "Ragione Sociale" } }, 1, 0);
                gridHeaderLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.FillAndExpand, Content = new Label() { Text = "N° Ordine", HorizontalTextAlignment = TextAlignment.Start } }, 2, 0);
                gridHeaderLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.EndAndExpand, Content = new Label() { Text = "Totale" } }, 3, 0);
                gridHeaderLayout.Children.Add(new ContentView() { HorizontalOptions = LayoutOptions.End, Content = new Label() { Text = "Stato" } }, 4, 0);

                return gridHeaderLayout;
            });
            listView.ItemTemplate = new DataTemplate(typeof(OrdiniListaItemCell));
            listView.SeparatorColor = Color.FromHex("f99643");
            listView.HorizontalOptions = LayoutOptions.StartAndExpand;
            listView.VerticalOptions = LayoutOptions.Center;
            listView.SetBinding<OrdiniListaViewModel>(ListViewExt.ItemsSourceProperty, x=>x.ListaOrdini);
            listView.SetBinding<OrdiniListaViewModel>(ListViewExt.ItemSelezionatoCommandProperty, x => x.ItemSelezionatoCommand, BindingMode.OneWay);
            listView.SetBinding<OrdiniListaViewModel>(ListView.IsRefreshingProperty, x => x.IsBusy,BindingMode.OneWay);
            
            #region FILTRI COLONNA SINISTRA
            var dteDallaData = new DatePicker();
            dteDallaData.SetBinding<OrdiniListaViewModel>(DatePicker.DateProperty, x => x.OrdiniLista_DallaDataRicerca);
            dteDallaData.Format = "d";
            dteDallaData.VerticalOptions = LayoutOptions.Fill;
            
            var dteAllaData = new DatePicker();
            dteAllaData.SetBinding<OrdiniListaViewModel>(DatePicker.DateProperty, x => x.OrdiniLista_AllaDataRicerca);
            dteAllaData.Format = "d";
            dteAllaData.VerticalOptions = LayoutOptions.Fill;

            var fsData = new FormattedString();
            fsData.Spans.Add(new Span { Text = "Filtra per data Ordine", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });
            
            StackLayout stackDate = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10),
                Children = {
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        Children = { new Label() { Text = "Dalla Data:"}, dteDallaData }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        Children = { new Label() { Text = "Alla Data:" }, dteAllaData }
                    }
                }
            };
            #endregion

            #region Filtri per Invio
            Switch swTutti = new Switch();
            swTutti.HorizontalOptions = LayoutOptions.EndAndExpand;
            swTutti.SetBinding<OrdiniListaViewModel>(Switch.IsToggledProperty, x => x.Filtro_ConsideraTutti, BindingMode.TwoWay);
            
            Switch swInviati = new Switch();
            swInviati.HorizontalOptions = LayoutOptions.EndAndExpand;
            swInviati.SetBinding<OrdiniListaViewModel>(Switch.IsToggledProperty, x => x.Filtro_ConsideraSoloSpediti, BindingMode.TwoWay);

            Switch swNonInviati = new Switch();
            swNonInviati.HorizontalOptions = LayoutOptions.EndAndExpand;
            swNonInviati.SetBinding<OrdiniListaViewModel>(Switch.IsToggledProperty, x => x.Filtro_ConsideraSoloNonSpediti,BindingMode.TwoWay);

            var fsInvio = new FormattedString();
            fsInvio.Spans.Add(new Span {Text= "Filtra per invio Ordine", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });

            StackLayout stackFiltriInvio = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(5),
                Children =
                {            
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Fill,
                        Children = { new Label() { Text = "Tutti" }, swTutti }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Fill,
                        Children = { new Label() { Text = "Inviati" }, swInviati }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Fill,
                        Children = { new Label() { Text = "Da spedire" }, swNonInviati }
                    }
                }
            };

            var stackLayoutSX = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                    new ContentView()
                    {
                        Padding = new Thickness(0,5),
                        Content = new Label() { FormattedText = fsData}
                    },
                    stackDate,
                    new ContentView()
                    {
                        Padding = new Thickness(0,5),
                        Content = new Label() { FormattedText = fsInvio}
                    },
                    stackFiltriInvio }
            };
            #endregion
            
            Grid gridLayout = new Grid();
            gridLayout.Padding = new Thickness(8);
            gridLayout.ColumnSpacing = 3;
            gridLayout.HorizontalOptions = LayoutOptions.StartAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.StartAndExpand;

            //gridLayout.Children.Add(new BoxView { Color = Color.Teal }, 0, 1);

            gridLayout.Children.Add(txtRicercaOrdine, 0, 0);
            Grid.SetColumnSpan(txtRicercaOrdine, 2);
            gridLayout.Children.Add(stackLayoutSX, 0, 1);
            gridLayout.Children.Add(listView, 1, 1);
            
            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) } );
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } );
            
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(230) });
            //gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });

            this.Content = gridLayout;
				
        }

        void Ricerca(object sender, EventArgs e)
        {

            //string filter = searchBar.Text;

            //listView.BeginRefresh();

            //if (string.IsNullOrWhiteSpace(filter))
            //{
            //    listView.ItemsSource = _OrdiniListaViewModel.ListaOrdini;
            //}
            //else
            //{
            //    listView.ItemsSource = _OrdiniListaViewModel.ListaOrdini.Where(x => x.RagioneSociale.ToLower().Contains(filter.ToLower()));
            //    //        .Where(x => x.Title.ToLower()
            //    //           .Contains(filter.ToLower()));
            //}

            //listView.EndRefresh();


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!_OrdiniListaViewModel.IsInitialized)
            {

                //Caso: eseguo sync e quindi riapro la pagina ordini - devo forzare refresh list ordini sincronizzato o meno
                await _OrdiniListaViewModel.ExecuteLoadSeedDataCommand();
                _OrdiniListaViewModel.IsInitialized = true;
            }
        }


        protected override void OnDisappearing()
        {
            //searchBar.SearchButtonPressed -= Ricerca;

        }




        Action<object> ApriDettaglioAction
        {
            get { return new Action<object>(o => ApriDettaglio((GEST_Documenti_Teste)o)); }
        }

        async Task ApriDettaglio(GEST_Documenti_Teste _GEST_Documenti_Teste )
        {
            //LeadDetailViewModel viewModel = new LeadDetailViewModel(Navigation, lead);

            //var leadDetailPage = new LeadDetailPage()
            //{
            //    BindingContext = viewModel,
            //    Title = TextResources.Details,

            //};
            //if (Device.OS == TargetPlatform.iOS)
            //    leadDetailPage.Icon = Icon = new FileImageSource() { File = "LeadDetailTab" };


            //if (lead != null)
            //{
            //    leadDetailPage.Title = lead.Company;
            //}
            //else
            //{
            //    leadDetailPage.Title = "New Lead";
            //}

            //leadDetailPage.ToolbarItems.Add(
            //    new ToolbarItem(TextResources.Save, "save.png", async () =>
            //    {

            //        if (string.IsNullOrWhiteSpace(viewModel.Lead.Company))
            //        {
            //            await DisplayAlert("Missing Information", "Please fill in the lead's company to continue", "OK");
            //            return;
            //        }

            //        var answer =
            //            await DisplayAlert(
            //                title: TextResources.Leads_SaveConfirmTitle,
            //                message: TextResources.Leads_SaveConfirmDescription,
            //                accept: TextResources.Save,
            //                cancel: TextResources.Cancel);

            //        if (answer)
            //        {
            //            viewModel.SaveLeadCommand.Execute(null);

            //            await Navigation.PopAsync();
            //        }
            //    }));


            //await Navigation.PushAsync(leadDetailPage);
        }



    }
}
