using System.Diagnostics;

namespace AoC20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 20;
            int part = 1;
            bool test = false;
            int testNum = 0;

            string input = "./Input/day" + day.ToString("00");
            input += (test) ? "_test" + (testNum > 0 ? testNum.ToString() : "") + ".txt" : ".txt";

            Console.WriteLine("AoC 2020 - Day {0} , Part {1} - Test Data {2}", day, part, test);
            Stopwatch st = new();
            st.Start();
            string result = day switch
            {
                1 => day1(input, part),
                2 => day2(input, part),
                3 => day3(input, part),
                4 => day4(input, part),
                5 => day5(input, part),
                6 => day6(input, part),
                7 => day7(input, part),
                8 => day8(input, part),
                9 => day9(input, part),
                10 => day10(input, part),
                11 => day11(input, part),
                12 => day12(input, part),
                13 => day13(input, part),
                14 => day14(input, part),
                15 => day15(input, part),
                16 => day16(input, part),
                17 => day17(input, part),
                18 => day18(input, part),
                19 => day19(input, part),
                20 => day20(input, part),
                _ => throw new ArgumentException("Wrong day number - unimplemented")
            };
            st.Stop();
            Console.WriteLine("Result : {0}", result);
            Console.WriteLine("Elapsed : {0}", st.Elapsed.TotalSeconds);
        }

        static string day1(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day01.ReportFinder finder = new();
            finder.ParseInput(lines);
            return finder.Solve(part).ToString();
        }

        static string day2(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day02.PwdChecker checker = new();
            checker.ParseInput(lines);
            return checker.Solve(part).ToString();
        }

        static string day3(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day03.SlopeSled sled = new();
            sled.ParseInput(lines);
            return sled.Solve(part).ToString();
        }

        static string day4(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day04.PassportChecker checker = new();
            checker.ParseInput(lines);
            return checker.Solve(part).ToString();
        }

        static string day5(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day05.BoardingChecker checker = new();
            checker.ParseInput(lines);
            return checker.Solve(part).ToString();
        }

        static string day6(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day06.CustomOfficer officer = new();
            officer.ParseInput(lines);
            return officer.Solve(part).ToString();
        }

        static string day7(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day07.LuggageProcessor processor = new();
            processor.ParseInput(lines);
            return processor.Solve(part).ToString();
        }

        static string day8(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day08.HandHeldProcessor processor = new();
            processor.ParseInput(lines);
            return processor.Solve(part).ToString();
        }

        static string day9(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day09.XmasEncoder enc = new();
            enc.ParseInput(lines);
            return enc.Solve(part).ToString();
        }

        static string day10(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day10.JoltConnector conn = new();
            conn.ParseInput(lines);
            return conn.Solve(part).ToString();
        }

        static string day11(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day11.SeatSolver solver = new();
            solver.ParseInput(lines);
            return solver.Solve(part).ToString();
        }

        static string day12(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day12.ShipNavigator navi = new();
            navi.ParseInput(lines);
            return navi.Solve(part).ToString();
        }

        static string day13(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day13.BusStop busStop = new();
            busStop.ParseInput(lines);
            return busStop.Solve(part).ToString();
        }

        static string day14(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day14.BitMasker masker = new();
            masker.ParseInput(lines);
            return masker.Solve(part).ToString();
        }

        static string day15(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day15.Rambunctious ramb = new();
            ramb.ParseInput(lines);
            return ramb.Solve(part).ToString();
        }

        static string day16(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day16.TicketScanner scanner = new();
            scanner.ParseInput(lines);
            return scanner.Solve(part).ToString();
        }

        static string day17(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day17.CubeSystem cubes = new();
            cubes.ParseInput(lines);
            return cubes.Solve(part).ToString();
        }

        static string day18(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day18.MathParser parser = new();
            parser.ParseInput(lines);
            return parser.Solve(part).ToString();
        }

        static string day19(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day19.MonsterRegex mRegex = new();
            mRegex.ParseInput(lines, part);
            return mRegex.Solve().ToString();
        }

        static string day20(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day20.Puzzle puzzle = new();
            puzzle.ParseInput(lines);
            return puzzle.Solve(part).ToString();
        }
    }
}
