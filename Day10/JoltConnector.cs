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

        long SolvePart2()
        {
            var conns = connectors.Prepend(0).OrderByDescending(x => x);
            Dictionary<int, long> res = new();

            res[connectors.Max() + 3] = 1;

            foreach (var conn in conns)
                res[conn] = Enumerable.Range(1,3).Sum(i => res.ContainsKey(conn + i) ? res[conn + i] : 0);
            
            return res[0];
        }

        public long Solve(int part = 1)
            => part ==1 ?  SolvePart1() : SolvePart2();
    }
}
