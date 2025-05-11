using CommandLine;
using LargeFileSorter;
using LargeFileSorter.FileMerge;
using System.Collections.Concurrent;
using System.Diagnostics;


Parser.Default.ParseArguments<Options>(args)
    .WithParsed((options) => {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        ConcurrentQueue<string> generatedFiles = new ConcurrentQueue<string>();
        //IFileMerge fileMerge = new MemoryEfficientFileMerge();
        IFileMerge fileMerge = new InMemoryFileMerge();

        Directory.CreateDirectory(ConstStrings.TEMP_DIRECOTRY + Path.DirectorySeparatorChar);
        FileSplitter splitter = new FileSplitter();

        splitter.SplitFile("input_long_lines.txt", generatedFiles);
        //fileMerge.MergeFiles("input_large1.txt", "input_large2.txt", $"{tempFolderPath}result.txt");

        stopwatch.Stop();
        Console.WriteLine($"File sorted in: {stopwatch.Elapsed}");
    });

