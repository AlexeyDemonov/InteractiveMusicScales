using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InteractiveMusicScales
{
    partial class InterfaceData
    {
        public InterfaceData()
        {
            var notes = new Note[]
            {
                new Note(Sound.C),
                new Note(Sound.Cd),
                new Note(Sound.D),
                new Note(Sound.Dd),
                new Note(Sound.E),
                new Note(Sound.F),
                new Note(Sound.Fd),
                new Note(Sound.G),
                new Note(Sound.Gd),
                new Note(Sound.A),
                new Note(Sound.Ad),
                new Note(Sound.B)
            };

            this.pianoroll = new Pianoroll(notes);

            this.NoteCommand = new CommandParametrized( (arg) => CheckNote((Note)arg) );
        }

        void CheckNote(Note note)
        {
            note.IsChecked = !note.IsChecked;

            var pianoSwap = this.Pianoroll;
            this.Pianoroll = null;
            this.Pianoroll = pianoSwap;
        }
    }
}
