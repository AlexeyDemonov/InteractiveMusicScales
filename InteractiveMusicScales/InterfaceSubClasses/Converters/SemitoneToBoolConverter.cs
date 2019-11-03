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
    /// <summary>
    /// Converts value of Semitone enum type to boolean based on equality to provided parameter and backwards
    /// </summary>
    class SemitoneToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null || parameter == null)
                return DependencyProperty.UnsetValue;

            return ((Semitone)value) == (Semitone)Enum.Parse(typeof(Semitone), parameter.ToString(), ignoreCase:true );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return DependencyProperty.UnsetValue;

            if( ((bool)value) == true)
            {
                return (Semitone)(Enum.Parse(typeof(Semitone), parameter.ToString(), ignoreCase: true));
            }
            else
                return DependencyProperty.UnsetValue;
        }
    }
}
