using System;
using System.Collections.Generic;

namespace InteractiveMusicScales
{
    /// <summary>
    /// Compares two scales based on their names (in note ascending order), if name is incorrect it will be counted as 'higher' value
    /// </summary>
    internal class ScalesSorter : IComparer<Scale>
    {
        private static readonly String[] scalenameStart = new string[]
        {
            "C ", "C# ", "Cb ",
            "D ", "D# ", "Db ",
            "E ", "E# ", "Eb ",
            "F ", "F# ", "Fb ",
            "G ", "G# ", "Gb ",
            "A ", "A# ", "Ab ",
            "B ", "B# ", "Bb "
        };

        public int Compare(Scale left, Scale right)
        {
            /*guardian*/
            if (left == null)
                throw new ArgumentException("ScalesSorter.Compare: argument 'left' was null");
            if (right == null)
                throw new ArgumentException("ScalesSorter.Compare: argument 'right' was null");
            if (left.Name == null)
                throw new ArgumentException("ScalesSorter.Compare: 'left' scale Name was null");
            if (right.Name == null)
                throw new ArgumentException("ScalesSorter.Compare: 'right' scale Name was null");

            string leftname = left.Name;
            string rightname = right.Name;

            //Check for two equal names
            if (leftname == rightname) return 0;

            //Find indexes for both scales
            int length = scalenameStart.Length;

            int leftIndex = -1;
            for (int i = 0; i < length; i++)
            {
                if (leftname.StartsWith(scalenameStart[i]))
                {
                    leftIndex = i;
                    break;
                }
            }

            int rightindex = -1;
            for (int i = 0; i < length; i++)
            {
                if (rightname.StartsWith(scalenameStart[i]))
                {
                    rightindex = i;
                    break;
                }
            }

            //Check if one or even both indexes were not found
            if (leftIndex == -1 || rightindex == -1)
            {
                //At least one of them is '-1'
                if (leftIndex == rightindex)
                    return string.Compare(leftname, rightname);//They are both incorrect to us - use standart algorythm
                //If we're still here, then we may be sure that one and only one of them is '-1' and it's either left or right index
                else
                    return (leftIndex == -1) ? 1 : -1;//If one of the names is incorrect it should be counted as 'higher' value
            }

            //Both indexes are correct

            if (leftIndex == rightindex)
            {
                //Both names are starting with same symbols
                //Let the standart algorythm handle this case
                return string.Compare(leftname, rightname);
            }
            else
            {
                return (leftIndex > rightindex) ? 1 : -1;
            }
        }
    }
}