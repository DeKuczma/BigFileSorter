using LargeFileSorter.Comparators;

namespace LargeFileSorter.FileMerge
{
    public class InMemoryFileMerge : IFileMerge
    {
        private readonly IComparator _comparator = new InMemoryComparator();

        public void MergeFiles(string firstFile, string secondFile)
        {
            using (var firstReader = new StreamReader(firstFile))
            using (var firstReaderCopy = new StreamReader(firstFile))
            using (var secondReader = new StreamReader(secondFile))
            using (var secondReaderCopy = new StreamReader(secondFile))
            {

                while (!firstReader.EndOfStream || !secondReader.EndOfStream)
                {
                    //Console.WriteLine(comparator.Compare(reader1, reader2));
                    if (_comparator.Compare(firstReaderCopy, secondReaderCopy) < 0)
                    {
                        Console.WriteLine(firstReader.ReadLine());
                        firstReaderCopy.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine(secondReader.ReadLine());
                        secondReaderCopy.ReadLine();
                    }

                    if (secondReader.EndOfStream)
                    {
                        while (!firstReader.EndOfStream)
                        {
                            Console.WriteLine(firstReader.ReadLine());
                        }
                    }
                    else if (firstReader.EndOfStream)
                    {
                        while (!secondReader.EndOfStream)
                            Console.WriteLine(secondReader.ReadLine());
                    }
                }
            }
        }
    }
}
