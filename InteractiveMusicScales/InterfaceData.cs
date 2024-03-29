﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace InteractiveMusicScales
{
    partial class InterfaceData
    {
        //==============================================================
        //Fields
        private const int STRINGS_COUNT = 12;

        private int lastVisibleString;
        private Scale[] ScalesBasic;
        private List<Scale> AdditionalScales;
        private List<Scale> ScalesAll;

        //==============================================================
        //Constructor
        public InterfaceData()
        {
            Application.Current.Startup += Handle_ApplicationStart;
            Application.Current.Exit += Handle_ApplicationExit;
        }

        //==============================================================
        //Events
        public Func<Scale[]> Request_LoadAdditionalScales;

        public Action<Scale[]> Request_SaveAdditionalScales;
        public Func<SettingsRequestEventArgs> Request_LoadSettings;
        public Action<SettingsRequestEventArgs> Request_SaveSettings;
        public Func<Dictionary<string, string>> Request_LoadLocalization;

        //==============================================================
        //Loading
        private void Handle_ApplicationStart(object sender, EventArgs args)
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

            if (loadedSettings != null)
            {
                this.pianorollSemitone = loadedSettings.PianorollSemitone;
                this.fretboardSemitone = loadedSettings.FretboardSemitone;
                this.circleSemitone = loadedSettings.CircleSemitone;
                this.lastVisibleString = loadedSettings.LastVisibleString;

                for (int i = 0; i < STRINGS_COUNT && i < loadedSettings.FretboardStrings.Length; i++)
                {
                    this.fretboard[i] = loadedSettings.FretboardStrings[i];
                }
            }
            else
            {
                this.pianorollSemitone = Semitone.Sharp;
                this.fretboardSemitone = Semitone.Sharp;
                this.circleSemitone = Semitone.Flat;
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

            this.ScalesBasic = new Scale[]
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

            var loadedScales = Request_LoadAdditionalScales?.Invoke();

            if (loadedScales != null && loadedScales.Length > 0)
            {
                AdditionalScales = new List<Scale>(loadedScales);
                ScalesAll.AddRange(AdditionalScales);
            }
            else
            {
                AdditionalScales = new List<Scale>();
            }

            this.ScalesAll.Sort(new ScalesSorter());
            this.scalesToShow = ScalesAll.ToArray();

            this.ScaleCommand = new CommandParametrized((arg) => UpdateInterfaceWithScale((Scale)arg));

            this.ClearUICommand = new Command(ClearUI);
            this.SaveScaleCommand = new Command(RunSaveScaleDialog);
            this.DeleteScaleCommand = new Command(DeleteSelectedScale);

            this.scalesCirclesHolder = new ScalesCirclesHolder(ScalesBasic, divideToNumberOfCircles: 2);
            this.TurnCircleLeftCommand = new Command(TurnCircleLeft);
            this.TurnCircleRightCommand = new Command(TurnCircleRight);

            this.Localizer = new Localizer(Request_LoadLocalization?.Invoke());
        }

        //==============================================================
        //Partial Methods
        partial void ToggleNoteCheck(Note note);

        partial void UpdateFretBoardNoteBindings();

        partial void AddString();

        partial void RemoveString();

        partial void UpdateInterfaceWithScale(Scale scale);

        partial void ClearUI();

        partial void RunSaveScaleDialog();

        partial void DeleteSelectedScale();

        partial void TurnCircleLeft();

        partial void TurnCircleRight();

        //==============================================================
        //Closing the app
        private void Handle_ApplicationExit(object sender, EventArgs args)
        {
            var fretboardStrings = new Note[STRINGS_COUNT];

            for (int i = 0; i < fretboardStrings.Length; i++)
            {
                fretboardStrings[i] = this.fretboard[i];
            }

            var settingsToSave = new SettingsRequestEventArgs
                (
                    pianorollSemitone: this.pianorollSemitone,
                    fretboardSemitone: this.fretboardSemitone,
                    circleSemitone: this.circleSemitone,
                    fretboardStrings: fretboardStrings,
                    lastVisibleString: this.lastVisibleString
                );

            Request_SaveSettings?.Invoke(settingsToSave);
        }
    }
}