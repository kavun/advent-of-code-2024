namespace adventofcode2024;

public class Day01 : IDay
{
    public Answer Solve(IEnumerable<string> lines)
    {
        var pairs = lines
            .Select(l => l
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(e => int.Parse(e)));

        var col1 = pairs.Select(p => p.First()).Order().ToArray();
        var col2 = pairs.Select(p => p.Last()).Order().ToArray();
        var rightCounts = new Dictionary<int, int>();

        var part1 = 0L;
        for (var i = 0; i < col1.Length; i++)
        {
            var item1 = col1[i];
            var item2 = col2[i];

            if (!rightCounts.ContainsKey(item2))
            {
                rightCounts[item2] = 0;
            }

            rightCounts[item2]++;

            part1 += Math.Abs(item2 - item1);
        }

        var part2 = 0L;
        for (var i = 0; i < col1.Length; i++)
        {
            var item1 = col1[i];

            part2 += item1 * (rightCounts.TryGetValue(item1, out var item1Val) ? item1Val : 0);
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
