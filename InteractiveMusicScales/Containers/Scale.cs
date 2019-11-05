using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class Scale
    {
        public string Name { get; }
        public Sound Sound { get; }
        public Sound Keynote { get; }

        public Scale(string name, Sound keynote, Sound sound)
        {
            this.Name = name;
            this.Keynote = keynote;
            this.Sound = sound;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
