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

        private bool IsInLoop(out int acumOut)
        {
            int ptr = 0;
            int acum = 0;
            HashSet<int> executedInstructions = new();

            while (ptr>=0 && ptr<sourceCode.Count)
            {
                if (!executedInstructions.Add(ptr))
                {
                    acumOut = acum;
                    return true;
                }

                var ins = sourceCode[ptr];

                if (ins.Instruction == "acc")
                    acum += ins.Argument;

                ptr += (ins.Instruction == "jmp") ? ins.Argument : 1;
            }
            acumOut = acum;
            return false;
        }

        private int SolvePart1()
        {
            int acum;
            IsInLoop(out acum);
            return acum;
        }

        private int SolvePart2()
        {
            // Bruteforce ? :P
            int acum = 0;
            for (int i = sourceCode.Count - 1; i >= 0; i--)
            {
                if (sourceCode[i].Instruction == "acc")
                    continue;

                sourceCode[i].Instruction = (sourceCode[i].Instruction == "nop") ? "jmp" : "nop";

                if (!IsInLoop(out acum))
                    return acum;

                sourceCode[i].Instruction = (sourceCode[i].Instruction == "nop") ? "jmp" : "nop";
            }
            return -1;
        }

        public int Solve(int part = 1)
            => part == 1 ? SolvePart1() : SolvePart2();
    }
}
