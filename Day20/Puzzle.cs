using AoC20.Common;

namespace AoC20.Day20
{
    class Tile
    {
        public long Id;
        string[] tile;

        public Tile(List<string> input)
        {
            Id = long.Parse(input[0].Replace("Tile ", "").Replace(":", ""));
            tile = new string[input.Count-1];
            for(int i = 1; i < input.Count; i++)
                tile[i-1] = input[i];
        }

        public Tile(long id, string[] tileElements)
        {
            Id = id;
            tile = new string[tileElements.Count()];
            Enumerable.Range(0, tileElements.Count()).ToList().ForEach(x => tile[x] = tileElements[x]);
        }

        public string Top
            => tile[0];
        public string Bottom
            => tile[^1];

        public string Left
            => new(tile.Select(x => x[0]).ToArray());
        public string Right
            => new(tile.Select(x => x[^1]).ToArray());

        public string[] Borders
            => [Top, Bottom, Left, Right];

        public bool MatchesBorder(string border)
            => Borders.Contains(border) || Borders.Contains(new string(border.Reverse().ToArray()));

        public Tile Rotate()
        {
            var newTile = new string[tile.Length];
            for(int i=0; i<tile.Length; i++)
                newTile[i] = new string(tile.Select(r => r[i]).Reverse().ToArray());

            return new(Id, newTile);
        }

        public Tile Flip()
        {
            var newTile = new string[tile.Length];
            for (int i = 0; i < tile.Length; i++)
                newTile[i] = new(tile[i].Reverse().ToArray());

            return new(Id, newTile);
        }

        public Tile[] Variants()
        {
            var variants = new List<Tile>();
            var current = this;
            for (int i = 0; i < 4; i++)
            {
                variants.Add(current);
                variants.Add(current.Flip());
                current = current.Rotate();
            }
            return variants.ToArray();
        }
    }

    internal class Puzzle
    {
        List<Tile> tiles = [];

        public void ParseInput(List<string> input)
        {
            var groups = ParseUtils.SplitBy(input, "");
            groups.ForEach(x => tiles.Add(new Tile(x)));
        }

        long SolvePart1()
        {
            long res = 1;
            foreach (var tile in tiles)
            {
                var otherTiles = tiles.Where(t => t.Id != tile.Id).ToList();
                var matchingEdges = tile.Borders.Where( e => otherTiles.Any(t => t.Borders.Contains(e) || t.Borders.Contains(new string(e.Reverse().ToArray())) )).ToList();

                if (matchingEdges.Count == 2)
                    res *= tile.Id;
            }

            return res;
        }

        public long Solve(int part = 1)
            => SolvePart1();
    }
}
