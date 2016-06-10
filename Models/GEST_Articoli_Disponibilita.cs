
using System;
using System.Linq;

namespace Mutanda.Models
{
    public class GEST_Articoli_Disponibilita : BaseModel
    {
        public GEST_Articoli_Disponibilita()
        {
        }

        public string CodDep { get; set; }
        public string CodArt { get; set; }
        public decimal QtaDisponibile { get; set; }
        public decimal QtaDisponibileSuArrivi { get; set; }
    }
}

