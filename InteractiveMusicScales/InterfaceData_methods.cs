using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    partial class InterfaceData
    {
        partial void ToggleNoteCheck(Note note)
        {
            note.IsChecked = !note.IsChecked;
            UpdateAllNoteBindings();
        }

        void UpdateAllNoteBindings()
        {
            var pianoSwap = this.Pianoroll;
            this.Pianoroll = null;
            this.Pianoroll = pianoSwap;

            UpdateFretBoardNoteBindings();
        }

        partial void UpdateFretBoardNoteBindings()
        {
            var fretSwap = this.Fretboard;
            this.Fretboard = null;
            this.Fretboard = fretSwap;
        }

        partial void AddString()
        {
            if (lastVisibleString < MAX_STRING_INDEX)
            {
                lastVisibleString++;
                this.stringVisibility[lastVisibleString] = true;

                UpdateStringsVisibility();
            }
        }

        partial void RemoveString()
        {
            if (lastVisibleString > MIN_STRING_INDEX)
            {
                this.stringVisibility[lastVisibleString] = false;
                lastVisibleString--;

                UpdateStringsVisibility();
            }
        }

        void UpdateStringsVisibility()
        {
            var stringsSwap = this.StringVisibility;
            this.StringVisibility = null;
            this.StringVisibility = stringsSwap;
        }

        partial void UpdateInterfaceWithScale(Scale scale)
        {
            foreach (var note in Notes)
            {
                if(note.Sound == scale.Keynote)
                    note.IsKeynote = true;
                else
                    note.IsKeynote = false;

                //The magic of bitwise comparison, if the sound of a note is included in the scale
                //at the output of the bitwise OR we will get the same scale
                if ( (scale.Sound | note.Sound) == scale.Sound)
                    note.IsChecked = true;
                else
                    note.IsChecked = false;
            }

            UpdateAllNoteBindings();
        }

        void DropAllNotes()
        {
            foreach (var note in Notes)
            {
                note.IsKeynote = false;
                note.IsChecked = false;
            }

            UpdateAllNoteBindings();
        }
    }
}
