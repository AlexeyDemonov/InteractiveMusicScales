using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales.Interface
{
    class SharpNoteNameConverter : AbstractTextValueConverter
    {
        public SharpNoteNameConverter()
        {
            base.dictionary = new Dictionary<string, string>()
            {
                {"Cd","C#"},
                {"Dd","D#"},
                {"Fd","F#"},
                {"Gd","G#"},
                {"Ad","A#"},
            };
        }
    }
}
