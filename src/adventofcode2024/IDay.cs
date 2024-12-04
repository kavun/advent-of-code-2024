namespace adventofcode2024;

public interface IDay
{
    Answer Solve(IEnumerable<string> lines);
}

public class Answer
{
    public long Part1 { get; set; }
    public long Part2 { get; set; }
}
