using System.Threading.Tasks;
using System.Collections.Generic;
using Mutanda.Models;
using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;

namespace Mutanda
{
    public interface IDataService
    {
        Task Init();

        #region Ordini Teste e righe
        Task<IEnumerable<GEST_Ordini_Teste>> Get_GEST_Ordini_Teste_SearchAsync(string searchTerm, DateTime dallaData, DateTime allaData);
		Task<GEST_Ordini_Teste> Get_GEST_Ordini_Teste_ByIdDoc (string _IdDoc);
		Task<IEnumerable<GEST_Ordini_Righe>> Get_GEST_Ordini_RigheAsync(string _IdDoc, string searchTerm);
        Task<IEnumerable<GEST_Clienti_Anagrafica>> Get_GEST_Clienti_Anagrafica_SearchAsync( string _filter);
		Task<GEST_Clienti_Anagrafica> Get_GEST_Clienti_Anagrafica_ByIdAnagrafica(int _IdAnag);
        #endregion

        #region FAMIGLIE E CLASSI
        //Task  GEST_Articoli_Famiglie_InsertAsync(GEST_Articoli_Famiglie item);
        //Task<IEnumerable<GEST_Articoli_Famiglie>> Get_GEST_Articoli_Famiglie_Async();
        //Task GEST_Articoli_Classi_InsertAsync(GEST_Articoli_Classi item);
        //Task<IEnumerable<GEST_Articoli_Classi>> Get_GEST_Articoli_Classi_Async();
        #endregion

        Task<IEnumerable<GEST_Articoli_Anagrafica>> Get_GEST_Articoli_Anagrafica_SearchAsync(string searchTerm, string _Famiglie, string _Classi, string _Nature);
        Task<IEnumerable<GEST_Articoli_Listini>> Get_GEST_Articoli_Listino_SearchAsync(string CodListino);
        Task<List<GEST_Articoli_Nature>> GetGEST_Articoli_NatureAsync_SearchAsync(string _MatrixClassi);
        //Task DeleteGEST_Ordini_TesteAsync(GEST_Ordini_Teste item);
        //Task DeleteGEST_Ordini_RigheAsync(GEST_Ordini_Righe item);

        #region Sync----------------------------------------------------------------------------
        Task SyncAll(bool incremental);
        bool DoesLocalDBExist();
        Task DeleteLocalDataBase();
        Task<Authorization> GetAuthorizationAsync();
        Task SaveAuthorizationAsync(Authorization item);
        Task<List<Permissions>> GetPermission_Async(bool syncOn = true);
        Task SyncPermission(bool incremental);

        #region Anagrafica Indirizzi
        Task<List<GEST_Clienti_Anagrafica_Indirizzi>> GetGEST_Clienti_Anagrafica_IndirizziAsync(bool syncOn = true);
        Task SyncGEST_Clienti_Anagrafica_Indirizzi(bool incremental);
        Task SaveGEST_Clienti_Anagrafica_Indirizzi(GEST_Clienti_Anagrafica_Indirizzi item);
        #endregion

        #region Anagrafica Articoli
        Task<List<GEST_Articoli_Anagrafica>> GetGEST_Articoli_AnagraficaAsync(bool syncOn = true);
        Task SyncGEST_Articoli_Anagrafica(bool incremental);
        #endregion

        #region Anagrafica BarCode
        Task<List<GEST_Articoli_BarCode>> GetGEST_Articoli_BarCodeAsync(bool syncOn = true);
        Task SyncGEST_Articoli_BarCode(bool incremental);
        #endregion

        #region Articoli Classi
        Task<List<GEST_Articoli_Classi>> GetGEST_Articoli_ClassiAsync(bool syncOn = true);
        Task SyncGEST_Articoli_Classi(bool incremental);
        #endregion

        #region Articoli Disponibilità
        Task<List<GEST_Articoli_Disponibilita>> GetGEST_Articoli_DisponibilitaAsync(bool syncOn = true);
        Task SyncGEST_Articoli_Disponibilita(bool incremental);
        #endregion

