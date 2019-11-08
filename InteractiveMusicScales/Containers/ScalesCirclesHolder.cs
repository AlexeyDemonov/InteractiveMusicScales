using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales.Containers
{
    class ScalesCirclesHolder
    {
        Scale[] allScales;
        int[] circlesStartIndexes;

        int circleLength;
        int shift;

        public ScalesCirclesHolder(Scale[] scales, int divideToNumberOfCircles = 1)
        {
            this.allScales = scales;
            this.circlesStartIndexes = new int[divideToNumberOfCircles];
            this.circleLength = scales.Length / divideToNumberOfCircles;

            int index = 0;
            circlesStartIndexes[0] = 0;

            for (int i = 1; i < circlesStartIndexes.Length; i++)
            {
                index += circleLength;
                circlesStartIndexes[i] = index;
            }

            shift = 0;
        }

        public void ShiftRight() => shift++;
        public void ShiftLeft() => shift--;

        public Scale this[int circle, int index]
        {
            get
            {
                //TOBEDONE
                throw new NotImplementedException();
            }
        }
    }
}
