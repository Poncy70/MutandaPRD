using System;
using Xamarin.Forms;
using System.Globalization;
//using Mutanda.Models.Local;
using Mutanda.Services;

namespace Mutanda.Converters
{
    public class CloutStateToString : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returnValue = string.Empty;

            if (value == null)
                return string.Empty;

            if (value is short)
            {
                var newValue = value as short?;

                returnValue = (newValue == ((int)CloudState.caricatoEsincronizzato)) ? "Inviato" : "Non Inviato";
            }
            return returnValue;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
