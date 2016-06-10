using Mutanda.Extensions;
using Mutanda.Models;
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
    public class ClientiDettaglioViewModel : BaseViewModel
    {
        #region PRIVATE
        private int IDAnagrafica { get; set; }
        private IDataService _DataClient;
        private string _ListaCodCatCliente { get; set; }
        private string _ListaCodListino { get; set; }
        private string _ListaCodPorto { get; set; }
        private string _Note { get; set; }
        private bool _ClienteAttivo { get; set; }
        private int _IdAgente { get; set; }
        private int _ListaIdIndSpedMerceDefault { get; set; }
        private string _Iban { get; set; }

        private bool _IsEnabled;
        #endregion PRIVATE
        
        public ClientiDettaglioViewModel(INavigation navigation = null) : base(navigation)
        {
            _DataClient = DependencyService.Get<IDataService>();
            init();

            this._RagioneSociale = "Ragione Sociale";
            this._Indirizzo = "Indirizzo";
            this.IsInitialized = false;
            this._IsEnabled = false;
            
        }

        private async Task init()
        {
            this.ListaCodPagamento = (await _DataClient.GetGEST_Condizione_PagamentoAsync(true)).ToObservableCollection();
            this._ListaListini = (await _DataClient.GetGEST_ListiniAsync(true)).ToObservableCollection();
            this._ListaCategorieClienti = (await _DataClient.GetGEST_CategorieClientiAsync(true)).ToObservableCollection();
            this._ListaPorti = (await _DataClient.GetGEST_PortoAsync(true)).ToObservableCollection();
        }

        public void NuovoCliente()
        {
            //await Navigation.PushAsync(new NuovoOrdine(this.Navigation, string.Empty));
            this.RagioneSociale = String.Empty;
            this.PartitaIva = String.Empty;
            this.CodiceFiscale = String.Empty;
            this.Indirizzo = String.Empty;
            this.CAP = String.Empty;
            this.Citta = String.Empty;
            this.Provincia = String.Empty;
            this.Telefono = String.Empty;
            this.Cellulare = String.Empty;
            this.Email = String.Empty;
            this.Web = String.Empty;
            this.SelectedCodPagamento = null;
            
            this.IsEnabled = true;
        }

        #region Property
        private string _RagioneSociale { get; set; }
        public string RagioneSociale
        {
            get { return _RagioneSociale; }
            set
            {
                _RagioneSociale = value;
                OnPropertyChanged("RagioneSociale");
            }
        }

        private string _PartitaIva { get; set; }
        public string PartitaIva
        {
            get { return _PartitaIva; }
            set
            {
                _PartitaIva = value;
                OnPropertyChanged("PartitaIva");
            }
        }

        private string _CodiceFiscale { get; set; }
        public string CodiceFiscale
        {
            get { return _CodiceFiscale; }
            set
            {
                _CodiceFiscale = value;
                OnPropertyChanged("CodiceFiscale");
            }
        }

        private string _Indirizzo { get; set; }
        public string Indirizzo
        {
            get { return _Indirizzo; }
            set
            {
                _Indirizzo = value;
                OnPropertyChanged("Indirizzo");
            }
        }

        private string _CAP { get; set; }
        public string CAP
        {
            get { return _CAP; }
            set
            {
                _CAP = value;
                OnPropertyChanged("CAP");
            }
        }

        private string _Citta { get; set; }
        public string Citta
        {
            get { return _Citta; }
            set
            {
                _Citta = value;
                OnPropertyChanged("Citta");
            }
        }
        
        private string _Provincia { get; set; }
        public string Provincia
        {
            get { return _Provincia; }
            set
            {
                _Provincia = value;
                OnPropertyChanged("Provincia");
            }
        }

        private string _Telefono { get; set; }
        public string Telefono
        {
            get { return _Telefono; }
            set
            {
                _Telefono = value;
                OnPropertyChanged("Telefono");
            }
        }

        private string _Cellulare { get; set; }
        public string Cellulare
        {
            get { return _Cellulare; }
            set
            {
                _Cellulare = value;
                OnPropertyChanged("Cellulare");
            }
        }

        private string _Email { get; set; }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _Web { get; set; }
        public string Web
        {
            get { return _Web; }
            set
            {
                _Web = value;
                OnPropertyChanged("Web");
            }
        }

        private string _CloudState { get; set; }
        public string CloudState
        {
            get { return _CloudState; }
            set
            {
                _CloudState = value;
                OnPropertyChanged("CloudState");
            }
        }

        private IList<GEST_Condizione_Pagamento> _ListaCodPagamento { get; set; }
        public IList<GEST_Condizione_Pagamento> ListaCodPagamento
        {
            get { return _ListaCodPagamento; }
            set { _ListaCodPagamento = value;
                OnPropertyChanged("SelectedCodPagamento");
            }
        }
        private GEST_Condizione_Pagamento _SelectedCodPagamento;
        public GEST_Condizione_Pagamento SelectedCodPagamento
        {
            get { return _SelectedCodPagamento; }
            set
            {
                if (value == _SelectedCodPagamento) return;
                _SelectedCodPagamento = value;
                OnPropertyChanged("SelectedCodPagamento");
            }
        }

        private IList<GEST_Listini> _ListaListini { get; set; }
        public IList<GEST_Listini> ListaListini
        {
            get { return _ListaListini; }
            set { _ListaListini = value; }
        }
        private GEST_Listini _SelectedListino;
        public GEST_Listini SelectedListino
        {
            get { return _SelectedListino; }
            set
            {
                if (value == _SelectedListino) return;
                _SelectedListino = value;
                OnPropertyChanged("SelectedListino");
            }
        }

        private IList<GEST_CategorieClienti> _ListaCategorieClienti { get; set; }
        public IList<GEST_CategorieClienti> ListaCategorieClienti
        {
            get { return _ListaCategorieClienti; }
            set { _ListaCategorieClienti = value; }
        }
        private GEST_CategorieClienti _SelectedCategorieClienti;
        public GEST_CategorieClienti SelectedCategorieClienti
        {
            get { return _SelectedCategorieClienti; }
            set
            {
                if (value == _SelectedCategorieClienti) return;
                _SelectedCategorieClienti = value;
                OnPropertyChanged("SelectedCategorieClienti");
            }
        }

        private IList<GEST_Porto> _ListaPorti { get; set; }
        public IList<GEST_Porto> ListaPorti
        {
            get { return _ListaPorti; }
            set { _ListaPorti = value; }
        }
        private GEST_Porto _SelectedPorti;
        public GEST_Porto SelectedPorti
        {
            get { return _SelectedPorti; }
            set
            {
                if (value == _SelectedPorti) return;
                _SelectedPorti = value;
                OnPropertyChanged("SelectedPorti");
            }
        }

        //private string _ListaCodPorto { get; set; }
        //private string _Note { get; set; }
        //private bool _ClienteAttivo { get; set; }
        //private int _CloudState { get; set; }
        //private int _IdAgente { get; set; }
        //private int _ListaIdIndSpedMerceDefault { get; set; }
        //private string _Iban { get; set; }

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public ICommand NuovoClienteCommand
        {
            get { return new Command(NuovoCliente); }
        }

        #endregion


    }
}
