using AoC20.Common;

namespace AoC20.Day03
{
    internal class SlopeSled
    {
        Dictionary<Coord2D, char> Map = new();
        int MapHeight = 0;
        int MapWidth = 0;

        void ParseLine(string line, int row)
            => Enumerable.Range(0, line.Length).ToList().ForEach(x => Map[(x, row)] = line[x]);

        public void ParseInput(List<string> lines)
        {
            MapHeight = lines.Count;
            MapWidth = lines[0].Length;
            Enumerable.Range(0, lines.Count).ToList().ForEach(x => ParseLine(lines[x], x));
        }

        int SolvePart1()
        {
            int x = 0; 
            int trees = 0;

            for (int y = 0; y < MapHeight; y++, x = (x + 3) % MapWidth)
                trees += (Map[(x, y)] == '#') ? 1 : 0;

            return trees;
        }

        public int Solve(int part = 1)
            => SolvePart1();

    }
}
