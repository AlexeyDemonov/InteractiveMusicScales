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
        //==============================================================
        //Constructor
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
            this.pianorollSemitone = Semitone.Sharp;

            this.NoteCommand = new CommandParametrized( (arg) => ToggleNoteCheck((Note)arg) );
        }


        //==============================================================
        //Methods
        void ToggleNoteCheck(Note note)
        {
            note.IsChecked = !note.IsChecked;
            UpdateAllNoteBindings();
        }

        void UpdateAllNoteBindings()
        {
            var pianoSwap = this.Pianoroll;
            this.Pianoroll = null;
            this.Pianoroll = pianoSwap;
        }
    }
}
