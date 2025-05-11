using LargeFileSorter.Comparators;
using LargeFileSorter.FileMerge;

IFileMerge fileMerge = new MemoryEfficientFileMerge();

fileMerge.MergeFiles("test1.txt", "test2.txt", "result.txt");
