namespace AoC20.Day15
{
    internal class Rambunctious
    {
        List<int> nums = [];
        public void ParseInput(List<string> lines)
            => nums = lines[0].Split(",").Select(int.Parse).ToList();

        long PlayGame(long endTurn)
        {
            Dictionary<long, long[]> last = new();

            for (int i = 0; i < nums.Count-1; i++)
                last[nums[i]]= [i];

            long turn = nums.Count;
            long num = nums[^1];

            while (turn != endTurn)
            {
                if (!last.ContainsKey(num))
                {
                    last[num] = [turn - 1];
                    num = 0;
                }
                else
                {
                    last[num] = [turn - 1, last[num][0]];
                    num = last[num][0] - last[num][1];
                }
                turn++;
            }
            return num;
        }

        public long Solve(int part = 1)
            => part == 1 ? PlayGame(2020) : PlayGame(30000000);

    }
}
