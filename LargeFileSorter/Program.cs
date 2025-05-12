using CommandLine;
using LargeFileSorter;
using LargeFileSorter.FileMerge;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;


Options options = null;
Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o => options = o);

if (options == null)
    return;

var stopwatch = new Stopwatch();
stopwatch.Start();

Queue<string> generatedFiles = new Queue<string>();
var mergerOrchestrator = new FileMergerOrchestrator(FileMergerEnumerator.InMemory);

Directory.CreateDirectory(ConstStrings.TEMP_DIRECOTRY + Path.DirectorySeparatorChar);
FileSplitter splitter = new FileSplitter();

await splitter.SplitFile(options.InputFile, generatedFiles);

await mergerOrchestrator.OrchestrateFileMerge(generatedFiles,options.OutputFile);

stopwatch.Stop();
Console.WriteLine($"File sorted in: {stopwatch.Elapsed}");