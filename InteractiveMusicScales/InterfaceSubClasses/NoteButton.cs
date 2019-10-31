using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InteractiveMusicScales
{
    class NoteButton : Button
    {
        public Note Note { get; set; }

        public bool IsSelected
        {
            get => Note?.IsSelected ?? false;
        }

        public bool IsKeyNote
        {
            get => Note?.IsKeynote ?? false;
        }
    }
}
