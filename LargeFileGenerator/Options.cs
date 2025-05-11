using CommandLine;

namespace LargeFileGenerator
{
    public class Options
    {
        [Option('s', "size", Required = true, HelpText = "Size of generated output file.")]
        public long TargetSize { get; set; }

        [Option("min", Required = false, Default = 1024, HelpText = "Min length of generated string.")]
        public long MinSentenceLength { get; set; }

        [Option("max", Required = false, Default = 1024 * 5, HelpText = "Max length of generated string.")]
        public int MaxSentenceLength { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file full path.")]
        public string OutputFile { get; set; }


        [Option('c', "repeat", Required = false, Default = 30, HelpText = "Percent of redundant strings in file (range 0 - 100)")]
        public int RepeatChance { get; set; }
    }
}
