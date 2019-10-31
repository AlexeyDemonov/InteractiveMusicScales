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

        public Scale(string name, Sound sound, Sound keynote)
        {
            this.Name = name;
            this.Sound = sound;
            this.Keynote = keynote;
        }
    }
}
