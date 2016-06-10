
using System;
using System.Linq;

namespace Mutanda.Models
{
    public class GEST_Ordini_Righe : BaseModel
    {
        public GEST_Ordini_Righe()
        {

        }

        //Eì il legame con le teste
        //Assume il valore Id della testa
        public string IdSlave { get; set; }
        public int IdRiga { get; set; }
        public short TipoRiga { get; set; }
        public string CodArt { get; set; }
        public string Descrizione { get; set; }
        public decimal Qta { get; set; }
        public string CodUnMis { get; set; }
        public string CodIva { get; set; }
        public decimal ValUnit { get; set; }
        public decimal Sc1 { get; set; }
        public decimal Sc2 { get; set; }
        public decimal Sc3 { get; set; }
        public decimal Sc4 { get; set; }
        public decimal ImportoSconto { get; set; }
        public DateTime? DataPresuntaConsegna { get; set; }
        public decimal Imponibile { get; set; }
        public decimal Imposta { get; set; }
        public decimal Totale { get; set; }
        public decimal NCP_QtaScontoMerce { get; set; }
        public short CloudState { get; set; }
    }
}
