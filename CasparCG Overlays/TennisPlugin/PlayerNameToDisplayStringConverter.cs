using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TennisPlugin
{
    class PlayerNameToDisplayStringConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "null";
            if (value is TennisNameData.IPlayerName)
            {
                var Casted = (TennisNameData.IPlayerName)value;
                return string.Format("{0} ({1} {2})", Casted.ShortName, Casted.FirstName, Casted.LastName);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }

    }
}
