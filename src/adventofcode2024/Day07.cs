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
            var operands = parts[1].Trim().Split(' ').Select(long.Parse).ToArray();
            var operatorCount = operands.Length - 1;
            var permutations = Math.Pow(2, operatorCount);
            for (var permutation = 0; permutation < permutations; permutation++)
            {
                var operators = Convert.ToString(permutation, 2)
                    .PadLeft(operatorCount, '0')
                    .Select(c => c == '0' ? '+' : '*')
                    .ToArray();

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
                    break;
                }
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
