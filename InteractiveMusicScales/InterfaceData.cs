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
            Application.Current.Startup += Handle_ApplicationStart;
        }

        //==============================================================
        //Events
        public Func<Scale[]> Request_LoadAdditionalScales;
        public Action<Scale[]> Request_SaveAdditionalScales;
        public Func<SettingsRequestEventArgs> Request_LoadSettings;

        //==============================================================
        //Loading
        void Handle_ApplicationStart(object sender, EventArgs args)
        {
            this.NoteCommand = new CommandParametrized((arg) => ToggleNoteCheck((Note)arg));

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
            this.fretboard = new Fretboard(notes, strings: STRINGS_COUNT);

            var loadedSettings = Request_LoadSettings?.Invoke();

            if(loadedSettings != null)
            {
                this.pianorollSemitone = loadedSettings.PianorollSemitone;
                this.fretboardSemitone = loadedSettings.FretboardSemitone;
                this.lastVisibleString = loadedSettings.LastVisibleString;

                for (int i = 0; i < STRINGS_COUNT; i++)
                {
                    this.fretboard[i] = loadedSettings.FretboardStrings[i];
                }
            }
            else
            {
                this.pianorollSemitone = Semitone.Sharp;
                this.fretboardSemitone = Semitone.Sharp;
                this.lastVisibleString = 5;

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
            }

            this.fretboard.Event_RootNotesChanged += UpdateFretBoardNoteBindings;

            this.stringVisibility = new bool[STRINGS_COUNT];

            for (int i = 0; i <= lastVisibleString; i++)
            {
                this.stringVisibility[i] = true;
            }

            this.AddStringCommand = new Command(AddString);
            this.RemoveStringCommand = new Command(RemoveString);

            ScalesBasic = new Scale[]
            {
                new Scale("C major", keynoteSound:Sound.C, scaleSound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B),
                new Scale("G major", keynoteSound:Sound.G, scaleSound:Sound.C | Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("D major", keynoteSound:Sound.D, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("A major", keynoteSound:Sound.A, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("E major", keynoteSound:Sound.E, scaleSound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("B major", keynoteSound:Sound.B, scaleSound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),

                new Scale("Gb major", keynoteSound:Sound.Fd, scaleSound:Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),
                new Scale("Db major", keynoteSound:Sound.Cd, scaleSound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad),
                new Scale("Ab major", keynoteSound:Sound.Gd, scaleSound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("Eb major", keynoteSound:Sound.Dd, scaleSound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("Bb major", keynoteSound:Sound.Ad, scaleSound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.A | Sound.Ad),

                new Scale("F major", keynoteSound:Sound.F, scaleSound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.Ad),

                new Scale("A minor", keynoteSound:Sound.A, scaleSound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.B),
                new Scale("E minor", keynoteSound:Sound.E, scaleSound:Sound.C | Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("B minor", keynoteSound:Sound.B, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.G | Sound.A | Sound.B),
                new Scale("F# minor", keynoteSound:Sound.Fd, scaleSound:Sound.Cd| Sound.D | Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("C# minor", keynoteSound:Sound.Cd, scaleSound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.A | Sound.B),
                new Scale("G# minor", keynoteSound:Sound.Gd, scaleSound:Sound.Cd| Sound.Dd| Sound.E | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),

                new Scale("Eb minor", keynoteSound:Sound.Dd, scaleSound:Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad| Sound.B),
                new Scale("Bb minor", keynoteSound:Sound.Ad, scaleSound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.Fd| Sound.Gd| Sound.Ad),
                new Scale("F minor", keynoteSound:Sound.F, scaleSound:Sound.C | Sound.Cd| Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("C minor", keynoteSound:Sound.C, scaleSound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.Gd| Sound.Ad),
                new Scale("G minor", keynoteSound:Sound.G, scaleSound:Sound.C | Sound.D | Sound.Dd| Sound.F | Sound.G | Sound.A | Sound.Ad),

                new Scale("D minor", keynoteSound:Sound.D, scaleSound:Sound.C | Sound.D | Sound.E | Sound.F | Sound.G | Sound.A | Sound.Ad),
            };

            this.ScalesAll = new List<Scale>(ScalesBasic);

            AdditionalScales = Request_LoadAdditionalScales?.Invoke() ?? new Scale[0];

            if(AdditionalScales.Length > 0)
                ScalesAll.AddRange(AdditionalScales);

            this.ScalesAll.Sort(new ScalesSorter());
            this.scalesToShow = ScalesAll.ToArray();

            this.ScaleCommand = new CommandParametrized((arg) => UpdateInterfaceWithScale((Scale)arg));
            this.ClearUICommand = new Command(ClearUI);
        }

        //==============================================================
        //Partial Methods
        partial void ToggleNoteCheck(Note note);
        partial void UpdateFretBoardNoteBindings();
        partial void AddString();
        partial void RemoveString();
        partial void UpdateInterfaceWithScale(Scale scale);
        partial void ClearUI();
    }
}
