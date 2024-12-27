
using System.IO;

namespace adventofcode2024;

public partial class Day13 : IDay
{
    public static string? Name => "Day 13: Claw Contraption";

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        foreach (var machine in SelectMachines(lines))
        {
            part1 += GetMinimumTokens(machine);
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    private class Machine
    {
        public int AX { get; set; }
        public int AY { get; set; }
        public int BX { get; set; }
        public int BY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString()
        {
            return $"A: ({AX},{AY}) B: ({BX},{BY}) Prize: ({X},{Y})";
        }
    }

    private IEnumerable<Machine> SelectMachines(IEnumerable<string> lines)
    {
        var machine = new Machine();
        List<string> split = ["A:", "B:", "Prize:"];
        var parsing = 3;
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                machine = new Machine();
                continue;
            }

            var idx = parsing % split.Count;
            var half = line.Split(split[idx])[1];
            var strings = half.Split(",");

            if (idx == 2)
            {
                var n = strings.Select(h => int.Parse(h.Split("=")[1])).ToArray();
                machine.X = n[0];
                machine.Y = n[1];
            }
            else
            {
                var n = strings.Select(h => int.Parse(h.Split("+")[1])).ToArray();
                if (idx == 0)
                {
                    machine.AX = n[0];
                    machine.AY = n[1];
                }
                else
                {
                    machine.BX = n[0];
                    machine.BY = n[1];
                }
            }

            parsing++;

            var nextIdx = parsing % split.Count;
            if (nextIdx == 0)
            {
                yield return machine;
            }
        }
    }

    private long GetMinimumTokens(Machine machine)
    {
        var x = machine.X;
        var y = machine.Y;
        var ax = machine.AX;
        var ay = machine.AY;
        var bx = machine.BX;
        var by = machine.BY;

        if (x % bx == 0 && y % by == 0 && x / bx == y / by)
        {
            return x / bx;
        }

        if (x % ax == 0 && y % ay == 0 && x / ax == y / ay)
        {
            return x / ax * 3;
        }

        Console.WriteLine(machine);
        return 0;
    }
}
