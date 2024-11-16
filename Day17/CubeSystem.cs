using AoC20.Common;

namespace AoC20.Day17
{
    internal class CubeSystem
    {
        HashSet<Coord3D> state = [];

        void ParseLine(int row, string line)
            => Enumerable.Range(0, line.Length).Where(x => line[x] == '#').ToList().ForEach(x => state.Add(new Coord3D(x, row, 0)));

        public void ParseInput(List<string> input)
            => Enumerable.Range(0, input.Count).ToList().ForEach(x => ParseLine(x, input[x]));

        public int ActiveAdjacentCubes(Coord3D coord, HashSet<Coord3D> state)
            => coord.GetNeighbors8().Count(x => state.Contains(x));

        HashSet<Coord3D> Evolve(HashSet<Coord3D> oldState)
        {
            HashSet<Coord3D> newState = new();

            (int minX, int maxX) = (oldState.Min(k => k.x)-1, oldState.Max(k => k.x)+1 );
            (int minY, int maxY) = (oldState.Min(k => k.y)-1, oldState.Max(k => k.y)+1 );
            (int minZ, int maxZ) = (oldState.Min(k => k.z)-1, oldState.Max(k => k.z)+1 );

            for (int i = minX; i <= maxX; i++)
                for (int j = minY; j <= maxY; j++)
                    for (int k = minZ; k <= maxZ; k++)
                    {
                        Coord3D pos = new(i, j, k);
                        var adjacent = ActiveAdjacentCubes(pos, oldState);

                        if ((adjacent == 2 || adjacent == 3) && oldState.Contains(pos))
                            newState.Add(pos);

                        if ( adjacent == 3 && !oldState.Contains(pos))
                            newState.Add(pos);
                    }

            return newState;
        }

        int SolvePart1()
        {
            for (int i = 0; i < 6; i++)
                state = Evolve(state);

            return state.Count();
        }

        public int Solve(int part = 1)
            => SolvePart1();
    }
}
