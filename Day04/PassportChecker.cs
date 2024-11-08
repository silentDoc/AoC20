namespace AoC20.Day04
{
    class Passport
    {
        List<string> entries = new();
        List<string> validFields = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];
        public Passport(List<string> input)
            => entries = input.SelectMany(x => x.Split(" ", StringSplitOptions.TrimEntries)).ToList();

        public bool IsValid
            => validFields.All(x => entries.Any(e => e.StartsWith(x)));
    }

    internal class PassportChecker
    {
        List<Passport> passes = new();
        public void ParseInput(List<string> lines)
        {
            List<string> set = new();

            foreach (string line in lines)
            {
                if (line == "")
                {
                    passes.Add(new Passport(set));
                    set.Clear();
                }
                else
                    set.Add(line);
            }
            passes.Add(new Passport(set));
        }

        public int Solve(int part = 1)
            => passes.Count(x => x.IsValid);
    }
}
