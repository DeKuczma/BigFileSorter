using LargeFileSorter.Models;
using System.Text;

namespace LargeFileSorter.FileMerge
{
    public class InMemoryFileMerge : IFileMerge
    {
        public void MergeFiles(string firstFile, string secondFile, string outputFile)
        {
            using (var firstReader = new StreamReader(firstFile, Encoding.UTF8, true, Options.ReaderBuferSize))
            using (var secondReader = new StreamReader(secondFile, Encoding.UTF8, true, Options.ReaderBuferSize))
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8, Options.WriterBuferSize))
            {
                var firstString = firstReader.ReadLine();
                var secondString = secondReader.ReadLine();
                while ((firstString != null && firstString.Any())
                    || ((secondString != null && secondString.Any())))
                {
                    if (firstString == null || !firstString.Any())
                    {
                        secondString = ProcessStream(writer, secondReader, secondString);
                    }
                    else
                    {
                        if (secondString == null || !secondString.Any())
                        {
                            firstString = ProcessStream(writer, firstReader, firstString);
                        }
                        else
                        {
                            var firstLine = new Line(firstString);
                            var secondLine = new Line(secondString);
                            if (firstLine.Compare(secondLine) < 0)
                            {
                                firstString = ProcessStream(writer, firstReader, firstString);
                            }
                            else
                            {
                                secondString = ProcessStream(writer, secondReader, secondString);
                            }
                        }
                    }
                }
            }
        }

        private string? ProcessStream(StreamWriter writer, StreamReader reader, string? text)
        {
            writer.WriteLine(text);
            return reader.ReadLine();
        }
    }
}
