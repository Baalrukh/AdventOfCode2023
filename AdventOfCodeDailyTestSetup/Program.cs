using System;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using AdventOfCode2023;
using TextCopy;

namespace AdventOfCodeDailyTestSetup;

internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        string solutionRoot = args[0];
        if (string.IsNullOrEmpty(solutionRoot))
        {
            Console.WriteLine("You need to add solution root as argument");
            return;
        }

        var referenceType = typeof(Day01);
        int day = FindLastDay(referenceType) + 1;

        string? clipboard = new Clipboard().GetText();
        if (string.IsNullOrEmpty(clipboard))
        {
            Console.WriteLine("You need to copy the puzzle input into the clipboard");
            return;
        }

        File.WriteAllText(Path.Combine(solutionRoot, $"AdventOfCode2023/Input/day{day:00}.txt"), clipboard);

        CopyTemplate("ExerciseTemplate.txt", day, Path.Combine(solutionRoot, $"AdventOfCode2023/Day{day:00}.cs"));
        CopyTemplate("TestTemplate.txt", day,
            Path.Combine(solutionRoot, $"AdventOfCode2023.Test/Day{day:00}Tests.cs"));

        UpdateCsprojFile(solutionRoot, day);
    }

    private static void CopyTemplate(string templateFile, int day, string exerciseClassPath)
    {
        string[] exerciseLines = File.ReadAllLines(templateFile)
            .Select(x => x.Replace("{day}", day.ToString("00")))
            .ToArray();
        File.WriteAllLines(exerciseClassPath, exerciseLines);
    }

    private static void UpdateCsprojFile(string solutionRoot, int day)
    {
        string csprojPath = Path.Combine(solutionRoot, "AdventOfCode2023/AdventOfCode2023.csproj");
        var lines = File.ReadAllLines(csprojPath).ToList();
        int findLastIndex = lines.FindLastIndex(x => x.Contains("Include=\"Input\\day"));
        int index = findLastIndex + 3;
        lines.Insert(index, "      </Content>");
        lines.Insert(index, "        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>");
        lines.Insert(index, $"      <Content Include=\"Input\\day{day:00}.txt\">");

        File.WriteAllLines(csprojPath, lines);
    }

    private static int FindLastDay(Type referenceType)
    {
        return Enumerable.Range(1, 24).Last(i =>
            referenceType.Assembly.GetType($"{referenceType.Namespace}.Day{i:00}") != null);
    }
}