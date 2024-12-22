namespace adventofcode2024;

public partial class Day10 : IDay
{
    public static string? Name => "Day 10: Hoof It";

    public record struct XY(int X, int Y)
    {
        public override readonly string ToString() => $"({X},{Y})";
    }

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var trailHeads = new List<XY>();
        var topography = lines.Select((line, y) =>
        {
            return line.Select((c, x) =>
            {
                if (c == '0')
                {
                    trailHeads.Add(new XY(x, y));
                }

                return int.Parse(c.ToString());
            }).ToArray();
        }).ToArray();

        part1 += trailHeads.Sum(head => GetAdjascentInclines(topography, head).Distinct().Count());
        part2 += trailHeads.Sum(head => GetAdjascentInclines(topography, head).Length);

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    private XY[] GetAdjascentInclines(int[][] topography, XY head)
    {
        var y = head.Y;
        var x = head.X;

        var currentSpot = topography[y][x];
        if (currentSpot == 9)
        {
            return [head];
        }

        var incline = currentSpot + 1;

        var options = (new XY[4]
            {
                new(x - 1, y),
                new(x + 1, y),
                new(x, y - 1),
                new(x, y + 1)
            })
            .Where(adj => InBounds(topography, adj))
            .Where(adj => incline == topography[adj.Y][adj.X])
            .SelectMany(opt => GetAdjascentInclines(topography, opt))
            .ToArray();

        return options;
    }

    public static bool InBounds(int[][] grid, XY xy) =>
        xy.X >= 0 && xy.X < grid[0].Length && xy.Y >= 0 && xy.Y < grid.Length;
}
