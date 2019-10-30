using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class Note
    {
        public Sound Sound { get; }

        public bool IsSelected { get; set; } = false;

        public bool IsKeynote { get; set; } = false;


        public Note(Sound sound)
        {
            this.Sound = sound;
        }


        public override int GetHashCode()
        {
            return (int)Sound;
        }

        public override bool Equals(object obj)
        {
            if (obj is Note)
            {
                return this.Sound == ((Note)obj).Sound;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return Sound.ToString();
        }
    }
}
