using CommandLine;
using LargeFileGenerator;
using System.Diagnostics;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed((options) =>
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        IFileGenerator fileGenerator = new FileGenerator(options);
        fileGenerator.GenerateFile();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    });

