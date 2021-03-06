﻿using System;

namespace Mutanda.Models
{
    public class GEST_ParametriDevice : BaseModel
    {
        public GEST_ParametriDevice()
        {
        }
        
        public string CodListino { get; set; }
        public int VisualizzazioneRigaGriglia { get; set; }
        public int VisualizzazioneInSchermata { get; set; }
        public DateTime ? UltimoAggiornamentoDati { get; set; }
        public DateTime ? UltimoAggiornamentoImmagini { get; set; }
        public string PrefissoNumerazione { get; set; }
        public string SuffissoNumerazione { get; set; }
        public bool AbilitaCodUnMisListino { get; set; }
        public bool ModificaQuantitaManuale { get; set; }
        public int TipoApplicazione { get; set; }
        public bool QtaCompilateDefault { get; set; }
        public int VersionCode { get; set; }
        public string VersionName { get; set; }
        public string DeviceMail { get; set; }
    }
}

