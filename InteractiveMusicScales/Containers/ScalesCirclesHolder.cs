using System;

namespace InteractiveMusicScales
{
    internal class ScalesCirclesHolder
    {
        private Scale[] allScales;
        private int[] circlesStartIndexes;

        private int circleLength;
        private int shift;

        public ScalesCirclesHolder(Scale[] scales, int divideToNumberOfCircles = 1)
        {
            if (scales == null)
                throw new ArgumentNullException(nameof(scales));
            if (scales.Length == 0)
                throw new ArgumentException("ScalesCirclesHolder.Ctor: 'scales' array can not be empty (its length was zero)");
            if (divideToNumberOfCircles < 1)
                throw new ArgumentException($"ScalesCirclesHolder.Ctor: 'divideToNumberOfCircles' argument can not be zero or negative (its value was {divideToNumberOfCircles} )");

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

        public void ShiftRight() => shift--;

        public void ShiftLeft() => shift++;

        public Scale this[int circle, int index]
        {
            get
            {
                if (circle < 0)
                    throw new ArgumentException($"ScalesCirclesHolder.Indexer: 'circle' argument cannot be negative (its value was {circle} )");
                if (circle >= circlesStartIndexes.Length)
                    throw new ArgumentException($"ScalesCirclesHolder.Indexer: 'circle' argument cannot be greater or equal to number of circles (its value was {circle} )");

                int lowerBound = circlesStartIndexes[circle];
                int higherBound = ((circle + 1) == circlesStartIndexes.Length)//In other words, is this a last circle?
                    ? allScales.Length
                    : circlesStartIndexes[circle + 1];

                int realIndex = circlesStartIndexes[circle] + shift + index;

                while (realIndex < lowerBound)
                {
                    realIndex += circleLength;
                }

                while (realIndex >= higherBound)
                {
                    realIndex -= circleLength;
                }

                return allScales[realIndex];
            }
        }
    }
}