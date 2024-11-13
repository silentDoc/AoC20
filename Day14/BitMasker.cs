using System.Text;

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
            MemPos = (InsType == InstructionType.memset) ? int.Parse(parts[0].Replace("mem[", "").Replace("]", "")) : -1;
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

        List<string> BuildAddressListRecursive(char[] address)
        {
            if (!address.Contains('X'))
                return [new(address)];

            List<string> retVal = [];

            for (int i = 0; i < address.Count(); i++)
                if (address[i] == 'X')
                {
                    var with0 = address.ToArray();
                    var with1 = address.ToArray();
                    with0[i] = '0';
                    with1[i] = '1';

                    retVal.AddRange(BuildAddressListRecursive(with0));
                    retVal.AddRange(BuildAddressListRecursive(with1));
                    break; // Important to not keep processing
                }

            return retVal;
        }

        public List<long> BuildAddressList(long address)
        {
            var binAddr = Convert.ToString(address, 2);
            var sb = new StringBuilder();
            binAddr = binAddr.PadLeft(36, '0');

            for (int i = 0; i < binAddr.Length; i++)
                sb.Append(Mask[i] == '0' ? binAddr[i] : Mask[i]);

            return BuildAddressListRecursive(sb.ToString().ToCharArray())
                   .Select(x => Convert.ToInt64(x, 2)).ToList();
        }
    }

    internal class BitMasker
    {
        List<Instruction> InstructionList = [];
        public void ParseInput(List<string> lines)
            => lines.ForEach(x => InstructionList.Add(new Instruction(x)));

        long SolvePart(int part = 1)
        {
            Dictionary<long, long> Memory = new();
            BitMaskProcessor masker = new("");

            foreach (var ins in InstructionList)
                if (ins.InsType == InstructionType.mask)
                    masker = new(ins.Value);
                else if(part ==1)
                    Memory[ins.MemPos] = masker.GetNum(ins.NumValue);
                else
                {
                    List<long> addresses = masker.BuildAddressList(ins.MemPos);
                    addresses.ForEach(x => Memory[x] = ins.NumValue);
                }

            return Memory.Values.Sum();
        }

        public long Solve(int part = 1)
            => SolvePart(part);
    }
}
