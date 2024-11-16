using AoC20.Common;
using System.Text.RegularExpressions;

namespace AoC20.Day19
{
    internal class MonsterRegex
    {
        List<string> messages = [];
        Regex regex = new("");

        public void ParseInput(List<string> input, int part = 1)
        {
            var sections = ParseUtils.SplitBy(input, "");

            messages = sections[1];
            var rules = sections[0].Select(x => x.Split(": ")).ToDictionary(x => x[0], x => x[1]);

            // Dirty but practical - Unroll the loops several time until the end result is not affected
            if (part == 2)
            {
                // 359
                // rules["8"] = "42 | 42 42";            
                // rules["11"] = "42 31 | 42 42 31 31";

                // 396
                // rules["8"] = "42 | 42 42 | 42 42 42";            
                // rules["11"] = "42 31 | 42 42 31 31 | 42 42 42 31 31 31";

                // 411
                // rules["8"] = "42 | 42 42 | 42 42 42 | 42 42 42 42";            
                // rules["11"] = "42 31 | 42 42 31 31 | 42 42 42 31 31 31 | 42 42 42 42 31 31 31 31";

                // 412
                // rules["8"] = "42 | 42 42 | 42 42 42 | 42 42 42 42 | 42 42 42 42 42";
                // rules["11"] = "42 31 | 42 42 31 31 | 42 42 42 31 31 31 | 42 42 42 42 31 31 31 31 | 42 42 42 42 42 31 31 31 31 31";

                // 412
                rules["8"] = "42 | 42 42 | 42 42 42 | 42 42 42 42 | 42 42 42 42 42 | 42 42 42 42 42 42";
                rules["11"] = "42 31 | 42 42 31 31 | 42 42 42 31 31 31 | 42 42 42 42 31 31 31 31 | 42 42 42 42 42 31 31 31 31 31 | 42 42 42 42 42 42 31 31 31 31 31 31";
            }

            regex = new Regex($"^{BuildRegex(rules, "0")}$");
        }

        private string BuildRegex(IReadOnlyDictionary<string, string> map, string rule)
        {
            var value = map[rule];

            if (value.StartsWith("\""))
                return value[1..^1];

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
