
using System;
using System.Linq;

namespace Mutanda.Models
{
    public class GEST_Articoli_Nature : BaseModel
    {
        public GEST_Articoli_Nature()
        {
        }

        public string CodNatura { get; set; }
        public string Descrizione { get; set; }
        public string CodClasse { get; set; }
        public string Icona { get; set; }
        public int Ordinamento { get; set; }
    }
}

