using System.Collections.Generic;

namespace InteractiveMusicScales.Interface
{
    internal class HiddenNoteNameConverter : AbstractTextValueConverter
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