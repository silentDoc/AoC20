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

        public long Solve(int part = 1)
            => SolvePart1();
    }
}
