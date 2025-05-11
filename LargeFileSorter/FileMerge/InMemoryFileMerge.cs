using LargeFileSorter.Models;

namespace LargeFileSorter.FileMerge
{
    public class InMemoryFileMerge : IFileMerge
    {
        public void MergeFiles(string firstFile, string secondFile, string outputFile)
        {
            using (var firstReader = new StreamReader(firstFile))
            using (var secondReader = new StreamReader(secondFile))
            using (var writer = new StreamWriter(outputFile))
            {
                var firstString = firstReader.ReadLine();
                var secondString = secondReader.ReadLine();
                while ((firstString != null && firstString.Any())
                    || ((secondString != null && secondString.Any())))
                {
                    if (firstString == null || !firstString.Any())
                    {
                        secondString = WriteToFile(writer, secondReader, secondString);
                    }
                    else
                    {

                        if (secondString == null || !secondString.Any())
                        {
                            firstString = WriteToFile(writer, firstReader, firstString);
                        }
                        else
                        {
                            var firstLine = new Line(firstString);
                            var secondLine = new Line(secondString);
                            if (firstLine.Compare(secondLine) < 0)
                            {
                                firstString = WriteToFile(writer, firstReader, firstString);
                            }
                            else
                            {
                                secondString = WriteToFile(writer, secondReader, secondString);
                            }
                        }
                    }
                }
            }
        }

        private string? WriteToFile(StreamWriter writer, StreamReader reader, string text)
        {
            Console.WriteLine(text);
            writer.WriteLine(text);
            return reader.ReadLine();
        }
    }
}
