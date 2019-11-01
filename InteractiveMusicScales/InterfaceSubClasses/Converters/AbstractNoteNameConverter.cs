using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InteractiveMusicScales.Interface
{
    abstract class AbstractNoteNameConverter : IValueConverter
    {
        protected Dictionary<string,string> dictionary;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && dictionary.TryGetValue(value.ToString(), out string convertedName) == true)
                return convertedName;
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
