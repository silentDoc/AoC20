namespace AoC20.Day07
{
    internal class Bag
    {
        public string Color;
        public Dictionary<Bag, int> Contents = new();

        public Bag(string color)
            => Color = color;

        public bool ContainsBag(string color)
            => Contents.Any(c => c.Key.Color == color) || Contents.Any(c => c.Key.ContainsBag(color));

        public int CountContents()
            => Contents.Sum(x => x.Value * (1 + x.Key.CountContents()));
    }

    internal class LuggageProcessor
    {
        List<Bag> Bags = [];

        public void ParseLine(string inputLine)
        {
            var parts = inputLine.Split(" bags contain ", StringSplitOptions.TrimEntries);
            var color = parts[0];
            
            Bag newBag = Bags.Any(x => x.Color == color) ? Bags.First(x => x.Color == color) : new(color);
            
            if (parts[1] == "no other bags.")
                return;

            var contents = parts[1].Replace(".", "").Replace("bags", ""). Replace("bag", "").
                                    Split(",", StringSplitOptions.TrimEntries);

            foreach (var content in contents)
            {
                var prt = content.Split(" ", StringSplitOptions.TrimEntries);
                var contentColor = string.Concat(prt[1], " ",  prt[2]);
                var amount = int.Parse(prt[0]);

                var contentBag = Bags.Any(x => x.Color == contentColor) ? Bags.First(x => x.Color == contentColor) : new(contentColor);
                newBag.Contents[contentBag] = amount;

                if(!Bags.Any(x => x.Color == contentColor))
                    Bags.Add(contentBag);
            }
            if (!Bags.Any(x => x.Color == color))
                Bags.Add(newBag);
        }

        public void ParseInput(List<string> input)
            => input.ForEach(ParseLine);

        public int Solve(int part = 1)
            => part == 1 ? Bags.Count(x => x.ContainsBag("shiny gold")) : Bags.First(x => x.Color== "shiny gold").CountContents();
    }
}
