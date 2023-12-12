using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day11 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        return GetGalaxyDistancesSum(lines, 1);
    }

    public static long GetGalaxyDistancesSum(string[] lines, int expanseSize)
    {
        var galaxies = ParseGalaxiesAndExpand(lines, expanseSize).ToList();

        long sum = 0;
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                sum += (galaxies[j] - galaxies[i]).ManhattanDistance;
            }
        }

        return sum;
    }


    public static IEnumerable<IntVector2> ParseGalaxiesAndExpand(string[] lines, int expanseSize)
    {
        var rawPositions = ParseGalaxyPositionsWithYExpand(lines, expanseSize).OrderBy(p => p.X).ToList();
        int xOffset = 0;
        int prevX = 0;
        foreach (var rawPosition in rawPositions)
        {
            if (rawPosition.X > prevX)
            {
                xOffset += (rawPosition.X - prevX - 1) * expanseSize;
                prevX = rawPosition.X;
            }

            yield return new IntVector2(rawPosition.X + xOffset, rawPosition.Y);
        }
    }

    private static IEnumerable<IntVector2> ParseGalaxyPositionsWithYExpand(string[] lines, int expanseSize)
    {
        int yOffset = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            bool hasGalaxyOnLine = false;
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    hasGalaxyOnLine = true;
                    yield return new IntVector2(x, y + yOffset);
                }
            }

            if (!hasGalaxyOnLine)
            {
                yOffset += expanseSize;
            }
        }
    }

    public long ExecutePart2(string[] lines)
    {
        return GetGalaxyDistancesSum(lines, 1_000_000 - 1);
    }
}
