﻿using AoC20.Common;

namespace AoC20.Day11
{
    static class Seat
    { 
        public static char Empty = 'L';
        public static char Full = '#';
        public static char Floor = '.';
    }
    internal class SeatSolver
    {
        Dictionary<Coord2D, char> SeatMap = new();

        void ParseLine(int row, string line)
            => Enumerable.Range(0, line.Length).ToList().ForEach(i => SeatMap[(i, row)] = line[i]);


        public void ParseInput(List<string> lines)
            => Enumerable.Range(0, lines.Count).ToList().ForEach(x => ParseLine(x, lines[x]));

        string GetState(Dictionary<Coord2D, char> seatMap)
            => string.Concat(seatMap.Values);
               
        Dictionary<Coord2D, char> Step(Dictionary<Coord2D, char> seatMap)
        {
            int maxX = SeatMap.Keys.Max(k => k.x);
            int maxY = SeatMap.Keys.Max(k => k.y);
            Dictionary<Coord2D, char> retVal = new();

            foreach(var key in seatMap.Keys) 
            {
                if (seatMap[key] == Seat.Floor)
                {
                    retVal[key] = Seat.Floor;
                    continue;
                }

                var neighs = key.GetNeighbors8().Where(n => n.x >= 0 && n.y >= 0 && n.y <= maxY && n.x <= maxX).ToList();
                var occupied = neighs.Count(k => seatMap[k] == Seat.Full);

                if (seatMap[key] == Seat.Empty && occupied == 0)
                    retVal[key] = Seat.Full;
                else if (seatMap[key] == Seat.Full && occupied >= 4)
                    retVal[key] = Seat.Empty;
                else
                    retVal[key] = seatMap[key];
            }

            return retVal;
        }
    
        private int SolvePart1()
        {
            string previousState = "";
            var currentState = GetState(SeatMap);
            Dictionary<Coord2D, char> nextStep = new();

            foreach (var key in SeatMap.Keys)
                nextStep[key] = SeatMap[key];

            while (previousState != currentState)
            {
                nextStep = Step(nextStep);
                previousState = currentState;
                currentState = GetState(nextStep);
            }

            return nextStep.Values.Count(x => x == Seat.Full);
        }

        public int Solve(int part = 1)
            => SolvePart1();
    }
}