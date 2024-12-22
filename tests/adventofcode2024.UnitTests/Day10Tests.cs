namespace adventofcode2024.UnitTests;

public class Day10Tests
{
    [Test]
    [Arguments("""
        89010123
        78121874
        87430965
        96549874
        45678903
        32019012
        01329801
        10456732
        """, 36, 81)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day10().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }
}
