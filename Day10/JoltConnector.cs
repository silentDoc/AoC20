using AoC20.Common; // List utils

namespace AoC20.Day10
{
    internal class JoltConnector
    {
        List<int> connectors = [];

        public void ParseInput(List<string> lines)
            => connectors = lines.Select(int.Parse).ToList();

        int SolvePart1()
        {
            var dict = connectors.Prepend(0).Append(connectors.Max() + 3)
                                 .OrderBy(x => x)
                                 .Windowed(2)
                                 .GroupBy(x => x[1] - x[0]).ToDictionary(x => x.Key, x => x.Count());
            return dict[1] * dict[3];
        }

        public int Solve(int part = 1)
            => SolvePart1();
    }
}
