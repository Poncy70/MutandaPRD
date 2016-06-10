using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mutanda.Converters
{
    public class NumeroOrdineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnValue = string.Empty;

            if (value == null)
                return "Da Assegnare";

            //if (value is int)
            //{
            //    var newValue = value as int?;

            //    returnValue = (newValue == (int)CloudState.Default) ? "Inviato" : "Non Inviato";
            //}
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
