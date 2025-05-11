using CommandLine;

namespace LargeFileSorter
{
    public class Options
    {
        [Option('i', "input", Required = false, HelpText = "Input file path.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file path.")]
        public string OutputFile { get; set; }
    }
}
