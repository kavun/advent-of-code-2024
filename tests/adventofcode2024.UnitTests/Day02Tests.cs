namespace adventofcode2024.UnitTests;

public class Day02Tests
{
    [Test]
    [Arguments("""
        7 6 4 2 1
        1 2 7 8 9
        9 7 6 2 1
        1 3 2 4 5
        8 6 4 4 1
        1 3 6 7 9
        """, 2, 4)]
    [Arguments("""
        41 41 39 37 36 33 31 29
        1 2 3 2 4 5 6
        1 0 1 2 3 4 5
        1 2 3 4 3 5 6
        """, 0, 4)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day02().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }
}
