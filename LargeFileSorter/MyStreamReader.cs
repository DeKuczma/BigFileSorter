namespace LargeFileSorter
{
    public class MyStreamReader : StreamReader
    {
        private readonly int SPECIAL_ASCII_CHARACTERS = 31;

        private readonly int ASCII_TABULATOR = 11;

        private long dataRead;
        public MyStreamReader(string path) : base(path)
        {
            dataRead = 0;
        }

        public void ResetPositionToPreviousLne()
        {
            BaseStream.Position = dataRead;
            DiscardBufferedData();
            char c = (char)Peek();
            /*while()
            {

            }*/
        }

        public int ReadAlphanumeric()
        {
            if (IsNextCharacterAlphanumericOrWhitespace())
            {
                return Read();
            }
            return -1;
        }

        public void SkipEndOfLine()
        {
            while(!IsNextCharacterAlphanumericOrWhitespace())
            {
                int c = Read();
                if (c == -1)
                    return;

                dataRead++;
            }
        }

        public void RollBackRead()
        {
            BaseStream.Position = dataRead;
            DiscardBufferedData();
        }

        public void WriteLineToFileByChar(StreamWriter writer)
        {
            while(IsNextCharacterAlphanumericOrWhitespace())
            {
                writer.Write(char.ConvertFromUtf32(Read()));
                dataRead++;
            }
            writer.Write('\n');
            SkipEndOfLine();
        }

        private bool IsNextCharacterAlphanumericOrWhitespace()
        {
            int c = Peek();
            if (c <= SPECIAL_ASCII_CHARACTERS && c != ASCII_TABULATOR)
                return false;
            return true;
        }
    }
}
