using AoC20.Common;

namespace AoC20.Day16
{
    record Rule
    {
        public string Name { get; init; }
        public int[] Range1 = new int[2];
        public int[] Range2 = new int[2];

        public Rule(string input)
        {
            var parts = input.Split(':');
            Name = parts[0];
            var ranges = parts[1].Split(" or ");
            var rangeValues = ranges[0].Split("-").Select(int.Parse).ToList();
            Range1 = [rangeValues[0], rangeValues[1]];
            rangeValues = ranges[1].Split("-").Select(int.Parse).ToList();
            Range2 = [rangeValues[0], rangeValues[1]];
        }

        public bool IsValid(int value)
            => (value >= Range1[0] && value <= Range1[1]) || (value >= Range2[0] && value <= Range2[1]);
    }

    internal class TicketScanner
    {
        List<Rule> rules = [];
        List<int> myTicket = [];
        List<List<int>> nearbyTickets = [];

        public void ParseInput(List<string> input)
        {
            var sections = ParseUtils.SplitBy(input, "");
            sections[0].ForEach(x => rules.Add(new(x)));

            myTicket = sections[1][1].Split(",").Select(int.Parse).ToList();

            foreach (var ticket in sections[2].Skip(1))
                nearbyTickets.Add(ticket.Split(",").Select(int.Parse).ToList());
        }

        int SolvePart1()
        {
            int retVal = 0;

            foreach (var ticket in nearbyTickets)
                retVal += ticket.Where(x => !rules.Any(r => r.IsValid(x))).Sum();

            return retVal;
        }

        public int Solve(int part = 1)
            => SolvePart1();
    }
}
