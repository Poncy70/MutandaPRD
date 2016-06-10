using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.ViewModels;
using Mutanda.Views;
using Mutanda.Pages.Ordini.NuovoOrdine;

namespace Mutanda.Pages
{
	public class NuovoOrdine: TabbedPage
	{
		NuovoOrdineViewModel _NuovoOrdineViewModel;
		INavigation _navi;
		public NuovoOrdine (INavigation navi, string m_IdDoc,bool IsSoloLettura)
		{
			_NuovoOrdineViewModel = new NuovoOrdineViewModel(navi, m_IdDoc, IsSoloLettura);
            this.BindingContext = _NuovoOrdineViewModel;

            if (((NuovoOrdineViewModel)this.BindingContext).IsSoloLettura)
            {

                this.Children.Add(new NuovoOrdine_DatiOrdine() { BindingContext = _NuovoOrdineViewModel, Title = "Riepilogo" });
                this.Children.Add(new NuovoOrdine_Note() { BindingContext = _NuovoOrdineViewModel, Title = "Note" });
                
                //this.CurrentPageChanged += async (object sender, EventArgs e) =>
                //{
                //    //Senza fare tante valutazioni eseguo refresh data consegna su tutti gli articoli
                //    //await _NuovoOrdineViewModel.AggiornaDataConsegnaDefault();
                //    //await _NuovoOrdineViewModel.AggiornaOrdinato(string.Empty);
                //    await _NuovoOrdineViewModel.AggiornaPrezzoEsconti();
                //    await _NuovoOrdineViewModel.AggiornaListaArticoliRaggruppata();
                //};
                return;
            }

            if (((NuovoOrdineViewModel)this.BindingContext).IsInEdit)
            {
                

                this.Children.Add(new NuovoOrdine_SelezArticoli() { BindingContext = _NuovoOrdineViewModel, Title = "Caricamento" });
                this.Children.Add(new NuovoOrdine_ArticoliInOrdine() { BindingContext = _NuovoOrdineViewModel, Title = "Azioni Massive" });
                this.Children.Add(new NuovoOrdine_Note() { BindingContext = _NuovoOrdineViewModel, Title = "Note" });
                this.Children.Add(new NuovoOrdine_DatiOrdine(){ BindingContext = _NuovoOrdineViewModel, Title = "Riepilogo" });

                this.CurrentPageChanged += async (object sender, EventArgs e) =>
                {
                    //Senza fare tante valutazioni eseguo refresh data consegna su tutti gli articoli
                    await _NuovoOrdineViewModel.AggiornaDataConsegnaDefault();
                    await _NuovoOrdineViewModel.AggiornaOrdinato(string.Empty);
                    await _NuovoOrdineViewModel.AggiornaPrezzoEsconti();
                    await _NuovoOrdineViewModel.AggiornaListaArticoliRaggruppata();
                };
                return;
            }
             
			this.Children.Add (new NuovoOrdine_SelezCliente() { BindingContext = _NuovoOrdineViewModel, Title = "Cliente" });
			this.Children.Add(new NuovoOrdine_SelezArticoli()  { BindingContext = _NuovoOrdineViewModel, Title="Caricamento" });
			this.Children.Add(new NuovoOrdine_ArticoliInOrdine() { BindingContext = _NuovoOrdineViewModel, Title="Azioni Massive" });
            this.Children.Add(new NuovoOrdine_Note() { BindingContext = _NuovoOrdineViewModel, Title = "Note" });
            this.Children.Add(new NuovoOrdine_DatiOrdine() { BindingContext = _NuovoOrdineViewModel, Title="Riepilogo" });

            this.CurrentPageChanged += async (object sender, EventArgs e) =>
            {
                //Senza fare tante valutazioni eseguo refresh data consegna su tutti gli articoli
                await _NuovoOrdineViewModel.AggiornaDataConsegnaDefault();
                await _NuovoOrdineViewModel.AggiornaOrdinato(string.Empty);
                await _NuovoOrdineViewModel.AggiornaPrezzoEsconti();
                await _NuovoOrdineViewModel.AggiornaListaArticoliRaggruppata();
                    

            };
            
        }


		

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if (!_NuovoOrdineViewModel.IsInitialized)
			{
				await _NuovoOrdineViewModel.ExecuteLoadSeedDataCommand();
				_NuovoOrdineViewModel.IsInitialized = true;
			}

            //NavigationPage.SetHasNavigationBar(this, false);
        }
	}
}

