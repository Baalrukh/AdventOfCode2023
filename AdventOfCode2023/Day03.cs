using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day03 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        var parts = EnumerateParts(lines).ToList();
        var partPositions = parts.Select(x => x.pos).ToList();
        var numbers = EnumeratePartNumbers(lines, parts.Select(x => x.partType).Distinct());
        return numbers.Where(x => x.IsAdjacentToPart(partPositions)).Sum(x => x.Value);
    }

    private IEnumerable<PartNumber> EnumeratePartNumbers(string[] lines, IEnumerable<char> partSymbols)
    {
        var separator = new[] {'.'}.Concat(partSymbols).ToArray();
        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            var numberStrings = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            int position = 0;
            foreach (var numberString in numberStrings)
            {
                var indexOf = line.IndexOf(numberString, position, StringComparison.Ordinal);
                var partNumber = new PartNumber(new IntVector2(indexOf, y), numberString.Length, int.Parse(numberString));
                yield return partNumber;
                position = indexOf + numberString.Length + 1;
            }
        }
    }

    private IEnumerable<(char partType, IntVector2 pos)> EnumerateParts(string[] lines)
    {
        var width = lines[0].Length;
        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < width; x++)
            {
                if ((line[x] != '.') && !char.IsDigit(line[x]))
                {
                    yield return (line[x], new IntVector2(x, y));
                }
            }
        }
    }


    public long ExecutePart2(string[] lines)
    {
        var allParts = EnumerateParts(lines).ToList();
        var gearParts = allParts.Where(x => x.partType == '*').ToList();
        var numbers = EnumeratePartNumbers(lines, allParts.Select(x => x.partType).Distinct()).ToList();

        long Selector((char partType, IntVector2 pos) x)
        {
            var partNumbers = numbers.Where(y => y.IsAdjacentToPart(x.pos)).ToList();
            if (partNumbers.Count == 2)
            {
                return partNumbers[0].Value * partNumbers[1].Value;
            }

            return 0;
        }

        return gearParts.Select(Selector)
            .Sum();
    }

    private class PartNumber
    {
        public readonly IntVector2 StartPosition;
        public readonly int Length;
        public readonly int Value;

        public PartNumber(IntVector2 startPosition, int length, int value)
        {
            StartPosition = startPosition;
            Length = length;
            Value = value;
        }

        public bool IsAdjacentToPart(IntVector2 part)
        {
            return (part.X >= StartPosition.X - 1)
                   && (part.X <= StartPosition.X + Length)
                   && (part.Y >= StartPosition.Y - 1)
                   && (part.Y <= StartPosition.Y + 1);
        }

        public bool IsAdjacentToPart(List<IntVector2> parts)
        {
            var isAdjacentToPart = parts.Any(IsAdjacentToPart);
            // Console.WriteLine($"{Value} => {isAdjacentToPart}");
            return isAdjacentToPart;
        }

        public override string ToString()
        {
            return $"{Value} ({StartPosition})";
        }
    }
}
