namespace adventofcode2024;

public partial class Day08 : IDay
{
    public static string? Name => "Day 8: Resonant Collinearity";

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var antennas = new Dictionary<char, List<XY>>();
        var antiAntennas = new HashSet<XY>();
        var grid = lines.Select((row, y) => row.ToCharArray().Select((c, x) =>
        {
            if (c != '.')
            {
                antennas.TryAdd(c, []);
                antennas[c].Add(new XY(x, y));
            }

            return c;
        }).ToArray()).ToArray();

        foreach (var (freq, freqAntennas) in antennas)
        {
            foreach (var antA in freqAntennas)
            {
                foreach (var antB in freqAntennas.Where(a => a != antA))
                {
                    if (antiAntennas.Add(antA))
                    {
                        part2++;
                    }

                    if (antiAntennas.Add(antB))
                    {
                        part2++;
                    }

                    var diff1 = new XY(antA.X - antB.X, antA.Y - antB.Y);
                    var diff2 = new XY(-(antA.X - antB.X), -(antA.Y - antB.Y));
                    var option1 = new XY(antA.X + diff1.X, antA.Y + diff1.Y);
                    var option2 = new XY(antB.X + diff2.X, antB.Y + diff2.Y);
                    List<(XY, XY)> options = [(option1, diff1), (option2, diff2)];
                    foreach (var (option, diff) in options)
                    {
                        if (IsInBounds(grid, option))
                        {
                            if (antiAntennas.Add(option))
                            {
                                part1++;
                            }

                            var next = option;
                            var nextInBounds = true;
                            while (nextInBounds)
                            {
                                next = new XY(next.X + diff.X, next.Y + diff.Y);
                                nextInBounds = IsInBounds(grid, next);
                                if (nextInBounds && antiAntennas.Add(next))
                                {
                                    part2++;
                                }
                            }
                        }
                    }
                }
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part1 + part2
        };
    }

    public static bool IsInBounds(char[][] grid, XY xy) =>
        xy.X >= 0 && xy.X < grid[0].Length && xy.Y >= 0 && xy.Y < grid.Length;

    public record struct XY(int X, int Y)
    {
        public override readonly string ToString() => $"({X}, {Y})";
    }
}
