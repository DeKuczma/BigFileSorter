using LargeFileSorter.Models;

namespace LargeFileSorter
{
    public class FileSplitter
    {
        private readonly int MAX_LINES_LOAD = 606000;
        private readonly int MAX_CONCURRENT_TASKS = 5;
        public async Task SplitFile(string file, Queue<string> generatedFiles)
        {
            using var reader = new StreamReader(file);

            long generated = 0;
            List<Task<string>> tasks = new List<Task<string>>();
            string singleLine;
            List<Line> lines = new List<Line>();

            while((singleLine = reader.ReadLine()) != null)
            {
                lines.Add(new Line(singleLine));

                if (lines.Count == MAX_LINES_LOAD || reader.EndOfStream)
                {
                    List<Line> linesCopy = lines.ToList();
                    long fileSufix = generated;
                    string fileName = $"{ConstStrings.TEMP_DIRECOTRY}{Path.DirectorySeparatorChar}{ConstStrings.TEMP_FILES_NAME}{fileSufix}{ConstStrings.TEMP_FILES_EXTENSION}";
                    generated++;

                    if (tasks.Count == MAX_CONCURRENT_TASKS)
                    {
                        var processed = await Task.WhenAny(tasks);
                        var result = processed.Result;
                        generatedFiles.Enqueue(result);
                        tasks.Remove(processed);
                    }

                    tasks.Add(Task<string>.Run(() => {

                        linesCopy.Sort(Line.CompareElements);
                        using (var writer = new StreamWriter(fileName))
                        {
                            foreach (var line in linesCopy)
                            {
                                writer.WriteLine(line.ToString());
                            }
                        }
                        return Task.FromResult(fileName);
                    }));

                    lines = new List<Line>();
                }
            }

            await Task.WhenAll(tasks);
            foreach(var task in tasks)
            {
                generatedFiles.Enqueue(task.Result);
            }
        }
    }
}
