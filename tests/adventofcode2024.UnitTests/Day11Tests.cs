namespace adventofcode2024.UnitTests;

public class Day11Tests
{
    [Test]
    [Arguments("""
        125 17
        """, 55312, 0)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day11().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }

    [Test]
    [Arguments("""
        125 17
        """, 6, 22)]
    public async Task Part1(string input, int blinkTimes, long expectedCount)
    {
        var stones = new Day11().BlinkTimes(input.Split(" ").Select(long.Parse).ToArray(), blinkTimes);
        await Assert.That(stones.Count).IsEqualTo((int)expectedCount);
    }
}
