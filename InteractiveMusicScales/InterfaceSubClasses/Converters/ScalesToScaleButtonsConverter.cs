using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InteractiveMusicScales.Interface
{
    internal class ScalesToScaleButtonsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Scale[])
            {
                var scales = (Scale[])value;
                int length = scales.Length;

                var buttons = new ScaleButton[length];

                for (int i = 0; i < length; i++)
                {
                    buttons[i] = new ScaleButton() { Scale = scales[i] };
                }

                return buttons;
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