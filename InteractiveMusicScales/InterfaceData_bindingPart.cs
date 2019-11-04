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
        //Buttons
        public ICommand NoteCommand { get; }

        //==============================================================
        //Notes
        public Note[] Notes { get; }

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
        public ICommand AddStringCommand { get; }
        public ICommand RemoveStringCommand { get; }

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
        Scale[] ScalesAll;

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

        Scale currentScale;
        public Scale CurrentScale
        {
            get => currentScale;
            set
            {
                if(value != null)
                {
                    currentScale = value;
                    UpdateInterfaceWithScale(currentScale);
                    RaisePropertyChange();
                }
            }
        }

        //==========================================================================
        //==========================================================================
        //Methods
        partial void UpdateInterfaceWithScale(Scale scale);
    }
}
