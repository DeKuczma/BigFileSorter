using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
            int comaprisonResult = Text.CompareTo(line.Text);
            if (comaprisonResult != 0)
            {
                return comaprisonResult;
            }

            if(Number < line.Number)
            {
                return -1;
            }

            return 1;
        }

        public override string ToString()
        {
            return $"{Number}{SEPARATOR}{Text}";
        }
    }
}
