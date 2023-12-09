namespace AdventOfCode2023;

public class Day09 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        return lines.Sum(GetExtrapolation);
    }

    public long ExecutePart2(string[] lines)
    {
        return lines.Sum(GetBackwardExtrapolation);
    }

    public long GetExtrapolation(string line)
    {
        long[] input = line.Split(' ').Select(long.Parse).ToArray();
        return GetExtrapolation(input);
    }

    private long GetExtrapolation(long[] input)
    {
        long[] deltas = input.Skip(1).Select(((val, index) => val - input[index])).ToArray();
        if (deltas.All(x => x == 0))
        {
            return input[^1];
        }

        return input[^1] + GetExtrapolation(deltas);
    }

    public long GetBackwardExtrapolation(string line)
    {
        long[] input = line.Split(' ').Select(long.Parse).ToArray();
        return GetBackwardExtrapolation(input);
    }

    private long GetBackwardExtrapolation(long[] input)
    {
        long[] deltas = input.Skip(1).Select(((val, index) => val - input[index])).ToArray();
        if (deltas.All(x => x == 0))
        {
            return input[0];
        }

        return input[0] - GetBackwardExtrapolation(deltas);
    }
}
