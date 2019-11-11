using System.Windows;
using System.Windows.Controls;

namespace InteractiveMusicScales.Interface
{
    internal class ScaleButton : Button
    {
        public static DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(Scale), typeof(ScaleButton));

        public Scale Scale
        {
            get => (Scale)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }
    }
}