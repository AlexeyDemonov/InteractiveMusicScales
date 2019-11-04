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

            for (int i = 0; i < Notes.Length; i++)
            {
                if(Notes[i].IsKeynote)
                    Notes[i].IsKeynote = false;
            }

            FilterAvailableScales(note);

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

        void FilterAvailableScales(Note note)
        {

            if(note.IsChecked)
                currentSound |= note.Sound;//Adds note to current sound
            else
                currentSound ^= note.Sound;//Removes note from current sound


            if(currentSound == 0)
            {
                ScalesToShow = ScalesAll;
            }
            else
            {
                List<Scale> fittingScales = new List<Scale>();

                foreach (var scale in ScalesAll)
                {
                    //The magic of bitwise comparison, if 'currentSound' - set of notes is a subset of a scale,
                    //then at the output of the bitwise 'OR' operation we will get the same scale
                    if( (scale.Sound | currentSound) == scale.Sound )
                        fittingScales.Add(scale);
                }

                ScalesToShow = fittingScales.ToArray();
            }
        }

        partial void UpdateInterfaceWithScale(Scale scale)
        {
            currentSound = scale.Sound;

            foreach (var note in Notes)
            {
                if(note.Sound == scale.Keynote)
                    note.IsKeynote = true;
                else
                    note.IsKeynote = false;

                //The magic of bitwise comparison, if the sound of a note is included to the scale,
                //then at the output of the bitwise 'OR' operation we will get the same scale
                if ( (scale.Sound | note.Sound) == scale.Sound)
                    note.IsChecked = true;
                else
                    note.IsChecked = false;
            }

            UpdateAllNoteBindings();
        }

        partial void DropAllNotes()
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
