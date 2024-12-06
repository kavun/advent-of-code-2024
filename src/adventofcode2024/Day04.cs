namespace adventofcode2024;

public partial class Day04 : IDay
{
    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var allXs = new List<(int x, int y)>();

        var grid = lines.Select((l, x) =>
            l.Select((ltr, y) =>
            {
                var code = ltr switch
                {
                    'X' => 1,
                    'M' => 2,
                    'A' => 3,
                    'S' => 4,
                    _ => 0
                };

                if (code == 1)
                {
                    allXs.Add((x, y));
                }

                return code;
            }).ToArray()).ToArray();

        foreach (var x in allXs)
        {
            var adjMs = GetAdj(x, grid, 2);
            foreach (var m in adjMs)
            {
                var adjAs = GetAdj(m.Item1, grid, 3, m.Item2);
                foreach (var a in adjAs)
                {
                    var adjSs = GetAdj(a.Item1, grid, 4, a.Item2);
                    if (adjSs.Any())
                    {
                        part1++;
                    }
                }
            }

        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    private IEnumerable<((int x, int y), (int dx, int dy))> GetAdj((int x, int y) point, int[][] xy, int code, (int dx, int dy)? dir = null)
    {
        if (dir != null)
        {
            int nextX = point.x + dir.Value.dx;
            int nextY = point.y + dir.Value.dy;
            if (nextX < 0 || nextX > xy[point.x].Length - 1 || nextY < 0 || nextY > xy.Length - 1)
            {
                yield break;
            }

            if (xy[nextX][nextY] == code)
            {
                yield return ((nextX, nextY), dir.Value);
            }
            yield break;
        }

        var allDirs = new List<(int dx, int dy)>
        {
            (0, 1),
            (0, -1),
            (1, 0),
            (-1, 0),
            (1, 1),
            (1, -1),
            (-1, 1),
            (-1, -1)
        };

        foreach (var nextDir in allDirs)
        {
            int nextX = point.x + nextDir.dx;
            int nextY = point.y + nextDir.dy;
            if (nextX < 0 || nextX > xy[point.x].Length - 1 || nextY < 0 || nextY > xy.Length - 1)
            {
                continue;
            }

            if (xy[nextX][nextY] == code)
            {
                yield return ((nextX, nextY), nextDir);
            }
        }

        yield break;
    }
}
