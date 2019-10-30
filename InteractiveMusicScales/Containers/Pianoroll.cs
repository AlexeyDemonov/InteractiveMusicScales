using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class Pianoroll : AbstractNotesHolder
    {

        public Pianoroll(Note[] notes) : base(notes, "Pianoroll")
        {
            /*guardians*/
            if (notes == null)
                throw new ArgumentNullException("Pianoroll.Ctor: 'notes' array can not be null");
            if (notes.Length == 0)
                throw new ArgumentException("Pianoroll.Ctor: 'notes' array can not be empty (its length was zero)");
        }

        /// <summary>
        /// Returns the note from requested position (in semitones, starting with zero)
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
                while (index > base.notescount)
                    index -= base.notescount;

                return base.notes[index];
            }
        }

    }
}
