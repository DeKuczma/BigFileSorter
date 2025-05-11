using LargeFileSorter.Comparators;
using System.Text;

namespace LargeFileSorter.FileMerge
{
    public class MemoryEfficientFileMerge : IFileMerge
    {
        private readonly IComparator _comparator;

        public MemoryEfficientFileMerge()
        {
            _comparator = new MemoryEfficientComparator();
        }

        public void MergeFiles(string firstFile, string secondFile, string outputFile)
        {
            using (var firstReader = new MyStreamReader(firstFile))
            using (var secondReader = new MyStreamReader(secondFile))
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8, Options.WriterBuferSize))
            {
                var readers = new MyStreamReader[] { firstReader, secondReader };
                while (!readers[0].EndOfStream || !readers[1].EndOfStream)
                {
                    var compareResult = _comparator.Compare(readers);
                    if (compareResult == -1)
                    {
                        readers[0].WriteLineToFileByChar(writer);
                    }
                    else
                    {
                        readers[1].WriteLineToFileByChar(writer);
                    }

                    if (readers[0].EndOfStream)
                    {
                        while (!readers[1].EndOfStream)
                        {
                            readers[1].WriteLineToFileByChar(writer);
                        }
                    }

                    if (readers[1].EndOfStream)
                    {
                        if (readers[0].EndOfStream)
                        {
                            while (!readers[0].EndOfStream)
                            {
                                readers[0].WriteLineToFileByChar(writer);
                            }
                        }

                    }
                }
            }
        }
    }
}
