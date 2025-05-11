namespace LargeFileSorter.FileMerge
{
    public class MemoryEfficientFileMerge : IFileMerge
    {
        public void MergeFiles(string firstFile, string secondFile, string outputFile)
        {
            long firstLengthRead = 0;

            using (var firstReader = new StreamReader(firstFile))
            using (var secondReader = new StreamReader(secondFile))
            using (var writer = new StreamWriter(outputFile))
            {

            }
        }
    }
}
