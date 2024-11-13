namespace AoC20.Day13
{
    internal class BusStop
    {
        int TimeStamp = 0;
        List<int> buses = [];
        List<string> splittedInput = [];

        public void ParseInput(List<string> lines)
        {
            TimeStamp = int.Parse(lines[0]);
            splittedInput = lines[1].Split(",", StringSplitOptions.TrimEntries).ToList();
            buses = splittedInput.Where(x => x!= "x").Select(int.Parse).ToList();
        }

        long FindFirstBus()
        {
            var mins = Enumerable.Range(TimeStamp, buses.Max());
            var minBus = mins.First(m => buses.Any(x => m % x == 0));
            var id = buses.First(x => minBus % x == 0);
            return (minBus - TimeStamp) * id;
        }

        long WinGoldCoin()
        {
            var indexes = buses.Select(x => x.ToString()).Select(x => splittedInput.IndexOf(x)).ToList();

            long step = buses[0]; // Bus
            long mins = 0L;

            for(int i =1; i<buses.Count;i++)
            {
                while ((mins + indexes[i]) % buses[i] != 0)
                    mins += step;

                step *= buses[i];
            }
            return mins;
        }
        public long Solve(int part = 1)
            => part ==1 ?  FindFirstBus() : WinGoldCoin();
    }
}
