﻿using System;
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
        Note currentKeynote;
        Scale currentShowScale;
        Sound currentSound = 0;
        const int MIN_STRING_INDEX = 2;
        const int MAX_STRING_INDEX = 11;

        //==============================================================
        //Common
        void MarkIncludedNotes(Sound sound)
        {
            foreach (var note in Notes)
            {
                //The magic of bitwise comparison, if the sound of a note is included to the 'sound' - combination of notes,
                //then at the output of the bitwise 'OR' operation we will get the same 'sound'
                if ((sound | note.Sound) == sound)
                    note.IsChecked = true;
                else
                    note.IsChecked = false;
            }
        }

        partial void ClearUI()
        {
            RemoveScaleIfAny();

            foreach (var note in Notes)
            {
                note.IsChecked = false;
            }

            currentSound = 0;

            FilterAvailableScales();
            UpdateAllNoteBindings();
        }

        partial void ToggleNoteCheck(Note note)
        {
            if(currentShowScale != null)
            {
                //Restore UI before Scale was selected
                RemoveScaleIfAny();
                MarkIncludedNotes(currentSound);
            }

            note.IsChecked = !note.IsChecked;//Will be used in UI to mark note as checked/unchecked

            if (note.IsChecked)
                currentSound |= note.Sound;//Adds note to current sound
            else
                currentSound ^= note.Sound;//Removes note from current sound

            FilterAvailableScales();
            UpdateAllNoteBindings();
        }

        //==============================================================
        //UI update
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
        //Scale selection
        void RemoveScaleIfAny()
        {
            if (currentShowScale != null)
            {
                currentShowScale.IsChecked = false;
                currentShowScale = null;
            }

            if (currentKeynote != null)
            {
                currentKeynote.IsKeynote = false;
                currentKeynote = null;
            }
        }

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
            if(scale != currentShowScale)
            {
                RemoveScaleIfAny();//Clear previous scale

                currentShowScale = scale;
                scale.IsChecked = true;

                foreach (var note in Notes)
                {
                    if( note.Sound == scale.KeynoteSound )
                    {
                        note.IsKeynote = true;
                        currentKeynote = note;
                        break;
                    }
                }

                MarkIncludedNotes(scale.Sound);
            }
            else/* if (scale == currentShowScale)*/
            {
                //Just remove current scale and go back to custom mode
                RemoveScaleIfAny();
                MarkIncludedNotes(currentSound);
            }

            UpdateAllNoteBindings();
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
    }
}
