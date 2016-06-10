namespace Mutanda.Models
{
    public class GEST_Anagrafica_Indirizzi : BaseModel
    {
        public GEST_Anagrafica_Indirizzi()
        {
            RagioneSociale = string.Empty;
        }

        public int IDAnagrafica { get; set; }
        public string IdIndirizzo { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }

    }
}
