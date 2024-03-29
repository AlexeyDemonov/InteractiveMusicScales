﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace InteractiveMusicScales
{
    partial class InterfaceData
    {
        //==============================================================
        //Fields
        private Note currentKeynote;

        private Scale currentShowScale;
        private Sound currentSound = 0;
        private const int MIN_STRING_INDEX = 2;
        private const int MAX_STRING_INDEX = 11;

        //==============================================================
        //Common
        private void MarkIncludedNotes(Sound sound)
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
            UpdateUI();
        }

        partial void ToggleNoteCheck(Note note)
        {
            if (currentShowScale != null)
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
            UpdateUI();
        }

        //==============================================================
        //UI update
        private void UpdateUI()
        {
            UpdatePianorollNoteBindings();
            UpdateFretBoardNoteBindings();
            UpdateScaleBindings();
            UpdateCircleBindings();
        }

        private void UpdatePianorollNoteBindings()
        {
            var pianoSwap = this.Pianoroll;
            this.Pianoroll = null;
            this.Pianoroll = pianoSwap;
        }

        partial void UpdateFretBoardNoteBindings()
        {
            var fretSwap = this.Fretboard;
            this.Fretboard = null;
            this.Fretboard = fretSwap;
        }

        private void UpdateScaleBindings()
        {
            var scaleSwap = this.ScalesToShow;
            this.ScalesToShow = null;
            this.ScalesToShow = scaleSwap;
        }

        private void UpdateCircleBindings()
        {
            var circleSwap = this.ScalesCirclesHolder;
            this.ScalesCirclesHolder = null;
            this.ScalesCirclesHolder = circleSwap;
        }

        //==============================================================
        //Scale selection
        private void RemoveScaleIfAny()
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

        private void FilterAvailableScales()
        {
            if (currentSound == 0)
            {
                ScalesToShow = ScalesAll.ToArray();
            }
            else
            {
                int initialCapacity = ScalesAll.Count >> 1; // equals to ScalesAll.Count / 2
                List<Scale> fittingScales = new List<Scale>(initialCapacity);

                foreach (var scale in ScalesAll)
                {
                    //The magic of bitwise comparison, if 'currentSound' - set of notes is a subset of a scale,
                    //then at the output of the bitwise 'OR' operation we will get the same scale
                    if ((scale.Sound | currentSound) == scale.Sound)
                        fittingScales.Add(scale);
                }

                ScalesToShow = fittingScales.ToArray();
            }
        }

        partial void UpdateInterfaceWithScale(Scale scale)
        {
            if (scale != currentShowScale)
            {
                RemoveScaleIfAny();//Clear previous scale

                currentShowScale = scale;
                scale.IsChecked = true;

                foreach (var note in Notes)
                {
                    if (note.Sound == scale.KeynoteSound)
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

            UpdateUI();
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

        private void UpdateStringsVisibility()
        {
            var stringsSwap = this.StringVisibility;
            this.StringVisibility = null;
            this.StringVisibility = stringsSwap;
        }

        //==============================================================
        //Scale Add/Delete
        partial void RunSaveScaleDialog()
        {
            /*guardians*/
            if (currentShowScale != null)
            {
                WarnUser(this.Localizer["Cannot save the new scale while other scale is in display"]);
                return;
            }

            if (currentSound == 0)
            {
                WarnUser(this.Localizer["Cannot save empty scale"]);
                return;
            }

            var selectedNotes = new List<Note>();

            foreach (var note in Notes)
            {
                if (note.IsChecked)
                    selectedNotes.Add(note);
            }

            var dialogWindow = new ScaleSaveDialogWindow(selectedNotes.ToArray());
            dialogWindow.DataContext = this;
            var dialogResult = dialogWindow.ShowDialog();

            if (dialogResult == true)
            {
                /*guardians*/
                if (dialogWindow.ScaleName == null)
                    throw new ArgumentNullException("dialogWindow.ScaleName");
                if (dialogWindow.KeynoteOfChoice == null)
                    throw new ArgumentNullException("dialogWindow.KeynoteOfChoice");

                string scaleName = dialogWindow.ScaleName;
                Sound keynoteSound = dialogWindow.KeynoteOfChoice.Sound;

                Scale newScale = new Scale(scaleName, keynoteSound, currentSound);

                AdditionalScales.Add(newScale);

                ScalesAll.Add(newScale);
                ScalesAll.Sort(new ScalesSorter());

                Request_SaveAdditionalScales?.Invoke(AdditionalScales.ToArray());

                MessageBox.Show(this.Localizer["Scale successfully saved"]);

                ClearUI();
            }
        }

        private void WarnUser(string warning)
        {
            MessageBox.Show(warning, string.Empty, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        partial void DeleteSelectedScale()
        {
            if (currentShowScale == null)
            {
                WarnUser(this.Localizer["To delete the scale one must first select it"]);
                return;
            }

            var scaleToDelete = currentShowScale;
            bool itIsBasicScale = default(bool);

            foreach (var scale in ScalesBasic)
            {
                if (Object.ReferenceEquals(scaleToDelete, scale))
                {
                    itIsBasicScale = true;
                    break;
                }
            }

            if (itIsBasicScale)
            {
                WarnUser(this.Localizer["Cannot delete basic scale"]);
                return;
            }

            var confirmation = MessageBox.Show(this.Localizer["Delete this scale? Are you sure?"], "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation != MessageBoxResult.Yes)
                return;

            //Point of no return
            AdditionalScales.Remove(scaleToDelete);
            ScalesAll.Remove(scaleToDelete);

            Request_SaveAdditionalScales?.Invoke(AdditionalScales.ToArray());

            MessageBox.Show(this.Localizer["Scale deleted"]);

            ClearUI();
        }

        //==============================================================
        //Scales Circle
        partial void TurnCircleLeft()
        {
            this.scalesCirclesHolder.ShiftLeft();
            UpdateCircleBindings();
        }

        partial void TurnCircleRight()
        {
            this.ScalesCirclesHolder.ShiftRight();
            UpdateCircleBindings();
        }
    }
}