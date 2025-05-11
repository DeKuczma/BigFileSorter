using LargeFileSorter;
using LargeFileSorter.FileMerge;
using System.Collections.Concurrent;
using System.Diagnostics;


//Parser.Default.ParseArguments<Options>(args)
//    .WithParsed(async (options) => {
//        var stopwatch = new Stopwatch();
//        stopwatch.Start();

//        BlockingCollection<string> generatedFiles = new BlockingCollection<string>();
//        //IFileMerge fileMerge = new MemoryEfficientFileMerge();
//        //IFileMerge fileMerge = new InMemoryFileMerge();

//        Directory.CreateDirectory(ConstStrings.TEMP_DIRECOTRY + Path.DirectorySeparatorChar);
//        FileSplitter splitter = new FileSplitter();

//        await splitter.SplitFile("input_long_lines.txt", generatedFiles);

//        //var mergerOrchestrator = new FileMergerOrchestrator(FileMergerEnumerator.InMemory);
//        //await mergerOrchestrator.OrchestrateFileMerge(generatedFiles, "output.txt");
//        //fileMerge.MergeFiles("input_large1.txt", "input_large2.txt", $"{tempFolderPath}result.txt");



//        stopwatch.Stop();
//        Console.WriteLine($"File sorted in: {stopwatch.Elapsed}");
//    });

var stopwatch = new Stopwatch();
stopwatch.Start();

Queue<string> generatedFiles = new Queue<string>();
var mergerOrchestrator = new FileMergerOrchestrator(FileMergerEnumerator.InMemory);
//IFileMerge fileMerge = new MemoryEfficientFileMerge();
//IFileMerge fileMerge = new InMemoryFileMerge();

Directory.CreateDirectory(ConstStrings.TEMP_DIRECOTRY + Path.DirectorySeparatorChar);
FileSplitter splitter = new FileSplitter();

await splitter.SplitFile("input_new_large.txt", generatedFiles);

await mergerOrchestrator.OrchestrateFileMerge(generatedFiles, "output.txt");
//fileMerge.MergeFiles("input_large1.txt", "input_large2.txt", $"{tempFolderPath}result.txt");



stopwatch.Stop();
Console.WriteLine($"File sorted in: {stopwatch.Elapsed}");