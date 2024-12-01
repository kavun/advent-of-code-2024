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
        """, 11)]
    public async Task Part1(string input, long expectedAnswer)
    {
        var actualAnswer = new Day1().Part1(input);

        await Assert.That(actualAnswer).IsEqualTo(expectedAnswer);
    }
}
