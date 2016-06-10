using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Mutanda.Models
{
	public class SelezioneListView : INotifyPropertyChanged
	{
	    public string Item { get; set; }
	    bool isSelected = false;

        string _Descrizione = string.Empty;
	    public bool IsSelected
	    {
	        get
	        {
	            return isSelected;
	        }
	        set
	        {
	            if (isSelected != value)
	            {
	                isSelected = value;
	                PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
	            }
	        }
	    }


        public string Descrizione
        {
            get
            {
                return _Descrizione;
            }
            set
            {
                if (_Descrizione != value)
                {
                    _Descrizione = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Descrizione"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
	}
}

