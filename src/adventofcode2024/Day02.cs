namespace adventofcode2024;

public class Day02 : IDay
{
    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;

        foreach (var line in lines)
        {
            var safe = false;
            var lineNums = line.Split(' ').Select(long.Parse).ToArray();
            string? prevDir = null;
            for (var i = 0; i < lineNums.Length - 1; i++)
            {
                var a = lineNums[i];
                var b = lineNums[i + 1];

                if (a == b)
                {
                    safe = false;
                    break;
                }

                var currDir = a > b ? "DESC" : "ASC";
                prevDir ??= currDir;
                var unsafeDir = prevDir != currDir;
                var unsafeDiff = Math.Abs(a - b) > 3;
                if (unsafeDir || unsafeDiff)
                {
                    safe = false;
                    break;
                }

                safe = true;
            }

            part1 += safe ? 1 : 0;
        }

        var part2 = 0L;

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
