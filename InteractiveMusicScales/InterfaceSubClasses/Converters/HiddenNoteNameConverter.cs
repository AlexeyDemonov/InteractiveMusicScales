using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales.Interface
{
    class HiddenNoteNameConverter : AbstractNoteNameConverter
    {
        public HiddenNoteNameConverter()
        {
            base.dictionary = new Dictionary<string, string>()
            {
                {"Cd"," "},
                {"Dd"," "},
                {"Fd"," "},
                {"Gd"," "},
                {"Ad"," "},
            };
        }
    }
}
