
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

        part1 += stones.Sum(s => GetBlinkedCount(s, 25));
        part2 += stones.Sum(s => GetBlinkedCount(s, 75));

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }

    private readonly Dictionary<(long, int), long> _resultCountByStoneBlinksLeft = [];

    private long GetBlinkedCount(long stone, int blinkTimes)
    {
        if (blinkTimes == 0)
        {
            return 1;
        }

        blinkTimes--;
        
        if (_resultCountByStoneBlinksLeft.TryGetValue((stone, blinkTimes), out var cachedCount))
        {
            return cachedCount;
        }

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

        var count = stones.Sum(s => GetBlinkedCount(s, blinkTimes));

        _resultCountByStoneBlinksLeft.TryAdd((stone, blinkTimes), count);

        return count;
    }
}
