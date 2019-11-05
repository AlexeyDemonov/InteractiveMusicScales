using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    partial class InterfaceData
    {
        //==============================================================
        //Fields
        Sound currentSound = 0;
        Sound currentKeyNote = 0;
        const int MIN_STRING_INDEX = 2;
        const int MAX_STRING_INDEX = 11;

        //==============================================================
        //Common
        partial void ToggleNoteCheck(Note note)
        {
            note.IsChecked = !note.IsChecked;

            if (note.IsChecked)
                currentSound |= note.Sound;//Adds note to current sound
            else
                currentSound ^= note.Sound;//Removes note from current sound

            if(currentKeyNote != 0)
                for (int i = 0; i < Notes.Length; i++)
                        Notes[i].IsKeynote = false;

            currentKeyNote = 0;

            FilterAvailableScales();

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


        //==============================================================
        //Fretboard part
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

        //==============================================================
        //Scale selection
        void FilterAvailableScales()
        {
            if(currentSound == 0)
            {
                ScalesToShow = ScalesAll.ToArray();
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
            //Clear interface if applying the same scale
            if(scale.Sound == currentSound && scale.Keynote == currentKeyNote)
            {
                DropAllNotes();
                return;
            }

            currentSound = scale.Sound;
            currentKeyNote = scale.Keynote;

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
            currentSound = 0;
            currentKeyNote = 0;

            foreach (var note in Notes)
            {
                note.IsKeynote = false;
                note.IsChecked = false;
            }

            FilterAvailableScales();
            UpdateAllNoteBindings();
        }
    }
}
