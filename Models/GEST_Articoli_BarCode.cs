
using System;
using System.Linq;

namespace Mutanda.Models
{
    public class GEST_Articoli_BarCode : BaseModel
    {
        public string CodArt { get; set; }
        public string TextCode { get; set; }
        public int BarCodeType { get; set; }
    }
}
