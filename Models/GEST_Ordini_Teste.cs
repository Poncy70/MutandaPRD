using System;
using System.Collections.Generic;

namespace Mutanda.Models
{
    public class GEST_Ordini_Teste : BaseModel
    {
        public GEST_Ordini_Teste()
        {
            RigheOrdine = new List<GEST_Ordini_Righe>();
        }

        public string RagioneSociale { get; set; }
        public string PartitaIva { get; set; }
        public string CodiceFiscale { get; set; }
        public DateTime DataDocumento { get; set; }
        public string NumeroOrdineDevice { get; set; }
        public string CodPagamento { get; set; }
        public string CodListino { get; set; }
        public decimal TotaleDocumento { get; set; }
        public short CloudState { get; set; }
        public DateTime? DataConsegna { get; set; }
        public decimal TotaleConsegna { get; set; }
        public int IdAgente { get; set; }
        public int IdAnagrafica { get; set; }
        public string Note { get; set; }
        public int IdIndSpedMerce { get; set; }
        public string RagSocSped { get; set; }
        public string IndirizzoSped { get; set; }
        public string CittaSped { get; set; }
        public string CapSped { get; set; }
        public string ProvSped { get; set; }
        public string NazioneSped { get; set; }
        public string DeviceMail { get; set; }
        public string IdDevice { get; set; }
        public int NrRigheTot { get; set; }
        public List<GEST_Ordini_Righe> RigheOrdine { get; set; }
    }
}
