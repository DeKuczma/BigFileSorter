using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeFileSorter.FileMerge
{
    public class FileMergeFactory
    {
        public InMemoryFileMerge GetFileMerge(FileMergerEnumerator fileMerger)
        {
            switch(fileMerger)
            {
                case FileMergerEnumerator.InMemory:
                default:
                    return new InMemoryFileMerge();
            }
        }
    }
}
