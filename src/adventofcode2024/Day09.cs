namespace adventofcode2024;

public partial class Day09 : IDay
{
    public static string? Name => "Day 9: Disk Fragmenter";

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var line = lines.First().ToCharArray();
        var pos = 0;
        var files = (int)Math.Ceiling(line.Length / 2m) - 1;
        var file = 0;
        var lefting = 0;
        for (var i = 0; i < line.Length; i++)
        {
            var space = i % 2 != 0;
            if (space)
            {
                pos++;
            }
            else
            {
                var count = int.Parse(line[i].ToString());
                while (count > 0)
                {
                    part1 += pos * file;
                    count++;
                    pos++;
                }
                file++;
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
