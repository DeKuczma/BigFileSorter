namespace LargeFileSorter.Models
{
    public class LineQueueWrapper
    {
        public Line ProcessingLine { get; private set; }
        public StreamReader Reader { private get; init; }
        public string InputFile { private get; init; }

        public LineQueueWrapper(string file)
        {
            InputFile = file;
            Reader = new StreamReader(InputFile);
            ReadNextLine();
        }

        public bool ReadNextLine()
        {
            string? inputLine;
            if ((inputLine = Reader.ReadLine()) != null)
            {
                ProcessingLine = new Line(inputLine);
                return true;
            }
            else
            {
                Reader.Dispose();
                File.Delete(InputFile);
                return false;
            }
        }
    }
}
