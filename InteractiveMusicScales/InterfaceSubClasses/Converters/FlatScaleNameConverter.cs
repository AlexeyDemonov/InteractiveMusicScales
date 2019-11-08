using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales.Interface
{
    class FlatScaleNameConverter : AbstractTextValueConverter
    {
        public FlatScaleNameConverter()
        {
            base.dictionary = new Dictionary<string, string>()
            {
                {"C major","C"},
                {"G major","G"},
                {"D major","D"},
                {"A major","A"},
                {"E major","E"},
                {"B major","B"},

                {"Gb major","Gb"},
                {"Db major","Db"},
                {"Ab major","Ab"},
                {"Eb major","Eb"},
                {"Bb major","Bb"},

                {"F major","F"},

                {"A minor","Am"},
                {"E minor","Em"},
                {"B minor","Bm"},
                {"F# minor","F#m"},
                {"C# minor","C#m"},
                {"G# minor","G#m"},

                {"Eb minor","Ebm"},
                {"Bb minor","Bbm"},
                {"F minor","Fm"},
                {"C minor","Cm"},
                {"G minor","Gm"},

                {"D minor","Dm"},
            };
        }
    }
}
