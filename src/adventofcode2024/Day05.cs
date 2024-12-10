namespace adventofcode2024;

public partial class Day05 : IDay
{
    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var section1 = true;
        var mustBeBefores = new Dictionary<int, HashSet<int>>();
        var validMids = new List<int>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                section1 = false;
                continue;
            }

            if (section1)
            {
                var parts = line.Split("|");
                var a = int.Parse(parts[0]);
                var b = int.Parse(parts[1]);
                mustBeBefores.TryAdd(a, []);
                mustBeBefores[a].Add(b);
            }
            else
            {
                var parts = line
                    .Split(",")
                    .Select(int.Parse)
                    .ToArray();

                var leftParts = new HashSet<int>();
                var partValid = true;
                var part2Arr = new List<int>();

                for (var i = 0; i < parts.Length; i++)
                {
                    var num = parts[i];
                    var mustBeBefore = mustBeBefores.GetValueOrDefault(num, []);
                    var numValid = mustBeBefore.All(m => !leftParts.Contains(m));

                    if (!numValid)
                    {
                        partValid = false;

                        var firstIndex = mustBeBefore
                            .Where(leftParts.Contains)
                            .Select(m => part2Arr.IndexOf(m))
                            .Order()
                            .FirstOrDefault(-1);
                        part2Arr.Insert(firstIndex == -1 ? i : firstIndex, num);
                    }
                    else
                    {
                        part2Arr.Add(num);
                    }

                    leftParts.Add(num);
                }

                if (partValid)
                {
                    part1 += parts[(int)Math.Floor(parts.Length / 2m)];
                }
                else
                {
                    part2 += part2Arr[(int)Math.Floor(part2Arr.Count / 2m)];
                }
            }
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
