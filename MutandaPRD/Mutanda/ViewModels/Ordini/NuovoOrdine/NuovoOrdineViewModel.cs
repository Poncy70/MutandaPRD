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
using Mutanda.ViewModels.Base;
using System.Threading;
using Plugin.DeviceInfo;
using System.Net.Http;

namespace Mutanda.ViewModels
{

    public class NuovoOrdineViewModel:BaseViewModel
	{
		private ObservableCollectionWithEvents<SelezioneListView> _ListaFamigliePerSelezioneArticoli;
		private ObservableCollectionWithEvents<SelezioneListView> _ListaClassiPerSelezioneArticoli;
        private ObservableCollectionWithEvents<SelezioneListView> _ListaNaturePerSelezioneArticoli;
        private ObservableCollection<GEST_Articoli_Anagrafica_SelezioneArticolo> _ListaArticoli;


        private ObservableCollectionWithEvents<SelezioneListView> _ListaFamigliePerArticoliInOrdine;
        private ObservableCollectionWithEvents<SelezioneListView> _ListaClassiPerArticoliInOrdine;
        private ObservableCollectionWithEvents<SelezioneListView> _ListaNaturePerArticoliInOrdine;


        private ObservableCollection<GEST_Clienti_Anagrafica_Indirizzi> _ListaIndirizziDiSpedizione;


        //é la lista originale degli articoli in ordine
        //Per perfomance la lascio semplicemente List
        private List<GEST_Ordini_Righe_DettaglioOrdine> _ListaArticoliInOrdine;

        //Rappresenta _ListaArticoliInOrdine ma filtrato secondo i filtri correnti
        //Occorre fare in questo modo poichè filtrare la lista originale dà errore e malfunzioni per cui occorre 
        //lavorare con lista originale e restituire una lista filltrata
        //Occorre osservare che le due liste sono sincronizzate con il seguente metodo
        //Essendo che insiston su stessi oggetti allora modifica su un oggetto si ripercuote anche sullo stesso oggetto dell'altra lista
        //Occorre solo tenere aggiornate le due liste per inserimenti e cancellazioni, ma proponendo per inserimenti lo stesso oggetto
        private ObservableCollection<GEST_Ordini_Righe_DettaglioOrdine> _ListaArticoliInOrdineFiltrato;

		private ObservableCollection<GEST_Clienti_Anagrafica> _ListaAnagrafica;


        //Lavoro con 2 liste - così quando filtro agisco sulla lista e non da db che è molto più lento
        private ObservableCollection<GEST_Clienti_Anagrafica> _ListaAnagraficaOriginale;

        private GEST_Clienti_Anagrafica _GEST_Anagrafica_ClienteSelezionato;

        private GEST_Clienti_Anagrafica_Indirizzi _GEST_Clienti_Anagrafica_IndirizziSelezionato;


        private ObservableCollection<Grouping<string, GEST_Ordini_Righe_DettaglioOrdine>> _ListaArticoliInOrdineRaggruppatoPerClasse;
        private string _SelezionaCliente_TestoRicerca;
        private string _ArticoliInOrdine_TestoRicerca;


        private string _CondizionePagamentoImpostata;

        //Per far andare in modo corretto il binding nei due versi occorre lavorare solo sulle stringhe !
        private ObservableCollection<string> _ListaCondizPagamento;

        private int _IdAnagrafica=0;
        private string _RagioneSocialeClienteSelezionato;

        private DateTime _DataConsegnaDefault=DateTime.Now;
        private decimal _PercSconto1Default = 0;

		private readonly Command _SalvaOrdineInCorsoCommand;
        private readonly Command _CancellaOrdineInCorsoCommand;
		private readonly Command<GEST_Articoli_Anagrafica_SelezioneArticolo> _SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnUpCommand;
		private readonly Command<GEST_Articoli_Anagrafica_SelezioneArticolo> _SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnDownCommand;
        private readonly Command<GEST_Articoli_Anagrafica_SelezioneArticolo> _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand;
        private readonly Command<GEST_Articoli_Anagrafica_SelezioneArticolo> _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand;
        
        private readonly Command<GEST_Ordini_Righe_DettaglioOrdine> _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDownCommand;
        private readonly Command<GEST_Ordini_Righe_DettaglioOrdine> _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUpCommand;
        private readonly Command<GEST_Ordini_Righe_DettaglioOrdine> _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand;
        private readonly Command<GEST_Ordini_Righe_DettaglioOrdine> _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand;
        private readonly Command<GEST_Ordini_Righe_DettaglioOrdine> _ArticoliInOrdineItemCell_CancellaItemCommand;

        private string _SelezArticoli_TestoRicerca;
        private string _DatiOrdine_Note;
        private readonly Command _ArticoliInOrdine_NuovaQtaBtnUpCommand;
        private readonly Command _ArticoliInOrdine_NuovaQtaBtnDownCommand;
        private readonly Command _ArticoliInOrdine_NuovaQtaScontoMerceBtnUpCommand;
        private readonly Command _ArticoliInOrdine_NuovaQtaScontoMerceBtnDownCommand;
        private readonly Command _ArticoliInOrdine_ConfermaModificaMassivaCommand;
        private readonly Command _ArticoliInOrdine_AnnullaFiltriFamigliaClasseNatureCommand;
        private readonly Command _ArticoliInOrdine_ReimpostaFiltriFamigliaClasseNatureCommand;
        private readonly Command _ArticoliInOrdine_SelezionaTuttiGliarticoliVisualizzatiCommand;
        private readonly Command _ArticoliInOrdine_DeSelezionaTuttiGliarticoliVisualizzatiCommand;
        private readonly Command _ArticoliInOrdine_CancellaSelezionatiCommand;

        private readonly Command _SelezionaArticoli_AnnullaFiltriFamigliaClasseNatureCommand;
        private readonly Command _SelezionaArticoli_ReimpostaFiltriFamigliaClasseNatureCommand;

        private readonly Command<object> _SelezCliente_ClienteSelezionatoCommand;
        private readonly Command<object> _SelezCliente_IndirizzoSelezionatoCommand;

        
        private readonly Command _SelezionaArticoli_AggiungiRigheAdOrdineCommand;

        private readonly Command<object> _ArticoliInOrdine_ArticoloSelezionatoCommand;

        IDataService _DataClient;
		INavigation _navi;

        private CancellationTokenSource cancellationTokenSource;

        string _IdDoc = string.Empty;

        //Se true siamo in editing ordine già inserito altrimenti in nuovo
        bool _IsInEdit = false;

        //Se true allora è solo lettura
        bool _IsSoloLettura = false;

        private decimal _ArticoliInOrdine_NuovaQta;
        private decimal _ArticoliInOrdine_NuovaQtaScontoMerce;
        private DateTime _ArticoliInOrdine_NuovaDataConsegna;
        private decimal _ArticoliInOrdine_NuovoPercSconto1;
        private decimal _ArticoliInOrdine_NuovoPercSconto2;
        private bool _ArticoliInOrdine_ConsideraNuovaDataConsegna;



        //Se è impostato _IdDoc e IsReadOnly = false significa che siamo in modifica di un ordine
        //Se è impostato _IdDoc e IsReadOnly = true significa che siamo in solo lettura di un ordine
        //se _IdDoc è vuoto allora siamo in nuovo
        public NuovoOrdineViewModel (INavigation navi,string m_IdDoc,bool IsReadOnly)
		{

            _IdDoc = m_IdDoc;

            if (_IdDoc.Length > 0)
            {
                _IsSoloLettura = IsReadOnly;
                if (_IsSoloLettura)
                {

                }else
                {
                    _IsInEdit = true;
                }
            }


            _DataClient = DependencyService.Get<IDataService>();
            //DeterminaStatoViewModel();
            _ListaArticoli = new ObservableCollection<GEST_Articoli_Anagrafica_SelezioneArticolo>();
			_ListaArticoliInOrdine = new List<GEST_Ordini_Righe_DettaglioOrdine>();
            _ListaArticoliInOrdineFiltrato= new ObservableCollection<GEST_Ordini_Righe_DettaglioOrdine>();
			_ListaAnagrafica = new ObservableCollection<GEST_Clienti_Anagrafica>();
            _ListaAnagraficaOriginale = new ObservableCollection<GEST_Clienti_Anagrafica>();
            _ListaArticoliInOrdineRaggruppatoPerClasse = new ObservableCollection<Grouping<string, GEST_Ordini_Righe_DettaglioOrdine>>();

            _ListaCondizPagamento = new ObservableCollection<string>();
            //_RicercaArticoliCommand = new Command(SelezArticoli_RicercaArticoli, () => !string.IsNullOrWhiteSpace(TestoRicercaArticoli));
            _SalvaOrdineInCorsoCommand = new Command(SalvaOrdineInCorso, () => _ListaArticoliInOrdine.Count>0 && _GEST_Anagrafica_ClienteSelezionato != null && !IsBusy);
            _CancellaOrdineInCorsoCommand = new Command(CancellaOrdineInCorso, () =>  !IsBusy);

			_SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnUpCommand =  new Command<GEST_Articoli_Anagrafica_SelezioneArticolo>(SelezionaArticoli_QtaArticoliDaOrdinareBtnUp, (param) =>!IsBusy);

			_SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnDownCommand = new Command<GEST_Articoli_Anagrafica_SelezioneArticolo>(SelezionaArticoli_QtaArticoliDaOrdinareBtnDown, (param) =>!IsBusy);

            _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand = new Command<GEST_Articoli_Anagrafica_SelezioneArticolo>(SelezionaArticoli_QtaScontoMerceArticoliDaOrdinareBtnUp, (param) => !IsBusy);

            _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand = new Command<GEST_Articoli_Anagrafica_SelezioneArticolo>(SelezionaArticoli_QtaScontoMerceArticoliDaOrdinareBtnDown, (param) => !IsBusy);


            _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDownCommand = new Command<GEST_Ordini_Righe_DettaglioOrdine>(ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDown, (param) => !IsBusy);

            _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUpCommand =
               new Command<GEST_Ordini_Righe_DettaglioOrdine>(ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUp,
                   (param) => !IsBusy);


            _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand =
                new Command<GEST_Ordini_Righe_DettaglioOrdine>(ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDown,
                    (param) => !IsBusy);

            _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand =
               new Command<GEST_Ordini_Righe_DettaglioOrdine>(ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUp,
                   (param) => !IsBusy);

            _ArticoliInOrdineItemCell_CancellaItemCommand =
               new Command<GEST_Ordini_Righe_DettaglioOrdine>(ArticoliInOrdineItemCell_CancellaItem,
                   (param) => !IsBusy);

            _ArticoliInOrdine_CancellaSelezionatiCommand =
               new Command(ArticoliInOrdine_CancellaSelezionati,
                   () => !IsBusy);


            _ArticoliInOrdine_NuovaQtaBtnUpCommand = new Command(ArticoliInOrdine_NuovaQtaBtnUp, () => !IsBusy);


            _ArticoliInOrdine_NuovaQtaBtnDownCommand =
              new Command(ArticoliInOrdine_NuovaQtaBtnDown,
                  () => !IsBusy);

            _ArticoliInOrdine_NuovaQtaScontoMerceBtnUpCommand =
              new Command(ArticoliInOrdine_NuovaQtaScontoMerceBtnUp,
                  () => !IsBusy);

            _ArticoliInOrdine_NuovaQtaScontoMerceBtnDownCommand =
              new Command(ArticoliInOrdine_NuovaQtaScontoMerceBtnDown,
                  () => !IsBusy);


            _ArticoliInOrdine_ConfermaModificaMassivaCommand =
              new Command(ArticoliInOrdine_ConfermaModificaMassiva,
                  () => !IsBusy);


            _ArticoliInOrdine_NuovaDataConsegna = DateTime.Now;
            _ArticoliInOrdine_NuovoPercSconto1 = 0;
            _ArticoliInOrdine_NuovoPercSconto2 = 0;
            _ArticoliInOrdine_ConsideraNuovaDataConsegna = false;


            _SelezionaArticoli_AggiungiRigheAdOrdineCommand =new Command(SelezionaArticoli_AggiungiRigheAdOrdine, () => !IsBusy);

            _SelezCliente_ClienteSelezionatoCommand = new Command<object>(SelezCliente_ClienteSelezionato);
            _SelezCliente_IndirizzoSelezionatoCommand = new Command<object>(SelezCliente_IndirizzoSelezionato, f => !IsBusy);
            _ArticoliInOrdine_ArticoloSelezionatoCommand = new Command<object>(ArticoliInOrdine_ArticoloSelezionato);

            _SelezionaArticoli_AnnullaFiltriFamigliaClasseNatureCommand = new Command(SelezionaArticoli_AnnullaFiltriFamigliaClasseNature,() =>!IsBusy);
            _SelezionaArticoli_ReimpostaFiltriFamigliaClasseNatureCommand = new Command(SelezionaArticoli_SelezionaTuttiFiltriFamigliaClasseNature, () => !IsBusy);

            _ArticoliInOrdine_AnnullaFiltriFamigliaClasseNatureCommand = new Command(ArticoliInOrdine_AnnullaFiltriFamigliaClasseNature, () => !IsBusy);
            _ArticoliInOrdine_ReimpostaFiltriFamigliaClasseNatureCommand = new Command(ArticoliInOrdine_SelezionaTuttiFiltriFamigliaClasseNature, () => !IsBusy);

            _ArticoliInOrdine_SelezionaTuttiGliarticoliVisualizzatiCommand = new Command(ArticoliInOrdine_SelezionaTuttiGLiarticoliVisualizzati, () => !IsBusy);

            _ArticoliInOrdine_DeSelezionaTuttiGliarticoliVisualizzatiCommand = new Command(ArticoliInOrdine_DeSelezionaTuttiGLiarticoliVisualizzati, () => !IsBusy);

            _GEST_Anagrafica_ClienteSelezionato = null;
            _GEST_Clienti_Anagrafica_IndirizziSelezionato = null;

            IsInitialized = false;

			_navi = navi;

			_ListaFamigliePerSelezioneArticoli = new ObservableCollectionWithEvents<SelezioneListView>();
            _ListaClassiPerSelezioneArticoli = new ObservableCollectionWithEvents<SelezioneListView>();
            _ListaNaturePerSelezioneArticoli = new ObservableCollectionWithEvents<SelezioneListView>();


            _ListaFamigliePerArticoliInOrdine = new ObservableCollectionWithEvents<SelezioneListView>();
            _ListaClassiPerArticoliInOrdine = new ObservableCollectionWithEvents<SelezioneListView>();
            _ListaNaturePerArticoliInOrdine = new ObservableCollectionWithEvents<SelezioneListView>();

            _ListaIndirizziDiSpedizione = new ObservableCollection<GEST_Clienti_Anagrafica_Indirizzi>();

            ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(true);
            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);
            


        }
        


