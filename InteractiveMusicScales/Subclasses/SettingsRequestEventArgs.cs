using System;

namespace InteractiveMusicScales
{
    internal class SettingsRequestEventArgs : EventArgs
    {
        public readonly Semitone PianorollSemitone;
        public readonly Semitone FretboardSemitone;
        public readonly Semitone CircleSemitone;
        public readonly Note[] FretboardStrings;
        public readonly int LastVisibleString;

        public SettingsRequestEventArgs(Semitone pianorollSemitone, Semitone fretboardSemitone, Semitone circleSemitone, Note[] fretboardStrings, int lastVisibleString)
        {
            if (fretboardStrings == null)
                throw new ArgumentNullException(nameof(fretboardStrings));

            this.PianorollSemitone = pianorollSemitone;
            this.FretboardSemitone = fretboardSemitone;
            this.CircleSemitone = circleSemitone;
            this.FretboardStrings = fretboardStrings;
            this.LastVisibleString = lastVisibleString;
        }
    }
}