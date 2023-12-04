using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day01 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        return lines.Sum(ParseLine);

        int ParseLine(string x)
        {
            var digits = x.Where(char.IsDigit).ToArray();
            var result = 10 * (digits[0] - '0') + (digits[^1] - '0');
            return result;
        }
    }


    public long ExecutePart2(string[] lines)
    {
        var textDigits = new[] {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

        return lines.Sum(ParseLine2);

        int ParseLine2(string line)
        {
            var startPositions = textDigits.Select(x => line.IndexOf(x, StringComparison.Ordinal)).ToArray();
            var indexOfMin = startPositions.IndexOfMin(x => x == -1 ? int.MaxValue : x);
            var startDigit = (indexOfMin % 9) + 1;

            var startEndPositions = textDigits.Select(x => line.LastIndexOf(x, StringComparison.Ordinal)).ToArray();
            var indexOfMax = startEndPositions.IndexOfMax(x => x);
            var endDigit = (indexOfMax % 9) + 1;

            var result = startDigit * 10 + endDigit;
            return result;
        }
    }
}