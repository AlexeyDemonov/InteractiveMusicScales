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
        //==============================================================
        //Properties
        public Note Note
        {
            get => (Note)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }

        public bool IsChecked
        {
            get => Note?.IsChecked ?? false;
        }

        public bool IsKeyNote
        {
            get => Note?.IsKeynote ?? false;
        }


        //==============================================================
        //Dependencies for binding
        public static DependencyProperty NoteProperty = DependencyProperty.Register("Note", typeof(Note), typeof(NoteButton));
    }
}
