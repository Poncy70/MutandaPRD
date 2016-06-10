using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutanda.Models;
using Xamarin.Forms;
using System.Windows.Input;
using Mutanda.Services;
using System.Collections.ObjectModel;
using Mutanda.Extensions;
using Mutanda.Pages;
using Mutanda.ViewModels.Base;
using System.Threading;
using Mutanda.Pages;

namespace Mutanda.ViewModels
{
    public class OrdiniListaViewModel : BaseViewModel
    {
        private ObservableCollection<GEST_Ordini_Teste> _ListaOrdini;
        IDataService _DataClient;

        private bool _Filtro_ConsideraTutti ;
        private bool _Filtro_ConsideraSoloSpediti;
        private bool _Filtro_ConsideraSoloNonSpediti ;

        private CancellationTokenSource cancellationTokenSource;
        public OrdiniListaViewModel()
        {
            //ApriDettaglioCommandDaVM = ApriDettaglioCommand;
            _DataClient = DependencyService.Get<IDataService>();

            ListaOrdini = new ObservableCollection<GEST_Ordini_Teste>();

            IsInitialized = false;

            _OrdiniLista_DallaDataRicerca =  new DateTime( DateTime.Now.Year, DateTime.Now.Month,1);
            //Così becco l'ultimo gg del mese


            _OrdiniLista_AllaDataRicerca = _OrdiniLista_DallaDataRicerca.AddMonths(1).AddDays(-1);

            Filtro_ConsideraTutti = true;

            MessagingCenter.Subscribe<NuovoOrdineViewModel>(this,"REFRESHLISTAORDINI", (param) =>
			{
				ExecuteLoadSeedDataCommand();
			});
            
            MessagingCenter.Subscribe<SyncRootPages>(this, "REFRESHLISTAORDINI", (param) =>
            {
                ExecuteLoadSeedDataCommand();
            });

        }

        #region Proprietà
        private string _OrdiniLista_TestoRicerca;
        public string OrdiniLista_TestoRicerca
        {
            get { return _OrdiniLista_TestoRicerca; }
            set
            {
                _OrdiniLista_TestoRicerca = value;
                OnPropertyChanged("OrdiniLista_TestoRicerca");

                if (IsInitialized)
                    OrdiniLista_RicercaOrdine();
            }
        }

        public ObservableCollection<GEST_Ordini_Teste> ListaOrdini
        {
            get { return _ListaOrdini; }

            set
            {
                _ListaOrdini = value;
                OnPropertyChanged("ListaOrdini");
            }
        }


        private DateTime _OrdiniLista_DallaDataRicerca;
        public DateTime OrdiniLista_DallaDataRicerca
        {
            get { return _OrdiniLista_DallaDataRicerca; }
            set
            {
                _OrdiniLista_DallaDataRicerca = value;
                OnPropertyChanged("OrdiniLista_DallaDataRicerca");

                if (IsInitialized)
                    OrdiniLista_RicercaOrdine();
            }
        }

        private DateTime _OrdiniLista_AllaDataRicerca;
        public DateTime OrdiniLista_AllaDataRicerca
        {
            get { return _OrdiniLista_AllaDataRicerca; }
            set
            {
                _OrdiniLista_AllaDataRicerca = value;
                OnPropertyChanged("OrdiniLista_AllaDataRicerca");

                if (IsInitialized)
                    OrdiniLista_RicercaOrdine();
            }
        }


        public bool Filtro_ConsideraTutti
        {
            get { return _Filtro_ConsideraTutti; }

            set
            {
                _Filtro_ConsideraTutti = value;

                if (_Filtro_ConsideraTutti)
                { 
                    Filtro_ConsideraSoloNonSpediti = true;
                    Filtro_ConsideraSoloSpediti = true;
                }
                OnPropertyChanged("Filtro_ConsideraTutti");

                if (IsInitialized)
                    OrdiniLista_RicercaOrdine();
            }
        }

        public bool Filtro_ConsideraSoloNonSpediti
        {
            get { return _Filtro_ConsideraSoloNonSpediti; }

            set
            {
                _Filtro_ConsideraSoloNonSpediti = value;
                
                OnPropertyChanged("Filtro_ConsideraSoloNonSpediti");

                if (IsInitialized)
                    OrdiniLista_RicercaOrdine();
            }
        }

