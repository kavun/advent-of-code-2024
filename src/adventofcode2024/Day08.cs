namespace adventofcode2024;

public partial class Day08 : IDay
{

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
                    var diff = new XY(antA.X - antB.X, antA.Y - antB.Y);
                    var l = new XY(antA.X + diff.X, antA.Y + diff.Y);
                    var r = new XY(antB.X + -diff.X, antB.Y + -diff.Y);
                    List<XY> options = [l, r];
                    foreach (var option in options)
                    {
                        if (IsInBounds(grid, option) && !antiAntennas.Contains(option))
                        {
                            antiAntennas.Add(option);
                            part1++;
                        }
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

    public static bool IsInBounds(char[][] grid, XY xy) =>
        xy.X >= 0 && xy.X < grid[0].Length && xy.Y >= 0 && xy.Y < grid.Length;
    
    public record struct XY(int X, int Y)
    {
        public override readonly string ToString() => $"({X}, {Y})";
    }
}
