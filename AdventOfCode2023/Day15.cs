namespace AdventOfCode2023;

public class Day15 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        return lines[0].Split(',').Sum(ComputeHash);
    }

    public long ExecutePart2(string[] lines)
    {
        return -2;
    }

    public static int ComputeHash(string text)
    {
        return text.Aggregate(0, (res, c) => ((res + c) * 17) % 256);

    }
}
