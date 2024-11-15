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

        long SolvePart1()
        {
            int retVal = 0;

            foreach (var ticket in nearbyTickets)
                retVal += ticket.Where(x => !rules.Any(r => r.IsValid(x))).Sum();

            return (long) retVal;
        }

        bool IsValid(List<int> ticket)
           => ticket.All(x => rules.Any(r => r.IsValid(x)));

        List<string> ValidRules(int value)
            => rules.Where(r => r.IsValid(value)).Select(x => x.Name).ToList();

        long SolvePart2()
        {
            Dictionary<int, List<string>> commonElements = new();
            Dictionary<int, string> ticketFields = new();

            var validTickets = nearbyTickets.Where(x => IsValid(x)).Prepend(myTicket).ToList();

            for (int i = 0; i < myTicket.Count; i++)
                commonElements[i] = rules.Select(x => x.Name).ToList();

            foreach (var ticket in validTickets)
                for (int i = 0; i < myTicket.Count; i++)
                    commonElements[i] = commonElements[i].Intersect(ValidRules(ticket[i])).ToList();

            for (int i = 0; i < myTicket.Count; i++)
            {
                var singleIndex = commonElements.Keys.First(x => commonElements[x].Count() == 1);
                var element = commonElements[singleIndex][0];
                ticketFields[singleIndex] = element;

                foreach (var fields in commonElements.Values)
                    if (fields.Contains(element))
                        fields.Remove(element);
            }

            var retIndices = ticketFields.Keys.Where(x => ticketFields[x].StartsWith("departure")).ToList();

            long retVal = 1;
            foreach (var pos in retIndices)
                retVal *= (long)myTicket[pos];

            return retVal;
        }

        public long Solve(int part = 1)
            => part ==1 ? SolvePart1() : SolvePart2();
    }
}
