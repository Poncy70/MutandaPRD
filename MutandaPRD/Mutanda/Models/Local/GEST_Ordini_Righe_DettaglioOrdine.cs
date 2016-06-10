using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using Mutanda.Services;

namespace Mutanda.Models
{

    
    public class GEST_Ordini_Righe_DettaglioOrdine : GEST_Ordini_Righe, INotifyPropertyChanged
	{
        private bool _SenzaRicalcoloRiga;
        private bool _SenzaControlloQta;

        public GEST_Ordini_Righe_DettaglioOrdine()
        {

        }


        //se SenzaRicalcolo = false la riga NON viene ricalcolata
        public GEST_Ordini_Righe_DettaglioOrdine(bool SenzaRicalcoloRiga,bool SenzaControlloQta)
        {
            _SenzaRicalcoloRiga = SenzaRicalcoloRiga;
            _SenzaControlloQta = SenzaControlloQta;
        }

        //private readonly Task _initializingTask;
        public  GEST_Ordini_Righe_DettaglioOrdine(GEST_Ordini_Righe c)

        {

            foreach (PropertyInfo prop in c.GetType().GetRuntimeProperties())
            {

                PropertyInfo prop2 = c.GetType().GetRuntimeProperty(prop.Name);

                prop2.SetValue(this, prop.GetValue(c, null), null);

            }

            //Essendo ridefiniti occorre esplicitamente instanziarli
            Qta = c.Qta;
            NCP_QtaScontoMerce = c.NCP_QtaScontoMerce;
            Sc1 = c.Sc1;
            Sc2 = c.Sc2;
            Sc3 = c.Sc3;
            Sc4 = c.Sc4;
            ValUnit = c.ValUnit;
            Imponibile = c.Imponibile;
            DataPresuntaConsegna = c.DataPresuntaConsegna;

        }

        


