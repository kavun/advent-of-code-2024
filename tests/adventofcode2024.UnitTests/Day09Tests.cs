namespace adventofcode2024.UnitTests;

public class Day09Tests
{
    [Test]
    [Arguments("""
        2333133121414131402
        """, 1928, 0)]
    [Arguments("""
        12345
        """, 60, 0)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day09().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }
}
