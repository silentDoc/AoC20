using AoC20.Common;
using System.Text.RegularExpressions;

namespace AoC20.Day19
{
    internal class MonsterRegex
    {
        List<string> messages = [];
        Regex regex = new("");

        public void ParseInput(List<string> input)
        {
            var sections = ParseUtils.SplitBy(input, "");

            messages = sections[1];
            var rules = sections[0].Select(x => x.Split(": ")).ToDictionary(x => x[0], x => x[1]);
            regex = new Regex($"^{BuildRegex(rules, "0")}$");
        }

        private string BuildRegex(IReadOnlyDictionary<string, string> map, string rule)
        {
            var value = map[rule];

            if (value.StartsWith("\""))
            {
                return value[1..^1];
            }

            var subRules = value.Split(" | ");
            var subRegexes = subRules.Select(x => x.Split(" ").Select(y => BuildRegex(map, y)).ToArray());
            var regexes = subRegexes.Select(x => string.Join("", x)).ToArray();

            return $"({string.Join("|", regexes)})";
        }

        int SolvePart1()
            => messages.Count(x => regex.IsMatch(x));

        public long Solve(int part = 1)
            => SolvePart1();
    }
}
