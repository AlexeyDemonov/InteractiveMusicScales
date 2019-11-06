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
        public Sound KeynoteSound { get; }
        public bool IsChecked { get; set; }

        public Scale(string name, Sound keynoteSound, Sound scaleSound)
        {
            this.Name = name;
            this.KeynoteSound = keynoteSound;
            this.Sound = scaleSound;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
