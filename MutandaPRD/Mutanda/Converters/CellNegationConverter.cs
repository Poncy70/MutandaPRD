using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;

namespace Mutanda.Converters
{
    public class CellNegationConverter : IValueConverter
    {
        
        public CellNegationConverter()
        {
            
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return !(bool)value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
