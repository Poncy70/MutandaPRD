﻿namespace Mutanda.Models
{
    public class GEST_Articoli_Famiglie : BaseModel
    {
        public GEST_Articoli_Famiglie()
        {
        }

        public string CodFamiglia { get; set; }
        public string Descrizione { get; set; }
        public string Icona { get; set; }
        public int Ordinamento { get; set; }
    }
}

