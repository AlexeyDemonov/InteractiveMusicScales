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
        int lastVisibleString;
        
        //==============================================================
        //Constructor
        public InterfaceData()
        {
            this.NoteCommand = new CommandParametrized( (arg) => ToggleNoteCheck((Note)arg) );

            var notes = new Note[]
            {
                new Note(Sound.C), //0
                new Note(Sound.Cd),//1
                new Note(Sound.D), //2
                new Note(Sound.Dd),//3
                new Note(Sound.E), //4
                new Note(Sound.F), //5
                new Note(Sound.Fd),//6
                new Note(Sound.G), //7
                new Note(Sound.Gd),//8
                new Note(Sound.A), //9
                new Note(Sound.Ad),//10
                new Note(Sound.B)  //11
            };

            this.Notes = notes;

            this.pianoroll = new Pianoroll(notes);
            this.pianorollSemitone = Semitone.Sharp;

            this.fretboard = new Fretboard(notes, strings: STRINGS_COUNT);
            this.fretboardSemitone = Semitone.Sharp;

            this.fretboard[0] = notes[4]; //E
            this.fretboard[1] = notes[11];//B
            this.fretboard[2] = notes[7]; //G
            this.fretboard[3] = notes[2]; //D
            this.fretboard[4] = notes[9]; //A
            this.fretboard[5] = notes[4]; //E
            this.fretboard[6] = notes[11];//B
            this.fretboard[7] = notes[7]; //G
            this.fretboard[8] = notes[2]; //D
            this.fretboard[9] = notes[9]; //A
            this.fretboard[10] = notes[4];//E
            this.fretboard[11] = notes[11];//B

            this.fretboard.Event_RootNotesChanged += UpdateFretBoardNoteBindings;

            this.lastVisibleString = 5;
            this.stringVisibility = new bool[STRINGS_COUNT];

            for (int i = 0; i <= lastVisibleString; i++)
            {
                this.stringVisibility[i] = true;
            }

            this.AddStringCommand = new Command(AddString);
            this.RemoveStringCommand = new Command(RemoveString);


            Scale[] scales = new Scale[]
            {
                new Scale("Test A", Sound.A, Sound.C | Sound.E | Sound.F | Sound.A),
                new Scale("Test B", Sound.B, Sound.Cd | Sound.Dd | Sound.B),
                new Scale("Test C", Sound.C, Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B | Sound.C),
            };

            this.ScalesAll = scales;
            this.scalesToShow = scales;

            this.ScaleCommand = new CommandParametrized( (arg) => UpdateInterfaceWithScale( (Scale)arg ) );
        }


        //==============================================================
        //Methods
        partial void ToggleNoteCheck(Note note);
        partial void UpdateFretBoardNoteBindings();
        partial void AddString();
        partial void RemoveString();
        partial void UpdateInterfaceWithScale(Scale scale);
        partial void DropAllNotes();
    }
}
