using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales.Interface
{
    class SharpScaleNameConverter : AbstractTextValueConverter
    {
        public SharpScaleNameConverter()
        {
            base.dictionary = new Dictionary<string, string>()
            {
                {"C major","C"},
                {"G major","G"},
                {"D major","D"},
                {"A major","A"},
                {"E major","E"},
                {"B major","B"},

                {"Gb major","F#"},
                {"Db major","C#"},
                {"Ab major","G#"},
                {"Eb major","D#"},
                {"Bb major","A#"},

                {"F major","F"},

                {"A minor","Am"},
                {"E minor","Em"},
                {"B minor","Bm"},
                {"F# minor","F#m"},
                {"C# minor","C#m"},
                {"G# minor","G#m"},

                {"Eb minor","D#m"},
                {"Bb minor","A#m"},
                {"F minor","Fm"},
                {"C minor","Cm"},
                {"G minor","Gm"},

                {"D minor","Dm"},
            };
        }
    }
}
