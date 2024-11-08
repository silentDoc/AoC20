namespace AoC20.Day01
{
    internal class ReportFinder
    {
        List<int> elements = new List<int>();

        public void ParseInput(List<string> input)
            => elements = input.Select(x => int.Parse(x)).ToList();

        int SolvePart1()
        {
            var x = elements.First(x => elements.Any(y => y + x == 2020));
            return x * (2020 - x);
        }

        int SolvePart2()
        {
            foreach (var z in elements)
                if (elements.Any(x => elements.Any(y => y + x == 2020 - z )))
                {
                    var x = elements.First(x => elements.Any(y => y + x == 2020 - z ) );
                    return x * z * (2020 - x - z);
                }
            return -1;
        }

        public int Solve(int part = 1)
            => part == 1 ? SolvePart1() : SolvePart2();
    }
}
