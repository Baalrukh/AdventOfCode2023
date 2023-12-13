using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day13 : Exercise
{
    public enum SymmetryAxis
    {
        VerticalAxis,
        HorizontalAxis
    }
    
    
    public long ExecutePart1(string[] lines)
    {
        var patterns = lines.Batch(string.IsNullOrEmpty, true);
        return patterns.Select(x => FindSymmetry(x, CheckVerticalSymmetryAt))
            .Sum(x => (x.symmetryAxis == SymmetryAxis.HorizontalAxis ? 1 : 100) * x.offset);
    }

    private delegate bool VerticalSymmetryChecker(List<string> lines, int axisY);
    
    private (SymmetryAxis symmetryAxis, int offset) FindSymmetry(List<string> lines, VerticalSymmetryChecker verticalSymmetryChecker)
    {
        for (int axisY = 0; axisY < lines.Count - 1; axisY++)
        {
            if (verticalSymmetryChecker(lines, axisY))
            {
                return (SymmetryAxis.VerticalAxis, axisY + 1);
            }
        }

        int width = lines[0].Length;
        char[][] chars = Enumerable.Range(0, width).Select(x => new char[lines.Count]).ToArray();
        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < width; x++)
            {
                chars[x][y] = lines[y][x];
            }
        }

        lines = chars.Select(x => new string(x)).ToList();
        
        for (int axisX = 0; axisX < width; axisX++)
        {
            if (verticalSymmetryChecker(lines, axisX))
            {
                return (SymmetryAxis.HorizontalAxis, axisX + 1);
            }
        }
        
        throw new Exception();
    }

    private bool CheckVerticalSymmetryAt(List<string> lines, int axisY)
    {
        if (lines[axisY] != lines[axisY + 1])
        {
            return false;
        }

        int y0 = axisY - 1;
        int y1 = axisY + 2;
        while ((y0 >= 0) && (y1 < lines.Count))
        {
            if (lines[y0] != lines[y1])
            {
                return false;
            }

            y0--;
            y1++;
        }

        return true;
    }

    private int GetDifferenceCount(string line1, string line2)
    {
        return line1.Zip(line2).Count(pair => pair.First != pair.Second);
    }
    
    private bool CheckSmudgedVerticalSymmetryAt(List<string> lines, int axisY)
    {
        int diffCount = GetDifferenceCount(lines[axisY], lines[axisY + 1]);
        if (diffCount > 1)
        {
            return false;
        }

        int y0 = axisY - 1;
        int y1 = axisY + 2;
        while ((y0 >= 0) && (y1 < lines.Count))
        {
            diffCount += GetDifferenceCount(lines[y0], lines[y1]);
            if (diffCount > 1)
            {
                return false;
            }

            y0--;
            y1++;
        }

        return diffCount == 1;
    }

    public long ExecutePart2(string[] lines)
    {
        var patterns = lines.Batch(string.IsNullOrEmpty, true);
        return patterns.Select(x => FindSymmetry(x, CheckSmudgedVerticalSymmetryAt))
            .Sum(x => (x.symmetryAxis == SymmetryAxis.HorizontalAxis ? 1 : 100) * x.offset);

    }
}
