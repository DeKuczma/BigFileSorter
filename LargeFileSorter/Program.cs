using LargeFileSorter.FileMerge;

IFileMerge fileMerge = new InMemoryFileMerge();

fileMerge.MergeFiles("test1.txt", "test2.txt", "result.txt");