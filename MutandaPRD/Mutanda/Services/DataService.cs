using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;
using Mutanda.Models;
using Mutanda.Services;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Net.Http;
using Plugin.DeviceInfo;
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(DataService))]

namespace Mutanda.Services
{
    public class DataService : IDataService
    {
        // sync tables
        IMobileServiceSyncTable<GEST_Clienti_Anagrafica_Indirizzi> GESTClientiAnagraficaIndirizziTable;
        IMobileServiceSyncTable<GEST_Articoli_Anagrafica> GESTArticoliAnagraficaTable;
        IMobileServiceSyncTable<GEST_Articoli_BarCode> GESTArticoliBarCodeTable;
        IMobileServiceSyncTable<GEST_Articoli_Classi> GESTArticoliClassiTable;
        IMobileServiceSyncTable<GEST_Articoli_Famiglie> GESTArticoliFamiglieTable;
        IMobileServiceSyncTable<GEST_Articoli_Immagini> GESTArticoliImmaginiTable;
        IMobileServiceSyncTable<GEST_Articoli_Listini> GESTArticoliListiniTable;
        IMobileServiceSyncTable<GEST_Articoli_Nature> GESTArticoliNatureTable;
        IMobileServiceSyncTable<GEST_CategorieClienti> GESTCategorieClientiTable;
        IMobileServiceSyncTable<GEST_Condizione_Pagamento> GESTCondizionePagamentoTable;
        IMobileServiceSyncTable<GEST_Iva> GESTIvaTable;
        IMobileServiceSyncTable<GEST_Listini> GESTListiniTable;
        IMobileServiceSyncTable<GEST_Ordini_Righe> GESTOrdiniRigheTable;
        IMobileServiceSyncTable<GEST_Ordini_Teste> GESTOrdiniTesteTable;
        IMobileServiceSyncTable<DEVICE_ParametriDevice> GESTParametriDeviceTable;
        IMobileServiceSyncTable<GEST_Scala_Sconti> GESTScalaScontiTable;
        IMobileServiceSyncTable<GEST_UnitaMisura> GESTUnitaMisuraTable;
        IMobileServiceSyncTable<GEST_Update_Immagini> GESTUpdateImmaginiTable;
        IMobileServiceSyncTable<GEST_Articoli_Disponibilita> GESTArticoliDisponibilitaTable;
        IMobileServiceSyncTable<GEST_Clienti_Anagrafica> GESTClientiAnagraficaTable;
        IMobileServiceSyncTable<GEST_Porto> GESTPortoTable;
        IMobileServiceSyncTable<Permissions> PermissionTable;
        IMobileServiceSyncTable<Authorization> AuthorizationTable;
            
        MobileServiceSQLiteStore store;

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public IMobileServiceClient MobileService { get; set; }

        public static string DeviceMail { get; set; }

        public DataService()
        {
            MobileService = AzureService.Instance.GetMobileServiceClient();
        }

        public bool DoesLocalDBExist()
        {
            return MobileService.SyncContext.IsInitialized;
        }

        public async Task Init()
        {
            if (MobileService.SyncContext.IsInitialized)
                return;

            var path = "OrderEntry.db";

            store = new MobileServiceSQLiteStore(path);
            store.DefineTable<GEST_Clienti_Anagrafica_Indirizzi>();
            store.DefineTable<GEST_Articoli_Anagrafica>(); ;
            store.DefineTable<GEST_Articoli_BarCode>();
            store.DefineTable<GEST_Articoli_Classi>();
            store.DefineTable<GEST_Articoli_Famiglie>();
            store.DefineTable<GEST_Articoli_Immagini>();
            store.DefineTable<GEST_Articoli_Listini>();
            store.DefineTable<GEST_Articoli_Nature>();
            store.DefineTable<GEST_CategorieClienti>();
            store.DefineTable<GEST_Condizione_Pagamento>();
            store.DefineTable<GEST_Iva>();
            store.DefineTable<GEST_Listini>();
            store.DefineTable<GEST_Ordini_Righe>();
            store.DefineTable<GEST_Ordini_Teste>();
            store.DefineTable<DEVICE_ParametriDevice>();
            store.DefineTable<GEST_Scala_Sconti>();
            store.DefineTable<GEST_UnitaMisura>();
            store.DefineTable<GEST_Update_Immagini>();
            store.DefineTable<GEST_Articoli_Disponibilita>();
            store.DefineTable<GEST_Clienti_Anagrafica>();
            store.DefineTable<GEST_Porto>();
            store.DefineTable<Permissions>();
            store.DefineTable<Authorization>();

            try
            {
                await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Sync Failed: {0}", ex.Message);
            }

            GESTClientiAnagraficaIndirizziTable = MobileService.GetSyncTable<GEST_Clienti_Anagrafica_Indirizzi>();
            GESTArticoliAnagraficaTable = MobileService.GetSyncTable<GEST_Articoli_Anagrafica>(); ;
            GESTArticoliBarCodeTable = MobileService.GetSyncTable<GEST_Articoli_BarCode>();
            GESTArticoliClassiTable = MobileService.GetSyncTable<GEST_Articoli_Classi>();
            GESTArticoliFamiglieTable = MobileService.GetSyncTable<GEST_Articoli_Famiglie>();
            GESTArticoliImmaginiTable = MobileService.GetSyncTable<GEST_Articoli_Immagini>();
            GESTArticoliListiniTable = MobileService.GetSyncTable<GEST_Articoli_Listini>();
            GESTArticoliNatureTable = MobileService.GetSyncTable<GEST_Articoli_Nature>();
            GESTCategorieClientiTable = MobileService.GetSyncTable<GEST_CategorieClienti>();
            GESTCondizionePagamentoTable = MobileService.GetSyncTable<GEST_Condizione_Pagamento>();
            GESTIvaTable = MobileService.GetSyncTable<GEST_Iva>();
            GESTListiniTable = MobileService.GetSyncTable<GEST_Listini>();
            GESTOrdiniRigheTable = MobileService.GetSyncTable<GEST_Ordini_Righe>();
            GESTOrdiniTesteTable = MobileService.GetSyncTable<GEST_Ordini_Teste>();
            GESTParametriDeviceTable = MobileService.GetSyncTable<DEVICE_ParametriDevice>();
            GESTScalaScontiTable = MobileService.GetSyncTable<GEST_Scala_Sconti>();
            GESTUnitaMisuraTable = MobileService.GetSyncTable<GEST_UnitaMisura>();
            GESTUpdateImmaginiTable = MobileService.GetSyncTable<GEST_Update_Immagini>();
            GESTArticoliDisponibilitaTable = MobileService.GetSyncTable<GEST_Articoli_Disponibilita>();
            GESTClientiAnagraficaTable = MobileService.GetSyncTable<GEST_Clienti_Anagrafica>();
            GESTPortoTable = MobileService.GetSyncTable<GEST_Porto>();
            PermissionTable = MobileService.GetSyncTable<Permissions>();
            AuthorizationTable = MobileService.GetSyncTable<Authorization>();
        }
        
