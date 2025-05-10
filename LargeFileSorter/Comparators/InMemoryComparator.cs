using LargeFileSorter.Models;

namespace LargeFileSorter.Comparators
{
    public class InMemoryComparator : IComparator
    {
        public int Compare(StreamReader firstStream, StreamReader secondStream)
        {
            var firstPosition = firstStream.BaseStream.Position;
            var secondPosition = secondStream.BaseStream.Position;

            var firstString = firstStream.ReadLine();
            int result;

            if (firstString == null || !firstString.Any())
            {
                result = 1;
            }
            else
            {
                var secondString = secondStream.ReadLine();

                if (secondString == null || !secondString.Any())
                {
                    result = -1;
                }
                else
                {
                    var firstLine = new Line(firstString);
                    var secondLine = new Line(secondString);
                    result = firstLine.Compare(secondLine);
                }
            }


            RestoreStreamPosition(firstStream, firstPosition);
            RestoreStreamPosition(secondStream, secondPosition);

            return result;
        }

        private void RestoreStreamPosition(StreamReader stream, long position)
        {
            stream.BaseStream.Position = position;
            stream.DiscardBufferedData();
        }
    }
}
