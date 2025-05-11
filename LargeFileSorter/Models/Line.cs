using System.Xml.Linq;

namespace LargeFileSorter.Models
{
    public class Line
    {
        private const string SEPARATOR = ". ";
        public int Number { get; init; }
        public string Text { get; init; }

        public Line(string inputLine)
        {
            string[] values = inputLine.Split(". ", 2);
            Number = int.Parse(values[0]);
            Text = values[1];
        }

        public int Compare(Line line)
        {
            return CompareElements(this, line);
        }

        public override string ToString()
        {
            return $"{Number}{SEPARATOR}{Text}";
        }
        public static int CompareElements(Line x, Line y)
        {
            int comaprisonResult = x!.Text.CompareTo(y.Text);
            if (comaprisonResult != 0)
            {
                return comaprisonResult;
            }

            if (x.Number < y.Number)
            {
                return -1;
            }

            return 1;
        }
    }
}
