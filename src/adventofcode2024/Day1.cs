namespace adventofcode2024;

public class Day1 : IDay
{
    public long Part1(string input)
    {
        var pairs = input
            .Split(Environment.NewLine)
            .Select(l => l
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(e => int.Parse(e)));

        var col1 = pairs.Select(p => p.First()).Order().ToArray();
        var col2 = pairs.Select(p => p.Last()).Order().ToArray();

        var total = 0;
        for (var i = 0; i < col1.Length; i++)
        {
            var item1 = col1[i];
            var item2 = col2[i];

            total += Math.Abs(item2 - item1);
        }

        return total;
    }

    public long Part2(string input)
    {
        return 0;
    }
}
