namespace LargeFileSorter
{
    public class MyStreamReader : StreamReader
    {
        private readonly int SPECIAL_ASCII_CHARACTERS = 31;

        private readonly int LINE_SEPARATION_CHARACTERS = 2;
        private readonly int ASCII_STARTING_NUMBER_POSITION = (int)'0';

        private readonly int ASCII_TABULATOR = 11;

        private long dataRead;
        public MyStreamReader(string path) : base(path)
        {
            dataRead = 0;
        }

        public int ReadNumberFromLine()
        {
            int number = 0;
            int c;
            if (Peek() == -1)
                return -1;
            while((c = Peek()) != (int)'.')
            {
                c = Read();
                number = number * 10 + c - ASCII_STARTING_NUMBER_POSITION;
            }

            SkipCarachters(LINE_SEPARATION_CHARACTERS);
            return number;
        }


        public void ResetPositionToPreviousLne()
        {
            BaseStream.Position = dataRead;
            DiscardBufferedData();
            char c = (char)Peek();
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

        private void SkipCarachters(int number)
        {
            for (int i = 0; i < number; i++)
                Read();
        }
    }
}
