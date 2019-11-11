using System.Windows;
using System.Windows.Controls;

namespace InteractiveMusicScales.Interface
{
    internal class NoteButton : Button
    {
        public static DependencyProperty NoteProperty = DependencyProperty.Register("Note", typeof(Note), typeof(NoteButton));

        public Note Note
        {
            get => (Note)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }
    }
}