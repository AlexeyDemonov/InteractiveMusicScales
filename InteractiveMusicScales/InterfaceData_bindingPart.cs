using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InteractiveMusicScales
{
    partial class InterfaceData : INotifyPropertyChanged
    {
        //==========================================================================
        //Binder
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChange([CallerMemberName] string propertyname = default(string))
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
        private Pianoroll pianoroll;

        public Pianoroll Pianoroll
        {
            get => pianoroll;

            private set
            {
                pianoroll = value;
                RaisePropertyChange();
            }
        }

        private Semitone pianorollSemitone;

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

        private Fretboard fretboard;

        public Fretboard Fretboard
        {
            get => fretboard;
            set
            {
                fretboard = value;
                RaisePropertyChange();
            }
        }

        private Semitone fretboardSemitone;

        public Semitone FretboardSemitone
        {
            get => fretboardSemitone;
            set
            {
                fretboardSemitone = value;
                RaisePropertyChange();
            }
        }

        private bool[] stringVisibility;

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

        private Scale[] scalesToShow;

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

        private ScalesCirclesHolder scalesCirclesHolder;

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
        public Localizer Localizer { get; private set; }
    }
}