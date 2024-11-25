namespace AoC20.Day23
{
    class Cup
    {
        public int Label;
        public Cup next = null;
    }

    internal class CupCrab
    {
        int[] arrangement = [];

        public void ParseInput(List<string> input)
            => arrangement = input[0].Select(x => int.Parse(x.ToString())).ToArray();

        void SingleMove(ref int current)
        {
            var arrLen = arrangement.Length;
            var currentLabel = arrangement[current];

            int[] pickedCups = [arrangement[(current + 1) % arrLen], arrangement[(current + 2) % arrLen], arrangement[(current + 3) % arrLen]];
            int[] remaining = arrangement.ToList().Where(x => !pickedCups.Contains(x)).ToArray();

            var destination = currentLabel - 1;
            if (destination == 0)
                destination = 9;

            while (pickedCups.Contains(destination))
            {
                destination--;
                if (destination == 0)
                    destination = 9;
            }

            int destIndex = remaining.ToList().IndexOf(destination);
            
            var left = remaining[0..(destIndex+1)];
            var right = remaining[(destIndex + 1)..];
            arrangement = [..left,..pickedCups,..right];

            current = (arrangement.ToList().IndexOf(currentLabel) +1) % arrLen;
        }

        string CrabMove(int numMoves)
        {
            var current = 0;

            for (int i = 0; i < numMoves; i++)
                SingleMove(ref current);

            var resList = arrangement.ToList();
            var ind = resList.IndexOf(1);
            int[] elements = [.. arrangement[(ind + 1)..], .. arrangement[0..ind]];

            return string.Concat(elements.Select(x => x.ToString()).ToList());
        }

        string SolvePart2()
        {
            Dictionary<int, Cup> mapCups = new();

            Cup start = new Cup();
            start.Label = arrangement[0];
            mapCups[start.Label] = start;

            Cup current = start;
            for (int i = 1; i < arrangement.Count(); i++)
            {
                Cup newCup = new();
                newCup.Label = arrangement[i];
                mapCups[newCup.Label] = newCup;
                current.next = newCup;
                current = current.next;
            }
            for (int i = 10; i <= 1000000; i++)
            {
                Cup newCup = new();
                newCup.Label = i;
                mapCups[newCup.Label] = newCup;
                current.next = newCup;
                current = current.next;
            }
            current.next = start;

            int numMoves = 10000000;
            
            current = start;
            for (int i = 0; i < numMoves; i++)
            {
                var pick = current.next;
                var destination = current.Label-1;
                
                if (destination == 0)
                    destination = 1000000;

                while (pick.Label == destination || pick.next.Label == destination || pick.next.next.Label == destination)
                {
                    destination--;
                    if (destination == 0)
                        destination = 1000000;
                }

                Cup target = mapCups[destination];
                Cup newNextToCurrent = pick.next.next.next;

                Cup newNextToPick = target.next;

                current.next = newNextToCurrent;
                target.next = pick;
                pick.next.next.next = newNextToPick;

                current = current.next;
            }

            long op1 = mapCups[1].next.Label;
            long op2 = mapCups[1].next.next.Label;

            return (op1 * op2).ToString();
        }

        public string Solve(int part = 1)
            => part ==1 ? CrabMove(100) : SolvePart2();
    }
}
