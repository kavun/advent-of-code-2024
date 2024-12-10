namespace adventofcode2024;

public partial class Day05 : IDay
{
    public Answer Solve(IEnumerable<string> lines)
    {
        var part1 = 0L;
        var part2 = 0L;

        var section1 = true;
        var mustBeBefores = new Dictionary<int, HashSet<int>>();
        var cannotBeBefores = new Dictionary<int, HashSet<int>>();
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
                mustBeBefores.TryAdd(a, new HashSet<int>());
                mustBeBefores[a].Add(b);
                cannotBeBefores.TryAdd(b, new HashSet<int>());
                cannotBeBefores[b].Add(a);
            }
            else
            {
                var parts = line
                    .Split(",")
                    .Select(int.Parse)
                    .ToArray();

                var leftParts = new HashSet<int>();
                var rightParts = new HashSet<int>(parts.Skip(1));
                var partValid = true;

                for (var i = 0; i < parts.Length; i++)
                {
                    var num = parts[i];
                    var mustBeBefore = mustBeBefores.GetValueOrDefault(num, new HashSet<int>());
                    var cannotBeBefore = cannotBeBefores.GetValueOrDefault(num, new HashSet<int>());

                    var numValid = mustBeBefore.All(m => !leftParts.Contains(m))
                        && cannotBeBefore.All(m => !rightParts.Contains(m));
                    if (!numValid)
                    {
                        partValid = false;
                        break;
                    }

                    leftParts.Add(num);
                    rightParts = new HashSet<int>(parts.Skip(i + 1));
                }

                if (partValid)
                {
                    part1 += parts[(int)Math.Floor(parts.Length / 2m)];
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
