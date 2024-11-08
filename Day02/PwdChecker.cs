using System.Text.RegularExpressions;

namespace AoC20.Day02
{
    record Element
    {
        public int Low = 0; 
        public int High =0;
        public char Letter = ' ';
        public string Pwd = "";

        public Element(string inputLine)
        {
            Regex rg = new Regex(@"(\d+)-(\d+) ([a-z]): ([a-z]+)");
            var groups = rg.Match(inputLine).Groups;
            Low = int.Parse(groups[1].Value);
            High = int.Parse(groups[2].Value);
            Letter = char.Parse(groups[3].Value);
            Pwd = groups[4].Value;
        }

        public bool CompliesPolicy
            => Low <= Pwd.Count(x => x==Letter) && High >= Pwd.Count(y => y==Letter);
    }

    internal class PwdChecker
    {
        List<Element> policies = new List<Element>();
        
        public void ParseInput(List<string> lines)
            => lines.ForEach(x => policies.Add(new Element(x)));

        public int Solve(int part = 1)
            => policies.Count(x => x.CompliesPolicy);
    }
}
