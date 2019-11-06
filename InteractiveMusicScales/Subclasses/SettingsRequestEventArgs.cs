﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InteractiveMusicScales
{
    class SettingsRequestEventArgs : EventArgs
    {
        public readonly Semitone PianorollSemitone;
        public readonly Semitone FretboardSemitone;
        public readonly Note[] FretboardStrings;
        public readonly int LastVisibleString;

        public SettingsRequestEventArgs(Semitone pianorollSemitone, Semitone fretboardSemitone, Note[] fretboardStrings, int lastVisibleString)
        {
            if(fretboardStrings == null)
                throw new ArgumentNullException(nameof(fretboardStrings));

            this.PianorollSemitone = pianorollSemitone;
            this.FretboardSemitone = fretboardSemitone;
            this.FretboardStrings = fretboardStrings;
            this.LastVisibleString = lastVisibleString;
        }
    }
}