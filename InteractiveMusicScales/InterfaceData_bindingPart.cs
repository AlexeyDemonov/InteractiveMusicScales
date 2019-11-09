using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InteractiveMusicScales
{
    partial class InterfaceData : INotifyPropertyChanged
    {
        //==========================================================================
        //Binder
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChange([CallerMemberName] string propertyname = default(string))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        //==========================================================================
        //==========================================================================
        //Binded properties

        //==============================================================
        //Common
        public ICommand NoteCommand { get; private set; }
        public Note[] Notes { get; private set; }

        //==============================================================
        //Pianoroll
        Pianoroll pianoroll;
        public Pianoroll Pianoroll
        {
            get => pianoroll;

            private set
            {
                pianoroll = value;
                RaisePropertyChange();
            }
        }

        Semitone pianorollSemitone;
        public Semitone PianorollSemitone
        {
            get => pianorollSemitone;
            set
            {
                pianorollSemitone = value;
                RaisePropertyChange();
            }
        }

        //==============================================================
        //Fretboard
        public ICommand AddStringCommand { get; private set; }
        public ICommand RemoveStringCommand { get; private set; }

        Fretboard fretboard;
        public Fretboard Fretboard
        {
            get => fretboard;
            set
            {
                fretboard = value;
                RaisePropertyChange();
            }
        }

        Semitone fretboardSemitone;
        public Semitone FretboardSemitone
        {
            get => fretboardSemitone;
            set
            {
                fretboardSemitone = value;
                RaisePropertyChange();
            }
        }

        bool[] stringVisibility;
        public bool[] StringVisibility
        {
            get => stringVisibility;
            set
            {
                stringVisibility = value;
                RaisePropertyChange();
            }
        }

        //==============================================================
        //Scale selector
        public ICommand ScaleCommand { get; private set; }
        public ICommand ClearUICommand { get; private set; }

        Scale[] scalesToShow;
        public Scale[] ScalesToShow
        {
            get => scalesToShow;
            set
            {
                scalesToShow = value;
                RaisePropertyChange();
            }
        }

        //==============================================================
        //Scale save/delete
        public ICommand SaveScaleCommand { get; private set; }
        public ICommand DeleteScaleCommand { get; private set; }

        //==============================================================
        //Scales circle
        public ICommand TurnCircleLeftCommand { get; private set; }
        public ICommand TurnCircleRightCommand { get; private set; }

        public Semitone circleSemitone;
        public Semitone CircleSemitone
        {
            get => circleSemitone;
            set
            {
                circleSemitone = value;
                RaisePropertyChange();
            }
        }

        ScalesCirclesHolder scalesCirclesHolder;
        public ScalesCirclesHolder ScalesCirclesHolder
        {
            get => scalesCirclesHolder;
            set
            {
                scalesCirclesHolder = value;
                RaisePropertyChange();
            }
        }

        //==============================================================
        //Localization

    }
}
