namespace adventofcode2024;

public partial class Day09 : IDay
{
    public static string? Name => "Day 9: Disk Fragmenter";

    public Answer Solve(IEnumerable<string> lines)
    {
        // 12345
        //
        // 0..111....22222
        //
        // 0..111....22222
        // 02.111....2222.
        // 022111....222..
        // 0221112...22...
        // 02211122..2....
        // 022111222......


        // 00...111...2...333.44.5555.6666.777.888899
        // 009..111...2...333.44.5555.6666.777.88889.
        // 0099.111...2...333.44.5555.6666.777.8888..
        // 00998111...2...333.44.5555.6666.777.888...
        // 009981118..2...333.44.5555.6666.777.88....
        // 0099811188.2...333.44.5555.6666.777.8.....
        // 009981118882...333.44.5555.6666.777.......
        // 0099811188827..333.44.5555.6666.77........
        // 00998111888277.333.44.5555.6666.7.........
        // 009981118882777333.44.5555.6666...........
        // 009981118882777333644.5555.666............
        // 00998111888277733364465555.66.............
        // 0099811188827773336446555566..............

        var part1 = 0L;
        var part2 = 0L;
        var line = lines.First().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        var fileR = (int)Math.Ceiling(line.Length / 2m) - 1;
        Console.WriteLine($"Files: {fileR}");
        var fileL = 0;

        //var state = "";
        //void AddState(string s)
        //{
        //    state += s;
        //    Console.WriteLine($"State: {state}");
        //}

        var disk = new List<int?>();

        for (var i = 0; i < line.Length; i++)
        {
            var space = i % 2 != 0;
            var count = line[i];
            while (count > 0)
            {
                disk.Add(space ? null : fileL);
                count--;
            }
            if (!space)
            {
                fileL++;
            }
        }

        var diskIdxL = 0;
        var diskIdxR = disk.Count - 1;

        foreach (var val in disk)
        {
            if (val == null)
            {
                var next = disk[diskIdxR];
                while (next == null)
                {
                    diskIdxR--;
                    next = disk[diskIdxR];
                }

                if (diskIdxL >= diskIdxR)
                {
                    //Console.WriteLine($"file break: {diskIdxL} >= {diskIdxR}");
                    break;
                }

                //AddState(next.Value.ToString());

                part1 += next.Value * diskIdxL;

                diskIdxR--;
            }
            else
            {
                if (diskIdxL > diskIdxR)
                {
                    //Console.WriteLine($"file break: {diskIdxL} >= {diskIdxR}");
                    break;
                }

                //AddState(val.Value.ToString());

                part1 += val.Value * diskIdxL;
            }

            diskIdxL++;
        }

        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}

public partial class Day09_BadAttempt
{
    public static string? Name => "Day 9: Disk Fragmenter";

    public Answer Solve(IEnumerable<string> lines)
    {
        // 12345
        //
        // 0..111....22222
        //
        // 0..111....22222
        // 02.111....2222.
        // 022111....222..
        // 0221112...22...
        // 02211122..2....
        // 022111222......


        // 00...111...2...333.44.5555.6666.777.888899
        // 009..111...2...333.44.5555.6666.777.88889.
        // 0099.111...2...333.44.5555.6666.777.8888..
        // 00998111...2...333.44.5555.6666.777.888...
        // 009981118..2...333.44.5555.6666.777.88....
        // 0099811188.2...333.44.5555.6666.777.8.....
        // 009981118882...333.44.5555.6666.777.......
        // 0099811188827..333.44.5555.6666.77........
        // 00998111888277.333.44.5555.6666.7.........
        // 009981118882777333.44.5555.6666...........
        // 009981118882777333644.5555.666............
        // 00998111888277733364465555.66.............
        // 0099811188827773336446555566..............

        var part1 = 0L;
        var part2 = 0L;
        var line = lines.First().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        var pos = 0;
        var fileR = (int)Math.Ceiling(line.Length / 2m) - 1;
        Console.WriteLine($"Files: {fileR}");
        var fileL = 0;

        var state = "";
        void AddState(string s)
        {
            state += s;
            Console.WriteLine($"State: {state}");
        }

        var rightCount = 0;

        var currCountL = 0;
        var currCountR = 0;

        for (var i = 0; i < line.Length; i++)
        {
            var space = i % 2 != 0;
            if (space)
            {
                currCountR = line[i];
                while (currCountR > 0)
                {
                    if (rightCount == 0)
                    {
                        rightCount = line[fileR * 2];
                    }

                    AddState(fileR.ToString());

                    currCountR--;
                    rightCount--;
                    pos++;

                    part1 += pos * fileR;

                    if (rightCount == 0)
                    {
                        fileR--;
                    }

                    //if (fileR < fileL)
                    //{
                    //    break;
                    //}
                    Console.WriteLine($"space: {space}; currCountL: {currCountL}; currCountR: {currCountR}; rightCount: {rightCount}; fileL: {fileL}; fileR: {fileR}; pos: {pos}; part1: {part1}");

                    //if (fileL == fileR && rightCount == currCountL)
                    //{
                    //    break;
                    //}
                }
            }
            else
            {
                currCountL = line[i];

                //if (fileL == fileR && rightCount == currCountL - 1)
                //{
                //    break;
                //}

                while (currCountL > 0)
                {
                    AddState(fileL.ToString());
                    part1 += pos * fileL;
                    currCountL--;
                    pos++;

                    Console.WriteLine($"space: {space}; currCountL: {currCountL}; currCountR: {currCountR}; rightCount: {rightCount}; fileL: {fileL}; fileR: {fileR}; pos: {pos}; part1: {part1}");

                    //if (fileL == fileR && rightCount == currCountL)
                    //{
                    //    break;
                    //}
                }

                //if (fileL == fileR && rightCount == currCountL)
                //{
                //    break;
                //}

                fileL++;

                //if (fileR < fileL)
                //{
                //    break;
                //}

                //if (fileL == fileR && rightCount == currCountL)
                //{
                //    break;
                //}
            }

            //if (fileL == fileR && rightCount == currCountL)
            //{
            //    break;
            //}
        }

        Console.WriteLine($"----");
        Console.WriteLine($"----");
        Console.WriteLine($"----");
        Console.WriteLine($"----");
        Console.WriteLine($"----");
        Console.WriteLine($"----");
        Console.WriteLine($"----");
        Console.WriteLine($"----");


        return new Answer()
        {
            Part1 = part1,
            Part2 = part2
        };
    }
}
