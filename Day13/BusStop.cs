namespace AoC20.Day13
{
    internal class BusStop
    {
        int TimeStamp = 0;
        List<int> buses = [];
        public void ParseInput(List<string> lines)
        {
            TimeStamp = int.Parse(lines[0]);
            buses = lines[1].Split(",").Where(x => x!= "x").Select(int.Parse).ToList();
        }

        long FindFirstBus()
        {
            var mins = Enumerable.Range(TimeStamp, buses.Max());
            var minBus = mins.First(m => buses.Any(x => m % x == 0));
            var id = buses.First(x => minBus % x == 0);
            return (minBus - TimeStamp) * id;
        }

        public long Solve(int part = 1)
            => FindFirstBus();
    }
}
