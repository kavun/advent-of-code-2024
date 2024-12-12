using System.Text.RegularExpressions;

namespace adventofcode2024;

public partial class Day03 : IDay
{
    public static string? Name => "Day 3: Mull It Over";

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        part1 = lines.Sum(line => MulRegex().Matches(line).Sum(MultMatch));

        var on = true;
        var dontParts = string.Join("", lines).Split("don't");
        foreach (var dontPart in dontParts)
        {
            var doParts = dontPart.Split("do()");
            foreach (var doPart in doParts)
            {
                part2 += on ? MulRegex().Matches(doPart).Sum(MultMatch) : 0;
                if (doParts.Length > 1)
                {
                    on = true;
                }
            }

            if (dontParts.Length > 1)
            {
                on = false;
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    private long MultMatch(Match m) => long.Parse(m.Groups[1].Value) * long.Parse(m.Groups[2].Value);

    [GeneratedRegex(@"mul\(([0-9]+),([0-9]+)\)")]
    private static partial Regex MulRegex();
}
