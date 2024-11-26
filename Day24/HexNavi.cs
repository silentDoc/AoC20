using AoC20.Common;

namespace AoC20.Day24
{
    // https://www.redblobgames.com/grids/hexagons/
    // Amazing resource for hex navigation stuff
    // I will be using the “odd-r” horizontal layout - shoves odd rows right

    internal class HexNavi
    {
        Dictionary<Coord2D, bool> tiles = new();
        List<List<string>> directions = [];
        void ParseLine(string line)
        { 
            List<string> list = new List<string>();

            for (int i = 0; i < line.Count(); i++)
            {
                string subStr = line.Substring(i);

                var dir = subStr.StartsWith("se") ? "se" :
                          subStr.StartsWith("sw") ? "sw" :
                          subStr.StartsWith("ne") ? "ne" :
                          subStr.StartsWith("nw") ? "nw" :
                          subStr.StartsWith("w")  ? "w"  :
                          subStr.StartsWith("e")  ? "e"  : "";

                list.Add(dir);
                i += dir.Length == 1 ? 0 : 1;
            }
            directions.Add(list);
        }

        Coord2D HexMove(Coord2D current, string direction)
            => direction switch
            {
                "e" => (current.x + 1, current.y),
                "w" => (current.x - 1, current.y),
                "ne" => current.y % 2 == 0 ? (current.x, current.y - 1)    : (current.x + 1, current.y - 1),
                "se" => current.y % 2 == 0 ? (current.x, current.y + 1)    : (current.x + 1, current.y + 1),
                "nw" => current.y % 2 == 0 ? (current.x -1, current.y - 1) : (current.x, current.y - 1),
                "sw" => current.y % 2 == 0 ? (current.x -1, current.y + 1) : (current.x, current.y + 1),
                _ => throw new Exception("Invalid direction : " + direction)
            };

        public void ParseInput(List<string> lines)
            => lines.ForEach(ParseLine);

        Coord2D Walk(List<string> path)
        {
            Coord2D start = (0, 0);
            Coord2D current = start;

            foreach(var step in path)
                current = HexMove(current, step);

            return current;
        }

        List<Coord2D> GetHexNeighbors(Coord2D position)
        {
            List<Coord2D> neighbors = [HexMove(position, "e"), HexMove(position, "w"), HexMove(position, "se"), HexMove(position, "ne"), HexMove(position, "nw"), HexMove(position, "sw")];
            return neighbors.Where(x => tiles.ContainsKey(x)).ToList();
        }

        int SolvePart1()
        {
            foreach (var walk in directions)
            { 
                var position = Walk(walk);
                
                if (tiles.ContainsKey(position))
                    tiles[position] = !tiles[position];
                else
                    tiles[position] = true;
            }

            return tiles.Values.Count(x => x == true);
        }

        int SolvePart2()
        {
            SolvePart1();

            for (int day = 0; day < 100; day++)
            { 
                int minX = tiles.Keys.Min(c => c.x) -2;
                int maxX = tiles.Keys.Max(c => c.x) +2;
                int minY = tiles.Keys.Min(c => c.x) -2;
                int maxY = tiles.Keys.Max(c => c.x) +2;

                for (int i = minX; i <= maxX; i++)
                    for (int j = minY; j <= maxY; j++)
                    {
                        Coord2D pos = (i, j);
                        if (tiles.ContainsKey(pos))
                            continue;
                        tiles[pos] = false;
                    }

                var flipWhite = tiles.Keys.Where(k => tiles[k] == false 
                                                      && k.x >= minX && k.x <= maxX && k.y >= minY && k.y <= maxY
                                                      && GetHexNeighbors(k).Count(x => tiles[x]==true) == 2 );

                var flipBlack = tiles.Keys.Where(k => tiles[k] == true
                                                      && k.x > minX && k.x < maxX && k.y > minY && k.y < maxY);

                flipBlack = flipBlack.Where(k => GetHexNeighbors(k).Count(x => tiles[x] == true) == 0 || GetHexNeighbors(k).Count(x => tiles[x] == true) > 2);

                var tilesToSwitch = flipBlack.Concat(flipWhite).ToHashSet();

                foreach (var tile in tilesToSwitch)
                    tiles[tile] = !tiles[tile];

                Console.WriteLine("Day " + (day + 1).ToString() + " : Black tiles :" + tiles.Values.Count(x => x == true).ToString());
            }

            return tiles.Values.Count(x => x == true);
        }

        public int Solve(int part = 1)
            => part ==1 ? SolvePart1() : SolvePart2();
    }
}