        public async Task SyncAll(bool incremental)
        {
            try
            {
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Preparo Sync");
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Anagrafica_Indirizzi,GEST_Articoli_Anagrafica");
                await SyncGEST_Clienti_Anagrafica_Indirizzi(incremental);
                await SyncGEST_Articoli_Anagrafica(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Clienti_Anagrafica_Indirizzi,GEST_Articoli_Anagrafica");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Articoli_BarCode,GEST_Articoli_Classi");
                await SyncGEST_Articoli_BarCode(incremental);
                await SyncGEST_Articoli_Classi(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Articoli_BarCode,GEST_Articoli_Classi");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Articoli_Disponibilita,GEST_Articoli_Famiglie");
                await SyncGEST_Articoli_Disponibilita(incremental);
                await SyncGEST_Articoli_Famiglie(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Articoli_Disponibilita,GEST_Articoli_Famiglie");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Articoli_Immagini,GEST_Articoli_Nature");
                await SyncGEST_Articoli_Immagini(incremental);
                await SyncGEST_Articoli_Nature(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Articoli_Immagini,GEST_Articoli_Nature");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_CategorieClienti,GEST_Clienti_Anagrafica");
                await SyncGEST_CategorieClienti(incremental);
                await SyncGEST_Clienti_Anagrafica(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_CategorieClienti,Clienti_Anagrafica");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Iva,GEST_Listini");
                await SyncGEST_Iva(incremental);
                await SyncGEST_Listini(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Iva,GEST_Listini");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Articoli_Listini");
                await SyncGEST_Articoli_Listini(incremental);
                //await SyncGEST_Ordini_Righe(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Articoli_Listini");

                //MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_ParametriDevice");
                //await SyncGEST_Ordini_Teste(incremental);
                //MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_ParametriDevice");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Scala_Sconti,GEST_UnitaMisura");
                await SyncGEST_Scala_Sconti(incremental);
                await SyncGEST_UnitaMisura(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Scala_Sconti, GEST_UnitaMisura");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Update_Immagini, GEST_Porto");
                await SyncGEST_Update_Immagini(incremental);
                await SyncGEST_Porto(incremental);
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync GEST_Update_Immagini, GEST_Porto");

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync GEST_Condizione_Pagamento");
                await SyncGEST_Condizione_Pagamento(incremental);

                //await GESTClientiAnagraficaTable.PullAsync(null, GESTClientiAnagraficaTable.CreateQuery());
                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Sync Eseguito");
            }
            catch (Exception exc)
            {
                Debug.WriteLine("ERROR AzureService.SyncAll(): " + exc.Message);
            }
        }

