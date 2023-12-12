using System.Diagnostics;
using System.Text;

namespace AdventOfCode2023;

public class Day12 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        var solver = new PossiblePatternsSolver();
        return lines.Sum(x => solver.GetArrangementCount(x));
    }

    public long ExecutePart2(string[] lines)
    {
        return -1;
        // return lines.Select(RepeatLine).Sum(PossiblePatternsSolver.GetArrangementsCount);
    }

    public static string[] RepeatLine(string line)
    {
        var tokens = line.Split(' ');
        return new[] {string.Join("?", Enumerable.Repeat(tokens[0], 5)), string.Join(",", Enumerable.Repeat(tokens[1], 5))};
    }


    public class PossiblePatternsSolver
    {
        public int GetArrangementCount(string line)
        {
            var tokens = line.Split(' ');
            return GetArrangementsCount(tokens);
        }

        public static int GetArrangementsCount(string[] tokens)
        {
            return GetArrangementsCount(tokens[0], tokens[1].Split(',').Select(int.Parse).ToArray());
        }

        private static int GetArrangementsCount(string line, int[] blockSizes)
        {
            IEnumerable<string> possiblePatterns = EnumeratePossiblePatterns(line.Length, blockSizes);

            return possiblePatterns.Count(x => Matches(line, x));
        }

        private static bool Matches(string line, string pattern)
        {
            Console.WriteLine(pattern);
            Debug.WriteLine(pattern);
            return line.Zip(pattern).All(x => (x.First == x.Second) || (x.First == '?'));
        }

        private static IEnumerable<string> EnumeratePossiblePatterns(int lineLength, int[] blockSizes)
        {
            var margin = lineLength - (blockSizes.Sum() + blockSizes.Length - 1);
            byte[] chars = new byte[lineLength];

            return GeneratePatterns(chars, 0, margin, blockSizes, 0);
        }

        private static IEnumerable<string> GeneratePatterns(byte[] chars, int outIndex, int margin, int[] blockSizes, int blockIndex)
        {
            if (blockIndex == blockSizes.Length)
            {
                for (int i = outIndex; i < chars.Length; i++)
                {
                    chars[i] = (byte) '.';
                }

                yield return Encoding.ASCII.GetString(chars);
                yield break;
            }

            for (int i = 0; i <= margin; i++)
            {
                int index = outIndex;
                for (int j = 0; j < i; j++)
                {
                    chars[index + j] = (byte)'.';
                }

                index += i;

                for (int j = 0; j < blockSizes[blockIndex]; j++)
                {
                    chars[index + j] = (byte)'#';
                }

                index += blockSizes[blockIndex];

                if (blockIndex != blockSizes.Length - 1)
                {
                    chars[index++] = (byte)'.';
                }

                foreach (var generatePattern in GeneratePatterns(chars, index, margin - i, blockSizes,
                             blockIndex + 1))
                {
                    yield return generatePattern;
                }
            }
        }
    }


    public class BruteForceSolver
    {
        public static int GetArrangementCount(string line)
        {
            var tokens = line.Split(' ');
            return GetArrangementsCount(tokens[0], tokens[1].Split(',').Select(int.Parse).ToArray());
        }

        private static int GetArrangementsCount(string line, int[] blockSizes)
        {
            return EnumerateAllPatterns(line)
                .Count(x => Matches(x, blockSizes));
        }

        public static bool Matches(string line, int[] blockSizes)
        {
            var tokens = line.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != blockSizes.Length)
            {
                return false;
            }

            return tokens.Zip(blockSizes).All(pair => pair.First.Length == pair.Second);
        }

        private static IEnumerable<string> EnumerateAllPatterns(string line)
        {
            return EnumerateAllPatterns(Encoding.ASCII.GetBytes(line), 0);
        }

        private static IEnumerable<string> EnumerateAllPatterns(byte[] line, int index)
        {
            if (index == line.Length)
            {
                yield return Encoding.ASCII.GetString(line);
                yield break;
            }

            if (line[index] == '?')
            {
                line[index] = (byte) '#';
                foreach (var pattern in EnumerateAllPatterns(line, index + 1))
                {
                    yield return pattern;
                }

                line[index] = (byte) '.';
                foreach (var pattern in EnumerateAllPatterns(line, index + 1))
                {
                    yield return pattern;
                }

                line[index] = (byte) '?';
            }
            else
            {
                foreach (var pattern in EnumerateAllPatterns(line, index + 1))
                {
                    yield return pattern;
                }
            }
        }
    }
}
