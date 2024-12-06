using System.Text.RegularExpressions;

namespace adventofcode2024;

public class Day03 : IDay
{
    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        part1 = lines.Sum(line =>
            Regex.Matches(line, @"mul\(([0-9]+),([0-9]+)\)")
                .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value)));

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
