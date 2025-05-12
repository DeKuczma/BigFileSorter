using LargeFileSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeFileSorter.Utils
{
    public class LineComparer : IComparer<Line>
    {
        public int Compare(Line? x, Line? y)
        {
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            return x.Compare(y);
        }
    }
}
