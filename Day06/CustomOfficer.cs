﻿using AoC20.Common;

namespace AoC20.Day06
{
    internal class CustomForm
    { 
        List<string> entries = new();

        public CustomForm(List<string> input)
            => input.ForEach(x => entries.Add(x));

        public int Questions
            => entries.SelectMany(x => x).Distinct().Count();

        public int CommonQuestions
           => entries.SelectMany(x => x).Distinct().Count(x => entries.All(e => e.Contains(x)));
    }
    
    internal class CustomOfficer
    {
        List<CustomForm> forms = new();

        public void ParseInput(List<string> lines)
            => ParseUtils.SplitBy(lines, "").ForEach(x => forms.Add(new CustomForm(x)));

        public int Solve(int part = 1)
            => part == 1 ? forms.Sum(f => f.Questions) : forms.Sum(f => f.CommonQuestions);
    }
}