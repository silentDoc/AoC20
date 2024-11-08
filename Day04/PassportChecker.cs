using AoC20.Common;
using System.Text.RegularExpressions;

namespace AoC20.Day04
{
    class Passport
    {
        Dictionary<string, string> Passdata = new();

        List<string> entries = new();
        List<string> validFields = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];
        public Passport(List<string> input)
        {
            entries = input.SelectMany(x => x.Split(" ", StringSplitOptions.TrimEntries)).ToList();
            // Part 2
            foreach (var entry in entries)
            {
                var keyValue = entry.Split(":", StringSplitOptions.TrimEntries);
                Passdata[keyValue[0]] = keyValue[1];
            }
        }
        public bool IsValid
            => validFields.All(x => entries.Any(e => e.StartsWith(x)));

        // Part 2
        Regex regexYear = new Regex(@"(^\d{4}$)");
        Regex regexHairColor = new Regex(@"(^#[0-9, a-f]{6}$)");
        Regex regexPid = new Regex(@"(^[0-9]{9}$)");
        List<string> validEyeColor = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];

        public bool HasValidBirthYear
            => Passdata.ContainsKey("byr") && regexYear.IsMatch(Passdata["byr"]) && int.Parse(Passdata["byr"]) >= 1920 && int.Parse(Passdata["byr"]) <= 2002;
        public bool HasValidIssueYear
            => Passdata.ContainsKey("iyr") && regexYear.IsMatch(Passdata["iyr"]) && int.Parse(Passdata["iyr"]) >= 2010 && int.Parse(Passdata["iyr"]) <= 2020;
        public bool HasValidExpiryYear
            => Passdata.ContainsKey("eyr") && regexYear.IsMatch(Passdata["eyr"]) && int.Parse(Passdata["eyr"]) >= 2020 && int.Parse(Passdata["eyr"]) <= 2030;
        
        bool hasValidHeight()
        { 
            if(!Passdata.ContainsKey("hgt"))
                return false;
            var str = Passdata["hgt"];

            var cmPos = str.IndexOf("cm");
            var inPos = str.IndexOf("in");

            if (cmPos==-1 && inPos==-1)
                return false;

            var strValue = (cmPos == -1) ? str.Substring(0, inPos) : str.Substring(0, cmPos);

            if(strValue.Any(x => !char.IsDigit(x))) 
                return false;    

            var numValue = int.Parse(strValue);

            return cmPos == -1 ? numValue >= 59 && numValue <= 76 : numValue >= 150 && numValue <= 193;
        }

        public bool HasValidHeight
            => hasValidHeight();

        public bool HasValidHairColor
            => Passdata.ContainsKey("hcl") && regexHairColor.IsMatch(Passdata["hcl"]);

        public bool HasValidEyeColor
            => Passdata.ContainsKey("ecl") && validEyeColor.Contains(Passdata["ecl"]);

        public bool HasValidPassId
            => Passdata.ContainsKey("pid") && regexPid.IsMatch(Passdata["pid"]);

        public bool IsValidPart2
          => HasValidBirthYear && HasValidIssueYear && HasValidExpiryYear &&
             HasValidHeight && HasValidHairColor && HasValidEyeColor && HasValidPassId;
    }


    internal class PassportChecker
    {
        List<Passport> passes = new();
        public void ParseInput(List<string> lines)
        {
            var entries = ParseUtils.SplitBy(lines, "");
            entries.ForEach(x => passes.Add(new Passport(x)));
        }

        public int Solve(int part = 1)
            => part == 1 ? passes.Count(x => x.IsValid) : passes.Count(x => x.IsValidPart2);
    }
}