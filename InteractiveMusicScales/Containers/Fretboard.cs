using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    class Fretboard : AbstractNotesHolder
    {
        int[] shifts;

        public Fretboard(Note[] notes, int strings) : base(notes, "Fretboard")
        {
            /*guardian*/
            if (strings < 1)
                throw new ArgumentException($"Fretboard.Ctor: 'strings' argument can not be zero or negative (its value was {strings} )");

            this.shifts = new int[ strings ];
        }

        /// <summary>
        /// Returns the note from requested string at requested fret according to this string root note
        /// </summary>
        /// <param name="string"></param>
        /// <param name="fret"></param>
        /// <returns></returns>
        public Note this[int @string, int fret]
        {
            get
            {
                /*guardians*/
                if (@string < 0)
                    throw new ArgumentException($"Fretboard.GetIndexer: '@string' argument can not be negative (its value was {@string} )");
                if (@string >= shifts.Length)
                    throw new ArgumentException($"Fretboard.GetIndexer: '@string' argument can not be greater than strings last index (its value was {@string}, last index of strings is {shifts.Length} )");
                if (fret < 0)
                    throw new ArgumentException($"Fretboard.GetIndexer: 'fret' argument can not be negative (its value was {fret} )");


                int index = fret + shifts[@string];

                //I do not expect incoming indexes be multiple times larger than the last index
                //that is why I am setting looped subtraction instead of division with remainder
                while (index >= base.notescount)
                    index -= base.notescount;

                return base.notes[index];
            }
        }

        /// <summary>
        /// Sets the root note of the string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public Note this[int @string]
        {
            set
            {
                /*guardians*/
                if (@string < 0)
                    throw new ArgumentException($"Fretboard.SetIndexer: '@string' argument can not be negative (its value was {@string} )");
                if (@string > shifts.Length)
                    throw new ArgumentException($"Fretboard.SetIndexer: '@string' argument can not be greater than strings last index (its value was {@string}, last index of strings is {shifts.Length} )");
                if (value == null)
                    throw new ArgumentNullException($"Fretboard.SetIndexer: Note value can not be null");

                bool isSet = default(bool);

                for (int i = 0; i < base.notescount; i++)
                {
                    if(base.notes[i].Equals(value))
                    {
                        shifts[@string] = i;
                        isSet = true;
                        break;
                    }
                }

                /*post-check*/
                if(!isSet)
                    throw new ArgumentException($"Fretboard.SetIndexer: Note value cannot be new in relation to what was provided to class constructor ( value was {value.ToString()} )");
            }
        }


    }
}
