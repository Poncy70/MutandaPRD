using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;

namespace Mutanda.Converters
{
    public class CellSelectedBackgroundConverter : IValueConverter
    {
        bool _logicaColore;

        //Se logica colore true alloa se il value è true allora allora il colore è default altrimenti viceserva
        public CellSelectedBackgroundConverter(bool logicaColore)
        {
            _logicaColore = logicaColore;

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_logicaColore)
                return (bool)value ? Color.Default : Color.Silver;
            else
                return (bool)value ?  Color.Silver: Color.Default ;

            //return (bool)value ? Color.Default : Color.Silver;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
