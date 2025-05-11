namespace LargeFileSorter.Comparators
{
    public interface IComparator
    {
        //return same values as string.ComapreTo
        public int Compare(MyStreamReader[] readers);
    }
}
