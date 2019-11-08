using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace InteractiveMusicScales.Interface
{
    class NotesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is Note[] && parameter != null && parameter is AbstractTextValueConverter)
            {
                var converter = (AbstractTextValueConverter)parameter;
                var builder = new StringBuilder();

                foreach (var note in (Note[])value)
                {
                    builder.Append
                        (
                        converter.Convert( note.Sound.ToString(), null, null, null)
                        )
                        .Append(" ");
                }

                return builder.ToString();
            }
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
