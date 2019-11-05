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
        Scale[] ScalesBasic;
        Scale[] AdditionalScales;
        List<Scale> ScalesAll;

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

            ScalesBasic = new Scale[]
            {
                new Scale("C major", keynote:Sound.C, sound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B),
                new Scale("G major", keynote:Sound.G, sound:Sound.C | Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("D major", keynote:Sound.D, sound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("A major", keynote:Sound.A, sound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("E major", keynote:Sound.E, sound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("B major", keynote:Sound.B, sound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),

                new Scale("Gb major", keynote:Sound.Fd, sound:Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),
                new Scale("Db major", keynote:Sound.Cd, sound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad),
                new Scale("Ab major", keynote:Sound.Gd, sound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("Eb major", keynote:Sound.Dd, sound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("Bb major", keynote:Sound.Ad, sound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.A | Sound.Ad),

                new Scale("F major", keynote:Sound.F, sound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.Ad),

                new Scale("A minor", keynote:Sound.A, sound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B),
                new Scale("E minor", keynote:Sound.E, sound:Sound.C | Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("B minor", keynote:Sound.B, sound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("F# minor", keynote:Sound.Fd, sound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("C# minor", keynote:Sound.Cd, sound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("G# minor", keynote:Sound.Gd, sound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),

                new Scale("Eb minor", keynote:Sound.Dd, sound:Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),
                new Scale("Bb minor", keynote:Sound.Ad, sound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad),
                new Scale("F minor", keynote:Sound.F, sound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("C minor", keynote:Sound.C, sound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("G minor", keynote:Sound.G, sound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.A | Sound.Ad),

                new Scale("D minor", keynote:Sound.D, sound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.Ad),
            };

            this.ScalesAll = new List<Scale>( ScalesBasic );
            this.ScalesAll.Sort(new ScalesSorter());

            this.scalesToShow = ScalesAll.ToArray();

            this.ScaleCommand = new CommandParametrized( (arg) => UpdateInterfaceWithScale( (Scale)arg ) );
            this.ClearUICommand = new Command(DropAllNotes);
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
