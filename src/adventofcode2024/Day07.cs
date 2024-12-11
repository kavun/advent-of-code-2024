using System.Text;

namespace adventofcode2024;

public partial class Day07 : IDay
{
    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            var testValue = long.Parse(parts[0]);

            var foundStuff = new HashSet<string>();

            var operands = parts[1].Trim().Split(' ').Select(long.Parse).ToArray();
            var operatorCount = operands.Length - 1;
            var permutations = Math.Pow(2, operatorCount);

            //Console.WriteLine();
            //Console.WriteLine("Part 1");
            //Console.WriteLine();

            for (var permutation = 0; permutation < permutations; permutation++)
            {
                var operators = Convert.ToString(permutation, 2)
                    .PadLeft(operatorCount, '0')
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
                }

                if (accumulatedValue == testValue)
                {
                    part1 += testValue;
                    foundStuff.Add(FormatEquation(testValue, operands, operators));
                    //Console.WriteLine(FormatEquation(testValue, operands, operators));
                    break;
                }
            }

            //Console.WriteLine();
            //Console.WriteLine("Part 2");
            //Console.WriteLine();

            //var permutationsPart2 = Math.Pow(3, operatorCount);
            //var found = false;
            //for (var permutation = 0; permutation < permutationsPart2; permutation++)
            //{
            //    var operators = ToBase(permutation, 3)
            //        .PadLeft(operatorCount, '0')
            //        .Select(c => c == '0' ? '+' : c == '1' ? '*' : '|')
            //        .ToArray();

            //    var combinedOperands = new List<long>();
            //    for (var i = 0; i < operands.Length; i++)
            //    {
            //        if (i < operators.Length && operators[i] == '|')
            //        {
            //            combinedOperands.Add(long.Parse($"{operands[i]}{operands[i + 1]}"));
            //            i++;
            //        }
            //        else
            //        {
            //            combinedOperands.Add(operands[i]);
            //        }
            //    }

            //    var operatorCount2 = combinedOperands.Count - 1;
            //    var permutations2 = Math.Pow(2, operatorCount2);
            //    for (var permutation2 = 0; permutation2 < permutations2; permutation2++)
            //    {
            //        var operators2 = Convert.ToString(permutation2, 2)
            //            .PadLeft(operatorCount2, '0')
            //            .Select(c => c == '0' ? '+' : '*')
            //            .ToArray();

            //        var accumulatedValue = combinedOperands[0];
            //        for (var i = 1; i < combinedOperands.Count; i++)
            //        {
            //            var next = combinedOperands[i];
            //            accumulatedValue = operators2[i - 1] switch
            //            {
            //                '+' => accumulatedValue + next,
            //                '*' => accumulatedValue * next,
            //                _ => throw new Exception("Invalid operator")
            //            };
            //        }

            //        if (accumulatedValue == testValue)
            //        {
            //            var foundEquation = FormatEquation(accumulatedValue, combinedOperands, operators2);
            //            if (!foundStuff.Contains(foundEquation))
            //            {
            //                foundStuff.Add(foundEquation);
            //                //Console.WriteLine(foundEquation);
            //                part2 += testValue;
            //                found = true;
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            //Console.WriteLine("NO: " + FormatEquation(accumulatedValue, combinedOperands, operators2));
            //        }
            //    }

            //    if (found)
            //    {
            //        break;
            //    }
            //}
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
