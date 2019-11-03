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
        public ICommand NoteCommand { get; private set; }

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
    }
}
