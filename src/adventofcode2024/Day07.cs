using System.Text;

namespace adventofcode2024;

public partial class Day07 : IDay
{
    public static string? Name => "Day 7: Bridge Repair";

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            var testValue = long.Parse(parts[0]);
            var operands = parts[1].Trim().Split(' ').Select(long.Parse).ToArray();
            var operatorSlots = operands.Length - 1;
            var permutations = Math.Pow(2, operatorSlots);
            var foundPart1 = false;

            for (var permutation = 0; permutation < permutations; permutation++)
            {
                var operators = Convert.ToString(permutation, 2)
                    .PadLeft(operatorSlots, '0')
                    .Select(c => c == '0' ? '+' : '*')
                    .ToList();

                var accumulatedValue = operands[0];
                for (var i = 1; i < operands.Length; i++)
                {
                    var next = operands[i];
                    accumulatedValue = operators[i - 1] switch
                    {
                        '+' => accumulatedValue + next,
                        '*' => accumulatedValue * next,
                        _ => throw new Exception("Invalid operator")
                    };

                    if (accumulatedValue > testValue)
                    {
                        break;
                    }
                }

                if (accumulatedValue == testValue)
                {
                    part1 += testValue;
                    foundPart1 = true;
                    break;
                }
            }

            if (foundPart1)
            {
                continue;
            }

            var permutationsPart2 = Math.Pow(3, operatorSlots);
            for (var permutation = 0; permutation < permutationsPart2; permutation++)
            {
                var operators = ToBase(permutation, 3)
                    .PadLeft(operatorSlots, '0')
                    .Select(c => c == '0' ? '+' : c == '1' ? '*' : '|')
                    .ToArray();

                if (!operators.Any(o => o == '|'))
                {
                    continue;
                }

                var accumulatedValue = operands[0];
                for (var i = 1; i < operands.Length; i++)
                {
                    var next = operands[i];

                    accumulatedValue = operators[i - 1] switch
                    {
                        '+' => accumulatedValue + next,
                        '*' => accumulatedValue * next,
                        '|' => long.Parse($"{accumulatedValue}{next}"),
                        _ => throw new Exception("Invalid operator")
                    };

                    if (accumulatedValue > testValue)
                    {
                        break;
                    }
                }

                if (accumulatedValue == testValue)
                {
                    part2 += testValue;
                    break;
                }
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2 + part1
        };
    }

    private static string FormatEquation(long testValue, IEnumerable<long> operands, IEnumerable<char> operators)
    {
        var found = $"{testValue} = {string.Join("", operands.Zip([.. operators, ' '], (oand, op) => $"{oand}{op}"))}".Trim(['+', '*']);
        return found;
    }

    public static string ToBase(int value, int toBase)
    {
        var result = new StringBuilder();
        do
        {
            result.Insert(0, "0123456789ABCDEF"[value % toBase]);
            value /= toBase;
        }
        while (value > 0);

        return result.ToString();
    }
}
