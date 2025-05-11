namespace LargeFileSorter.Comparators
{
    public class MemoryEfficientComparator : IComparator
    {
        public int Compare(MyStreamReader[] readers)
        {

            int firstNumber = readers[0].ReadNumberFromLine();
            int secondNumber = readers[1].ReadNumberFromLine();

            while(true)
            {
                int result = 0;
                int firstC = readers[0].ReadAlphanumeric();
                if (firstC == -1)
                {
                    result = 1;
                }
                else
                {
                    int secondC = readers[1].ReadAlphanumeric();
                    if (secondC == -1)
                    {
                        result = -1;
                    }
                    else if (firstC < secondC)
                    {
                        result = -1;
                    }
                    else if(firstC > secondC)
                    {
                        result = 1;
                    }

                }

                if(result != 0)
                {
                    RollBackLineRead(readers);
                    return result;
                }

            }
        }

        private void RollBackLineRead(MyStreamReader[] readers)
        {
            foreach(var reader in readers)
            {
                reader.RollBackRead();
            }
        }
    }
}
