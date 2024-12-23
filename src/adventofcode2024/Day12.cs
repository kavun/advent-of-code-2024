namespace adventofcode2024;

public partial class Day12 : IDay
{
    public static string? Name => "Day 12: Garden Groups";

    private record struct XY(int X, int Y)
    {
        public override readonly string ToString() => $"({X},{Y})";
    }

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var garden = lines.Select(l => l.ToCharArray()).ToArray();
        var width = garden[0].Length;
        var height = garden.Length;
        var start = new XY(0, 0);
        var visited = new HashSet<XY>();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var plot = new XY(x, y);
                if (visited.Contains(plot))
                {
                    continue;
                }

                var region = GetAdjacentPlots(garden, visited, plot);

                part1 += region.Length * region.Sum(a => a.exposedPerimeter);
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    private (XY plot, int exposedPerimeter)[] GetAdjacentPlots(char[][] garden, HashSet<XY> visited, XY plot)
    {
        visited.Add(plot);
        var y = plot.Y;
        var x = plot.X;
        var crop = garden[y][x];
        var adjacentPlots = (new XY[4]
            {
                new(x - 1, y),
                new(x + 1, y),
                new(x, y - 1),
                new(x, y + 1)
            })
            .Where(adj => InBounds(garden, adj))
            .Where(adj => crop == garden[adj.Y][adj.X]);
        
        var adjacentCount = adjacentPlots.Count();
        var unvisitedAdjPlots = adjacentPlots.Where(visited.Add).ToArray();

        return [
            (plot, 4 - adjacentCount),
            ..unvisitedAdjPlots.SelectMany(adj => GetAdjacentPlots(garden, visited, adj))
        ];
    }

    private bool InBounds(char[][] garden, XY xy) =>
        xy.X >= 0 && xy.X < garden[0].Length && xy.Y >= 0 && xy.Y < garden.Length;
}
