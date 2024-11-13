namespace AoC20.Day15
{
    internal class Rambunctious
    {
        List<int> nums = [];
        public void ParseInput(List<string> lines)
            => nums = lines[0].Split(",").Select(int.Parse).ToList();

        int PlayGame(int endTurn)
        {
            int turn = nums.Count;

            while (turn != endTurn)
            {
                var num = nums[^1];
                if (nums.Count(x => x == num) == 1)
                    nums.Add(0);
                else
                {
                    int last = nums.Count-1;
                    int secondToLast = Enumerable.Range(0,nums.Count-1).Last( x=> nums[x]==num);
                    nums.Add(last-secondToLast);
                }

                turn++;
            }
            return nums[^1];
        }

        public int Solve(int part = 1)
            => PlayGame(2020);

    }
}
