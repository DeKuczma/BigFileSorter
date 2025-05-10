using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeFileSorter.Comparators
{
    public interface IComparator
    {
        //return same values as string.ComapreTo
        public int Compare(StringWriter first, StringWriter second);
    }
}
