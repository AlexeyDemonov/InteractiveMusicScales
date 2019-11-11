using System.Collections.Generic;

namespace InteractiveMusicScales.Interface
{
    internal class FlatNoteNameConverter : AbstractTextValueConverter
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