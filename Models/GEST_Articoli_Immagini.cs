namespace Mutanda.Models
{
    public class GEST_Articoli_Immagini : BaseModel
    {
        public GEST_Articoli_Immagini()
        {
        }

        public string CodArt { get; set; }
        public string NomeFile { get; set; }
        public bool FotoDefault { get; set; }
    }
}

