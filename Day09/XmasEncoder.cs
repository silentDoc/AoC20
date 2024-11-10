namespace AoC20.Day09
{
    internal class XmasEncoder
    {
        List<long> numbers = new();
        int pre = 25;
        public void ParseInput(List<string> input)
            => numbers = input.Select(long.Parse).ToList();

        bool Check(int position)
        {
            var preamble = numbers[(position - pre)..position];
            return preamble.Any(x => preamble.Contains(numbers[position] - x) && x!= numbers[position]/2);
        }

        long SolvePart1()
        {
            for (int i = pre; i < numbers.Count; i++)
                if (!Check(i))
                    return numbers[i];
            return -1;
        }

        long SolvePart2()
        {
            int position = 0;

            for (int i = pre; i < numbers.Count; i++)
                if (!Check(i))
                {
                    position = i;
                    break;
                }

            for (int i = position; i > 0; i--)
            {
                int amount = 1;
                var checkRange = numbers[(i - amount)..i];
                while (checkRange.Sum() < numbers[position])
                {
                    amount++;
                    checkRange = numbers[(i - amount)..i];
                }

                if (checkRange.Sum() == numbers[position])
                    return checkRange.Min() + checkRange.Max();
            }
            
            return -1;
        }

        public long Solve(int part = 1)
            => part == 1 ? SolvePart1() : SolvePart2();
    }
}
