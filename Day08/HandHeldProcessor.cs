namespace AoC20.Day08
{
    public class CodeLine
    {
        public string Instruction = "";
        public int Argument = 0;

        public CodeLine(string instruction, int argument)
        {
            Instruction = instruction;
            Argument = argument;
        }   
    }

    internal class HandHeldProcessor
    {
        List<CodeLine> sourceCode = new();

        void ParseLine(string line)
        { 
            var parts = line.Split(' ', StringSplitOptions.TrimEntries);
            sourceCode.Add(new CodeLine(parts[0], int.Parse(parts[1])));
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(ParseLine);

        private int SolvePart1()
        {
            int ptr = 0;
            int acum = 0;
            HashSet<int> executedInstructions = new();

            while (true)
            {
                if (!executedInstructions.Add(ptr))
                    return acum;

                var ins = sourceCode[ptr];

                if (ins.Instruction == "acc")
                    acum += ins.Argument;

                ptr += (ins.Instruction == "jmp") ? ins.Argument : 1;
            }
        }

        public int Solve(int part = 1)
            => SolvePart1();
    }
}
