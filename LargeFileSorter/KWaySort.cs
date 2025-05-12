using LargeFileSorter.Models;
using LargeFileSorter.Utils;

namespace LargeFileSorter
{
    public class KWaySort
    {
        public void SortFiles(List<string> generatedFiles, string outputFile)
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

            using var writer = new StreamWriter(outputFile);

            while(queue.Count != 0)
            {
                LineQueueWrapper element = queue.Dequeue();
                writer.WriteLine(element.ProcessingLine);
                if(element.ReadNextLine())
                {
                    queue.Enqueue(element, element.ProcessingLine);
                }
            }
        }
    }
}
