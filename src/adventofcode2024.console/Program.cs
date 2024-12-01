using adventofcode2024;

var dayTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(IDay).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

var inputFiles = Directory.GetFiles("input", "*.txt");
foreach (var inputFilePath in inputFiles)
{
    var dayName = Path.GetFileNameWithoutExtension(inputFilePath);
    var dayType = dayTypes.FirstOrDefault(t => t.Name == dayName);
    _ = dayType ?? throw new InvalidOperationException($"No matching day found for {inputFilePath}");

    var day = Activator.CreateInstance(dayType) as IDay;
    _ = day ?? throw new InvalidOperationException("Could not create instance of day");

    Console.WriteLine(dayName);
    var input = File.ReadAllText(inputFilePath);
    var output1 = day.Part1(input);
    Console.WriteLine($"\tPart 1: {output1}");
    var output2 = day.Part2(input);
    Console.WriteLine($"\tPart 2: {output2}");
}
