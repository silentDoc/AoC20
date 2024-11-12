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
               (0, -1)  => Direction.West,    // N -> W
               (-1, 0)  => Direction.South,   // W -> S
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

        int Navigate(int part=1)
        {
            Coord2D waypoint = (10, -1);    // 10 East, 1 north

            foreach (var step in Steps)
            {
                Coord2D movAmount = step.Action switch
                {
                    'N' => Direction.North * step.Amount,
                    'S' => Direction.South * step.Amount,
                    'E' => Direction.East * step.Amount,
                    'W' => Direction.West * step.Amount,
                    _ => (0, 0)
                };

                if(part==1)
                    CurrentPosition += movAmount;
                else
                    waypoint += movAmount; 


                if (step.Action == 'R')
                    for (int i = 0; i < step.Amount / 90; i++)
                        if(part==1)
                            CurrentDir = TurnRight(CurrentDir);
                        else
                            waypoint = (-1 * waypoint.y, waypoint.x);
                

                if (step.Action == 'L')
                    for (int i = 0; i < step.Amount / 90; i++)
                        if(part == 1)
                            CurrentDir = TurnLeft(CurrentDir);
                        else
                            waypoint = (waypoint.y, -1 * waypoint.x);

                if (step.Action == 'F')
                    if (part == 1)
                        CurrentPosition += CurrentDir * step.Amount;
                    else
                        CurrentPosition += waypoint * step.Amount;
            }

            return CurrentPosition.Manhattan((0, 0));
        }

        public int Solve(int part = 1)
            => Navigate(part);
    }
}