        #region Articoli Famiglie
        Task<List<GEST_Articoli_Famiglie>> GetGEST_Articoli_FamiglieAsync(bool syncOn = true);
        Task SyncGEST_Articoli_Famiglie(bool incremental);
        #endregion

        #region Articoli Immagini
        Task<List<GEST_Articoli_Immagini>> GetGEST_Articoli_ImmaginiAsync(bool syncOn = true);
        Task SyncGEST_Articoli_Immagini(bool incremental);
        #endregion

        #region Articoli Listini
        Task<List<GEST_Articoli_Listini>> GetGEST_Articoli_ListiniAsync(bool syncOn = true);
        Task SyncGEST_Articoli_Listini(bool incremental);
        #endregion

        #region Articoli Nature
        Task<List<GEST_Articoli_Nature>> GetGEST_Articoli_NatureAsync(bool syncOn = true);
        Task SyncGEST_Articoli_Nature(bool incremental);
        #endregion

        #region Categorie Clienti
        Task<List<GEST_CategorieClienti>> GetGEST_CategorieClientiAsync(bool syncOn = true);
        Task SyncGEST_CategorieClienti(bool incremental);
        #endregion

        #region Anagrafica Clienti
        Task<List<GEST_Clienti_Anagrafica>> GetGEST_Clienti_AnagraficaAsync(bool syncOn = true);
        Task SyncGEST_Clienti_Anagrafica(bool incremental);
        Task SaveGEST_Clienti_AnagraficaAsync(GEST_Clienti_Anagrafica item);
        #endregion

        #region Condizioni di pagamento
        Task<List<GEST_Condizione_Pagamento>> GetGEST_Condizione_PagamentoAsync(bool syncOn = true);
        Task SyncGEST_Condizione_Pagamento(bool incremental);
        #endregion

        #region Iva
        Task<List<GEST_Iva>> GetGEST_IvaAsync(bool syncOn = true);
        Task SyncGEST_Iva(bool incremental);
        #endregion

        #region Listini
        Task<List<GEST_Listini>> GetGEST_ListiniAsync(bool syncOn = true);
        Task SyncGEST_Listini(bool incremental);
        #endregion

        #region Righe Ordini
        Task<List<GEST_Ordini_Righe>> GetGEST_Ordini_RigheAsync(bool syncOn = true);
        Task SyncGEST_Ordini_Righe(bool incremental);
        Task SaveGEST_Ordini_RigheAsync(GEST_Ordini_Righe item, bool syncOn = true);
        #endregion

        #region Teste Ordini
        Task<List<GEST_Ordini_Teste>> GetGEST_Ordini_TesteAsync(bool syncOn = true);
        Task SyncGEST_Ordini_Teste(bool incremental);
        Task SaveGEST_Ordini_TesteAsync(GEST_Ordini_Teste item,bool syncOn = true);
        #endregion
        #region Parametri Device
        Task<List<DEVICE_ParametriDevice>> GetGEST_ParametriDeviceAsync(bool syncOn = true);
        Task SyncGEST_ParametriDevice(bool incremental);
        Task SaveGEST_ParametriDeviceAsync(DEVICE_ParametriDevice item);
        #endregion

        #region Scala Sconti
        Task<List<GEST_Scala_Sconti>> GetGEST_Scala_ScontiAsync(bool syncOn = true);
        Task SyncGEST_Scala_Sconti(bool incremental);
        #endregion

        #region Unità di Misura
        Task<List<GEST_UnitaMisura>> GetGEST_UnitaMisuraAsync(bool syncOn = true);
        Task SyncGEST_UnitaMisura(bool incremental);
        #endregion

        #region Update Immagini
        Task<List<GEST_Update_Immagini>> GetGEST_Update_ImmaginiAsync(bool syncOn = true);
        Task SyncGEST_Update_Immagini(bool incremental);
        #endregion

        #region Porto
        Task<List<GEST_Porto>> GetGEST_PortoAsync(bool syncOn = true);
        Task SyncGEST_Porto(bool incremental);
        #endregion

        #endregion

        #region Push Method
        Task PushAllGEST_Ordini_Async();
        Task<PushResult> PushGEST_Ordini_Async(GEST_Ordini_Teste ordine);

        Task PushAllChanged();
        #endregion
    }
}