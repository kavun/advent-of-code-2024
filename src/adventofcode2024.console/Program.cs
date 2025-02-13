﻿using System.Diagnostics;
using System.Reflection;
using adventofcode2024;
using Spectre.Console;

AnsiConsole.MarkupLine("""
    
    [green]Advent of Code[/] 2024
    
    """);

var dayTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(IDay).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);
var inputFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input"), "Day*.txt");

string GetName(string p)
{
    var dayName = Path.GetFileNameWithoutExtension(p);
    var dayType = dayTypes.FirstOrDefault(t => t.Name == dayName);
    var name = dayType?.GetProperty("Name", BindingFlags.Static | BindingFlags.Public)?.GetValue(null)?.ToString();
    return name ?? string.Empty;
}

var inputFilePath = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Which [green]day[/] input file would you like to solve?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more)[/]")
        .AddChoices(inputFiles.OrderDescending())
        .UseConverter(GetName));

var dayName = Path.GetFileNameWithoutExtension(inputFilePath);
var dayType = dayTypes.FirstOrDefault(t => t.Name == dayName);
_ = dayType ?? throw new InvalidOperationException($"No matching day found for {inputFilePath}");
var day = Activator.CreateInstance(dayType) as IDay;
_ = day ?? throw new InvalidOperationException($"Could not create instance of IDay for {inputFilePath}");

var sw = Stopwatch.StartNew();
var answer = day.Solve(File.ReadLines(inputFilePath));
sw.Stop();

var title = GetName(inputFilePath);
AnsiConsole.Write(new Panel($"""

    [grey]Part 1:[/] {answer.Part1}
    [grey]Part 2:[/] {answer.Part2}

    [grey]{sw.ElapsedMilliseconds} ms[/]
    """)
{
    Header = new PanelHeader($"[yellow]{title}[/]"),
    BorderStyle = new Style(foreground: Color.Green),
    Expand = false,
    Width = title.Length + 4,
});
