using LargeFileSorter.FileMerge;
using System.Diagnostics;


var stopwatch = new Stopwatch();
stopwatch.Start();

IFileMerge fileMerge = new MemoryEfficientFileMerge();

fileMerge.MergeFiles("test1.txt", "test2.txt", "result.txt");

stopwatch.Stop();
Console.WriteLine($"File sorted in: {stopwatch.Elapsed}");