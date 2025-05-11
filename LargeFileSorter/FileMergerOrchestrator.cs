using LargeFileSorter.FileMerge;
using System.Collections.Concurrent;

namespace LargeFileSorter
{
    public class FileMergerOrchestrator
    {
        private readonly FileMergerEnumerator _fileMerge;
        private readonly FileMergeFactory _factory;
        private readonly int MAX_CONCURRENT_TASKS = 5;

        public FileMergerOrchestrator(FileMergerEnumerator fileMerger)
        {
            _fileMerge = fileMerger;
            _factory = new FileMergeFactory();
        }

        public async Task OrchestrateFileMerge(Queue<string> generatedFiles, string outputFile)
        {
            List<Task<string>> runningTasks = new List<Task<string>>();
            int tasksToRun = generatedFiles.Count - 1;

            while (tasksToRun != 0)
            {
                if (runningTasks.Count == MAX_CONCURRENT_TASKS || generatedFiles.Count < 2)
                {
                    var finished = await Task<string>.WhenAny(runningTasks);
                    generatedFiles.Enqueue(finished.Result);
                    runningTasks.Remove(finished);
                }
                else
                {

                    string fileName = $"{ConstStrings.TEMP_DIRECOTRY}{Path.DirectorySeparatorChar}{ConstStrings.TEMP_FILES_NAME}_{tasksToRun}{ConstStrings.TEMP_FILES_EXTENSION}";
                    if (tasksToRun == 1)
                    {
                        fileName = outputFile;
                    }

                    var firstFile = generatedFiles.Dequeue();
                    var secondFile = generatedFiles.Dequeue();

                    IFileMerge merger = _factory.GetFileMerge(_fileMerge);

                    string output = fileName;
                    tasksToRun--;
                    runningTasks.Add(Task<string>.Run(() =>
                    {
                        merger.MergeFiles(firstFile, secondFile, output);
                        File.Delete(firstFile);
                        File.Delete(secondFile);
                        return Task.FromResult(output);
                    }));
                }
            }

            await Task.WhenAll(runningTasks);
        }
    }
}
