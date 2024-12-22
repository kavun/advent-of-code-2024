namespace adventofcode2024.UnitTests;

public class Day11Tests
{
    [Test]
    [Arguments("""
        125 17
        """, 55312, 65601038650482)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day11().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }
}
