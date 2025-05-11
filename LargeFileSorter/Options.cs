using CommandLine;

namespace LargeFileSorter
{
    public class Options
    {
        [Option('i', "input", Required = false, HelpText = "Input file path.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file path.")]
        public string OutputFile { get; set; }

        public static int WriterBuferSize { get; } = 1024 * 64;
        public static int ReaderBuferSize { get; } = 1024 * 64;
    }
}
