namespace AdventOfCode2023;

public class Day04 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        return lines.Sum(GetPoints);
    }

    private int GetPoints(string line)
    {
        return (int)Math.Round(Math.Pow(2, GetWinCountForLine(line) - 1));
    }

    private static int GetWinCountForLine(string line)
    {
        var tokens = line.Split(new[] {':', '|'});
        List<int> winningNumbers = tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        List<int> haveNumbers = tokens[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var winCount = haveNumbers.Count(x => winningNumbers.Contains(x));
        return winCount;
    }

    public long ExecutePart2(string[] lines)
    {
        var cardWinCounts = lines.Select(GetWinCountForLine).ToArray();
        long[] cardsCount = Enumerable.Range(0, cardWinCounts.Length).Select(x => 1L).ToArray();

        for (int i = 0; i < cardWinCounts.Length; i++)
        {
            for (int j = 0; j < cardWinCounts[i]; j++)
            {
                cardsCount[i + j + 1] += cardsCount[i];
            }
        }

        return cardsCount.Sum();
    }
}
