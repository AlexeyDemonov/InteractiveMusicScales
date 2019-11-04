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
        //==============================================================
        //Binder
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChange([CallerMemberName] string propertyname = default(string))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        //==============================================================
        //Binded properties
        public ICommand NoteCommand { get; }

        public Note[] Notes { get; }

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

        public ICommand AddStringCommand { get; }
        public ICommand RemoveStringCommand { get; }
    }
}