        #region Proprietà Command

        public ICommand SelezionaCliente_ClienteSelezionatoCommand
        {
            get
            {
                return _SelezCliente_ClienteSelezionatoCommand;
            }
        }


        public ICommand SelezCliente_IndirizzoSelezionatoCommand
        {
            get
            {
                return _SelezCliente_IndirizzoSelezionatoCommand;
            }
        }

        public ICommand ArticoliInOrdine_ArticoloSelezionatoCommand
        {
            get
            {
                return _ArticoliInOrdine_ArticoloSelezionatoCommand;
            }
        }


        public ICommand SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnUpCommand { 
			get { return _SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnUpCommand; } }

		public ICommand SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnDownCommand { 
			get { return _SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnDownCommand; } }



        public ICommand SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand
        {
            get { return _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand; }
        }

        public ICommand SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand
        {
            get { return _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand; }
        }

        public ICommand SelezionaArticoli_AnnullaFiltriFamigliaClasseCommand
        {
            get { return _SelezionaArticoli_AnnullaFiltriFamigliaClasseNatureCommand; }
        }

        public ICommand SelezionaArticoli_ReimpostaFiltriFamigliaClasseCommand
        {
            get { return _SelezionaArticoli_ReimpostaFiltriFamigliaClasseNatureCommand; }
        }

        public ICommand ArticoliInOrdine_AnnullaFiltriFamigliaClasseCommand
        {
            get { return _ArticoliInOrdine_AnnullaFiltriFamigliaClasseNatureCommand; }
        }

        public ICommand ArticoliInOrdine_ReimpostaFiltriFamigliaClasseCommand
        {
            get { return _ArticoliInOrdine_ReimpostaFiltriFamigliaClasseNatureCommand; }
        }

        public ICommand ArticoliInOrdine_SelezionaTuttiGliarticoliVisualizzatiCommand
        {
            get { return _ArticoliInOrdine_SelezionaTuttiGliarticoliVisualizzatiCommand; }
        }

        public ICommand ArticoliInOrdine_DeSelezionaTuttiGliarticoliVisualizzatiCommand
        {
            get { return _ArticoliInOrdine_DeSelezionaTuttiGliarticoliVisualizzatiCommand; }
        }


        public ICommand ArticoliInOrdine_CancellaSelezionatiCommand
        {
            get { return _ArticoliInOrdine_CancellaSelezionatiCommand; }
        }

        public ICommand SelezionaArticoli_AggiungiRigheAdOrdineCommand { 
			get { return _SelezionaArticoli_AggiungiRigheAdOrdineCommand; } }

        public ICommand SalvaOrdineInCorsoCommand {
            get { return _SalvaOrdineInCorsoCommand; } }


        public ICommand CancellaOrdineInCorsoCommand
        {
            get { return _CancellaOrdineInCorsoCommand; }
        }

        public ICommand ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDownCommand
        {
            get { return _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDownCommand; }
        }

        public ICommand ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUpCommand
        {
            get { return _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUpCommand; }
        }

        public ICommand ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand
        {
            get { return _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand; }
        }

        public ICommand ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand
        {
            get { return _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand; }
        }

        public ICommand ArticoliInOrdineItemCell_CancellaItemCommand
        {
            get { return _ArticoliInOrdineItemCell_CancellaItemCommand; }
        }

        public ICommand ArticoliInOrdine_NuovaQtaBtnUpCommand
        {
            get { return _ArticoliInOrdine_NuovaQtaBtnUpCommand; }
        }

        public ICommand ArticoliInOrdine_NuovaQtaBtnDownCommand
        {
            get { return _ArticoliInOrdine_NuovaQtaBtnDownCommand; }
        }

        public ICommand ArticoliInOrdine_NuovaQtaScontoMerceBtnUpCommand
        {
            get { return _ArticoliInOrdine_NuovaQtaScontoMerceBtnUpCommand; }
        }

        public ICommand ArticoliInOrdine_NuovaQtaScontoMerceBtnDownCommand
        {
            get { return _ArticoliInOrdine_NuovaQtaScontoMerceBtnDownCommand; }
        }

        public ICommand ArticoliInOrdine_ConfermaModificaMassivaCommand
        {
            get { return _ArticoliInOrdine_ConfermaModificaMassivaCommand; }
        }

        #endregion


        #region Metodi Privati

		//Si usa questo pattern per minimizzare il fatto di fare multi-subscribe all'evento
        private void ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(bool _Connetti)
        {
			_ListaFamigliePerSelezioneArticoli.ListItemChanged -= SelezArticoli_CambioFiltroFamiglieNature;
			_ListaClassiPerSelezioneArticoli.ListItemChanged -= SelezArticoli_CambioFiltroClassi;
			_ListaNaturePerSelezioneArticoli.ListItemChanged -= SelezArticoli_CambioFiltroFamiglieNature;


            if (_Connetti)
            {
                _ListaFamigliePerSelezioneArticoli.ListItemChanged += SelezArticoli_CambioFiltroFamiglieNature;
                _ListaClassiPerSelezioneArticoli.ListItemChanged += SelezArticoli_CambioFiltroClassi;
                _ListaNaturePerSelezioneArticoli.ListItemChanged += SelezArticoli_CambioFiltroFamiglieNature;
            }
            else
            {
//                _ListaFamigliePerSelezioneArticoli.ListItemChanged -= SelezArticoli_CambioFiltroFamiglieNature;
//                _ListaClassiPerSelezioneArticoli.ListItemChanged -= SelezArticoli_CambioFiltroClassi;
//                _ListaNaturePerSelezioneArticoli.ListItemChanged -= SelezArticoli_CambioFiltroFamiglieNature;
            }
        }

		//Si usa questo pattern per minimizzare il fatto di fare multi-subscribe all'evento
        private void ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(bool _Connetti)
        {
			_ListaFamigliePerArticoliInOrdine.ListItemChanged -= ArticoliInOrdine_CambioFiltroFamiglieNature;
			_ListaClassiPerArticoliInOrdine.ListItemChanged -= ArticoliInOrdine_CambioFiltroClassi;
			_ListaNaturePerArticoliInOrdine.ListItemChanged -= ArticoliInOrdine_CambioFiltroFamiglieNature;

            if (_Connetti)
            {
                _ListaFamigliePerArticoliInOrdine.ListItemChanged += ArticoliInOrdine_CambioFiltroFamiglieNature;
                _ListaClassiPerArticoliInOrdine.ListItemChanged += ArticoliInOrdine_CambioFiltroClassi;
                _ListaNaturePerArticoliInOrdine.ListItemChanged += ArticoliInOrdine_CambioFiltroFamiglieNature;
            }
            else
            {
//                _ListaFamigliePerArticoliInOrdine.ListItemChanged -= ArticoliInOrdine_CambioFiltroFamiglieNature;
//                _ListaClassiPerArticoliInOrdine.ListItemChanged -= ArticoliInOrdine_CambioFiltroClassi;
//                _ListaNaturePerArticoliInOrdine.ListItemChanged -= ArticoliInOrdine_CambioFiltroFamiglieNature;
            }
        }



        private void SelezionaArticoli_AnnullaFiltriFamigliaClasseNature()
        {
            if (IsBusy)
                return;

            ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(false);

			if (FamiglieVisibilePerSelezioneArticoli)
            {
				foreach (var item in _ListaFamigliePerSelezioneArticoli)
            {
                item.IsSelected = false;
            }
				_ListaClassiPerSelezioneArticoli.Clear ();
				_ListaNaturePerSelezioneArticoli.Clear ();
			}else
			{


				foreach (var item in _ListaClassiPerSelezioneArticoli)
            {
                item.IsSelected = false;
            }

				_ListaNaturePerSelezioneArticoli.Clear ();


			}
				
            ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(true);
            SelezArticoli_RicercaArticoli();


        }


        private void SelezionaArticoli_SelezionaTuttiFiltriFamigliaClasseNature()
        {
            //Elimino l'handler per poi riconnetterlo altrimenti per ogni variuazione riesegue il filtro

            if (IsBusy)
                return;

            ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(false);


            foreach (var item in ListaClassiPerSelezioneArticoli)
            {
                item.IsSelected = true;
            }
            foreach (var item in ListaFamigliePerSelezioneArticoli)
            {
                item.IsSelected = true;
            }

            foreach (var item in ListaNaturePerSelezioneArticoli)
            {
                item.IsSelected = true;
            }
			//così ricarica quanto manca
			SelezArticoli_CambioFiltroClassi();
            ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(true);
            if (IsInitialized)
                SelezArticoli_RicercaArticoli();


        }



