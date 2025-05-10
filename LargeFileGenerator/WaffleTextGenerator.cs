using System.Text.RegularExpressions;
using WaffleGenerator;

namespace LargeFileGenerator
{
    public class WaffleTextGenerator : ITextGenerator
    {
        private readonly Random _random;
        public WaffleTextGenerator()
        {
            _random = new();
        }

        public string GenerateString(long minSentenceLength, long maxLength)
        {
            long minLength = Math.Min(minSentenceLength, maxLength);
            int length = _random.Next((int)minLength, (int)maxLength);

            //generate block of text
            var generatedString = WaffleEngine.Text(
                paragraphs: 1,
                includeHeading: false);

            generatedString = Regex.Replace(generatedString, @"\t|\n|\r", String.Empty);

            return generatedString.Substring(0, length);

        }
    }
}
