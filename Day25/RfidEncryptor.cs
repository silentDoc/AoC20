namespace AoC20.Day25
{
    internal class RfidEncryptor
    {
        long cardPublic = 0;
        long doorPublic = 0;
        public void ParseInput(List<string> input)
        {
            cardPublic = long.Parse(input[0]);
            doorPublic = long.Parse(input[1]);
        }

        int FindLoopSize(long publicKey)
        {
            long subjectNumber = 1;
            int loopSize = 0;

            while (subjectNumber != publicKey)
            {
                subjectNumber *= 7;
                subjectNumber %= 20201227;
                loopSize++;
            }
            return loopSize;
        }

        long TransformKey(long subjectNumber, int loopSize)
        {
            long value = 1;
            for(int i=0; i<loopSize; i++)
            {
                value *= subjectNumber;
                value %= 20201227;
            }
            return value;
        }

        long FindEncryptionKey()
        { 
            int cardLoopSize = FindLoopSize(cardPublic);
            int doorLoopSize = FindLoopSize(doorPublic);

            return TransformKey(doorPublic, cardLoopSize);
        }

        public long Solve(int part = 1)
            => FindEncryptionKey();
    }
}