        private void ArticoliInOrdine_AnnullaFiltriFamigliaClasseNature()
        {
            if (IsBusy)
                return;

            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(false);

			if (FamiglieVisibilePerSelezioneArticoli)
			{
				foreach (var item in _ListaFamigliePerArticoliInOrdine)
				{
					item.IsSelected = false;
				}

				_ListaClassiPerArticoliInOrdine.Clear();
				_ListaNaturePerArticoliInOrdine.Clear ();


	            foreach (var item in _ListaClassiPerArticoliInOrdine)
	            {
	                item.IsSelected = false;
	            }
			}else 
			{
				if (ClassiVisibilePerSelezioneArticoli)
				{
					foreach (var item in _ListaClassiPerArticoliInOrdine)
					{
						item.IsSelected = false;
					}

					
					_ListaNaturePerArticoliInOrdine.Clear ();


					foreach (var item in _ListaClassiPerArticoliInOrdine)
					{
						item.IsSelected = false;
					}
				}
			}

            OnPropertyChanged("ListaFamigliePerArticoliInOrdine");
            OnPropertyChanged("ListaClassiPerArticoliInOrdine");
            OnPropertyChanged("ListaNaturePerArticoliInOrdine");

            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);
			if (IsInitialized)
				ArticoliInOrdine_RicercaArticoli();
        }


        private void ArticoliInOrdine_SelezionaTuttiFiltriFamigliaClasseNature()
        {
            //Elimino l'handler per poi riconnetterlo altrimenti per ogni variuazione riesegue il filtro

            if (IsBusy)
                return;

            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(false);

            foreach (var item in ListaFamigliePerArticoliInOrdine)
            {
                item.IsSelected = true;
            }

            foreach (var item in ListaClassiPerArticoliInOrdine)
            {
                item.IsSelected = true;
            }

            foreach (var item in ListaNaturePerArticoliInOrdine)
            {
                item.IsSelected = true;
            }

			//Così ricarica quanto manca
            ArticoliInOrdineFiltri_CambioFiltroClassi();

            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);

           
            if (IsInitialized)
                ArticoliInOrdine_RicercaArticoli();


        }

        private void ArticoliInOrdine_SelezionaTuttiGLiarticoliVisualizzati()
        {
            //Elimino l'handler per poi riconnetterlo altrimenti per ogni variuazione riesegue il filtro

            if (IsBusy)
                return;

            foreach(var item in ListaArticoliInOrdineFiltrato)
            {

                item.IsSelected = true;


            }

        }

        private void ArticoliInOrdine_DeSelezionaTuttiGLiarticoliVisualizzati()
        {
            //Elimino l'handler per poi riconnetterlo altrimenti per ogni variuazione riesegue il filtro

            if (IsBusy)
                return;

            foreach (var item in ListaArticoliInOrdineFiltrato)
            {

                item.IsSelected = false;


            }
        }

        private async void ArticoliInOrdine_CancellaSelezionati()
        {

            var res = await App.Current.MainPage.DisplayAlert("Articoli in ordine", "Si vuole procedere ?", "Si", "No");

            if (!res)
                return;

                foreach (var item in ListaArticoliInOrdineFiltrato.ToList())
            {
                if (!item.IsSelected)
                    continue;

                ListaArticoliInOrdineFiltrato.Remove(item);
                _ListaArticoliInOrdine.Remove(item);



                //if (ArticoliInOrdine_NuovaQta > 0)
                //    item.Qta = ArticoliInOrdine_NuovaQta;

                //if (ArticoliInOrdine_NuovaQtaScontoMerce > 0)
                //    item.NCP_QtaScontoMerce = ArticoliInOrdine_NuovaQtaScontoMerce;

                //if (ArticoliInOrdine_NuovoPercSconto1 > 0)
                //    item.Sc1 = ArticoliInOrdine_NuovoPercSconto1;

                //if (ArticoliInOrdine_NuovoPercSconto2 > 0)
                //    item.Sc2 = ArticoliInOrdine_NuovoPercSconto2;


                //if (ArticoliInOrdine_ConsideraNuovaDataConsegna)
                //    item.DataPresuntaConsegna = ArticoliInOrdine_NuovaDataConsegna;

                //item.IsSelected = false;
            }


            //Elimino l'handler per poi riconnetterlo altrimenti per ogni variuazione riesegue il filtro

            //if (IsBusy)
            //    return;

            //foreach (var item in ListaArticoliInOrdineFiltrato)
            //{

            //    item.IsSelected = false;


            //}
            OnPropertyChanged("ListaArticoliInOrdineFiltrato");
        }
        private void SelezArticoli_CambioFiltroFamiglieNature(object sender, PropertyChangedEventArgs e)
        {
            SelezArticoli_RicercaArticoli();
        }


        private  void ArticoliInOrdine_CambioFiltroFamiglieNature(object sender, PropertyChangedEventArgs e)
        {
            ArticoliInOrdine_RicercaArticoli();
        }

		async private void SelezArticoli_CambioFiltroClassi()
		{
			//Devo sistemare le nature
			//recupero la lista Classi che sono usate nella lista articoli in ordine


			string filtroclassi = string.Empty;
			foreach (var item in ListaClassiPerSelezioneArticoli)
			{
				if (item.IsSelected)
				{
					filtroclassi = (filtroclassi.Length > 0 ? filtroclassi + "," : string.Empty) + item.Item;

				}
			}

			ListaNaturePerSelezioneArticoli.Clear();

			if (filtroclassi.Length > 0)
			{
				ObservableCollection<GEST_Articoli_Nature> _GEST_Articoli_Nature_local = 
					(await _DataClient.GetGEST_Articoli_NatureAsync_SearchAsync(filtroclassi)).ToObservableCollection();
				foreach (var id in _GEST_Articoli_Nature_local)
				{
					
					//Testo che non ci sia già 

					if ((from p in ListaNaturePerSelezioneArticoli
						where p.Item == id.CodNatura
						select p).Count() <= 0)
					{

						ListaNaturePerSelezioneArticoli.Add(new SelezioneListView() { Item = id.CodNatura, IsSelected = true,Descrizione=id.Descrizione });
					}


				};
			}

			ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);


		}
        private async void SelezArticoli_CambioFiltroClassi(object sender, PropertyChangedEventArgs e)
        {

			if (IsBusy)
				return;
			ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(false);
			SelezArticoli_CambioFiltroClassi();
			ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(true);
			await SelezArticoli_RicercaArticoli();

        }


        //Occhio a disabilitare l'handler  ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(false) quando si richiama
        async private Task ArticoliInOrdineFiltri_CambioFiltroClassi()
        {
            //Devo sistemare le nature
            //recupero la lista Classi che sono usate nella lista articoli in ordine


            var ClassiDaCaricare = from item1 in ListaClassiPerArticoliInOrdine.ToList()
                                    where (_ListaArticoliInOrdine.Any(item2 => item2.CodClasse == item1.Item))
                                    select item1;

            string filtroclassi = string.Empty;
            foreach (var item in ClassiDaCaricare)
            {
                if (item.IsSelected)
                {
                    filtroclassi = (filtroclassi.Length > 0 ? filtroclassi + "," : string.Empty) + item.Item;

                }
            }

            ListaNaturePerArticoliInOrdine.Clear();

            if (filtroclassi.Length > 0)
            {
                ObservableCollection<GEST_Articoli_Nature> _GEST_Articoli_Nature_local = 
					(await _DataClient.GetGEST_Articoli_NatureAsync_SearchAsync(filtroclassi)).ToObservableCollection();
                foreach (var id in _GEST_Articoli_Nature_local)
                {
                    //Se c' qualche articolo che la usa la carico altrimenti no
                    GEST_Ordini_Righe_DettaglioOrdine _GEST_Ordini_Righe_DettaglioOrdine = _ListaArticoliInOrdine.Where(x => x.CodNatura.ToLower() == id.CodNatura.ToLower()).FirstOrDefault();
                    if (_GEST_Ordini_Righe_DettaglioOrdine!=null)
                    {
                        //Testo che non ci sia già 

                        if ((from p in ListaNaturePerArticoliInOrdine
                             where p.Item == id.CodNatura
                             select p).Count() <= 0)
                        {

							ListaNaturePerArticoliInOrdine.Add(new SelezioneListView() { Item = id.CodNatura, IsSelected = true, Descrizione=id.Descrizione });
                        }
                    }

                };
            }

            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);

        }


        async private void ArticoliInOrdine_CambioFiltroClassi(object sender, PropertyChangedEventArgs e)
        {
			if (IsBusy)
				return;

            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(false);
            await ArticoliInOrdineFiltri_CambioFiltroClassi();
            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);
            await ArticoliInOrdine_RicercaArticoli();

        }


        private void SelezionaArticoli_QtaArticoliDaOrdinareBtnDown(GEST_Articoli_Anagrafica_SelezioneArticolo _Articolo)
        {
            if (_Articolo.QtaDaOrdinare > 0)
            {
                _Articolo.QtaDaOrdinare = _Articolo.QtaDaOrdinare - 1;
            }
        }

        private void SelezionaArticoli_QtaArticoliDaOrdinareBtnUp(GEST_Articoli_Anagrafica_SelezioneArticolo _Articolo)
        {
            _Articolo.QtaDaOrdinare = _Articolo.QtaDaOrdinare + 1;
        }

        private void SelezionaArticoli_QtaScontoMerceArticoliDaOrdinareBtnUp(GEST_Articoli_Anagrafica_SelezioneArticolo _Articolo)
        {
            _Articolo.NCP_QtaScontoMerce = _Articolo.NCP_QtaScontoMerce + 1;
        }

        private void SelezionaArticoli_QtaScontoMerceArticoliDaOrdinareBtnDown(GEST_Articoli_Anagrafica_SelezioneArticolo _Articolo)
        {
            if (_Articolo.NCP_QtaScontoMerce > 0)
            {
                _Articolo.NCP_QtaScontoMerce = _Articolo.NCP_QtaScontoMerce - 1;
            }
        }


        private void ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDown(GEST_Ordini_Righe_DettaglioOrdine _Articolo)
        {
            if (_Articolo.Qta > 0)
            {
                _Articolo.Qta = _Articolo.Qta - 1;
            }
        }

        private void ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUp(GEST_Ordini_Righe_DettaglioOrdine _Articolo)
        {
            _Articolo.Qta = _Articolo.Qta + 1;
        }


        private void ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDown(GEST_Ordini_Righe_DettaglioOrdine _Articolo)
        {
            if (_Articolo.NCP_QtaScontoMerce > 0)
            {
                _Articolo.NCP_QtaScontoMerce = _Articolo.NCP_QtaScontoMerce - 1;
            }
        }

        private void ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUp(GEST_Ordini_Righe_DettaglioOrdine _Articolo)
        {
            _Articolo.NCP_QtaScontoMerce = _Articolo.NCP_QtaScontoMerce + 1;
        }

        private async void ArticoliInOrdine_NuovaQtaBtnUp()
        {

            ArticoliInOrdine_NuovaQta= ArticoliInOrdine_NuovaQta + 1;
            

        }

        private async void ArticoliInOrdine_NuovaQtaBtnDown()
        {
            if (ArticoliInOrdine_NuovaQta > 0)
            {
                ArticoliInOrdine_NuovaQta = ArticoliInOrdine_NuovaQta - 1;
            }

        }

        private async void ArticoliInOrdine_NuovaQtaScontoMerceBtnUp()
        {
            ArticoliInOrdine_NuovaQtaScontoMerce = ArticoliInOrdine_NuovaQtaScontoMerce + 1;
        }

        private async  void ArticoliInOrdine_NuovaQtaScontoMerceBtnDown()
        {
            if (ArticoliInOrdine_NuovaQtaScontoMerce > 0)
            {
                ArticoliInOrdine_NuovaQtaScontoMerce = ArticoliInOrdine_NuovaQtaScontoMerce - 1;
            }

        }

        private async void ArticoliInOrdine_ConfermaModificaMassiva()
        {

            var res = await App.Current.MainPage.DisplayAlert("Modifica Massiva", "Si vuole procedere ?", "Si", "No");

            if (!res)
                return;

            foreach (var item in ListaArticoliInOrdineFiltrato)
            {
                if (!item.IsSelected)
                    continue;
                if (ArticoliInOrdine_NuovaQta>0)
                    item.Qta = ArticoliInOrdine_NuovaQta;

                if (ArticoliInOrdine_NuovaQtaScontoMerce>0)
                    item.NCP_QtaScontoMerce = ArticoliInOrdine_NuovaQtaScontoMerce;

                if (ArticoliInOrdine_NuovoPercSconto1 > 0)
                    item.Sc1 = ArticoliInOrdine_NuovoPercSconto1;

                if (ArticoliInOrdine_NuovoPercSconto2 > 0)
                    item.Sc2 = ArticoliInOrdine_NuovoPercSconto2;


                if (ArticoliInOrdine_ConsideraNuovaDataConsegna)
                    item.DataPresuntaConsegna = ArticoliInOrdine_NuovaDataConsegna;

                item.IsSelected = false;
            }

        }

        private async void SelezCliente_ClienteSelezionato(object sender)
        {
            IsBusy = true;
            ChangeCanExecuteGlobale();
            _GEST_Anagrafica_ClienteSelezionato = ((GEST_Clienti_Anagrafica)sender);
            RagioneSocialeClienteSelezionato = _GEST_Anagrafica_ClienteSelezionato.RagioneSociale;

            if (_GEST_Anagrafica_ClienteSelezionato.CodPagamento.Length>0)
            {
                GEST_Condizione_Pagamento GEST_Condizione_Pagamento_Selez= (await _DataClient.GetGEST_Condizione_PagamentoAsync(false)).FirstOrDefault(i => i.CodPagamento.ToLower().Equals(_GEST_Anagrafica_ClienteSelezionato.CodPagamento.ToLower()));

                if (GEST_Condizione_Pagamento_Selez!=null)
                {
                    CondizionePagamentoImpostata = GEST_Condizione_Pagamento_Selez.CodPagamento.Replace('-', ' ') + "-" + GEST_Condizione_Pagamento_Selez.Descrizione.Replace('-', ' ');

                 }
            }

            PercSconto1Default = _GEST_Anagrafica_ClienteSelezionato.PercSconto1;

            //Imposto la lista degli indirizzi di spedizione
            ListaIndirizziDiSpedizione.Clear();
            List<GEST_Clienti_Anagrafica_Indirizzi> _GEST_Clienti_Anagrafica_Indirizzi = (await _DataClient.GetGEST_Clienti_Anagrafica_IndirizziAsync(false)).Where(x=>x.IDAnagrafica== _GEST_Anagrafica_ClienteSelezionato.IDAnagrafica).ToList();

            foreach (var item in _GEST_Clienti_Anagrafica_Indirizzi)
            {
                ListaIndirizziDiSpedizione.Add(new GEST_Clienti_Anagrafica_Indirizzi() {
                    IdIndirizzo =item.IdIndirizzo,
                    RagioneSociale=item.RagioneSociale,
                    Indirizzo = item.Indirizzo,
                    Citta =item.Citta});
        }
            OnPropertyChanged("ListaIndirizziDiSpedizione");


            //Caso particolare: l'indirizzo è solo uno per cui lo autoseleziono
            if (ListaIndirizziDiSpedizione.Count==1)
            {
                _GEST_Clienti_Anagrafica_IndirizziSelezionato = ListaIndirizziDiSpedizione[0];
            }
            else
            {
                _GEST_Clienti_Anagrafica_IndirizziSelezionato = null;
            }

            OnPropertyChanged("IndirizzoSpedizioneSelezionato");

            IsBusy = false;
            ChangeCanExecuteGlobale();
        }

        private async void SelezCliente_IndirizzoSelezionato(object sender)
        {
            if (sender!=null)
                _GEST_Clienti_Anagrafica_IndirizziSelezionato = ((GEST_Clienti_Anagrafica_Indirizzi)sender);

            OnPropertyChanged("IndirizzoSpedizioneSelezionato");



        }

        private async void ArticoliInOrdine_ArticoloSelezionato(object sender)
        {

            ((GEST_Ordini_Righe_DettaglioOrdine)sender).IsSelected = !((GEST_Ordini_Righe_DettaglioOrdine)sender).IsSelected;

        }

        private async void ArticoliInOrdineItemCell_CancellaItem(object sender)
        {
            var res = await App.Current.MainPage.DisplayAlert("Articoli in ordine", "Si vuole cancellare la riga corrente ?", "Si", "No"); 

            if (res)
            { 
                ChangeCanExecuteGlobale();
				GEST_Ordini_Righe_DettaglioOrdine _GEST_Ordini_Righe_DettaglioOrdine_InCancellazione = (GEST_Ordini_Righe_DettaglioOrdine)sender;
				string _CodArtArticoloInCancellazione = _GEST_Ordini_Righe_DettaglioOrdine_InCancellazione.CodArt;

//                if (IsInEdit)
//                    ((GEST_Ordini_Righe_DettaglioOrdine)sender).Deleted = true;

				ListaArticoliInOrdineFiltrato.Remove(_GEST_Ordini_Righe_DettaglioOrdine_InCancellazione);
				_ListaArticoliInOrdine.Remove(_GEST_Ordini_Righe_DettaglioOrdine_InCancellazione);
                await AggiornaOrdinato(_CodArtArticoloInCancellazione);

				//Ora verifico se cancellare la relativa famiglia/clsse/natura nella lista
				ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(false);

				var FamiglieDaEliminare = from item1 in ListaFamigliePerArticoliInOrdine.ToList()
						where !(_ListaArticoliInOrdine.Any(item2 => item2.CodFamiglia == item1.Item))
					select item1;

				foreach (var itemdaeliminare in FamiglieDaEliminare)
				{
					ListaFamigliePerArticoliInOrdine.Remove(itemdaeliminare);
            }

				var ClassiDaEliminare = from item1 in ListaClassiPerArticoliInOrdine.ToList()
						where !(_ListaArticoliInOrdine.Any(item2 => item2.CodClasse == item1.Item))
					select item1;

				foreach (var itemdaeliminare in ClassiDaEliminare)
				{
					ListaClassiPerArticoliInOrdine.Remove(itemdaeliminare);
        }

				var NatureDaEliminare = from item1 in ListaNaturePerArticoliInOrdine.ToList()
						where !(_ListaArticoliInOrdine.Any(item2 => item2.CodNatura == item1.Item))
					select item1;
				foreach (var itemdaeliminare in NatureDaEliminare)
				{
					ListaNaturePerArticoliInOrdine.Remove(itemdaeliminare);

				}

				OnPropertyChanged("ListaFamigliePerArticoliInOrdine");
				OnPropertyChanged("ListaClassiPerArticoliInOrdine");
				OnPropertyChanged("ListaNaturePerArticoliInOrdine");
       
				ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);

                ChangeCanExecuteGlobale();
            }
        }

        private async Task SelezCliente_RicercaCliente()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            cancellationTokenSource = new CancellationTokenSource();
            var cancellationTokenSourceLocal = cancellationTokenSource;

            if (cancellationTokenSourceLocal.IsCancellationRequested)
			{
                return;
			}

			IsBusy = true;
            ListaAnagrafica= _ListaAnagraficaOriginale.Where(x => x.RagioneSociale.ToLower().Contains(SelezionaCliente_TestoRicerca.ToLower()) ||
                             x.Indirizzo.ToLower().Contains(SelezionaCliente_TestoRicerca.ToLower())).OrderBy(b => b.RagioneSociale).ToObservableCollection();

            //ListaAnagrafica = (await _DataClient.Get_GEST_Clienti_Anagrafica_SearchAsync(0,SelezionaCliente_TestoRicerca)).ToObservableCollection();
            //OnPropertyChanged("ListaAnagrafica");
            //ListaAnagrafica = result.ToObservableCollection();
            //ListaAnagrafica = result.ToList();
            IsBusy = false;
        }


        private void ChangeCanExecuteGlobale()
        {
            _SelezionaArticoli_AggiungiRigheAdOrdineCommand.ChangeCanExecute();
            _SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnDownCommand.ChangeCanExecute();
            _SelezionaArticoliItemCell_QtaArticoliDaOrdinareBtnUpCommand.ChangeCanExecute();

            _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand.ChangeCanExecute();
            _SelezionaArticoliItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand.ChangeCanExecute();

            _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnDownCommand.ChangeCanExecute();
            _ArticoliInOrdineItemCell_QtaArticoliDaOrdinareBtnUpCommand.ChangeCanExecute();
            _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnDownCommand.ChangeCanExecute();
            _ArticoliInOrdineItemCell_QtaScontoMerceArticoliDaOrdinareBtnUpCommand.ChangeCanExecute();
            _ArticoliInOrdine_NuovaQtaBtnUpCommand.ChangeCanExecute();
            _ArticoliInOrdine_NuovaQtaBtnDownCommand.ChangeCanExecute(); 
            _ArticoliInOrdine_NuovaQtaScontoMerceBtnUpCommand.ChangeCanExecute(); 
            _ArticoliInOrdine_NuovaQtaScontoMerceBtnDownCommand.ChangeCanExecute();
            _ArticoliInOrdine_ConfermaModificaMassivaCommand.ChangeCanExecute();
            _ArticoliInOrdineItemCell_CancellaItemCommand.ChangeCanExecute();
            _SelezionaArticoli_AnnullaFiltriFamigliaClasseNatureCommand.ChangeCanExecute();
            _SelezionaArticoli_ReimpostaFiltriFamigliaClasseNatureCommand.ChangeCanExecute();
            _ArticoliInOrdine_AnnullaFiltriFamigliaClasseNatureCommand.ChangeCanExecute();
            _ArticoliInOrdine_ReimpostaFiltriFamigliaClasseNatureCommand.ChangeCanExecute();
            _ArticoliInOrdine_SelezionaTuttiGliarticoliVisualizzatiCommand.ChangeCanExecute();
            _ArticoliInOrdine_DeSelezionaTuttiGliarticoliVisualizzatiCommand.ChangeCanExecute();
            _ArticoliInOrdine_CancellaSelezionatiCommand.ChangeCanExecute();
            _SalvaOrdineInCorsoCommand.ChangeCanExecute();
            _CancellaOrdineInCorsoCommand.ChangeCanExecute();

            _SelezCliente_IndirizzoSelezionatoCommand.ChangeCanExecute();
        }

        private async Task SelezArticoli_RicercaArticoli()
		{
            if (!IsInitialized)
                return;


            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            cancellationTokenSource = new CancellationTokenSource();
            var cancellationTokenSourceLocal = cancellationTokenSource;

            IsBusy = true;

            ChangeCanExecuteGlobale();

            string filtrofamiglie = string.Empty;
            foreach (var item in ListaFamigliePerSelezioneArticoli)
            {
                if (item.IsSelected)
                {
                    filtrofamiglie= (filtrofamiglie.Length>0? filtrofamiglie + ",":string.Empty) + item.Item;
                }
            }

            string filtroclassi = string.Empty;
            foreach (var item in ListaClassiPerSelezioneArticoli)
            {
                if (item.IsSelected)
                {
                    filtroclassi = (filtroclassi.Length > 0 ? filtroclassi + ",":string.Empty) + item.Item;

                }
            }


            string filtronature = string.Empty;
            foreach (var item in ListaNaturePerSelezioneArticoli)
            {
                if (item.IsSelected)
                {
                    filtronature = (filtronature.Length > 0 ? filtronature + "," : string.Empty) + item.Item;

                }
            }

            ObservableCollection<GEST_Articoli_Anagrafica> _ListaArticoliNormale =
            (await _DataClient.Get_GEST_Articoli_Anagrafica_SearchAsync(SelezArticoli_TestoRicerca, filtrofamiglie, filtroclassi, filtronature)).ToObservableCollection();

			if (cancellationTokenSourceLocal.IsCancellationRequested)
				return;
			
            await ReimpostaListaArticoli(_ListaArticoliNormale);
            
            IsBusy = false;
            ChangeCanExecuteGlobale();
            await AggiornaDataConsegnaDefault();
            await AggiornaOrdinato(string.Empty);
            //await AggiornaPrezzoEsconti();

        }

        private async Task ArticoliInOrdine_RicercaArticoli()
        {
            if (!IsInitialized)
                return;

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            cancellationTokenSource = new CancellationTokenSource();
            var cancellationTokenSourceLocal = cancellationTokenSource;

            IsBusy = true;


            ChangeCanExecuteGlobale();

            string filtrofamiglie = string.Empty;
            foreach (var item in ListaFamigliePerArticoliInOrdine)
            {
                if (item.IsSelected)
                {
                    filtrofamiglie = (filtrofamiglie.Length > 0 ? filtrofamiglie + "," : string.Empty) + item.Item;

                }
            }

            string filtroclassi = string.Empty;
            foreach (var item in ListaClassiPerArticoliInOrdine)
            {
                if (item.IsSelected)
                {
                    filtroclassi = (filtroclassi.Length > 0 ? filtroclassi + "," : string.Empty) + item.Item;

                }
            }


            string filtronature = string.Empty;
            foreach (var item in ListaNaturePerArticoliInOrdine)
            {
                if (item.IsSelected)
                {
                    filtronature = (filtronature.Length > 0 ? filtronature + "," : string.Empty) + item.Item;

                }
            }

            ListaArticoliInOrdineFiltrato.Clear();

            //Caso particolare
            if (( (filtrofamiglie.Length>0) | (filtroclassi.Length>0) | (filtronature.Length > 0) ))
            { 

				IEnumerable<GEST_Ordini_Righe_DettaglioOrdine> _GEST_Ordini_Righe_DettaglioOrdineLocal =_ListaArticoliInOrdine;
                

				if (!String.IsNullOrEmpty (_ArticoliInOrdine_TestoRicerca)) {
					_GEST_Ordini_Righe_DettaglioOrdineLocal = _ListaArticoliInOrdine.Where (x => (x.CodArt.ToLower ().Contains (_ArticoliInOrdine_TestoRicerca.ToLower ()) ||
						x.Descrizione.ToLower ().Contains (_ArticoliInOrdine_TestoRicerca.ToLower ()) && x.Deleted != true));

				} 


                if (filtrofamiglie.Length > 0)
                {
                    string[] _Fam = filtrofamiglie.Split(',');
					_GEST_Ordini_Righe_DettaglioOrdineLocal = _GEST_Ordini_Righe_DettaglioOrdineLocal.Where(x => _Fam.Contains(x.CodFamiglia));

                }

                if (filtroclassi.Length > 0)
                {
                    string[] _Class = filtroclassi.Split(',');
                    _GEST_Ordini_Righe_DettaglioOrdineLocal = _GEST_Ordini_Righe_DettaglioOrdineLocal.Where(x => _Class.Contains(x.CodClasse));

                }

                if (filtronature.Length > 0)
                {
                    string[] _Nat = filtronature.Split(',');
                    _GEST_Ordini_Righe_DettaglioOrdineLocal = _GEST_Ordini_Righe_DettaglioOrdineLocal.Where(x => _Nat.Contains(x.CodNatura));

                }

                ListaArticoliInOrdineFiltrato = _GEST_Ordini_Righe_DettaglioOrdineLocal.ToObservableCollection();
            }
            if (cancellationTokenSourceLocal.IsCancellationRequested)
                return;


            IsBusy = false;
            ChangeCanExecuteGlobale();
            //await AggiornaDataConsegnaDefault();
            //await AggiornaOrdinato(string.Empty);
            //await AggiornaPrezzoEsconti();



        }

        //Data la lista degli articoli li mette nella lista articoli da visualizzare
		//impostando già prezzo e sconti
        private async Task ReimpostaListaArticoli(ObservableCollection<GEST_Articoli_Anagrafica> _ListaArticoliNormale)
        {

			ObservableCollection <GEST_Articoli_Listini> _List_GEST_Articoli_Listini_Selezionato = new ObservableCollection<GEST_Articoli_Listini> ();
			if (_GEST_Anagrafica_ClienteSelezionato != null)
			{	
				_List_GEST_Articoli_Listini_Selezionato = 
				(await _DataClient.Get_GEST_Articoli_Listino_SearchAsync(_GEST_Anagrafica_ClienteSelezionato.CodListino)).ToObservableCollection();

			}


            ListaArticoli.Clear();
            
            foreach (var id in _ListaArticoliNormale)
            {
				GEST_Articoli_Anagrafica_SelezioneArticolo _GEST_Articoli_Anagrafica_SelezioneArticolo =
					new GEST_Articoli_Anagrafica_SelezioneArticolo (id);
				_GEST_Articoli_Anagrafica_SelezioneArticolo.PercSconto1 = PercSconto1Default;

				GEST_Articoli_Listini _ListinoSelezionatoPerCodArt = _List_GEST_Articoli_Listini_Selezionato.FirstOrDefault(i => i.CodArt.Equals(id.CodArt));

				if (_ListinoSelezionatoPerCodArt!=null)
				{ 

					_GEST_Articoli_Anagrafica_SelezioneArticolo.ValUnit = _ListinoSelezionatoPerCodArt.Importo;
				}
				else
				{
					_GEST_Articoli_Anagrafica_SelezioneArticolo.ValUnit = 0;
				}

				ListaArticoli.Add(_GEST_Articoli_Anagrafica_SelezioneArticolo);
            };

            foreach (var articolo in ListaArticoliInOrdineFiltrato)
            {

                await AggiornaOrdinato(articolo.CodArt);
            }
        }

        private async void SalvaOrdineInCorso()
		{     
            if (_ListaArticoliInOrdine.Count()<=0)
            {
                await App.Current.MainPage.DisplayAlert("Salvataggio Ordine", "Nessun articolo in ordine", "Ok");
                return;
            }

            IsBusy = true;

            GEST_Ordini_Teste _New_GEST_Ordini_Teste;

            if (IsInEdit)
            {
                //_New_GEST_Ordini_Teste = (await _DataClient.GetGEST_Ordini_TesteAsync(false)).FirstOrDefault(x => x.Id == _IdDoc);
				_New_GEST_Ordini_Teste=await _DataClient.Get_GEST_Ordini_Teste_ByIdDoc(_IdDoc);
            }
            else
            {
                _New_GEST_Ordini_Teste = new GEST_Ordini_Teste();
                _New_GEST_Ordini_Teste.RagioneSociale = RagioneSocialeClienteSelezionato;
                _New_GEST_Ordini_Teste.PartitaIva = _GEST_Anagrafica_ClienteSelezionato.PartitaIva;
                _New_GEST_Ordini_Teste.CodiceFiscale = _GEST_Anagrafica_ClienteSelezionato.CodiceFiscale;
                _New_GEST_Ordini_Teste.DataDocumento = DateTime.Now;
                _New_GEST_Ordini_Teste.CloudState = (int)CloudState.inseritoEnonsincronizzato;
                _New_GEST_Ordini_Teste.IdAnagrafica = _GEST_Anagrafica_ClienteSelezionato.IDAnagrafica;
                _New_GEST_Ordini_Teste.DataConsegna = DataConsegnaDefault;
                _New_GEST_Ordini_Teste.IdDevice = CrossDeviceInfo.Current.Id;

                // Leggo l'IdAgente e la mail da cui è stato inserito l'ordine dalla tabella Authorization
                Authorization authorization = await _DataClient.GetAuthorizationAsync();

                if (authorization != null)
                {
                    _New_GEST_Ordini_Teste.IdAgente = authorization.IdAgente;
                    _New_GEST_Ordini_Teste.DeviceMail = authorization.DeviceMail;
                }

                if (CondizionePagamentoImpostata != null)
                {
                    var matrixcondizpagamento = CondizionePagamentoImpostata.Split('-');
                    _New_GEST_Ordini_Teste.CodPagamento = matrixcondizpagamento[0];
                }
            }
        
            _New_GEST_Ordini_Teste.Note = DatiOrdine_Note;

            _New_GEST_Ordini_Teste.NrRigheTot = _ListaArticoliInOrdine.Count();

            decimal sumLineTotal = (from od in _ListaArticoliInOrdine
                                    select od.Imponibile).Sum();

            //Calcolo la somma e salvo
            _New_GEST_Ordini_Teste.TotaleDocumento = sumLineTotal;
            _New_GEST_Ordini_Teste.TotaleConsegna = sumLineTotal;
            _New_GEST_Ordini_Teste.TotaleConsegna = sumLineTotal;

            if (_GEST_Clienti_Anagrafica_IndirizziSelezionato != null)
            { 
                _New_GEST_Ordini_Teste.IdIndSpedMerce = _GEST_Clienti_Anagrafica_IndirizziSelezionato.IdIndirizzo;
                _New_GEST_Ordini_Teste.RagSocSped = _GEST_Clienti_Anagrafica_IndirizziSelezionato.RagioneSociale;
                _New_GEST_Ordini_Teste.IndirizzoSped = _GEST_Clienti_Anagrafica_IndirizziSelezionato.Indirizzo;
                _New_GEST_Ordini_Teste.CittaSped = _GEST_Clienti_Anagrafica_IndirizziSelezionato.Citta;
                _New_GEST_Ordini_Teste.CapSped = _GEST_Clienti_Anagrafica_IndirizziSelezionato.Cap;
                _New_GEST_Ordini_Teste.ProvSped = _GEST_Clienti_Anagrafica_IndirizziSelezionato.Provincia;
            }

            try
            {
                await _DataClient.SaveGEST_Ordini_TesteAsync(_New_GEST_Ordini_Teste,false);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Errore Salvataggio Ordine", "Errore in fase di salvataggio " + e.Message, "OK");
                throw e;
            }
            finally
            {
                
            }

            int mIdRiga = 1;

            List<GEST_Ordini_Righe> TuttiGliArticoliInOrdineDaDB = new List<GEST_Ordini_Righe>() ;
            if (IsInEdit)
            {
                TuttiGliArticoliInOrdineDaDB.AddRange((await _DataClient.Get_GEST_Ordini_RigheAsync(_IdDoc, string.Empty)).ToList());
            }

            foreach (var item in _ListaArticoliInOrdine)
            {
                GEST_Ordini_Righe _GEST_Ordini_Righe;

                if (IsInEdit)
                {
                    if (item.Id == null)
                    {
                        //Nuova riga
                        _GEST_Ordini_Righe = new GEST_Ordini_Righe();
                    }
                    else
                    {
                        _GEST_Ordini_Righe=TuttiGliArticoliInOrdineDaDB.Where(x => x.Id == item.Id).FirstOrDefault();
                    }                           

                }
                else
                {
                    _GEST_Ordini_Righe = new GEST_Ordini_Righe();
                }

                _GEST_Ordini_Righe.CodArt = item.CodArt;
                _GEST_Ordini_Righe.Descrizione = item.Descrizione;
                _GEST_Ordini_Righe.CodUnMis = item.CodUnMis;
                _GEST_Ordini_Righe.CodIva = item.CodIva;

                if (item.NCP_QtaScontoMerce <= 0 && item.Qta <= 0)
                    continue;

                _GEST_Ordini_Righe.NCP_QtaScontoMerce = item.NCP_QtaScontoMerce;
                _GEST_Ordini_Righe.Qta = item.Qta;
                _GEST_Ordini_Righe.Sc1 = item.Sc1;
                _GEST_Ordini_Righe.Sc2 = item.Sc2;
                _GEST_Ordini_Righe.Sc3 = item.Sc3;
                _GEST_Ordini_Righe.Sc4 = item.Sc4;
                _GEST_Ordini_Righe.ValUnit = item.ValUnit;
                _GEST_Ordini_Righe.Imponibile = item.Imponibile;
                _GEST_Ordini_Righe.CloudState = (int)CloudState.inseritoEnonsincronizzato;

                //ImponibileTotale = ImponibileTotale + _GEST_Ordini_Righe.Imponibile;
                _GEST_Ordini_Righe.DataPresuntaConsegna = item.DataPresuntaConsegna;
                
                if (!_GEST_Ordini_Righe.DataPresuntaConsegna.HasValue)
                    _GEST_Ordini_Righe.DataPresuntaConsegna = DataConsegnaDefault;

                _GEST_Ordini_Righe.IdSlave = _New_GEST_Ordini_Teste.Id;
                _GEST_Ordini_Righe.IdRiga = mIdRiga;
                
                mIdRiga = mIdRiga + 1;
                _GEST_Ordini_Righe.TipoRiga = 1;

                await _DataClient.SaveGEST_Ordini_RigheAsync(_GEST_Ordini_Righe,false);
            }

            //Ora mi devo occupare delle righe ordine su db che non sono più nell'ordine
            if (IsInEdit)
            {
                var RigheOrdineDaCancellare = TuttiGliArticoliInOrdineDaDB.Where(p => !_ListaArticoliInOrdine.Any(p2 => p2.CodArt == p.CodArt));

                foreach (var itemDaCancellare in RigheOrdineDaCancellare)
                {
                    itemDaCancellare.CloudState = ((int)CloudState.cancellato);
                    await _DataClient.SaveGEST_Ordini_RigheAsync(itemDaCancellare);
                }
            }

            MessagingCenter.Send(this, "REFRESHLISTAORDINI");
            IsBusy = false;
            await App.Current.MainPage.DisplayAlert("Salvataggio Ordine", "Ordine correttamente salvato", "OK");
            await _navi.PopAsync();
		}

        private async void CancellaOrdineInCorso()
        {

            var res = await App.Current.MainPage.DisplayAlert("Ordine", "Si vuole cancellare l'ordine corrente ?", "Si", "No");

            if (!res)
            {
                return;
            }

            IsBusy = true;
            string GuidCorrente = string.Empty;

            GEST_Ordini_Teste _New_GEST_Ordini_Teste=  (await _DataClient.GetGEST_Ordini_TesteAsync(false)).FirstOrDefault(x => x.Id == _IdDoc);
            GuidCorrente = _New_GEST_Ordini_Teste.Id;

            _New_GEST_Ordini_Teste.CloudState = (int)CloudState.cancellato;

            await _DataClient.SaveGEST_Ordini_TesteAsync(_New_GEST_Ordini_Teste,false);


            GEST_Ordini_Teste _New_GEST_Ordini_Teste2 = (await _DataClient.GetGEST_Ordini_TesteAsync(false)).FirstOrDefault(x => x.Id == _IdDoc);

            //List<GEST_Ordini_Righe> TuttiGliArticoliInOrdine = new List<GEST_Ordini_Righe>();
            //if (IsInEdit)
            //{
            //    TuttiGliArticoliInOrdine.AddRange((await _DataClient.Get_GEST_Ordini_RigheAsync(_IdDoc, string.Empty)).ToList());
            //}


            foreach (var item in _ListaArticoliInOrdine)
            {
                item.CloudState = (int)CloudState.cancellato;
                await _DataClient.SaveGEST_Ordini_RigheAsync(item,false);
                

            }

            MessagingCenter.Send(this, "REFRESHLISTAORDINI");
            IsBusy = false;
            await _navi.PopAsync();
		}

        #endregion

        #region Metodi Pubblici
        public async Task AggiornaDataConsegnaDefault()
        {
            //ChangeCanExecuteGlobale();
            //if (IsBusy)
            //    return;

            //IsBusy = true;

            //foreach (GEST_Articoli_Anagrafica_SelezioneArticolo id in _ListaArticoli)
            //{
            //    //id.DataConsegna = DataConsegnaDefault;

            //}

            //IsBusy = false;
            //ChangeCanExecuteGlobale();
        }

        public async Task AggiornaListaArticoliRaggruppata()
        {
            ChangeCanExecuteGlobale();
            if (IsBusy)
                return;

            IsBusy = true;
            _ListaArticoliInOrdineRaggruppatoPerClasse.Clear();

            var sorted = (from ArticoloInOrdine in _ListaArticoliInOrdine
                         orderby ArticoloInOrdine.CodArt
                         group ArticoloInOrdine by ArticoloInOrdine.CodClasseConDescrizione into ArticoliGroup
                         select new Grouping<string, GEST_Ordini_Righe_DettaglioOrdine>(ArticoliGroup.Key, ArticoliGroup)).ToObservableCollection();





            //GEST_Ordini_Righe_DettaglioOrdine MMm = new GEST_Ordini_Righe_DettaglioOrdine() { Descrizione = "Totale" };
            //sorted[0].Add(MMm);

            foreach (var item in sorted)
            {

                //Devo aggiungere i totali

                GEST_Ordini_Righe_DettaglioOrdine _GEST_Ordini_Righe_DettaglioOrdineTotale = new GEST_Ordini_Righe_DettaglioOrdine(true,true);
                _GEST_Ordini_Righe_DettaglioOrdineTotale.Descrizione = "Sub Totale";



                _GEST_Ordini_Righe_DettaglioOrdineTotale.Imponibile = (from od in _ListaArticoliInOrdine
                                                                       where od.CodClasseConDescrizione==item.Key
                                                                       select od.Imponibile).Sum();

                //Non so perchè ma con query linq non va - lo devo fare alla vecchia maniera
                decimal QtaInOrdine = 0;
                decimal QtaScMerceInOrdine = 0;

                foreach (var itemarticolo in _ListaArticoliInOrdine.Where(x=>x.CodClasseConDescrizione == item.Key))
                {
                    QtaInOrdine = QtaInOrdine + itemarticolo.Qta;
                    QtaScMerceInOrdine = QtaScMerceInOrdine + itemarticolo.NCP_QtaScontoMerce;

                }

                _GEST_Ordini_Righe_DettaglioOrdineTotale.Qta = QtaInOrdine;
                _GEST_Ordini_Righe_DettaglioOrdineTotale.NCP_QtaScontoMerce = QtaScMerceInOrdine;
                _GEST_Ordini_Righe_DettaglioOrdineTotale.Qta = (from od in _ListaArticoliInOrdine
                                                                where od.CodClasseConDescrizione == item.Key
                                                                select od.Qta).Sum();


                _GEST_Ordini_Righe_DettaglioOrdineTotale.NCP_QtaScontoMerce = (from od in _ListaArticoliInOrdine
                                                                               where od.CodClasseConDescrizione == item.Key
                                                                               select od.NCP_QtaScontoMerce).Sum();


                item.Add(_GEST_Ordini_Righe_DettaglioOrdineTotale);
                


            }


            _ListaArticoliInOrdineRaggruppatoPerClasse = new ObservableCollection<Grouping<string, GEST_Ordini_Righe_DettaglioOrdine>>(sorted);
            OnPropertyChanged("ListaArticoliInOrdineRaggruppatoPerClasse");


            //Altrimenti non esegue mai il refresh del valore
            OnPropertyChanged("DatiOrdine_TotaleOrdine");
            OnPropertyChanged("DatiOrdine_DescrizioneClienteSelezionato");
            

            IsBusy = false;
            ChangeCanExecuteGlobale();
        }

       
        public async Task AggiornaPrezzoEsconti()
        {
            
            if (_GEST_Anagrafica_ClienteSelezionato == null)
                return;

            if (IsBusy)
                return;

            ChangeCanExecuteGlobale();
            IsBusy = true;

            ObservableCollection <GEST_Articoli_Listini> _List_GEST_Articoli_Listini_Selezionato = (await _DataClient.Get_GEST_Articoli_Listino_SearchAsync(_GEST_Anagrafica_ClienteSelezionato.CodListino)).ToObservableCollection();

            if (_List_GEST_Articoli_Listini_Selezionato.Count() <=0)
            {
                IsBusy = false;
                ChangeCanExecuteGlobale();
                return;
            }
         
            foreach (GEST_Articoli_Anagrafica_SelezioneArticolo id in _ListaArticoli)
            {
                id.PercSconto1 = PercSconto1Default;

                GEST_Articoli_Listini _ListinoSelezionatoPerCodArt = _List_GEST_Articoli_Listini_Selezionato.FirstOrDefault(i => i.CodArt.Equals(id.CodArt));

                if (_ListinoSelezionatoPerCodArt!=null)
                { 

                    id.ValUnit = _ListinoSelezionatoPerCodArt.Importo;
                }
                else
                {
                    id.ValUnit = 0;
                }
            }


            foreach (GEST_Ordini_Righe_DettaglioOrdine id in _ListaArticoliInOrdine)
            {
                GEST_Articoli_Listini _ListinoSelezionatoPerCodArt = _List_GEST_Articoli_Listini_Selezionato.FirstOrDefault(i => i.CodArt.Equals(id.CodArt));

                if (_ListinoSelezionatoPerCodArt != null)
                {

                    id.ValUnit = _ListinoSelezionatoPerCodArt.Importo;


                }
                else
                {
                    id.ValUnit = 0;
                    
                }
            }

            IsBusy = false;
            ChangeCanExecuteGlobale();
        }


        #endregion

        #region Proprietà

        //private FormattedString _mTipFormattedString;
        //public FormattedString TipFormattedString
        //{
        //    get { return _mTipFormattedString; }
        //    set
        //    {
        //        _mTipFormattedString = value;
        //        OnPropertyChanged("TipFormattedString");
        //    }
        //}

        public decimal ArticoliInOrdine_NuovaQta
        {
            get { return _ArticoliInOrdine_NuovaQta; }

            set
            {

                if (value > 0 && value <= 999)
                    _ArticoliInOrdine_NuovaQta = value;
                else
                    _ArticoliInOrdine_NuovaQta = 0;

                OnPropertyChanged("ArticoliInOrdine_NuovaQta");

            }
        }


        public DateTime ArticoliInOrdine_NuovaDataConsegna
        {
            get { return _ArticoliInOrdine_NuovaDataConsegna; }

            set
            {
                _ArticoliInOrdine_NuovaDataConsegna = value;
                OnPropertyChanged("ArticoliInOrdine_NuovaDataConsegna");
            }
        }




        public decimal ArticoliInOrdine_NuovaQtaScontoMerce
        {
            get { return _ArticoliInOrdine_NuovaQtaScontoMerce; }

            set
            {

                if (value > 0 && value <= 999)
                    _ArticoliInOrdine_NuovaQtaScontoMerce = value;
                else
                    _ArticoliInOrdine_NuovaQtaScontoMerce = 0;

                OnPropertyChanged("ArticoliInOrdine_NuovaQtaScontoMerce");

            }
        }


        public bool ArticoliInOrdine_ConsideraNuovaDataConsegna
        {
            get { return _ArticoliInOrdine_ConsideraNuovaDataConsegna; }

            set
            {
                _ArticoliInOrdine_ConsideraNuovaDataConsegna = value;
                OnPropertyChanged("ArticoliInOrdine_ConsideraNuovaDataConsegna");
            }
        }


        public decimal ArticoliInOrdine_NuovoPercSconto1
        {
            get { return _ArticoliInOrdine_NuovoPercSconto1; }

            set
            {
                _ArticoliInOrdine_NuovoPercSconto1 = value;
                OnPropertyChanged("ArticoliInOrdine_NuovoPercSconto1");

           

            }
        }


        public decimal ArticoliInOrdine_NuovoPercSconto2
        {
            get { return _ArticoliInOrdine_NuovoPercSconto2; }

            set
            {
                _ArticoliInOrdine_NuovoPercSconto2 = value;
                OnPropertyChanged("ArticoliInOrdine_NuovoPercSconto2");

                

            }
        }





        public ObservableCollection<string> ListaCondizPagamento
        {
            get {
                return _ListaCondizPagamento;
            }

            set
            {
                _ListaCondizPagamento = value;
                
                OnPropertyChanged("ListaCondizPagamento");
            }
        }


        
        //Contiene qc del genere
        //GEST_Condizione_Pagamento_Selez.CodPagamento.Replace('-', ' ') + "-" + GEST_Condizione_Pagamento_Selez.Descrizione.Replace('-', ' ');
        //Da migliorare
        
        public string CondizionePagamentoImpostata
        {
            get { return _CondizionePagamentoImpostata; }

            set
            {


                SetProperty(ref _CondizionePagamentoImpostata, value, "CondizionePagamentoImpostata");
                OnPropertyChanged("CondizionePagamentoImpostata");
            }
        }


        public ObservableCollectionWithEvents<SelezioneListView> ListaFamigliePerSelezioneArticoli
        {
            get { return _ListaFamigliePerSelezioneArticoli; }

            set
            {
                _ListaFamigliePerSelezioneArticoli = value;
                OnPropertyChanged("ListaFamigliePerSelezioneArticoli");
                OnPropertyChanged("FamiglieVisibilePerSelezioneArticoli");
            }
        }

        public ObservableCollectionWithEvents<SelezioneListView> ListaClassiPerSelezioneArticoli
		{
			get { return _ListaClassiPerSelezioneArticoli; }

			set
			{
                _ListaClassiPerSelezioneArticoli = value;
                OnPropertyChanged("ListaClassiPerSelezioneArticoli");
                OnPropertyChanged("ClassiVisibilePerSelezioneArticoli");
            }
		}


        public ObservableCollectionWithEvents<SelezioneListView> ListaNaturePerSelezioneArticoli
        {
            get { return _ListaNaturePerSelezioneArticoli; }

            set
            {
                _ListaNaturePerSelezioneArticoli = value;
                OnPropertyChanged("ListaNaturePerSelezioneArticoli");
                OnPropertyChanged("NatureVisibilePerSelezioneArticoli");
            }
        }


        public ObservableCollectionWithEvents<SelezioneListView> ListaFamigliePerArticoliInOrdine
        {
            get { return _ListaFamigliePerArticoliInOrdine; }

            set
            {
                _ListaFamigliePerArticoliInOrdine = value;
                OnPropertyChanged("ListaFamigliePerArticoliInOrdine");
                OnPropertyChanged("FamiglieVisibilePerSelezioneArticoli");
            }
        }

        public ObservableCollectionWithEvents<SelezioneListView> ListaClassiPerArticoliInOrdine
		{
            get { return _ListaClassiPerArticoliInOrdine; }

			set
			{
                _ListaClassiPerArticoliInOrdine = value;
                OnPropertyChanged("ListaClassiPerArticoliInOrdine");

            }
		}


        public ObservableCollectionWithEvents<SelezioneListView> ListaNaturePerArticoliInOrdine
        {
            get { return _ListaNaturePerArticoliInOrdine; }

            set
            {
                _ListaNaturePerArticoliInOrdine = value;
                OnPropertyChanged("ListaNaturePerArticoliInOrdine");
                OnPropertyChanged("NatureVisibilePerSelezioneArticoli");
            }
        }

        public ObservableCollection<GEST_Clienti_Anagrafica_Indirizzi> ListaIndirizziDiSpedizione
        {
            get {
                return _ListaIndirizziDiSpedizione;
            }

            set
            {
                _ListaIndirizziDiSpedizione = value;
                OnPropertyChanged("ListaIndirizziDiSpedizione");
            }
        }


        public bool ClassiVisibilePerSelezioneArticoli
        {
            get { return ListaClassiPerSelezioneArticoli.Count>0; }
            
        }

        public bool FamiglieVisibilePerSelezioneArticoli
        {
            get { return ListaFamigliePerSelezioneArticoli.Count > 0; }

        }

        public bool NatureVisibilePerSelezioneArticoli
        {
            get { return ListaNaturePerSelezioneArticoli.Count > 0; }

        }

        public ObservableCollection<GEST_Articoli_Anagrafica_SelezioneArticolo> ListaArticoli
		{
			get { return _ListaArticoli; }

			set
			{
				_ListaArticoli = value;
				OnPropertyChanged("ListaArticoli");
			}
		}

		public ObservableCollection<GEST_Ordini_Righe_DettaglioOrdine> ListaArticoliInOrdineFiltrato
		{
			get {

               return _ListaArticoliInOrdineFiltrato; 
            }

			set
			{
                _ListaArticoliInOrdineFiltrato = value;
				OnPropertyChanged("ListaArticoliInOrdineFiltrato");
			}
		}





        public ObservableCollection<Grouping<string, GEST_Ordini_Righe_DettaglioOrdine>> ListaArticoliInOrdineRaggruppatoPerClasse
        {
            get
            {
                return _ListaArticoliInOrdineRaggruppatoPerClasse;
            }

			set
			{
                _ListaArticoliInOrdineRaggruppatoPerClasse = value;
                OnPropertyChanged("ListaArticoliInOrdineRaggruppatoPerClasse");
			}
		}

		public ObservableCollection<GEST_Clienti_Anagrafica> ListaAnagrafica
		{
			get {
				return _ListaAnagrafica;
			}

			set
			{
				_ListaAnagrafica = value;
				OnPropertyChanged("ListaAnagrafica");
			}
		}

        public bool IsInEdit
        {
            get { return _IsInEdit; }

            set
            {
                _IsInEdit = value;
                OnPropertyChanged("IsInEdit");
            }
        }

        public bool IsSoloLettura
        {
            get { return _IsSoloLettura; }

            set
            {
                _IsSoloLettura = value;
                OnPropertyChanged("IsSoloLettura");
            }
        }


        public DateTime DataConsegnaDefault
		{
			get { return _DataConsegnaDefault; }

			set
			{
				_DataConsegnaDefault = value;
				OnPropertyChanged("DataConsegnaDefault");
			}
		}

        public decimal PercSconto1Default
        {
            get { return _PercSconto1Default; }

            set
            {

                if (value != _PercSconto1Default)
                {
                    if (value > 0 && value <= 100)
                        _PercSconto1Default = value;
                    else
                        _PercSconto1Default = 0;

                    OnPropertyChanged("PercSconto1Default");
                }
                
            }
        }


		public int IdAnagraficaClienteSelezionato
		{
			get { return _IdAnagrafica; }

			set
			{
				_IdAnagrafica = value;
				OnPropertyChanged("IdAnagraficaClienteSelezionato");
                _SalvaOrdineInCorsoCommand.ChangeCanExecute();
            }
		}

		

		public string RagioneSocialeClienteSelezionato
		{
			get { return _RagioneSocialeClienteSelezionato; }

			set
			{
                SetProperty(ref _RagioneSocialeClienteSelezionato, value, "RagioneSocialeClienteSelezionato");
			}
		}

        public string DatiOrdine_DescrizioneClienteSelezionato
        {
            get {
                if (_GEST_Anagrafica_ClienteSelezionato != null)
                    return _GEST_Anagrafica_ClienteSelezionato.RagioneSociale + "-" + 
                        _GEST_Anagrafica_ClienteSelezionato.Indirizzo + "-" + _GEST_Anagrafica_ClienteSelezionato.Citta + "-" + 
                        _GEST_Anagrafica_ClienteSelezionato.PartitaIva;

                return string.Empty; }

            
        }


        public string IndirizzoSpedizioneSelezionato
        {
            get {

                if (_GEST_Clienti_Anagrafica_IndirizziSelezionato == null)
                    return string.Empty;

                return _GEST_Clienti_Anagrafica_IndirizziSelezionato.Indirizzo + "-" + _GEST_Clienti_Anagrafica_IndirizziSelezionato.Citta;

            }
		}


        
        public string SelezionaCliente_TestoRicerca
        {
            get { return _SelezionaCliente_TestoRicerca; }
            set
            {
                _SelezionaCliente_TestoRicerca = value;
                OnPropertyChanged("SelezionaCliente_TestoRicerca");

				if (IsInitialized)
                    SelezCliente_RicercaCliente();
            }
        }

        

        public string SelezArticoli_TestoRicerca
        {
            get {
                return _SelezArticoli_TestoRicerca; }
            set
            {

                SetProperty (ref _SelezArticoli_TestoRicerca, value, "SelezArticoli_TestoRicerca");

				if ( IsInitialized)
                SelezArticoli_RicercaArticoli();
            }
        }


        public string ArticoliInOrdine_TestoRicerca
        {
            get
            {
                return _ArticoliInOrdine_TestoRicerca;
            }
            set
            {

                SetProperty(ref _ArticoliInOrdine_TestoRicerca, value, "ArticoliInOrdine_TestoRicerca");

				if (IsInitialized &&   ! IsBusy)
					ArticoliInOrdine_RicercaArticoli();
            }
        }


        public string DatiOrdine_Note
        {
            get { return _DatiOrdine_Note; }
            set
            {
                _DatiOrdine_Note = value;
                OnPropertyChanged("DatiOrdine_Note");
            }
        }


        public decimal DatiOrdine_TotaleOrdine
        {
            get {
                return (from od in _ListaArticoliInOrdine
                        select od.Imponibile).Sum();

            }
            
        }

        #endregion




        public async Task ExecuteLoadSeedDataCommand()
        {
            DependencyService.Get<IHudService>().ShowHud();

            if (IsBusy)
                return;

            IsBusy = true;
            ChangeCanExecuteGlobale();

            await _DataClient.Init();

            ObservableCollection<GEST_Articoli_Anagrafica> _ListaArticoliLocale=new ObservableCollection<GEST_Articoli_Anagrafica>();


            //La carico solo una volta e la uso diverse volte
            //Mi serve anche per solo lettura !!
            _ListaArticoliLocale = (await _DataClient.GetGEST_Articoli_AnagraficaAsync(false)).ToObservableCollection();



            if (IsInEdit|IsSoloLettura)
            {
                //Siamo in modifica
                GEST_Ordini_Teste GEST_Ordini_Teste = (await _DataClient.GetGEST_Ordini_TesteAsync(false)).FirstOrDefault(x => x.Id.ToLower() == _IdDoc.ToLower());

                DatiOrdine_Note = GEST_Ordini_Teste.Note;
                CondizionePagamentoImpostata = GEST_Ordini_Teste.CodPagamento;
                if (GEST_Ordini_Teste.DataConsegna.HasValue)
                    DataConsegnaDefault = (DateTime)GEST_Ordini_Teste.DataConsegna;
                else
                    DataConsegnaDefault = DateTime.Now;


                ObservableCollection <GEST_Ordini_Righe> TuttiGliArticoliInOrdine = (await _DataClient.Get_GEST_Ordini_RigheAsync(_IdDoc, string.Empty)).ToObservableCollection();

				_GEST_Anagrafica_ClienteSelezionato = 
					(await _DataClient.Get_GEST_Clienti_Anagrafica_ByIdAnagrafica(GEST_Ordini_Teste.IdAnagrafica));

//
//
//					(await _DataClient.Get_GEST_Clienti_Anagrafica_SearchAsync(GEST_Ordini_Teste.IdAnagrafica, string.Empty)).FirstOrDefault();
//				

                //ObservableCollection<GEST_Ordini_Righe_DettaglioOrdine> _Pippo=new ObservableCollection<GEST_Ordini_Righe_DettaglioOrdine>();
                foreach (var item in TuttiGliArticoliInOrdine)
                {
                    GEST_Ordini_Righe_DettaglioOrdine Articolodainserire = new GEST_Ordini_Righe_DettaglioOrdine(item);

                    //Non dovrebbe mai e poi mai succedere ma per sicurezza altrimenti la maschera dà errore in load
                    if (!Articolodainserire.DataPresuntaConsegna.HasValue)
                        Articolodainserire.DataPresuntaConsegna = GEST_Ordini_Teste.DataConsegna;

                    //Devo completarlo con il cod classe per il riepilogo e le ricerche
                    //GEST_Articoli_Anagrafica _AnagraficaArt = (await _DataClient.GetGEST_Articoli_AnagraficaAsync(false)).Where(x => x.CodArt.ToLower() == item.CodArt.ToLower()).FirstOrDefault();

                    GEST_Articoli_Anagrafica _AnagraficaArt = _ListaArticoliLocale.Where(x => x.CodArt.ToLower() == item.CodArt.ToLower()).FirstOrDefault();

                    if (_AnagraficaArt != null)
                    {
                        Articolodainserire.CodFamiglia = _AnagraficaArt.CodFamiglia;
                        Articolodainserire.CodClasse = _AnagraficaArt.CodClasse;
                        Articolodainserire.CodNatura = _AnagraficaArt.CodNatura;
                        GEST_Articoli_Classi _Classe = (await _DataClient.GetGEST_Articoli_ClassiAsync(false)).Where(x => x.CodClasse.ToLower() == _AnagraficaArt.CodClasse.ToLower()).FirstOrDefault();

                        if (_Classe != null)
                        {
                            Articolodainserire.CodClasseConDescrizione = _Classe.CodClasse + " - " + _Classe.Descrizione;
                        }

                        

                    }

                    _ListaArticoliInOrdine.Add(Articolodainserire);
                    _ListaArticoliInOrdineFiltrato.Add(Articolodainserire);
                }
                OnPropertyChanged("ListaArticoliInOrdine");

                ListaAnagrafica = new ObservableCollection<GEST_Clienti_Anagrafica>();
                ListaCondizPagamento = new ObservableCollection<string>();

            }
            else

            {

                //Siamo in nuovo
                ListaAnagrafica = _ListaAnagraficaOriginale= (await _DataClient.GetGEST_Clienti_AnagraficaAsync(false)).ToObservableCollection();

                //L'unico modo per farlo andare è lavorare solo sulle stringhe altrimenti in two way non va ! Cioè se imposto da codice un valore non mi viene valorizzato sul controllo
                List<GEST_Condizione_Pagamento> _GEST_Condizione_Pagamento = (await _DataClient.GetGEST_Condizione_PagamentoAsync(false));
                ObservableCollection<string> _GEST_Condizione_Pagamento_Local = new ObservableCollection<string>(); ;
                foreach (var item in _GEST_Condizione_Pagamento)
                {
                    _GEST_Condizione_Pagamento_Local.Add(item.CodPagamento.Replace('-', ' ') + "-" + item.Descrizione.Replace('-', ' '));
                }

                ListaCondizPagamento = _GEST_Condizione_Pagamento_Local;
                OnPropertyChanged("ListaCondizPagamento");

            }


            if (IsSoloLettura)
            {
                
                
            }else
            { 
                //qui siamo in editing o nuovo

                //Per levare ovehead
                ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(false);
                ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(false);


                //ObservableCollection<GEST_Articoli_Anagrafica> _ListaArticoliLocale = (await _DataClient.GetGEST_Articoli_AnagraficaAsync(false)).ToObservableCollection();


                //List<GEST_Articoli_Anagrafica_SelezioneArticolo> MMMMM = new List<GEST_Articoli_Anagrafica_SelezioneArticolo>();

                foreach (GEST_Articoli_Anagrafica id in _ListaArticoliLocale)
                {
                    //System.Diagnostics.Debug.WriteLine("XXXXXX" + id.CodArt);
                    //System.Diagnostics.Debug.WriteLine(_ListaArticoliLocale.Count() + "/" + _ListaArticoli.Count());
                    //MMMMM.Add(new GEST_Articoli_Anagrafica_SelezioneArticolo(id));
                    //_ListaArticoli.Add(new GEST_Articoli_Anagrafica_SelezioneArticolo(id));
                    ListaArticoli.Add(new GEST_Articoli_Anagrafica_SelezioneArticolo(id));
                    
                }

                //ListaArticoli = MMMMM.ToObservableCollection();
                OnPropertyChanged("ListaArticoli");

                ObservableCollection<GEST_Articoli_Famiglie> _GEST_Articoli_Famiglie_local = (await _DataClient.GetGEST_Articoli_FamiglieAsync(false)).ToObservableCollection();
                ObservableCollection<GEST_Articoli_Classi> _GEST_Articoli_Classi_local = (await _DataClient.GetGEST_Articoli_ClassiAsync(false)).ToObservableCollection();
                ObservableCollection<GEST_Articoli_Nature> _GEST_Articoli_Nature_local = (await _DataClient.GetGEST_Articoli_NatureAsync(false)).ToObservableCollection();

                foreach (var id in _GEST_Articoli_Famiglie_local)
                {
				    ListaFamigliePerSelezioneArticoli.Add(new SelezioneListView() { Item = id.CodFamiglia, IsSelected = true, Descrizione = id.Descrizione });

				    if (IsInEdit){
					    //Mi occupo di inserire la famiglia ma solo se relativa a un prodotto in ordine
					    if ((from p in _ListaArticoliInOrdine
						    where p.CodFamiglia == id.CodFamiglia
						    select p).Count() > 0)
					    {
                		    ListaFamigliePerArticoliInOrdine.Add(new SelezioneListView() { Item = id.CodFamiglia, IsSelected = true ,Descrizione=id.Descrizione});
					    }
				    }
                };
                OnPropertyChanged("ListaFamigliePerArticoliInOrdine");
                OnPropertyChanged("ListaFamigliePerSelezioneArticoli");
                OnPropertyChanged("FamiglieVisibilePerSelezioneArticoli");

                foreach (var id in _GEST_Articoli_Classi_local)
                {
				    ListaClassiPerSelezioneArticoli.Add(new SelezioneListView() { Item = id.CodClasse, IsSelected = true, Descrizione = id.Descrizione });

				    if (IsInEdit){
					    //Mi occupo di inserire la classe ma solo se relativa a un prodotto in ordine
					    if ((from p in _ListaArticoliInOrdine
						    where p.CodClasse == id.CodClasse
						    select p).Count() > 0)
					    {
						    ListaClassiPerArticoliInOrdine.Add(new SelezioneListView() { Item = id.CodClasse, IsSelected = true, Descrizione = id.Descrizione });
					    }
				    }
                };
                OnPropertyChanged("ListaClassiPerArticoliInOrdine");
                OnPropertyChanged("ListaClassiPerSelezioneArticoli");
                OnPropertyChanged("ClassiVisibilePerSelezioneArticoli");

                foreach (var id in _GEST_Articoli_Nature_local)
                {

                    ListaNaturePerSelezioneArticoli.Add(new SelezioneListView() { Item = id.CodNatura, IsSelected = true, Descrizione = id.Descrizione });

				    if (IsInEdit){
					    //Mi occupo di inserire la natura ma solo se relativa a un prodotto in ordine
					    if ((from p in _ListaArticoliInOrdine
						    where p.CodNatura == id.CodNatura
						    select p).Count() > 0)
                    {
						    ListaNaturePerArticoliInOrdine.Add(new SelezioneListView() { Item = id.CodNatura, IsSelected = true, Descrizione = id.Descrizione });
                    }
                    }
			    };

                OnPropertyChanged("ListaNaturePerArticoliInOrdine");
                OnPropertyChanged("ListaNaturePerSelezioneArticoli");
                OnPropertyChanged("NatureVisibilePerSelezioneArticoli");
            }

            IsInitialized = true;
			IsBusy = false;

            if (IsInEdit)
            {
                await AggiornaOrdinato(string.Empty);
                await AggiornaPrezzoEsconti();
            }
            if (IsSoloLettura)
            {
                //devo farlo subito perchè la prima maschera che visualizzo è quella del dettaglio ordini
                await AggiornaListaArticoliRaggruppata();
            }

            

            ChangeCanExecuteGlobale();

            ConnettiHandlerFiltriFamigliaClassePerSelezioneArticoli(true);
            ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine(true);

            DependencyService.Get<IHudService>().HideHud();
        }


        //private async void DeterminaStatoViewModel()
        //{
        //    if (_IdDoc.Length > 0)
        //    {
        //        GEST_Ordini_Teste _GEST_Ordini_Teste = (await _DataClient.Get_GEST_Ordini_Teste_SearchAsync(_IdDoc, string.Empty, DateTime.MinValue, DateTime.MinValue)).FirstOrDefault();
        //        //GEST_Ordini_Teste _GEST_Ordini_Teste = (_DataClient.GetGEST_Ordini_TesteAsync().Get_GEST_Ordini_Teste_SearchAsync(_IdDoc, string.Empty, DateTime.MinValue, DateTime.MinValue)).
        //        if (_GEST_Ordini_Teste!=null)
        //        {
        //            if (_GEST_Ordini_Teste.CloudState == ((int)CloudState.caricatoEsincronizzato))
        //                IsSoloLettura = true;
        //            else
        //                IsInEdit = true;
        //        }
        //        else
        //        {
        //            //Boh ??

        //        }


                
        //    }



        //}

        async private void SelezionaArticoli_AggiungiRigheAdOrdine()
		{
            if (IsBusy)
                return;

            IsBusy = true;
            ChangeCanExecuteGlobale();

            if (_GEST_Anagrafica_ClienteSelezionato == null)
            {
                await App.Current.MainPage.DisplayAlert("Aggiunta Articoli in ordine", "Non è stato specificato alcun cliente", "Ok");
                IsBusy = false;
                ChangeCanExecuteGlobale();
                return;
            }

            foreach (var item in ListaArticoli.Where(w => w.QtaDaOrdinare> 0 |  w.NCP_QtaScontoMerce > 0))
			{
				//Indica se è stato aggiunto un nuovo articolo
                bool DaAggiungere = false;

                GEST_Ordini_Righe_DettaglioOrdine Articolodainserire = _ListaArticoliInOrdine.Where(x => x.CodArt.ToLower() == item.CodArt.ToLower()).FirstOrDefault();

                if (Articolodainserire == null)
                { 
                    Articolodainserire = new GEST_Ordini_Righe_DettaglioOrdine();
                    DaAggiungere = true;
                }

				Articolodainserire.CodArt = item.CodArt;
                Articolodainserire.CodUnMis = item.CodUnMisVend;
                Articolodainserire.Descrizione = item.Descrizione;
				Articolodainserire.Qta = Articolodainserire.Qta+item.QtaDaOrdinare;
                Articolodainserire.NCP_QtaScontoMerce = Articolodainserire.NCP_QtaScontoMerce+item.NCP_QtaScontoMerce;
                Articolodainserire.Sc1 = item.PercSconto1;
                Articolodainserire.Sc2 = item.PercSconto2;
                Articolodainserire.Sc3 = item.PercSconto3;
                Articolodainserire.Sc4 = item.PercSconto4;
                Articolodainserire.ValUnit = item.ValUnit;
                Articolodainserire.DataPresuntaConsegna = DataConsegnaDefault;


                Articolodainserire.CodFamiglia = item.CodFamiglia;
                Articolodainserire.CodClasse = item.CodClasse;
                Articolodainserire.CodNatura = item.CodNatura;
                GEST_Articoli_Classi _Classe = (await _DataClient.GetGEST_Articoli_ClassiAsync(false)).Where(x => x.CodClasse.ToLower() == item.CodClasse.ToLower()).FirstOrDefault();

                if (_Classe != null)
                {
                    Articolodainserire.CodClasseConDescrizione = _Classe.CodClasse + " - " + _Classe.Descrizione;
                }



                if (DaAggiungere)
                {
                    _ListaArticoliInOrdine.Add(Articolodainserire);

					//Non serve ma in ogni caso lo inserisco
					_ListaArticoliInOrdineFiltrato.Add(Articolodainserire);

					ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine (false);
                    //Aggiorno la lista famiglie/classi/nature aggiungendo l'item se manca
                    if ((from p in ListaFamigliePerArticoliInOrdine
                         where p.Item == Articolodainserire.CodFamiglia
                         select p).Count() <= 0)
                    {
                        GEST_Articoli_Famiglie _Famiglia = (await _DataClient.GetGEST_Articoli_FamiglieAsync(false)).Where(x => x.CodFamiglia.ToLower() == Articolodainserire.CodFamiglia.ToLower()).FirstOrDefault();

                        if (_Famiglia != null)
                        {
                            ListaFamigliePerArticoliInOrdine.Add(new SelezioneListView() { Item = _Famiglia.CodFamiglia, IsSelected = true, Descrizione = _Famiglia.Descrizione });
                        }
                        OnPropertyChanged("ListaFamigliePerArticoliInOrdine");
                    }

                    if ((from p in ListaClassiPerArticoliInOrdine
                         where p.Item == Articolodainserire.CodClasse
                         select p).Count() <= 0)
                    {
						
                         _Classe = (await _DataClient.GetGEST_Articoli_ClassiAsync(false)).Where(x => x.CodClasse.ToLower() == item.CodClasse.ToLower()).FirstOrDefault();

                        if (_Classe != null)
                        {
                            ListaClassiPerArticoliInOrdine.Add(new SelezioneListView() { Item = _Classe.CodClasse, IsSelected = true, Descrizione = _Classe.Descrizione });
                        }
                        OnPropertyChanged("ListaClassiPerArticoliInOrdine");
                    }

                    
                    if ((from p in ListaNaturePerArticoliInOrdine
                         where p.Item == Articolodainserire.CodNatura
                         select p).Count() <= 0)
                    {
						
                        GEST_Articoli_Nature _Nature = (await _DataClient.GetGEST_Articoli_NatureAsync(false)).Where(x => x.CodNatura.ToLower() == Articolodainserire.CodNatura.ToLower()).FirstOrDefault();

                        if (_Nature != null)
                        {
                            ListaNaturePerArticoliInOrdine.Add(new SelezioneListView() { Item = _Nature.CodNatura, IsSelected = true, Descrizione = _Nature.Descrizione });
                        }
                        OnPropertyChanged("ListaNaturePerArticoliInOrdine");
                    }

					ConnettiHandlerFiltriFamigliaClassePerArticoliInOrdine (true);

                }

				//await AggiornaOrdinato (item.CodArt);
                //await AggiornaListaArticoliRaggruppata();
                //await ArticoliInOrdine_RicercaArticoli ();
                //item.QtaDaOrdinare = 0;
                //            item.NCP_QtaScontoMerce = 0;
            }


            IsBusy = false;

			//Necessario così ricarica la lista degli articoli da visualizzare cone le nuove quantità 
			await ArticoliInOrdine_RicercaArticoli ();
            
            ChangeCanExecuteGlobale();
            await App.Current.MainPage.DisplayAlert ("Inserimento ordine", "Righe correttamente inserite nell'ordine in corso","OK");
		}



        public async Task AggiornaOrdinato(string _CodArt)
		{
            
            ChangeCanExecuteGlobale();

            if (_CodArt.Length>0)
			{
				var StessiArticoliGiaInseriti= _ListaArticoliInOrdine.Where(x => x.CodArt.Equals(_CodArt));
				int qta = 0;
				foreach(var articolo in StessiArticoliGiaInseriti)
				{

					qta = qta + (int)articolo.Qta + (int)articolo.NCP_QtaScontoMerce;
				}

				var _ArticolodaAggiornare = _ListaArticoli.FirstOrDefault(i => i.CodArt == _CodArt);
				if (_ArticolodaAggiornare != null)
				{
					_ArticolodaAggiornare.QtaGiaInOrdine= qta;
				}
				return;
			}
			foreach (var item in _ListaArticoliInOrdine)
			{
				await AggiornaOrdinato (item.CodArt);

			}

            ChangeCanExecuteGlobale();
        }

		

		

	}
}


