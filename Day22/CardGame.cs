using AoC20.Common;

namespace AoC20.Day22
{
    internal class CardGame
    {
        List<int> deck1 = [];
        List<int> deck2 = [];

        public void ParseInput(List<string> input)
        {
            var decks = ParseUtils.SplitBy(input, "");
            deck1 = decks[0][1..].Select(x => int.Parse(x)).ToList();
            deck2 = decks[1][1..].Select(x => int.Parse(x)).ToList();
        }

        int SolvePart1()
        {
            while (deck1.Any() && deck2.Any())
            {
                if(deck1[0] > deck2[0])
                    deck1.AddRange([deck1[0] , deck2[0]]);
                else
                    deck2.AddRange([deck2[0], deck1[0]]);

                deck1.RemoveAt(0);
                deck2.RemoveAt(0);
            }

            var winning = deck1.Any() ? deck1 : deck2;
            var scores = Enumerable.Range(1, winning.Count).Reverse().ToList();

            return scores.Zip(winning).Select(el => el.First * el.Second).Sum();   
        }

        public int Solve(int part = 1)
            => SolvePart1();
    }
}
