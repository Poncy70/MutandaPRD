using Mutanda.Extensions;
using Mutanda.Models;
using Mutanda.Services;
using Mutanda.ViewModels.Base;
using Mutanda.Views.Clienti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mutanda.ViewModels.Clienti
{
    public class ClientiListaViewModel : BaseViewModel
    {
        private ObservableCollection<GEST_Clienti_Anagrafica> _ListaClienti;
        private IDataService _DataClient;
        private ClientiDettaglioViewModel _ClientiDettaglioViewModel;

        public ClientiListaViewModel(ClientiDettaglioViewModel clientiDettaglioViewModel, INavigation navigation = null) : base(navigation)
        {
            _ClientiDettaglioViewModel = clientiDettaglioViewModel;
            _DataClient = DependencyService.Get<IDataService>();
            _ListaClienti = new ObservableCollection<GEST_Clienti_Anagrafica>();

            IsInitialized = false;
        }

        #region Poperty
        public ObservableCollection<GEST_Clienti_Anagrafica> ListaClienti
        {
            get { return _ListaClienti; }

            set
            {
                _ListaClienti = value;
                OnPropertyChanged("ListaClienti");
            }
        }
        
        #endregion

        #region Command
        public ICommand ItemSelezionatoCommand
        {
            get { return new Command<GEST_Clienti_Anagrafica>(ItemSelezionato); }
        }
        #endregion

        public void ItemSelezionato(GEST_Clienti_Anagrafica cliente)
        {
            _ClientiDettaglioViewModel.RagioneSociale = cliente.RagioneSociale;
            _ClientiDettaglioViewModel.PartitaIva = cliente.PartitaIva;
            _ClientiDettaglioViewModel.CodiceFiscale = cliente.CodiceFiscale;
            _ClientiDettaglioViewModel.Indirizzo = cliente.Indirizzo;
            _ClientiDettaglioViewModel.Citta = cliente.Citta;
            _ClientiDettaglioViewModel.Indirizzo = cliente.Indirizzo;
            _ClientiDettaglioViewModel.CAP= cliente.CAP;
            _ClientiDettaglioViewModel.Telefono = cliente.Telefono;
            _ClientiDettaglioViewModel.Cellulare = cliente.Cellulare;
            _ClientiDettaglioViewModel.Email = cliente.Email;
            _ClientiDettaglioViewModel.Web = cliente.Web;
            
            //GEST_Condizione_Pagamento objCondPagamento = _ClientiDettaglioViewModel.ListaCodPagamento.Where(x => x.CodPagamento == cliente.CodPagamento).FirstOrDefault();
            //_ClientiDettaglioViewModel.SelectedCodPagamento = (objCondPagamento != null) ? objCondPagamento : new GEST_Condizione_Pagamento();

            //GEST_Listini objListino = _ClientiDettaglioViewModel.ListaListini.Where(x => x.CodListino == cliente.CodListino).FirstOrDefault();
            //_ClientiDettaglioViewModel.SelectedListino = (objListino != null) ? objListino : new GEST_Listini();

            //GEST_CategorieClienti objCatCategoriaClienti = _ClientiDettaglioViewModel.ListaCategorieClienti.Where(x => x.CodCatCliente == cliente.CodCatCliente).FirstOrDefault();
            //_ClientiDettaglioViewModel.SelectedCategorieClienti = (objCatCategoriaClienti != null) ? objCatCategoriaClienti : new GEST_CategorieClienti();

            //GEST_Porto objPorti = _ClientiDettaglioViewModel.ListaPorti.Where(x => x.CodPorto == cliente.CodPorto).FirstOrDefault();
            //_ClientiDettaglioViewModel.SelectedPorti = (objPorti != null) ? objPorti : new GEST_Porto();
        }

        public async Task ExecuteLoadSeedDataCommand()
        {
            DependencyService.Get<IHudService>().ShowHud();

            if (IsBusy)
                return;

            await _DataClient.Init();

            IsBusy = true;
            
            ListaClienti = (await _DataClient.GetGEST_Clienti_AnagraficaAsync(false)).ToObservableCollection();
            OnPropertyChanged("ListaClienti");
            IsInitialized = true;
            IsBusy = false;

            DependencyService.Get<IHudService>().HideHud();
        }
    }
}
