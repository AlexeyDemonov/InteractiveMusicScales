using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales.Interface
{
    class FlatNoteNameConverter : AbstractNoteNameConverter
    {
        public FlatNoteNameConverter()
        {
            base.dictionary = new Dictionary<string, string>()
            {
                {"Cd","Db"},
                {"Dd","Eb"},
                {"Fd","Gb"},
                {"Gd","Ab"},
                {"Ad","Bb"},
            };
        }
    }
}
