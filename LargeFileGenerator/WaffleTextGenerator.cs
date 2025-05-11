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
            int numberOfParagraphs = length / 80 + 1;
            var generatedString = WaffleEngine.Text(
                paragraphs: numberOfParagraphs,
                includeHeading: false);

            generatedString = Regex.Replace(generatedString, @"\t|\n|\r", String.Empty);

            return generatedString.Substring(0, Math.Min(generatedString.Length, length));
        }
    }
}
