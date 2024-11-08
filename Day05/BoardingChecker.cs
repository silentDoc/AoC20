namespace AoC20.Day05
{
    class BoardingPass
    {
        public int Row;
        public int Column;

        public BoardingPass(string input)
        {
            Row  = input[0] == 'F' ? 0 : 64;
            Row += input[1] == 'F' ? 0 : 32;
            Row += input[2] == 'F' ? 0 : 16;
            Row += input[3] == 'F' ? 0 : 8;
            Row += input[4] == 'F' ? 0 : 4;
            Row += input[5] == 'F' ? 0 : 2;
            Row += input[6] == 'F' ? 0 : 1;

            Column = input[7] == 'L' ? 0 : 4;
            Column += input[8] == 'L' ? 0 : 2;
            Column += input[9] == 'L' ? 0 : 1;
        }

        public int SeatId
            => Row * 8 + Column;
            
    }
    internal class BoardingChecker
    {
        List<BoardingPass> passes = new();

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => passes.Add(new BoardingPass(x)));

        public int Solve(int part = 1)
            => passes.Max(x => x.SeatId);
    }
}
