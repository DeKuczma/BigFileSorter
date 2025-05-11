using LargeFileSorter.Models;
using System.Collections.Concurrent;
using System.Xml;

namespace LargeFileSorter
{
    public class FileSplitter
    {
        private readonly int MAX_LINES_LOAD = 306000;
        public async Task SplitFile(string file, ConcurrentQueue<string> generatedFiles)
        {
            long generated = 0;
            List<Task> tasks = new List<Task>();
            using var reader = new StreamReader(file);
            string singleLine;
            List<Line> lines = new List<Line>();
            while((singleLine = reader.ReadLine()) != null)
            {

                lines.Add(new Line(singleLine));

                if (lines.Count == MAX_LINES_LOAD)
                {
                    List<Line> linesCopy = lines.ToList();
                    long fileSufix = generated;

                    tasks.Add(Task.Run(() =>
                    {
                        linesCopy.Sort(Line.CompareElements);
                        string fileName = $"{ConstStrings.TEMP_DIRECOTRY}{Path.DirectorySeparatorChar}{ConstStrings.TEMP_FILES_NAME}{fileSufix}{ConstStrings.TEMP_FILES_EXTENSION}";
                        using (var writer = new StreamWriter(fileName))
                        {
                            foreach (var line in linesCopy)
                            {
                                writer.WriteLine(line.ToString());
                            }
                        }
                        generatedFiles.Enqueue(fileName);
                    }));
                    generated++;

                    lines = new List<Line>();
                }
            }

            await Task.WhenAll(tasks);


        }
    }
}
