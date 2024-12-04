namespace adventofcode2024.UnitTests;

public class Day1Tests
{
    [Test]
    [Arguments("""
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """, 11, 0)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day01().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }
}
