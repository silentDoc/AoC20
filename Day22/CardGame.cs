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

        int CrabCombat()
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

            return GetScore( deck1.Any() ? deck1 : deck2);
        }

        string GetState(List<int> d1, List<int> d2)
            => string.Join(',', d1.Select(x => x.ToString()).ToList()) + ":" + string.Join(',', d2.Select(x => x.ToString()).ToList());

        int GetScore(IEnumerable<int> deck)
            => deck.Reverse().Select((value, index) => value * (index + 1)).Sum();

        int RecursiveGame(List<int> d1, List<int> d2)
        {
            HashSet<string> states = new();

            while (d1.Any() && d2.Any())
            {
                var state = GetState(d1,d2);

                if (!states.Add(state))
                    return 1;

                int card1 = d1[0];
                int card2 = d2[0];
                
                var roundResult = (d1.Count() > card1 && d2.Count() > card2) ? RecursiveGame(d1[1..(card1 + 1)], d2[1..(card2 + 1)])
                                                                             : (card1 > card2) ? 1 : 2;

                if(roundResult == 1)
                    d1.AddRange([card1, card2]);
                else
                    d2.AddRange([card2, card1]);

                d1.RemoveAt(0);
                d2.RemoveAt(0);
            }

            return d1.Any() ? 1 : 2;
        }

        int RecursiveCombat()
            => GetScore( RecursiveGame(deck1, deck2) == 1 ? deck1 : deck2 );

        public int Solve(int part = 1)
            => part == 1 ? CrabCombat() : RecursiveCombat();
    }
}
