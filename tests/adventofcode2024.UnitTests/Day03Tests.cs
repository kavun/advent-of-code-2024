namespace adventofcode2024.UnitTests;

public class Day03Tests
{
    [Test]
    [Arguments("""
        xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
        """, 161, 161)]
    [Arguments("""
        xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
        """, 161, 48)]
    [Arguments("""
        don't()mul(1,1)
        don't()mul(1,1)
        do()mul(1,1)    mul(1,1)
        don't()mul(1,1)
        mul(1,1)don't()mul(1,1)do()mul(1,1)don't()
        mul(1,1)
        do()mul(1,1)don't()don't()do()don't()do()mul(1,1)
        """, 11, 5)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day03().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }
}
