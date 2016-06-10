
using System;
using System.Linq;

namespace Mutanda.Models
{
    public class GEST_CategorieClienti : BaseModel
    { 
        public GEST_CategorieClienti()
        {
        }

        public string CodCatCliente { get; set; }
        public string Descrizione { get; set; }
    }
}