        public void RicalcolaRiga()
        {
            try
            {

                if (_SenzaRicalcoloRiga)
                    return;

                //if (Qta > 0 && NCP_QtaScontoMerce > 0)
                //    NCP_QtaScontoMerce = 0;

                //decimal QtaDaConsiderareNelCalcolo = 0;
                //if (Qta > 0)
                //    QtaDaConsiderareNelCalcolo = Qta;
                //else
                //    QtaDaConsiderareNelCalcolo = NCP_QtaScontoMerce;


                decimal ImponibileRigaCorrente = Qta * ValUnit;
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, Sc1);
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, Sc2);
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, Sc3);
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, Sc4);
                Imponibile = Math.Round(ImponibileRigaCorrente, 2);


            }
            catch (Exception e)
            {

                throw;
            }


            
        }

        private decimal CalcolaSconto(decimal _Importo, decimal _Percentuale)
        {
            try
            {
                if (_Percentuale <= 0)
                    return _Importo;

                decimal discount_value = (((_Importo) * (_Percentuale))) / 100;
                return (_Importo - Math.Round(discount_value, 2));
            }
            catch (Exception e)
            {

                throw;
            }



            
        }

        private decimal _Qta { get; set; }
        new public decimal Qta
        {
            get { return _Qta; }
            set
            {

                if (value != _Qta)
                {
                    if (_SenzaControlloQta)
                        _Qta = value;
                    else
                    { 
                        if (value>0 && value <= 999)
                            _Qta = value;
                        else
                            _Qta = 0;
                    }
                    OnPropertyChanged("Qta");
                    
                }

                //if (value > 0 && _NCP_QtaScontoMerce > 0)
                //    NCP_QtaScontoMerce = 0;

                RicalcolaRiga();

            }
        }

        private decimal _NCP_QtaScontoMerce { get; set; }
        new public decimal NCP_QtaScontoMerce
        {
            get { return _NCP_QtaScontoMerce; }
            set
            {
                if (value != _NCP_QtaScontoMerce)
                {
                    if(_SenzaControlloQta)
                        _NCP_QtaScontoMerce = value;
                    else
                    { 

                        if (value>0 && value <= 999 && !_SenzaRicalcoloRiga)
                            _NCP_QtaScontoMerce = value;
                        else
                            _NCP_QtaScontoMerce = 0;
                    }
                    OnPropertyChanged("NCP_QtaScontoMerce");
                    
                }
                //if (value > 0 && _Qta > 0)
                //    Qta = 0;

                //RicalcolaRiga();
            }
        }


        //Mi serve per raggruppare nel dettaglio finale
        private string _CodClasse { get; set; }
        public string CodClasse
        {
            get { return _CodClasse; }
            set
            {

                _CodClasse = value;
                OnPropertyChanged("CodClasse");
            }
        }


        //Utilizzato per il grouping dove non si riesce ad ottenere la descrizione della classe
        private string _CodClasseConDescrizione { get; set; }
        public string CodClasseConDescrizione
        {
            get { return _CodClasseConDescrizione; }
            set
            {

                _CodClasseConDescrizione = value;
                OnPropertyChanged("CodClasseConDescrizione");
            }
        }

        private string _CodFamiglia { get; set; }
        public string CodFamiglia
        {
            get { return _CodFamiglia; }
            set
            {

                _CodClasse = value;
                OnPropertyChanged("CodFamiglia");
            }
        }

        private string _CodNatura { get; set; }
        public string CodNatura
        {
            get { return _CodNatura; }
            set
            {

                _CodNatura = value;
                OnPropertyChanged("CodNatura");
            }
        }


        private decimal _Sc1 { get; set; }
        new public decimal Sc1
        {
            get { return _Sc1; }
            set
            {

                if (value != _Sc1)
                {
                    if (value > 0 && value <= 99)
                        _Sc1 = value;
                    else
                        _Sc1 = 0;

                    OnPropertyChanged("Sc1");
                    RicalcolaRiga();
                }

               
            }
        }

        private decimal _Sc2 { get; set; }
        new public decimal Sc2
        {
            get { return _Sc2; }
            set
            {


                if (value != _Sc2)
                {
                    if (value > 0 && value <= 99)
                        _Sc2 = value;
                    else
                        _Sc2 = 0;
                    OnPropertyChanged("Sc2");
                    RicalcolaRiga();
                }
            }
        }

        private decimal _Sc3 { get; set; }
        new public decimal Sc3
        {
            get { return _Sc3; }
            set
            {
                if (value > 0 && value <= 99)
                {
                    if (value > 0)
                        _Sc3 = value;
                    else
                        _Sc3 = 0;
                    OnPropertyChanged("Sc3");
                    RicalcolaRiga();
                }


            }
        }

        private decimal _Sc4 { get; set; }
        new public decimal Sc4
        {
            get { return _Sc4; }
            set
            {

                if (value > 0 && value <= 99)
                {
                    if (value > 0)
                        _Sc4 = value;
                    else
                        _Sc4 = 0;
                    OnPropertyChanged("Sc4");
                    RicalcolaRiga();
                }
            }
        }

        private decimal _ValUnit { get; set; }
        new public decimal ValUnit
        {
            get { return _ValUnit; }
            set
            {
                
                if (value != _ValUnit)
                {
                    _ValUnit = value;
                    OnPropertyChanged("ValUnit");
                    RicalcolaRiga();
                }

            }
        }

        private decimal _Imponibile { get; set; }
        new public decimal Imponibile
        {
            get { return _Imponibile; }
            set
            {
                _Imponibile = value;
                OnPropertyChanged("Imponibile");
            }
        }


        private DateTime? _DataPresuntaConsegna { get; set; }
        new public DateTime ? DataPresuntaConsegna
        {
            get { return _DataPresuntaConsegna; }
            set
            {
                _DataPresuntaConsegna = value;
                OnPropertyChanged("DataPresuntaConsegna");
            }
        }


        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set {
                _isSelected = value;
                OnPropertyChanged("IsSelected"); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }

    }
}

