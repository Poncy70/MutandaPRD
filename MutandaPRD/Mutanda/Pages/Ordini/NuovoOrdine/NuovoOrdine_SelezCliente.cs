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


namespace Mutanda.Pages
{
    public class NuovoOrdine_SelezCliente : ContentPage
    {
        protected ListViewExt listViewListaClienti;

        public NuovoOrdine_SelezCliente()
        {
            SearchBar txtRicercaCliente = new SearchBar();
            txtRicercaCliente.Placeholder = "Ricerca Cliente";
            txtRicercaCliente.SetBinding<NuovoOrdineViewModel>(SearchBar.TextProperty, x => x.SelezionaCliente_TestoRicerca);

            #region Lista Clienti           
            listViewListaClienti = new ListViewExt(ListViewCachingStrategy.RetainElement);
            listViewListaClienti.SeparatorColor = Color.FromHex("f99643");
            listViewListaClienti.HorizontalOptions = LayoutOptions.StartAndExpand;
            listViewListaClienti.VerticalOptions = LayoutOptions.StartAndExpand;
            listViewListaClienti.HasUnevenRows = true;
            listViewListaClienti.ItemTemplate = new DataTemplate(() => new NuovoOrdine_SelezClienteItemCell());
            listViewListaClienti.SetBinding(ListViewExt.ItemsSourceProperty, "ListaAnagrafica");
            listViewListaClienti.SetBinding(ListViewExt.ItemSelezionatoCommandProperty, "SelezionaCliente_ClienteSelezionatoCommand");
            #endregion

            var fsClienteSelezionato = new FormattedString();
            fsClienteSelezionato.Spans.Add(new Span { Text = "Cliente Selezionato", ForegroundColor = Color.FromHex("f99643"), FontSize = 16, FontAttributes = FontAttributes.Bold });
            Label lblCliente = new Label()
            {
                FormattedText = fsClienteSelezionato,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };

            var fsRagioneSociale = new FormattedString();
            fsRagioneSociale.Spans.Add(new Span { Text = "Ragione Sociale", FontAttributes = FontAttributes.Bold });
            Label lblRagioneSociale = new Label() { FormattedText = fsRagioneSociale };

            Label lblClienteSelezionato = new Label();
            lblClienteSelezionato.SetBinding(Label.TextProperty, "RagioneSocialeClienteSelezionato");

            var fsCondizioniPagamento = new FormattedString();
            fsCondizioniPagamento.Spans.Add(new Span { Text = "Condizioni Pagamento", FontAttributes = FontAttributes.Bold });
            Label lblCondizioniPagamento = new Label() { FormattedText = fsCondizioniPagamento };

            ExtendedPicker pckListaCodPagamento = new ExtendedPicker();
            pckListaCodPagamento.SetBinding<NuovoOrdineViewModel>(ExtendedPicker.ItemsSourceProperty, x => x.ListaCondizPagamento, BindingMode.TwoWay);
            pckListaCodPagamento.SetBinding<NuovoOrdineViewModel>(ExtendedPicker.SelectedItemProperty, x => x.CondizionePagamentoImpostata, BindingMode.TwoWay);

            var fsDataConsegna = new FormattedString();
            fsDataConsegna.Spans.Add(new Span { Text = "Data Consegna", FontAttributes = FontAttributes.Bold });
            Label lblDataConsegna = new Label() { FormattedText = fsDataConsegna };

            DatePicker dteDataDefault = new DatePicker() { Format = "dd/MM/yyyy" };
            //dteDataDefault.Date = DateTime.Now;
            dteDataDefault.SetBinding<NuovoOrdineViewModel>(DatePicker.DateProperty, x => x.DataConsegnaDefault);

            //dteDataDefault.SetBinding(DatePicker.DateProperty, "DataConsegnaDefault");
            dteDataDefault.VerticalOptions = LayoutOptions.Fill;


            var fsPercSc1Default = new FormattedString();
            fsPercSc1Default.Spans.Add(new Span { Text = "Perc. Sc1 Default", FontAttributes = FontAttributes.Bold });
            Label lblPercSc1Default = new Label() { FormattedText = fsPercSc1Default };

            Entry txtPercSconto1Default = new Entry()
            {
                Keyboard = Keyboard.Numeric,

            };
            txtPercSconto1Default.SetBinding<NuovoOrdineViewModel>(Entry.TextProperty, x => x.PercSconto1Default);

            var fsIndSpedizSelez = new FormattedString();
            fsIndSpedizSelez.Spans.Add(new Span { Text = "Indirizzo Sped. Selez.", FontAttributes = FontAttributes.Bold });
            Label lblIndSpedizioneSelezionatoIndic= new Label() { FormattedText = fsIndSpedizSelez };

            Label lblIndirizzoSelezionato = new Label();
            lblIndirizzoSelezionato.SetBinding<NuovoOrdineViewModel>(Label.TextProperty, x=>x.IndirizzoSpedizioneSelezionato);
            lblIndirizzoSelezionato.LineBreakMode = LineBreakMode.TailTruncation;

            var fsListaIndSpediz = new FormattedString();
            fsListaIndSpediz.Spans.Add(new Span { Text = "Lista Ind. Sped.", FontAttributes = FontAttributes.Bold });
            Label lblListaIndSpedizione = new Label() { FormattedText = fsListaIndSpediz };

            ListViewExt listViewIndSpedizione = new ListViewExt(ListViewCachingStrategy.RetainElement);
            listViewIndSpedizione.SeparatorColor = Color.FromHex("f99643");
            listViewIndSpedizione.HorizontalOptions = LayoutOptions.StartAndExpand;
            listViewIndSpedizione.VerticalOptions = LayoutOptions.StartAndExpand;
            listViewIndSpedizione.HasUnevenRows = true;
            listViewIndSpedizione.ItemTemplate = new DataTemplate(() => new NuovoOrdine_SelezClienteIndSpedizItemCell());
            listViewIndSpedizione.SetBinding<NuovoOrdineViewModel>(ListViewExt.ItemsSourceProperty, x => x.ListaIndirizziDiSpedizione);
            listViewIndSpedizione.SetBinding<NuovoOrdineViewModel>(ListViewExt.ItemSelezionatoCommandProperty, x => x.SelezCliente_IndirizzoSelezionatoCommand);

            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    lblCliente,
                    lblRagioneSociale,
                    lblClienteSelezionato,lblCondizioniPagamento, pckListaCodPagamento, lblDataConsegna,dteDataDefault,lblPercSc1Default,txtPercSconto1Default,
                    lblIndSpedizioneSelezionatoIndic,lblIndirizzoSelezionato,lblListaIndSpedizione,listViewIndSpedizione
                }
            };

            Grid gridLayout = new Grid();
            gridLayout.Padding = new Thickness(8);
            gridLayout.ColumnSpacing = 8;
            gridLayout.HorizontalOptions = LayoutOptions.StartAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.FillAndExpand;

            gridLayout.Children.Add(txtRicercaCliente, 0, 0);
            Grid.SetColumnSpan(txtRicercaCliente, 2);
            gridLayout.Children.Add(listViewListaClienti, 0, 1);
            gridLayout.Children.Add(stackLayout, 1, 1);

            gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(7, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });

            this.Content = gridLayout;

        }

        protected override void OnAppearing()
        {

        }


        protected override void OnDisappearing()
        {

        }





    }
}