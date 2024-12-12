namespace adventofcode2024;

public class Day02 : IDay
{
    public static string? Name => "Day 2: Red-Nosed Reports";

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        foreach (var line in lines)
        {
            //Console.WriteLine();
            //Console.WriteLine(line);

            var lineNums = line.Split(' ').Select(long.Parse).ToArray();

            bool safe = CheckLine(lineNums);

            if (!safe)
            {
                for (var i = 0; i < lineNums.Length; i++)
                {
                    var safe2 = CheckLine(lineNums.Where((_, j) => j != i).ToArray());
                    if (safe2)
                    {
                        part2 += 1;
                        break;
                    }
                }
            }
            else
            {
                part2 += 1;
                part1 += 1;
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    private static bool CheckLine(long[] lineNums)
    {

        var safe = false;
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

        //Console.WriteLine($"\t{string.Join(' ', lineNums)} {(safe ? "SAFE" : "NOTSAFE")}");

        return safe;
    }
}