        public bool Filtro_ConsideraSoloSpediti
        {
            get { return _Filtro_ConsideraSoloSpediti; }

            set
            {
                _Filtro_ConsideraSoloSpediti = value;
                OnPropertyChanged("Filtro_ConsideraSoloSpediti");

                if (IsInitialized)
                    OrdiniLista_RicercaOrdine();
            }
        }


        #endregion




        public ICommand ItemSelezionatoCommand
        {
            get { return new Command<GEST_Ordini_Teste>(ItemSelezionato); }
        }

        public ICommand VaiNuovoOrdineCommand
        {
            get { return new Command(VaiNuovoOrdine); }
        }

        async void ItemSelezionato(GEST_Ordini_Teste _GEST_Ordini_Teste)
        {




            var _NuovoOrdine_ArticoliInOrdine = new NuovoOrdine(this.Navigation, _GEST_Ordini_Teste.Id, _GEST_Ordini_Teste.CloudState == (int)CloudState.caricatoEsincronizzato);
            {
                //BindingContext = _NuovoOrdineViewModel
            };



            if (IsBusy)
				return;
			IsBusy = true;
            await Navigation.PushAsync(_NuovoOrdine_ArticoliInOrdine);
			IsBusy = false;
            //in questo caso me ne devo occupare da qui
            //if (!_NuovoOrdineViewModel.IsInitialized)
            //{
            //    await _NuovoOrdineViewModel.ExecuteLoadSeedDataCommand();
            //    _NuovoOrdineViewModel.IsInitialized = true;
            //}
        }

        public async void VaiNuovoOrdine()
        {
			if (IsBusy)
				return;
			IsBusy = true;
            
            await Navigation.PushAsync(new NuovoOrdine(this.Navigation,string.Empty,false));
			IsBusy = false;
        }

        public async Task ExecuteLoadSeedDataCommand()
        {
            if (IsBusy)
                return;

            DependencyService.Get<IHudService>().ShowHud();

            IsBusy = true;
            await _DataClient.Init();
            ListaOrdini = (await _DataClient.Get_GEST_Ordini_Teste_SearchAsync(string.Empty, 
				OrdiniLista_DallaDataRicerca, OrdiniLista_AllaDataRicerca)).ToObservableCollection();
            IsInitialized = true;
            IsBusy = false;
            Filtro_ConsideraTutti = true;
            DependencyService.Get<IHudService>().HideHud();
        }



        #region Metodi Privati


        private async Task OrdiniLista_RicercaOrdine()
        {
            if (!IsInitialized)
                return;

            if (IsBusy)
                return;

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            cancellationTokenSource = new CancellationTokenSource();
            var cancellationTokenSourceLocal = cancellationTokenSource;

            IsBusy = true;

            var result = (await  _DataClient.Get_GEST_Ordini_Teste_SearchAsync(OrdiniLista_TestoRicerca, 
				OrdiniLista_DallaDataRicerca, OrdiniLista_AllaDataRicerca));

            if (_Filtro_ConsideraTutti)
            {

            }
            else
            {
                if (_Filtro_ConsideraSoloSpediti && !_Filtro_ConsideraSoloNonSpediti)
                {

                    result = result.Where(x => x.CloudState == ((int)CloudState.caricatoEsincronizzato));
                }

                if (_Filtro_ConsideraSoloNonSpediti && !_Filtro_ConsideraSoloSpediti)
                {
                    result = result.Where(x => x.CloudState == ((int)CloudState.inseritoEnonsincronizzato));

                }

            }
            if (!_Filtro_ConsideraTutti && !_Filtro_ConsideraSoloNonSpediti && !_Filtro_ConsideraSoloSpediti)
            {
				//Caso particolare
				result = new List<GEST_Ordini_Teste> ();
            }

            if (cancellationTokenSourceLocal.IsCancellationRequested)
			{
				IsBusy = false;
                return;
			}    
			_ListaOrdini.Clear();


			ListaOrdini =result.ToObservableCollection ();

            IsBusy = false;
        }

        #endregion


    }
}
