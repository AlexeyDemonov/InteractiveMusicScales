using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveMusicScales.Interface
{
    class NoteButton : Button
    {
        public static DependencyProperty NoteProperty = DependencyProperty.Register("Note", typeof(Note), typeof(NoteButton));

        public Note Note
        {
            get => (Note)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }
    }
}
