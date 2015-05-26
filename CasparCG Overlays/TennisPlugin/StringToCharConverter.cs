using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TennisPlugin
{
    class StringToCharConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (value is char)
            {
                var Casted = (char)value;
                if (Casted == '\0') return string.Empty;
                return new string(Casted, 1);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return '\0';
            if (value is string)
            {
                var Casted = (string)value;
                if (Casted.Length == 0) return '\0';
                return Casted[0];
            }
            return Binding.DoNothing;
        }

    }
}
