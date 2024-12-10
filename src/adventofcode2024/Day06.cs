namespace adventofcode2024;

public partial class Day06 : IDay
{
    public record struct XY(int X, int Y);
    public static class Directions
    {
        public static XY Up => new(0, -1);
        public static XY Down => new(0, 1);
        public static XY Left => new(-1, 0);
        public static XY Right => new(1, 0);
        public static XY Turn(XY xy) => xy switch
        {
            { X: 0, Y: -1 } => Right,
            { X: 1, Y: 0 } => Down,
            { X: 0, Y: 1 } => Left,
            { X: -1, Y: 0 } => Up,
            _ => throw new Exception("Invalid direction")
        };
    }

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var guard = new XY(0, 0);
        var dir = Directions.Up;
        var grid = lines.Select((l, y) => l.ToCharArray().Select((c, x) =>
        {
            if (c == '^')
            {
                guard = new XY(x, y);
            }
            return c;
        }).ToArray()).ToArray();

        var patrol = new HashSet<XY>([guard]);
        var leftTheArea = false;
        while (!leftTheArea)
        {
            var next = new XY(guard.X + dir.X, guard.Y + dir.Y);
            leftTheArea = next.X < 0 || next.Y < 0 || next.X >= grid[0].Length || next.Y >= grid.Length;
            if (leftTheArea)
            {
                break;
            }

            var collision = grid[next.Y][next.X] == '#';
            if (!collision)
            {
                patrol.Add(next);
                guard = next;
            }
            else
            {
                dir = Directions.Turn(dir);
            }
        }

        part1 += patrol.Count;

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
