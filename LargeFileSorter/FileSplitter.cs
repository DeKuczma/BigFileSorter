using LargeFileSorter.Models;
using LargeFileSorter.Utils;
using System.Text;

namespace LargeFileSorter
{
    public class FileSplitter
    {
        private readonly int MAX_LOAD_SIZE = 1024 * 1024 * 250;
        private readonly int MAX_CONCURRENT_TASKS = 5;
        public async Task SplitFile(string file, List<string> generatedFiles)
        {
            using var reader = new StreamReader(file, Encoding.UTF8, true, Options.ReaderBuferSize);

            long generated = 0;
            string? singleLine;
            long loadedSize = 0;
            List<Task<string>> tasks = new List<Task<string>>();
            List<Line> lines = new List<Line>();

            while ((singleLine = reader.ReadLine()) != null)
            {
                loadedSize += singleLine.Length;
                lines.Add(new Line(singleLine));

                if (loadedSize >= MAX_LOAD_SIZE || reader.EndOfStream)
                {
                    List<Line> linesCopy = lines.ToList();
                    long fileSufix = generated;
                    string fileName = $"{ConstStrings.TEMP_DIRECOTRY}{Path.DirectorySeparatorChar}{ConstStrings.TEMP_FILES_NAME}{fileSufix}{ConstStrings.TEMP_FILES_EXTENSION}";

                    if (tasks.Count == MAX_CONCURRENT_TASKS)
                    {
                        var processed = await Task.WhenAny(tasks);
                        var result = processed.Result;
                        generatedFiles.Add(result);
                        tasks.Remove(processed);
                    }

                    tasks.Add(Task<string>.Run(() => {

                        linesCopy.Sort(Line.CompareElements);
                        using (var writer = new StreamWriter(fileName, false, Encoding.UTF8, Options.WriterBuferSize))
                        {
                            foreach (var line in linesCopy)
                            {
                                writer.WriteLine(line.ToString());
                            }
                        }
                        return Task.FromResult(fileName);
                    }));
                    
                    generated++;
                    loadedSize = 0;
                    lines = new List<Line>();
                }
            }

            await Task.WhenAll(tasks);
            foreach (var task in tasks)
            {
                generatedFiles.Add(task.Result);
            }
        }
    }
}
