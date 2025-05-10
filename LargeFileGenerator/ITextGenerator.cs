namespace LargeFileGenerator
{
    public interface ITextGenerator
    {
        public string GenerateString(long minSentenceLength, long maxLength);
    }
}
