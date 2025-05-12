using CommandLine;
using LargeFileSorter;
using LargeFileSorter.FileMerge;
using LargeFileSorter.Utils;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;


Options options = null;
Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o => options = o);

if (options == null)
    return;

var stopwatch = new Stopwatch();
stopwatch.Start();

List<string> generatedFiles = new List<string>();
FileSplitter splitter = new FileSplitter();
KWaySort kWay = new KWaySort();

Directory.CreateDirectory(ConstStrings.TEMP_DIRECOTRY + Path.DirectorySeparatorChar);
await splitter.SplitFile(options.InputFile, generatedFiles);
kWay.SortFiles(generatedFiles, options.OutputFile);

stopwatch.Stop();
Console.WriteLine($"File sorted in: {stopwatch.Elapsed}");