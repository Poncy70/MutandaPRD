using Mutanda.ViewModels.Clienti;
using Mutanda.Views.Clienti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mutanda.Pages.Clienti
{
    public class ClientiRootPage : ContentPage
    {
        ClientiListaViewModel _ClientiListaViewModel { get; set; }
        ClientiDettaglioViewModel _ClientiDettaglioViewModel { get; set; }

        public ClientiRootPage()
        {
            #region clienti view
            var clientiDettaglioView = new ClientiDettaglioView { BindingContext = _ClientiDettaglioViewModel = new ClientiDettaglioViewModel() };
            var clientiListaView = new ClientiListaView { BindingContext = _ClientiListaViewModel = new ClientiListaViewModel(_ClientiDettaglioViewModel) };
            #endregion

            this.BindingContext = _ClientiDettaglioViewModel;

            //ToolbarItem tbiNuovoClienti = new ToolbarItem() { Text = "Nuovo Cliente", Icon = "ic_action_new", Order=ToolbarItemOrder.Primary };
            //tbiNuovoClienti.SetBinding<ClientiDettaglioViewModel>(ToolbarItem.CommandProperty, x => x.NuovoClienteCommand, BindingMode.OneWay);
            //this.ToolbarItems.Add(tbiNuovoClienti);

            //ToolbarItem tbiModificaCliente = new ToolbarItem() { Text = "Modifica Cliente", Icon = "ic_action_edit", Order = ToolbarItemOrder.Secondary };
            //tbiModificaCliente.SetBinding<ClientiDettaglioViewModel>(ToolbarItem.CommandProperty, x => x.NuovoClienteCommand, BindingMode.OneWay);
            //this.ToolbarItems.Add(tbiModificaCliente);

            //Grid gridLayout = new Grid();
            //gridLayout.Children.Add(clientiListaView, 0, 0);
            //gridLayout.Children.Add(clientiDettaglioView, 1, 0);
            //gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(320) });
            //gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            Grid gridLayout = new Grid();
            gridLayout.Padding = new Thickness(8);
            gridLayout.ColumnSpacing = 3;
            gridLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            gridLayout.VerticalOptions = LayoutOptions.Fill;
            gridLayout.Children.Add(clientiListaView, 0, 0);
            gridLayout.Children.Add(clientiDettaglioView, 1, 0);
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(320) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            this.Content = gridLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!_ClientiListaViewModel.IsInitialized)
            {
                await _ClientiListaViewModel.ExecuteLoadSeedDataCommand();
                _ClientiListaViewModel.IsInitialized = true;
            }
        }

    }
}
