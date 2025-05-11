using CommandLine;
using LargeFileSorter;
using LargeFileSorter.FileMerge;
using System.Diagnostics;


Parser.Default.ParseArguments<Options>(args)
    .WithParsed((options) => {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        //IFileMerge fileMerge = new MemoryEfficientFileMerge();
        IFileMerge fileMerge = new InMemoryFileMerge();

        string tempFolderPath = "temp" + Path.DirectorySeparatorChar;

        Directory.CreateDirectory(tempFolderPath);
        fileMerge.MergeFiles("input_large1.txt", "input_large2.txt", $"{tempFolderPath}result.txt");

        stopwatch.Stop();
        Console.WriteLine($"File sorted in: {stopwatch.Elapsed}");
    });

