namespace LargeFileGenerator
{
    public class FileGenerator : IFileGenerator
    {
        private const int MAX_RANDOMIZED_SENTECES = 10000;
        private const int MIN_RANDOMIZED_SENTECES = 100;
        private const int BYTES_PER_REPETITION = (10 * 1024);

        private readonly ITextGenerator _textGenerator;
        private readonly Random _random;
        private readonly string _separationString = ". ";
        private readonly int _maxIntLength = int.MaxValue.ToString().Length;

        private readonly string _outputFilePath;
        private readonly int _repeatChance;
        private readonly long _minSentenceLength;
        private readonly int _maxSenteceLength;
        private readonly int _maxSentencesToRepeat;
        public long TargetSize { get; set; }

        private List<string> randomizedSentences;

        public FileGenerator(Options options)
        {
            _random = new();
            _textGenerator = new BogusTextGenerator();
            randomizedSentences = new();

            _outputFilePath = options.OutputFile;

            _minSentenceLength = options.MinSentenceLength;
            _maxSenteceLength = options.MaxSentenceLength;
            _repeatChance = options.RepeatChance;
            TargetSize = options.TargetSize;

            _maxSentencesToRepeat = Math.Clamp((int)TargetSize / BYTES_PER_REPETITION, MIN_RANDOMIZED_SENTECES, MAX_RANDOMIZED_SENTECES);
        }

        public void GenerateFile()
        {
            long sizeWritten = 0;

            using (StreamWriter writer = new StreamWriter(_outputFilePath))
            {
                while (sizeWritten < TargetSize)
                {

                    long number = _random.Next();
                    int numberLength = number.ToString().Length;

                    sizeWritten += numberLength + _separationString.Length;

                    long remainingToWrite = TargetSize - sizeWritten;

                    string generatedString = GenerateString(remainingToWrite);

                    sizeWritten += generatedString.Length;

                    if (sizeWritten + _maxIntLength + _separationString.Length >= TargetSize)
                    {
                        long remainingLength = TargetSize - sizeWritten;
                        generatedString += _textGenerator.GenerateString(remainingLength, remainingLength);
                        sizeWritten = TargetSize;
                    }

                    writer.WriteLine($"{number}{_separationString}{generatedString}");
                }
            }
        }

        private string GenerateString(long remainingToWrite)
        {
            bool shouldRepeat = (_random.Next(100) < _repeatChance);
            string generatedString;

            if (shouldRepeat && randomizedSentences.Any())
            {
                generatedString = randomizedSentences[_random.Next(randomizedSentences.Count - 1)];
                if(generatedString.Length > remainingToWrite)
                {
                    generatedString = generatedString.Substring(0, (int)remainingToWrite);
                }
            }
            else
            {
                generatedString = _textGenerator.GenerateString(_minSentenceLength,
                    Math.Min(_maxSenteceLength, remainingToWrite));

                if (randomizedSentences.Count < _maxSentencesToRepeat)
                {
                    randomizedSentences.Add(generatedString);
                }
            }
            return generatedString;
        }
    }
}
