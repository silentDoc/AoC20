using AoC20.Common;

namespace AoC20.Day12
{
    static class Direction
    {
        public static Coord2D North = (0  , -1);
        public static Coord2D South = (0  ,  1);
        public static Coord2D East  = (1  ,  0);
        public static Coord2D West  = (-1 ,  0);
    }

    record Step
    {
        public char Action;
        public int Amount;

        public Step(string input)
        {
            Action = input[0];
            Amount = int.Parse(input[1..]);
        }
    }

    internal class ShipNavigator
    {
        List<Step> Steps = [];
        Coord2D CurrentDir = Direction.East;
        Coord2D CurrentPosition = (0,0);

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => Steps.Add(new Step(x)));

        Coord2D TurnLeft(Coord2D currentDirection)
           => currentDirection switch
           {
               (0, -1) => Direction.West,     // N -> W
               (-1, 0) => Direction.South,    // W -> S
               (0,  1)  => Direction.East,    // S -> E
               (1,  0)  => Direction.North,   // E -> N
               _ => throw new Exception("Invalid direction " + currentDirection.ToString())
           };

        Coord2D TurnRight(Coord2D currentDirection)
            => currentDirection switch
            {
                (0 ,-1) => Direction.East,     // N -> E
                (1 , 0) => Direction.South,    // E -> S
                (0 , 1) => Direction.West,     // S -> W
                (-1, 0) => Direction.North,    // W -> N
                _ => throw new Exception("Invalid direction " + currentDirection.ToString())
            };

        int SolvePart1()
        {
            foreach (var step in Steps)
            {
                CurrentPosition += step.Action switch
                {
                    'N' => Direction.North * step.Amount,
                    'S' => Direction.South * step.Amount,
                    'E' => Direction.East * step.Amount,
                    'W' => Direction.West * step.Amount,
                    _ => (0, 0)
                };

                if (step.Action == 'R')
                    for (int i = 0; i < step.Amount / 90; i++)
                        CurrentDir = TurnRight(CurrentDir);

                if (step.Action == 'L')
                    for (int i = 0; i < step.Amount / 90; i++)
                        CurrentDir = TurnLeft(CurrentDir);

                if (step.Action == 'F')
                    CurrentPosition += CurrentDir * step.Amount;
            }

            return CurrentPosition.Manhattan((0, 0));
        }

        int SolvePart2()
        {
            Coord2D waypoint = (10, -1);    // 10 East, 1 north

            foreach (var step in Steps)
            {
                waypoint += step.Action switch
                {
                    'N' => Direction.North * step.Amount,
                    'S' => Direction.South * step.Amount,
                    'E' => Direction.East * step.Amount,
                    'W' => Direction.West * step.Amount,
                    _ => (0, 0)
                };

                if (step.Action == 'R')
                    for (int i = 0; i < step.Amount / 90; i++)
                        waypoint = (-1*waypoint.y, waypoint.x);

                if (step.Action == 'L')
                    for (int i = 0; i < step.Amount / 90; i++)
                        waypoint = (waypoint.y, -1* waypoint.x);

                if (step.Action == 'F')
                    CurrentPosition += waypoint * step.Amount;
            }

            return CurrentPosition.Manhattan((0, 0));
        }

        public int Solve(int part = 1)
            => part ==1 ? SolvePart1() : SolvePart2();
    }
}
