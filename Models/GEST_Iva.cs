namespace Mutanda.Models
{
    public class GEST_Iva : BaseModel
    {
        public GEST_Iva()
        {
        }
        
        public string CodIva { get; set; }
        public string Descrizione { get; set; }
        public decimal PercIva { get; set; }

    }
}

