namespace AoC20.Day01
{
    internal class ReportFinder
    {
        List<int> elements = new List<int>();
        public void ParseInput(List<string> input)
            => elements = input.Select(x => int.Parse(x)).ToList();

        int SolvePart1()
        {
            var x = elements.First(x => elements.Any(y => y + x == 2020 && y != x));
            return x * (2020 - x);
        }

        public int Solve(int part = 1)
            => SolvePart1();

    }
}
