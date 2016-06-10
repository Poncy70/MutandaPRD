using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace Mutanda.Models
{

    
    public class  GEST_Articoli_Anagrafica_SelezioneArticolo:GEST_Articoli_Anagrafica, INotifyPropertyChanged
	{

        public GEST_Articoli_Anagrafica_SelezioneArticolo(GEST_Articoli_Anagrafica c)
        {
            foreach (PropertyInfo prop in c.GetType().GetRuntimeProperties())
            {

                PropertyInfo prop2 = c.GetType().GetRuntimeProperty(prop.Name);
                prop2.SetValue(this, prop.GetValue(c, null), null);

            }

            //this._DataConsegna = DateTime.Now;
            this._QtaDaOrdinare = 0;
            this._QtaGiaInOrdine = 0;
            this._NCP_QtaScontoMerce=0;
            this.PercSconto1 = c.PercSconto1;
            this.PercSconto2 = c.PercSconto2;
            this.PercSconto3 = c.PercSconto3;
            this.PercSconto4 = c.PercSconto4;
            this.ValUnit = c.PrezzoVendita;

        }


        public GEST_Articoli_Anagrafica_SelezioneArticolo()
		{
           //_DataConsegna = DateTime.Now;
		}

        private decimal _QtaDaOrdinare { get; set; }
        public decimal QtaDaOrdinare
        {
            get { return _QtaDaOrdinare; }
            set
            {
                if (value != _QtaDaOrdinare)
                {

                    if (value>0 && value <=999)
                        _QtaDaOrdinare = value;
                    else
                        _QtaDaOrdinare = 0;
                    OnPropertyChanged("QtaDaOrdinare");
                    RicalcolaRiga();
                }
                

                //if (value > 0 && _NCP_QtaScontoMerce > 0)
                //    NCP_QtaScontoMerce = 0;
            }
        }

        private decimal _NCP_QtaScontoMerce { get; set; }
        public decimal NCP_QtaScontoMerce
        {
            get { return _NCP_QtaScontoMerce; }
            set
            {
               

                if (value != _NCP_QtaScontoMerce)
                {
                    if (value>0 && value <= 999)
                        _NCP_QtaScontoMerce = value;
                    else
                        _NCP_QtaScontoMerce = 0;

                    OnPropertyChanged("NCP_QtaScontoMerce");
                }

                //if (value > 0 && _QtaDaOrdinare > 0)
                //    QtaDaOrdinare = 0;


            }
        }


        private decimal _QtaGiaInOrdine { get; set; }
        public decimal QtaGiaInOrdine
        {
            get
            {
                return _QtaGiaInOrdine;
            }
            set
            {
                _QtaGiaInOrdine = value;
                OnPropertyChanged("QtaGiaInOrdine");

            }
        }

        private decimal _ValUnit { get; set; }
        public decimal ValUnit
        {
            get
            {
                return _ValUnit;
            }
            set
            {
                _ValUnit = value;
                OnPropertyChanged("ValUnit");
                RicalcolaRiga();

            }
        }

        private decimal _PercSconto1 { get; set; }
        new public decimal PercSconto1
        {
            get
            {
                return _PercSconto1;
            }
            set
            {
                if (value != _PercSconto1)
                {
                    if (value > 0 && _PercSconto1<=99)
                        _PercSconto1 = value;
                    else
                        _PercSconto1 = 0;

                    RicalcolaRiga();
                    OnPropertyChanged("PercSconto1");
                }
            }
        }

        private decimal _PercSconto2 { get; set; }
        new public decimal PercSconto2
        {
            get
            {
                return _PercSconto2;
            }
            set
            {
                if (value != _PercSconto2)
                {
                    if (value > 0 && _PercSconto2 <= 99)
                        _PercSconto2 = value;
                    else
                        _PercSconto2 = 0;

                    RicalcolaRiga();
                    OnPropertyChanged("PercSconto2");
                }
            }
        }


        private decimal _PercSconto3 { get; set; }
        public decimal PercSconto3

        {
            get
            {
                return _PercSconto3;
            }
            set
            {
                if (value != _PercSconto3)
                {
                    if (value > 0 && _PercSconto3 <= 99)
                        _PercSconto3 = value;
                    else
                        _PercSconto3 = 0;

                    RicalcolaRiga();
                    OnPropertyChanged("PercSconto3");
                }


            }
        }

        private decimal _PercSconto4 { get; set; }
        public decimal PercSconto4
        {
            get
            {
                return _PercSconto4;
            }
            set
            {
                if (value != _PercSconto4)
                {
                    if (value > 0 && _PercSconto4 <= 99)
                        _PercSconto4 = value;
                    else
                        _PercSconto4 = 0;

                    RicalcolaRiga();
                    OnPropertyChanged("PercSconto4");
                }

            }
        }



        public void RicalcolaRiga()
        {
            try
            {

                
                decimal ImponibileRigaCorrente = QtaDaOrdinare * ValUnit;
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, PercSconto1);
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, PercSconto2);
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, PercSconto3);
                ImponibileRigaCorrente = CalcolaSconto(ImponibileRigaCorrente, PercSconto4);
                Imponibile = Math.Round(ImponibileRigaCorrente, 2);


            }
            catch (Exception e)
            {

                throw;
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

