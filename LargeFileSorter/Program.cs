using LargeFileSorter.Comparators;
using LargeFileSorter.FileMerge;

IComparator comparator = new InMemoryComparator();
IFileMerge fileMerge = new InMemoryFileMerge();

fileMerge.MergeFiles("test1.txt", "test2.txt");


