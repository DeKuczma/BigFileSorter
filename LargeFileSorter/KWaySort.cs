using LargeFileSorter.Models;
using LargeFileSorter.Utils;
using System.Collections.Concurrent;

namespace LargeFileSorter
{
    public class KWaySort
    {
        public async Task SortFiles(List<string> generatedFiles, string outputFile)
        {
            List<Task> tasks = new List<Task>();
            var linesToWrite = new BlockingCollection<string>(boundedCapacity: generatedFiles.Count);

            tasks.Add(Task.Run(() => FileSorter(linesToWrite, generatedFiles)));
            tasks.Add(Task.Run(() => FileWriter(linesToWrite, outputFile)));

            await Task.WhenAll(tasks);
        }

        private static void FileSorter(BlockingCollection<string> linesToWrite, List<string> generatedFiles)
        {
            PriorityQueue<LineQueueWrapper, Line> queue = new PriorityQueue<LineQueueWrapper, Line>(new LineComparer());
            foreach (var file in generatedFiles)
            {
                LineQueueWrapper newWrapper = new LineQueueWrapper(file);
                if (newWrapper.ProcessingLine != null)
                {
                    queue.Enqueue(newWrapper, newWrapper.ProcessingLine);
                }
            }

            while (queue.Count != 0)
            {
                LineQueueWrapper element = queue.Dequeue();
                linesToWrite.Add(element.ProcessingLine.ToString());
                if (element.ReadNextLine())
                {
                    queue.Enqueue(element, element.ProcessingLine);
                }
            }

            linesToWrite.CompleteAdding();
        }

        private static void FileWriter(BlockingCollection<string> linesToWrite, string outputFile)
        {
            using var writer = new StreamWriter(outputFile);
            foreach (var line in linesToWrite.GetConsumingEnumerable())
            {
                writer.WriteLine(line);
            }
        }
    }
}
