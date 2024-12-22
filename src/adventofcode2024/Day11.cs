
namespace adventofcode2024;

public partial class Day11 : IDay
{
    public static string? Name => "Day 11: Plutonian Pebbles";

    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var line = lines.FirstOrDefault() ?? string.Empty;
        var stones = line.Split(' ').Select(long.Parse);

        var finalStones = BlinkTimes(stones, 25);
        
        part1 += finalStones.Count;

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    public ICollection<long> BlinkTimes(IEnumerable<long> stones, int blinkTimes)
    {
        return stones
            .SelectMany(s => BlinkStone(s, blinkTimes))
            .ToArray();
    }

    private IEnumerable<long> BlinkStone(long stone, int blinkTimes)
    {
        if (blinkTimes == 0)
        {
            return [stone];
        }

        blinkTimes--;

        var stones = new List<long>();

        if (stone == 0)
        {
            stones.Add(1);
        }
        else if (stone.ToString() is string stoneStr && stoneStr.Length % 2 == 0)
        {
            var mid = stoneStr.Length / 2;
            var ls = stoneStr.AsSpan(0, mid);
            var rs = stoneStr.AsSpan(mid, mid);
            var l = long.Parse(ls);
            var r = long.Parse(rs);
            stones.Add(l);
            stones.Add(r);
        }
        else
        {
            stones.Add(stone * 2024);
        }

        return stones.SelectMany(s => BlinkStone(s, blinkTimes));
    }
}
