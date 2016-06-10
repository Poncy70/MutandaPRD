using System;

namespace Mutanda.Models
{
    public class GEST_Update_Immagini : BaseModel
    {
        public GEST_Update_Immagini()
        {
            NomeFile = PathFile = TipoImmagine=string.Empty;
            Data = DateTime.Now;
        }
        
        public string NomeFile { get; set; }
        public string PathFile { get; set; }
        public DateTime Data { get; set; }
        public string TipoImmagine { get; set; }
    }
}

