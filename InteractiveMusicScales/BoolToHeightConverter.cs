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
    /// Converts boolean value to height
    /// </summary>
    class BoolToHeightConverter : IValueConverter
    {
        /// <summary>
        /// Returns height specified by convertion parameter or zero based on incoming boolean value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && parameter != null && value is bool && int.TryParse(parameter.ToString(), out int height) == true )
            {
                return ((bool)value == true) ? height : 0;
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
