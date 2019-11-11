using System;

namespace InteractiveMusicScales
{
    internal class Pianoroll : AbstractNotesHolder
    {
        public Pianoroll(Note[] notes) : base(notes, "Pianoroll")
        {
            //Argument checks are in abstract base class
        }

        /// <summary>
        /// Returns the note from requested position
        /// </summary>
        /// <param name="pianokey"></param>
        /// <returns></returns>
        public Note this[int pianokey]
        {
            get
            {
                /*guardian*/
                if (pianokey < 0)
                    throw new ArgumentException($"Pianoroll.GetIndexer: 'pianokey' argument can not be negative (its value was {pianokey} )");

                int index = pianokey;

                //I do not expect incoming indexes be multiple times larger than the last index
                //that is why I am setting looped subtraction instead of division with remainder
                while (index >= base.notescount)
                    index -= base.notescount;

                return base.notes[index];
            }
        }
    }
}