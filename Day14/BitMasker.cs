using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace AoC20.Day14
{
    enum InstructionType { mask, memset };
    record Instruction
    {
        public InstructionType InsType;
        public string Value = "";
        public int MemPos = -1;

        public Instruction(string inputLine)
        {
            InsType = inputLine.StartsWith("mask") ? InstructionType.mask : InstructionType.memset;
            var parts = inputLine.Split(" = ", StringSplitOptions.TrimEntries);
            Value = parts[1];
            if (InsType == InstructionType.memset)
                MemPos = int.Parse(parts[0].Replace("mem[", "").Replace("]", ""));
        }

        public long NumValue
            => long.Parse(Value);
    }

    class BitMaskProcessor
    {
        public string Mask = "";
        public BitMaskProcessor(string mask)
            => Mask = mask;

        public long GetNum(long num)
        {
            StringBuilder sb = new StringBuilder();
            var binNum = Convert.ToString(num, 2);
            binNum = binNum.PadLeft(36, '0');

            for (int i = 0; i < binNum.Length; i++)
                sb.Append(Mask[i] == 'X' ? binNum[i] : Mask[i]);

            return Convert.ToInt64(sb.ToString(), 2);
        }
    }
         

    internal class BitMasker
    {
        List<Instruction> InstructionList = [];
        public void ParseInput(List<string> lines)
            => lines.ForEach(x => InstructionList.Add(new Instruction(x)));

        long SolvePart1()
        {
            Dictionary<int, long> Memory = new();

            BitMaskProcessor masker = new("");
            foreach (var ins in InstructionList)
            {
                if (ins.InsType == InstructionType.mask)
                {
                    masker = new(ins.Value);
                    continue;
                }
                Memory[ins.MemPos] = masker.GetNum(ins.NumValue);
            }

            return Memory.Values.Sum();
        }

        public long Solve(int part = 1)
            => SolvePart1();
    }
}
