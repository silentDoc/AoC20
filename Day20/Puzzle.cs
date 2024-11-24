using AoC20.Common;
using System.Text;

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

        public bool MatchesBorder(Tile other)
        {
            var otherEdges = other.Borders.Concat(other.Borders.Select(e => new string(e.Reverse().ToArray())));
            return Borders.Intersect(otherEdges).Any();
        }

        public char GetAt(int row, int column)
            => tile[row][column];

        public void Display()
        {
            foreach (var str in tile)
                Console.WriteLine(str);
        }

        public HashSet<Coord2D> PatternMatch(List<string> monster)
        {
            HashSet<Coord2D> retVal = new();

            for (int j = 0; j < tile.Count() - monster.Count; j++)
                for (int i = 0; i < tile[0].Length - monster[0].Length; i++)
                {
                    List<string> comp = [];
                    for (int c = 0; c < monster.Count; c++)
                        comp.Add(tile[j+c].Substring(i, monster[0].Length));

                    var match = true;

                    for (int jj = 0; jj < monster.Count; jj++)
                        for (int ii = 0; ii < monster[0].Length; ii++)
                            if (monster[jj][ii] == '#')
                                match &= (comp[jj][ii] == '#');

                    if (match)
                    {
                        for (int jj = 0; jj < monster.Count; jj++)
                            for (int ii = 0; ii < monster[0].Length; ii++)
                                if (monster[jj][ii] == '#')
                                    retVal.Add(new Coord2D(ii+i, jj+j));
                    }
                }
            return retVal;
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

        Tile[,] ArrangeTiles(int gridDim)
        {
            // Step 1 - Arrange the tiles
            List<Tile> CornerTiles = [];
            List<Tile> EdgeTiles = [];
            List<Tile> MidTiles = [];

            foreach (var tile in tiles)
            {
                var otherTiles = tiles.Where(t => t.Id != tile.Id).ToList();
                var matchingEdges = tile.Borders.Where(e => otherTiles.Any(t => t.Borders.Contains(e) || t.Borders.Contains(new string(e.Reverse().ToArray())))).ToList();

                if (matchingEdges.Count == 2)
                    CornerTiles.Add(tile);
                else if (matchingEdges.Count == 3)
                    EdgeTiles.Add(tile);
                else                            // 4
                    MidTiles.Add(tile);
            }

            var grid = new Tile[gridDim, gridDim];

            // Upper row
            grid[0, 0] = CornerTiles.First();
            CornerTiles.Remove(CornerTiles.First());
            for (int j = 1; j < gridDim; j++)
            {
                if (j < gridDim - 1)
                {
                    var left = grid[0, j - 1];
                    var right = EdgeTiles.First(e => e.MatchesBorder(left));
                    grid[0, j] = right;
                    EdgeTiles.Remove(right);
                }
                else
                {
                    var left = grid[0, j - 1];
                    var right = CornerTiles.First(c => c.MatchesBorder(left));
                    grid[0, j] = right;
                    CornerTiles.Remove(right);
                }
            }

            // Rows in the middle
            for (int i = 1; i < gridDim - 1; i++)
                for (int j = 0; j < gridDim; j++)
                {
                    if (j == 0)
                    {
                        var top = grid[i - 1, j];
                        var bottom = EdgeTiles.First(e => e.MatchesBorder(top));
                        grid[i, j] = bottom;
                        EdgeTiles.Remove(bottom);
                    }
                    if (j > 0 && j < gridDim - 1)
                    {
                        var top = grid[i - 1, j];
                        var left = grid[i, j - 1];
                        var bottom = MidTiles.First(m => m.MatchesBorder(top) && m.MatchesBorder(left));
                        grid[i, j] = bottom;
                        MidTiles.Remove(bottom);
                    }
                    if (j == gridDim - 1)
                    {
                        var top = grid[i - 1, j];
                        var left = grid[i, j - 1];
                        var bottom = EdgeTiles.First(e => e.MatchesBorder(top) && e.MatchesBorder(left));
                        grid[i, j] = bottom;
                        EdgeTiles.Remove(bottom);
                    }
                }

            // Lower row
            int lowerRow = gridDim - 1;
            for (int j = 0; j < gridDim; j++)
            {
                if (j == 0)
                {
                    var top = grid[lowerRow - 1, j];
                    var bottom = CornerTiles.First(c => c.MatchesBorder(top));
                    grid[lowerRow, j] = bottom;
                    CornerTiles.Remove(bottom);
                }

                if (j > 0 && j < gridDim - 1)
                {
                    var top = grid[lowerRow - 1, j];
                    var left = grid[lowerRow, j - 1];
                    var bottom = EdgeTiles.First(e => e.MatchesBorder(top) && e.MatchesBorder(left));
                    grid[lowerRow, j] = bottom;
                    EdgeTiles.Remove(bottom);
                }

                if (j == gridDim - 1)
                {
                    var top = grid[lowerRow - 1, j];
                    var left = grid[lowerRow, j - 1];
                    var bottom = CornerTiles.Single();
                    grid[lowerRow, j] = bottom;
                    CornerTiles.Remove(bottom);
                }
            }

            // Find the right orientations for the tiles of the grid
            for (var i = 0; i < gridDim; i++)
                for (var j = 0; j < gridDim; j++)
                {
                    var top    = i > 0           ? grid[i - 1, j] : null;
                    var left   = j > 0           ? grid[i, j - 1] : null;
                    var bottom = i < gridDim - 1 ? grid[i + 1, j] : null;
                    var right  = j < gridDim - 1 ? grid[i, j + 1] : null;

                    var tile = grid[i, j];

                    var query = tile.Variants().Single(o =>
                        (top    == null || top.MatchesBorder(o.Top)) &&
                        (left   == null || left.MatchesBorder(o.Left)) &&
                        (bottom == null || bottom.MatchesBorder(o.Bottom)) &&
                        (right  == null || right.MatchesBorder(o.Right)));

                    grid[i, j] = query;
                }
            
            return grid;
        }

        List<string> BuildMap(Tile[,] grid, int gridDim)
        {
            List<string> retVal = [];
            int strLen = grid[0, 0].Top.Length;

            for (int i = 0; i < gridDim * strLen; i++)
            {
                StringBuilder sb = new();
                for (int j = 0; j < gridDim * strLen; j++)
                {
                    int row = i / strLen;
                    int column = j / strLen;
                    int offSetY = i % strLen;
                    int offSetX = j % strLen;

                    // Remove borders
                    if (offSetX == 0 || offSetX == strLen - 1 || offSetY == 0 || offSetY == strLen - 1)
                        continue;

                    sb.Append(grid[row, column].GetAt(offSetY, offSetX));
                }
                var line = sb.ToString();
                if (line != "")
                    retVal.Add(line);
            }
            return retVal;
        }

       int FindMonsters(List<string> map)
        {
            List<string> monster = ["                  # ",
                                    "#    ##    ##    ###", 
                                    " #  #  #  #  #  #   "];

            var mapTile = new Tile(int.MaxValue, map.ToArray());

            var variants = mapTile.Variants();

            foreach (var variant in variants)
            {
                var positionSet = variant.PatternMatch(monster);
                if (positionSet.Count > 0)
                    return map.SelectMany(x => x).Count(x => x == '#') - positionSet.Count;
            }
            return -1;
        }

        int SolvePart2()
        {
            int gridDim = (int)Math.Sqrt(tiles.Count()); // 9 tiles -- 3 (3x3) ; 16 tiles -- 4 (4x4) etc...
            var grid = ArrangeTiles(gridDim);
            var map  = BuildMap(grid, gridDim);

            // Display the map on screen
            var mapTile = new Tile(int.MaxValue, map.ToArray());
            return FindMonsters(map);
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
            => part == 1 ? SolvePart1() : SolvePart2();
    }
}
