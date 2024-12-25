namespace adventofcode2024.UnitTests;

public class Day12Tests
{
    [Test]
    [Arguments("""
        R
        """, 4, 4)]
    [Arguments("""
        RR
        """, 12, 8)]
    [Arguments("""
        RR
        RR
        """, 32, 16)]
    [Arguments("""
        RR
        RA
        """, 24 + 4, 18 + 4)]
    [Arguments("""
        RRR
        RAR
        RRR
        """, 128 + 4, 64 + 4)]
    [Arguments("""
        RRRRIICCFF
        RRRRIICCCF
        VVRRRCCFFF
        VVRCCCJFFF
        VVVVCJJCFE
        VVIVCCJJEE
        VVIIICJJEE
        MIIIIIJJEE
        MIIISIJEEE
        MMMISSJEEE
        """, 1930, 1206)]
    public async Task Solve(string input, long expectedPart1, long expectedPart2)
    {
        var actualAnswer = new Day12().Solve(input.Split(Environment.NewLine));

        await Assert.That(actualAnswer.Part1).IsEqualTo(expectedPart1);
        await Assert.That(actualAnswer.Part2).IsEqualTo(expectedPart2);
    }
}
