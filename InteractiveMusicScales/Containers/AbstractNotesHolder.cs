using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales
{
    abstract class AbstractNotesHolder
    {
        protected readonly Note[] notes;
        protected readonly int notescount;

        protected AbstractNotesHolder(Note[] notes, string derivedClassName)
        {
            if (notes == null)
                throw new ArgumentNullException($"{derivedClassName}.Ctor: 'notes' array can not be null");
            if (notes.Length == 0)
                throw new ArgumentException($"{derivedClassName}.Ctor: 'notes' array can not be empty (its length was zero)");

            this.notes = notes;
            this.notescount = notes.Length;
        }
    }
}
