using System.Collections.Generic;

namespace InteractiveMusicScales.Interface
{
    internal class SharpNoteNameConverter : AbstractTextValueConverter
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