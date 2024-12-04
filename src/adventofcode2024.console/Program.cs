using adventofcode2024;
using Spectre.Console;

AnsiConsole.MarkupLine("""
    
    [green]Advent of Code[/] 2024
    
    """);

var dayTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(IDay).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

var inputFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input"), "Day*.txt");

var inputFilePath = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Which [green]day[/] input file would you like to solve?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more)[/]")
        .AddChoices(inputFiles.OrderDescending())
        .UseConverter(Path.GetFileNameWithoutExtension));

var dayName = Path.GetFileNameWithoutExtension(inputFilePath);
var dayType = dayTypes.FirstOrDefault(t => t.Name == dayName);
_ = dayType ?? throw new InvalidOperationException($"No matching day found for {inputFilePath}");

var day = Activator.CreateInstance(dayType) as IDay;
_ = day ?? throw new InvalidOperationException($"Could not create instance of IDay for {inputFilePath}");

var answer = day.Solve(File.ReadLines(inputFilePath));

AnsiConsole.Write(new Panel($"""
    [yellow]Part 1[/]: {answer.Part1}
    [yellow]Part 2[/]: {answer.Part2}
    """)
{
    Header = new PanelHeader($"[red]{dayName}[/]")
});
