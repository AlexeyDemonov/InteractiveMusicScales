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
        //Fields
        const int STRINGS_COUNT = 12;
        const int MIN_STRING_INDEX = 2;
        const int MAX_STRING_INDEX = 11;
        int lastVisibleString;
        
        Sound currentSound = 0;

        //==============================================================
        //Constructor
        public InterfaceData()
        {
            this.NoteCommand = new CommandParametrized( (arg) => ToggleNoteCheck((Note)arg) );

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

            this.Notes = notes;

            this.pianoroll = new Pianoroll(notes);
            this.pianorollSemitone = Semitone.Sharp;

            this.fretboard = new Fretboard(notes, strings: STRINGS_COUNT);
            this.fretboardSemitone = Semitone.Sharp;

            this.fretboard[0] = new Note(Sound.E);
            this.fretboard[1] = new Note(Sound.B);
            this.fretboard[2] = new Note(Sound.G);
            this.fretboard[3] = new Note(Sound.D);
            this.fretboard[4] = new Note(Sound.A);
            this.fretboard[5] = new Note(Sound.E);
            this.fretboard[6] = new Note(Sound.B);
            this.fretboard[7] = new Note(Sound.G);
            this.fretboard[8] = new Note(Sound.D);
            this.fretboard[9] = new Note(Sound.A);
            this.fretboard[10] = new Note(Sound.E);
            this.fretboard[11] = new Note(Sound.B);

            this.fretboard.Event_RootNotesChanged += UpdateFretBoardNoteBindings;

            this.stringVisibility = new bool[] {true, true, true, true, true, true,
                                              false, false, false, false, false, false};

            this.lastVisibleString = 5;

            this.AddStringCommand = new Command(AddString);
            this.RemoveStringCommand = new Command(RemoveString);


            Scale[] scales = new Scale[]
            {
                new Scale("Test A", Sound.A | Sound.C | Sound.E | Sound.F, Sound.A),
                new Scale("Test B", Sound.B | Sound.Cd | Sound.Dd, Sound.B),
                new Scale("Test C", Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B, Sound.C),
            };

            this.ScalesAll = scales;
            this.scalesToShow = scales;
        }


        //==============================================================
        //Methods
        partial void ToggleNoteCheck(Note note);
        partial void UpdateFretBoardNoteBindings();
        partial void AddString();
        partial void RemoveString();
        partial void DropAllNotes();
    }
}