        #region Authorization
        public async Task<Authorization> GetAuthorizationAsync()
        {
            try
            {
                Authorization result = null;

                await Init();
                List<Authorization> listAuth = await AuthorizationTable.ToListAsync();

                if (listAuth.Count == 0)
                {
                    var networkConnection = DependencyService.Get<INetworkConnection>();
                    networkConnection.CheckNetworkConnection();
                    if (networkConnection.IsConnected)
                    {
                        var arguments = new Dictionary<string, string>();

                        // Gestione Emulatore Genymotion. Viene rilasciato "unknown" nell'IdDevice.
                        arguments.Add("idDevice", CrossDeviceInfo.Current.Id != "unknown" ? CrossDeviceInfo.Current.Id: "EMU001");

                        GEST_Ordini_Teste ordine = new GEST_Ordini_Teste();
                        result = await MobileService.InvokeApiAsync<GEST_Ordini_Teste, Authorization>("AuthorizationApi", ordine, HttpMethod.Post, arguments);
                        result.Id = Guid.NewGuid().ToString();

                        //await AuthorizationTable.InsertAsync(result);
                    }
                }
                else
                {
                    if (listAuth.Count() > 0)
                        result = listAuth.First();
                    else
                        result = new Authorization();
                }

                return result;
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return null;
        }

        public async Task SaveAuthorizationAsync(Authorization item)
        {
            try
            {
                await AuthorizationTable.InsertAsync(item);               
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Permissions>> GetPermission_Async(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncPermission(true);

                return (await PermissionTable.ToListAsync());
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<Permissions>();
        }

        public async Task SyncPermission(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio Sync Permessi utente");

                    string queryId = null;
                    if (incremental) queryId = "Permission";

                    await PermissionTable.PullAsync(queryId, PermissionTable.CreateQuery());
                    MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync Permessi utente");
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region Delete Local DataBase
        public async Task DeleteLocalDataBase()
        {
            try
            {
                await GESTClientiAnagraficaTable.PurgeAsync("GESTAnagrafica", GESTClientiAnagraficaTable.CreateQuery(), new CancellationToken());
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
        }
        #endregion

        #region GEST_Clienti_Anagrafiche
        public async Task<List<GEST_Clienti_Anagrafica>> GetGEST_Clienti_AnagraficaAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Clienti_Anagrafica(true);

                var result = await GESTClientiAnagraficaTable.Where(x => x.CloudState != ((int)CloudState.cancellato)).OrderBy(b => b.RagioneSociale).ToListAsync();

                return result.ToList();
                
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Clienti_Anagrafica>();
        }

        
		public async Task<GEST_Clienti_Anagrafica> Get_GEST_Clienti_Anagrafica_ByIdAnagrafica(int _IdAnag)
		{
			
			var result = await GESTClientiAnagraficaTable.Where (x => x.IDAnagrafica == _IdAnag).ToEnumerableAsync ();

			return result.SingleOrDefault ();
		}

		public async Task<IEnumerable<GEST_Clienti_Anagrafica>> Get_GEST_Clienti_Anagrafica_SearchAsync(string _filter)
        {
            try
            {
                //Folle ma così è più veloce
                if (string.IsNullOrEmpty(_filter))
				{
					var returnvalue = await GESTClientiAnagraficaTable.Where(x=> x.CloudState!=((int) CloudState.cancellato))
						.OrderBy(b => b.RagioneSociale)
						.ToEnumerableAsync();
					return returnvalue;
				}    
				else
				{
					var returnvalue2=await GESTClientiAnagraficaTable.Where(x => (x.RagioneSociale.ToLower().Contains(_filter.ToLower()) ||
                            x.Indirizzo.ToLower().Contains(_filter.ToLower())) && (x.CloudState != ((int)CloudState.cancellato)))
						.OrderBy(b => b.RagioneSociale)
						.ToEnumerableAsync();
					return returnvalue2;
				}
            }
            catch (Exception e)
	        {

            }

            return null;
        }

        public async Task SyncGEST_Clienti_Anagrafica(bool incremental)
        {
            try
            {
                //await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTAnagrafica";

                    IDataService dataService = DependencyService.Get<IDataService>();
                    Authorization authorization = await dataService.GetAuthorizationAsync();

                    if (authorization != null)
                    {
                        if (!authorization.SuperUser)
                            await GESTClientiAnagraficaTable.PullAsync(queryId, GESTClientiAnagraficaTable.Where(a => a.IdAgente == authorization.IdAgente));
                        else
                            await GESTClientiAnagraficaTable.PullAsync(queryId, GESTClientiAnagraficaTable.CreateQuery());
                    }
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task SaveGEST_Clienti_AnagraficaAsync(GEST_Clienti_Anagrafica item)
        {
            try
            {
                if (item.Id == null)
                    await GESTClientiAnagraficaTable.InsertAsync(item);
                else
                    await GESTClientiAnagraficaTable.UpdateAsync(item);
              
                await SyncGEST_Clienti_Anagrafica(true);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GET_Anagrafica_Indirizzi
        public async Task<List<GEST_Clienti_Anagrafica_Indirizzi>> GetGEST_Clienti_Anagrafica_IndirizziAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Clienti_Anagrafica_Indirizzi(true);

                return await GESTClientiAnagraficaIndirizziTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Clienti_Anagrafica_Indirizzi>();
        }

        public async Task SyncGEST_Clienti_Anagrafica_Indirizzi(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTAnagraficaIndirizzi";

                    await GESTClientiAnagraficaIndirizziTable.PullAsync(queryId, GESTClientiAnagraficaIndirizziTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task SaveGEST_Clienti_Anagrafica_Indirizzi(GEST_Clienti_Anagrafica_Indirizzi item)
        {
            try
            {
                if (item.Id == null)
                    await GESTClientiAnagraficaIndirizziTable.InsertAsync(item);
                else
                    await GESTClientiAnagraficaIndirizziTable.UpdateAsync(item);

                await SyncGEST_Clienti_Anagrafica_Indirizzi(true);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Anagrafica
        public async Task<List<GEST_Articoli_Anagrafica>> GetGEST_Articoli_AnagraficaAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_Anagrafica(true);

                return await GESTArticoliAnagraficaTable.OrderBy(b => b.CodMerc).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Anagrafica>();
        }

        public async Task SyncGEST_Articoli_Anagrafica(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliAnagrafica";

                    await GESTArticoliAnagraficaTable.PullAsync(queryId, GESTArticoliAnagraficaTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Status.ToString()); //Errors[0].RawResult
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Barcode
        public async Task<List<GEST_Articoli_BarCode>> GetGEST_Articoli_BarCodeAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_BarCode(true);

                return await GESTArticoliBarCodeTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_BarCode>();
        }

        public async Task SyncGEST_Articoli_BarCode(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliBarCode";

                    await GESTArticoliBarCodeTable.PullAsync(queryId, GESTArticoliBarCodeTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Classi
        public async Task<List<GEST_Articoli_Classi>> GetGEST_Articoli_ClassiAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_Classi(true);

                return await GESTArticoliClassiTable.OrderBy(b => b.CodClasse).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Classi>();
        }

        public async Task SyncGEST_Articoli_Classi(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliClassi";

                    await GESTArticoliClassiTable.PullAsync(queryId, GESTArticoliClassiTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Disponibilità
        public async Task<List<GEST_Articoli_Disponibilita>> GetGEST_Articoli_DisponibilitaAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_Disponibilita(true);

                return await GESTArticoliDisponibilitaTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Disponibilita>();
        }

        public async Task SyncGEST_Articoli_Disponibilita(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliDisponibilita";

                    await GESTArticoliDisponibilitaTable.PullAsync(queryId, GESTArticoliDisponibilitaTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Famiglie
        public async Task<List<GEST_Articoli_Famiglie>> GetGEST_Articoli_FamiglieAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_Famiglie(true);

                return await GESTArticoliFamiglieTable.OrderBy(b => b.CodFamiglia).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Famiglie>();
        }

        public async Task SyncGEST_Articoli_Famiglie(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliFamiglie";

                    await GESTArticoliFamiglieTable.PullAsync(queryId, GESTArticoliFamiglieTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Immagini
        public async Task<List<GEST_Articoli_Immagini>> GetGEST_Articoli_ImmaginiAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_Immagini(true);

                return await GESTArticoliImmaginiTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Immagini>();
        }

        public async Task SyncGEST_Articoli_Immagini(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliImmagini";

                    await GESTArticoliImmaginiTable.PullAsync(queryId, GESTArticoliImmaginiTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Listini
        public async Task<List<GEST_Articoli_Listini>> GetGEST_Articoli_ListiniAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_Listini(true);

                return await GESTArticoliListiniTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Listini>();
        }

        public async Task SyncGEST_Articoli_Listini(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliListini";

                    await GESTArticoliListiniTable.PullAsync(queryId, GESTArticoliListiniTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Articoli_Nature
        public async Task<List<GEST_Articoli_Nature>> GetGEST_Articoli_NatureAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Articoli_Nature(true);

                return await GESTArticoliNatureTable.OrderBy(b => b.Ordinamento).ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Nature>();
        }


        public async Task<List<GEST_Articoli_Nature>> GetGEST_Articoli_NatureAsync_SearchAsync(string _MatrixClassi)
        {
            try
            {
                if (String.IsNullOrEmpty(_MatrixClassi) )
                    return new List<GEST_Articoli_Nature>();

				IEnumerable<GEST_Articoli_Nature> _ListaNature =await GESTArticoliNatureTable.ToListAsync();
//                await (GetGEST_Articoli_NatureAsync(false));

                if (!String.IsNullOrEmpty(_MatrixClassi))
                {
                    string[] _Classi = _MatrixClassi.Split(',');
                    _ListaNature = _ListaNature.Where(x => _Classi.Contains(x.CodClasse)).OrderBy(x=>x.Ordinamento);

                    return _ListaNature.ToList();

                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Articoli_Nature>();
        }





        public async Task SyncGEST_Articoli_Nature(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTArticoliNature";

                    await GESTArticoliNatureTable.PullAsync(queryId, GESTArticoliNatureTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_CategorieClienti
        public async Task<List<GEST_CategorieClienti>> GetGEST_CategorieClientiAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_CategorieClienti(true);

                return await GESTCategorieClientiTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_CategorieClienti>();
        }

        public async Task SyncGEST_CategorieClienti(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTCategorieClienti";

                    await GESTCategorieClientiTable.PullAsync(queryId, GESTCategorieClientiTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Condizione_Pagamento
        public async Task<List<GEST_Condizione_Pagamento>> GetGEST_Condizione_PagamentoAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Condizione_Pagamento(true);

                return await GESTCondizionePagamentoTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Condizione_Pagamento>();
        }

        public async Task SyncGEST_Condizione_Pagamento(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTCondizionePagamento";

                    await GESTCondizionePagamentoTable.PullAsync(queryId, GESTCondizionePagamentoTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion
        
        #region GEST_Iva
        public async Task<List<GEST_Iva>> GetGEST_IvaAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Iva(true);

                return await GESTIvaTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Iva>();
        }

        public async Task SyncGEST_Iva(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTIva";

                    await GESTIvaTable.PullAsync(queryId, GESTIvaTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Listini
        public async Task<List<GEST_Listini>> GetGEST_ListiniAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Listini(true);

                return await GESTListiniTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Listini>();
        }

        public async Task SyncGEST_Listini(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTListini";

                    await GESTListiniTable.PullAsync(queryId, GESTListiniTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Ordini_Righe
        public async Task<List<GEST_Ordini_Righe>> GetGEST_Ordini_RigheAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Ordini_Righe(true);

                return (await GESTOrdiniRigheTable.ToListAsync()).Where(x=>x.Deleted!=true && x.CloudState!=((int)CloudState.cancellato) ).ToList();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Ordini_Righe>();
        }

        public async Task<IEnumerable<GEST_Ordini_Righe>> Get_GEST_Ordini_RigheAsync(string _IdSlave, string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(_IdSlave))
                {
                    if (string.IsNullOrEmpty(searchTerm))
                        return (await GESTOrdiniRigheTable.ToListAsync()).Where(x=>x.Deleted!=true && x.CloudState != ((int)CloudState.cancellato));
                    else
                        return (await GESTOrdiniRigheTable.ToListAsync()).Where(x => x.Deleted != true && x.CloudState != ((int)CloudState.cancellato)).Where((x => ((x.CodArt.ToLower().Contains(searchTerm.ToLower()) | x.Descrizione.ToLower().Contains(searchTerm.ToLower()) ))));
                }
                else
                {
                    if (string.IsNullOrEmpty(searchTerm))
                        return  (await GESTOrdiniRigheTable.ToListAsync()).Where(x => x.Deleted != true && x.CloudState != ((int)CloudState.cancellato)).Where(x => x.IdSlave == _IdSlave);
                    else
                        return (await GESTOrdiniRigheTable.ToListAsync()).Where(x => x.Deleted != true && x.CloudState != ((int)CloudState.cancellato)).Where((x => ((x.CodArt.ToLower().Contains(searchTerm.ToLower()) | x.Descrizione.ToLower().Contains(searchTerm.ToLower())) &
                              (x.IdSlave.ToLower() == _IdSlave.ToLower()))));
                }
            }

            
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Ordini_Righe>();
        }

        public async Task SyncGEST_Ordini_Righe(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTOrdiniRighe";

                    await GESTOrdiniRigheTable.PullAsync(queryId, GESTOrdiniRigheTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        private async Task RunSafe(Task task, bool notifyOnError = true, [CallerMemberName] string caller = "")
        {
            Exception exception = null;

            try
            {
                await Task.Run(() =>
                {
                    if (!cancellationTokenSource.IsCancellationRequested)
                        task.Wait();
                });
            }
            catch (TaskCanceledException)
            {

            }
            catch (AggregateException e)
            {
                var ex = e.InnerException;
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                exception = ex;
            }
            catch (Exception e)
            {
                exception = e;
            }
        }

        public async Task SaveGEST_Ordini_RigheAsync(GEST_Ordini_Righe item, bool syncOn = true)
        {
            try
            {
                if (item.Id == null && item.CloudState!=((int)CloudState.cancellato))
                { 
                    await GESTOrdiniRigheTable.InsertAsync(item);
                }
                else
                {
                    await GESTOrdiniRigheTable.UpdateAsync(item);
                }

                if (syncOn)
                    await SyncGEST_Ordini_Righe(true);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }


        //public async Task DeleteGEST_Ordini_RigheAsync(GEST_Ordini_Righe item)
        //{
        //    try
        //    {
        //        if (item.Id == null)
        //            return;

        //        else
        //            await GESTOrdiniRigheTable.DeleteAsync(item);


        //        await SyncGEST_Ordini_Righe(true);


        //    }
        //    catch (MobileServiceInvalidOperationException ex)
        //    {
        //        Debug.WriteLine(@"ERROR {0}", ex.Message);
        //    }
        //    catch (Exception ex2)
        //    {
        //        Debug.WriteLine(@"ERROR {0}", ex2.Message);
        //    }
        //}
        #endregion

        #region GEST_Ordini_Teste
        public async Task<List<GEST_Ordini_Teste>> GetGEST_Ordini_TesteAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Ordini_Teste(true);


                //Attenzione - è profondamente diverso fare GESTOrdiniTesteTable.Where(x => x.Deleted == false).ToListAsync()
                //e quanto sotto - solo con sotto funziona nel modo corretto
               
                return (await GESTOrdiniTesteTable.ToListAsync()).Where(x =>  x.CloudState != ((int)CloudState.cancellato)).ToList();


            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Ordini_Teste>();
        }

		public async Task<GEST_Ordini_Teste> Get_GEST_Ordini_Teste_ByIdDoc(string _IdDoc)
		{
			var result = await GESTOrdiniTesteTable.Where (x => x.Id == _IdDoc).ToEnumerableAsync ();
			return result.SingleOrDefault ();
		}

        //Sicuramente esiste qualche metodo più furbo: ma cosi almeno funge.....
        public async Task<IEnumerable<GEST_Ordini_Teste>> Get_GEST_Ordini_Teste_SearchAsync(string searchTerm, DateTime dallaData, DateTime allaData)
        {

            try
            {

                //Altrimenti non prende gli ordini dell'estremo superore
                DateTime NewAllaData = new DateTime(allaData.Year, allaData.Month, allaData.Day, 23, 59, 59);

				//var result=await GESTOrdiniTesteTable.Where(x=>x.CloudState!=((int)CloudState.cancellato)).ToEnumerableAsync();
				//Folle ma fatto in questo modo è più perfomante
                if (string.IsNullOrEmpty(searchTerm))
                {
					var result= await GESTOrdiniTesteTable.Where(x => x.DataDocumento >= dallaData && 
						x.DataDocumento <= NewAllaData && x.CloudState!=((int)CloudState.cancellato)).ToEnumerableAsync();
					return result;
                }
                else
				{
					var result2 =  await GESTOrdiniTesteTable.Where(x => 
						(x.RagioneSociale.ToLower().Contains(searchTerm.ToLower()) ||
							x.CittaSped.ToLower().Contains(searchTerm.ToLower())) && x.CloudState!=((int)CloudState.cancellato) &&
						x.DataDocumento >= dallaData && x.DataDocumento <= NewAllaData
					).ToEnumerableAsync();
					return result2;
				}
				
				//return result;


            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

			return new List<GEST_Ordini_Teste>();
        }

        public async Task SyncGEST_Ordini_Teste(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTOrdiniTeste";

                    await GESTOrdiniTesteTable.PullAsync(queryId, GESTOrdiniTesteTable.CreateQuery());                    
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task SaveGEST_Ordini_TesteAsync(GEST_Ordini_Teste item,bool syncOn = true)
        {
            try
            {
                if (item.Id == null)
                    await GESTOrdiniTesteTable.InsertAsync(item);
                else
                    await GESTOrdiniTesteTable.UpdateAsync(item);

                if (syncOn)
                    await SyncGEST_Ordini_Teste(true);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        #endregion

        #region GEST_ParametriDevice
        public async Task<List<DEVICE_ParametriDevice>> GetGEST_ParametriDeviceAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_ParametriDevice(true);

                return await GESTParametriDeviceTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<DEVICE_ParametriDevice>();
        }

        public async Task SyncGEST_ParametriDevice(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inzio Sync Parametri Device");

                    string queryId = null;
                    if (incremental) queryId = "GESTParametriDevice";

                    await GESTParametriDeviceTable.PullAsync(queryId, GESTParametriDeviceTable.CreateQuery());
                    MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Fine Sync Parametri Device");
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task SaveGEST_ParametriDeviceAsync(DEVICE_ParametriDevice item)
        {
            try
            {
                if (item.Id == null)
                    await GESTParametriDeviceTable.InsertAsync(item);
                else
                    await GESTParametriDeviceTable.UpdateAsync(item);

                await SyncGEST_ParametriDevice(true);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Scala_Sconti
        public async Task<List<GEST_Scala_Sconti>> GetGEST_Scala_ScontiAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Scala_Sconti(true);

                return await GESTScalaScontiTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Scala_Sconti>();
        }

        public async Task SyncGEST_Scala_Sconti(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTScalaSconti";

                    await GESTScalaScontiTable.PullAsync(queryId, GESTScalaScontiTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_UnitaMisura
        public async Task<List<GEST_UnitaMisura>> GetGEST_UnitaMisuraAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_UnitaMisura(true);

                return await GESTUnitaMisuraTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_UnitaMisura>();
        }

        public async Task SyncGEST_UnitaMisura(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTUnitaMisura";

                    await GESTUnitaMisuraTable.PullAsync(queryId, GESTUnitaMisuraTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Porto
        public async Task<List<GEST_Porto>> GetGEST_PortoAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Porto(true);

                return await GESTPortoTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Porto>();
        }
        public async Task SyncGEST_Porto(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTPorto";

                    await GESTPortoTable.PullAsync(queryId, GESTPortoTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Update_Immagini
        public async Task<List<GEST_Update_Immagini>> GetGEST_Update_ImmaginiAsync(bool syncOn = true)
        {
            try
            {
                if (syncOn)
                    await SyncGEST_Update_Immagini(true);

                return await GESTUpdateImmaginiTable.ToListAsync();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            return new List<GEST_Update_Immagini>();
        }

        public async Task SyncGEST_Update_Immagini(bool incremental)
        {
            try
            {
                await Init();

                var networkConnection = DependencyService.Get<INetworkConnection>();
                networkConnection.CheckNetworkConnection();
                if (networkConnection.IsConnected)
                {
                    string queryId = null;
                    if (incremental) queryId = "GESTUpdateImmagini";

                    await GESTUpdateImmaginiTable.PullAsync(queryId, GESTUpdateImmaginiTable.CreateQuery());
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }

            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);
            }

            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }
        #endregion

        #region GEST_Anagrafiche

        #endregion

        #region GEST_Famiglie e Classi
        //public async Task GEST_Articoli_Famiglie_InsertAsync(GEST_Articoli_Famiglie item)
        //{
        //    //if (item.Id == null)
        //    //    await _GEST_Articoli_Famiglie.InsertAsync(item);
        //    //else
        //    //    await _GEST_Articoli_Famiglie.UpdateAsync(item);

        //}
        //public async Task<IEnumerable<GEST_Articoli_Famiglie>> Get_GEST_Articoli_Famiglie_Async()
        //{
        //    //return await _GEST_Articoli_Famiglie.ToEnumerableAsync();
        //    return null;
        //}


        //public async Task GEST_Articoli_Classi_InsertAsync(GEST_Articoli_Classi item)
        //{
        //    //if (item.Id == null)
        //    //    await _GEST_Articoli_Classi.InsertAsync(item);
        //    //else
        //    //    await _GEST_Articoli_Classi.UpdateAsync(item);

        //}
        //public async Task<IEnumerable<GEST_Articoli_Classi>> Get_GEST_Articoli_Classi_Async()
        //{
        //    //return await _GEST_Articoli_Classi.ToEnumerableAsync();
        //    return null;
        //}


        #endregion

        #region GEST_Ordini reste e righe


  //      public async Task GEST_Ordini_Teste_InsertUpdateAsync(GEST_Ordini_Teste item)
  //      {
  //          //if (item.Id == null)
  //          //    await _GEST_Ordini_Teste.InsertAsync(item);
  //          //else
  //          //    await _GEST_Ordini_Teste.UpdateAsync(item);
            
  //      }

		//public async Task<IEnumerable<GEST_Ordini_Teste>> Get_GEST_Ordini_Teste_Async()
  //      {
  //          //return await _GEST_Ordini_Teste.ToEnumerableAsync();
  //          return null;
  //      }


       



        //public async Task GEST_Ordini_Righe_InsertUpdateAsync(GEST_Ordini_Righe _riga)
        //{
        //    //if (_riga.Id == null)
        //    //    await _GEST_Ordini_Righe.InsertAsync(_riga);
        //    //else
        //    //    await _GEST_Ordini_Righe.UpdateAsync(_riga);

        //}

        //public async Task<IEnumerable<GEST_Ordini_Righe>> Get_GEST_Ordini_Righe_Async(string _IdDocGuid)
        //{

        //    return null;
        //    //IEnumerable < GEST_Ordini_Righe > _Local_GEST_Ordini_Righe= await _GEST_Ordini_Righe.ToEnumerableAsync();

        //    //if (_IdDocGuid.Length <= 0)
        //    //    return _Local_GEST_Ordini_Righe;

        //    //return _Local_GEST_Ordini_Righe.Where(x => x.IdDoc.ToLower().Equals(_IdDocGuid.ToLower()));

        //}




        #endregion

        #region GEST_Articoli_Anagrafica
  //      public async Task GEST_Articoli_Anagrafica_InsertAsync(GEST_Articoli_Anagrafica item)
		//{

		//	//if (item.Id == null)
		//	//	await _GEST_Articoli_Anagrafica.InsertAsync(item);
		//	//else
		//	//	await _GEST_Articoli_Anagrafica.UpdateAsync(item);

		//}

		//public async Task<IEnumerable<GEST_Articoli_Anagrafica>> Get_GEST_Articoli_Anagrafica_Async()
		//{

  //          //return await _GEST_Articoli_Anagrafica.ToEnumerableAsync();
  //          return null;
  //      }


		public async Task<IEnumerable<GEST_Articoli_Anagrafica>> Get_GEST_Articoli_Anagrafica_SearchAsync(string searchTerm,string _Famiglie,string _Classi, string _Nature)
		{

            if (String.IsNullOrEmpty(_Famiglie) && String.IsNullOrEmpty(_Classi) && String.IsNullOrEmpty(_Nature))
                return Enumerable.Empty<GEST_Articoli_Anagrafica>()  ;

           IEnumerable <GEST_Articoli_Anagrafica> _ListaArticoli =
                await (GetGEST_Articoli_AnagraficaAsync(false));

            if (!String.IsNullOrEmpty(searchTerm))
            {
                _ListaArticoli = _ListaArticoli.Where(x => (x.CodArt.ToLower().Contains(searchTerm.ToLower()) ||
                x.Descrizione.ToLower().Contains(searchTerm.ToLower()) && x.Deleted!=true)) ;

            }
            
            if ( _Famiglie.Length>0 )
            {
                string[] _Fam = _Famiglie.Split(',');
                _ListaArticoli = _ListaArticoli.Where(x => _Fam.Contains(x.CodFamiglia));

            }

            if (_Classi.Length > 0)
            {
                string[] _Class = _Classi.Split(',');
                _ListaArticoli = _ListaArticoli.Where(x => _Class.Contains(x.CodClasse));

            }

            if (_Nature.Length > 0)
            {
                string[] _Nat = _Nature.Split(',');
                _ListaArticoli = _ListaArticoli.Where(x => _Nat.Contains(x.CodNatura));

            }
            return _ListaArticoli.OrderBy(b => b.CodMerc);
            
        }

        public async Task<IEnumerable<GEST_Articoli_Listini>> Get_GEST_Articoli_Listino_SearchAsync(string CodListino)
        {
           
            if (String.IsNullOrEmpty(CodListino))
                return Enumerable.Empty<GEST_Articoli_Listini>();


            //IEnumerable<GEST_Articoli_Listini> _ListaListiniArticoli =
            //   await (GetGEST_Articoli_ListiniAsync(false));


            //return  _ListaListiniArticoli.Where(x => x.CodListino.ToLower().Equals(CodListino.ToLower()));
            return (await (GetGEST_Articoli_ListiniAsync(false))).Where(x => x.CodListino.ToLower().Equals(CodListino.ToLower()));

        }

        #endregion

        #region Push Method
        public async Task PushAllChanged()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio invio dati al server");
            try
            {
                Task push = MobileService.SyncContext.PushAsync();
                await push;

                if (push.IsCompleted && !push.IsCanceled && !push.IsFaulted)
                {
                    // Vengono sincronizzati nuovamente gli ordini per aggiornare lo stato 
                    await SyncGEST_Ordini_Teste(true);
                }

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Invio dati effettuato con successo");
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
            catch (MobileServicePushFailedException exPush)
            {
                Debug.WriteLine(@"ERROR {0}", exPush.PushResult.Errors[0].RawResult);

                if (exPush.PushResult != null)
                    syncErrors = exPush.PushResult.Errors;
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.Result != null)
                        await error.UpdateOperationAsync(error.Item);
                }
            }

            //CONTROLLO PER ESTREMA SICUREZZA CHE TUTTO L'ORDINE COMPOLETO SIA ARRIVATO AL SERVER ALKTRIMENTI LO METTIAMO NUOVAMENTE IN STATO DA INVIARE
            var list = (await GetGEST_Ordini_RigheAsync(false)).Where(x => x.CloudState == (int)CloudState.inseritoEnonsincronizzato);
            foreach (var item in list)
            {
                GEST_Ordini_Teste teste = await Get_GEST_Ordini_Teste_ByIdDoc(item.IdSlave);
                if (teste != null)
                {
                    teste.CloudState = (int)CloudState.inseritoEnonsincronizzato;
                }
                await SaveGEST_Ordini_TesteAsync(teste);
            }

            if (list.Count() > 0)
            {
                await App.Current.MainPage.DisplayAlert("Sincronizzazione Ordini", "Problemi nell'invio dell'ordine", "OK");
            }

        }

        public async Task PushAllGEST_Ordini_Async()
        {
            MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Inizio invio ordini");
            List<GEST_Ordini_Teste> ordiniSync = await GESTOrdiniTesteTable.Where(a => a.CloudState == (short)CloudState.inseritoEnonsincronizzato).ToListAsync();

            if (ordiniSync.Count > 0)
            {
                int i = 1;
                int totOrdini = ordiniSync.Count;

                foreach (var ordine in ordiniSync)
                {
                    MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Invio ordine " + i + " di " + totOrdini);

                    // Leggo le righe collegate all'ordine
                    List<GEST_Ordini_Righe> righeOrdiniSync = await GESTOrdiniRigheTable.Where(a => a.IdSlave == ordine.Id).ToListAsync();

                    if (righeOrdiniSync.Count > 0)
                        ordine.RigheOrdine.AddRange(righeOrdiniSync);

                    PushResult result = await PushGEST_Ordini_Async(ordine);

                    if (result.OK)
                    {
                        ordine.CloudState = (short)CloudState.caricatoEsincronizzato;
                        ordine.NumeroOrdineDevice = result.NumeroOrdineDevice;
                        await GESTOrdiniTesteTable.UpdateAsync(ordine);

                        foreach (GEST_Ordini_Righe riga in ordine.RigheOrdine)
                        {
                            riga.CloudState = (short)CloudState.caricatoEsincronizzato;
                            await GESTOrdiniRigheTable.UpdateAsync(riga);
                        }
                    }
                }

                MessagingCenter.Send(this, "REFRESHLBLSTATUS", "Invio terminato con successo");
            }
        }

        public async Task<PushResult> PushGEST_Ordini_Async(GEST_Ordini_Teste ordine)
        {
            var arguments = new Dictionary<string, string>();
            arguments.Add("action", "Added");

            Task<PushResult> insert = MobileService.InvokeApiAsync<GEST_Ordini_Teste, PushResult>("OrdiniSaveApi", ordine, HttpMethod.Post, arguments);
            await insert;

            if (insert.IsCompleted)
                return insert.Result;
            else
                return new PushResult() { OK = false, Message = "Salvataggio non effettuato" };
        }
        #endregion  
    }
}

