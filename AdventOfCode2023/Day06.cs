namespace AdventOfCode2023;

public class Day06 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        var allNumbers = lines.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse)).ToList();
        var races = allNumbers[0].Zip(allNumbers[1]).Select(pair => new Race(pair.First, pair.Second));

        return races.Aggregate(1, (prod, race) => prod * race.GetWinPossibilities());
    }

    public long ExecutePart2(string[] lines)
    {
        var allNumbers = lines.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Aggregate("", (s, d) => s + d)).Select(long.Parse).ToList();
        var race = new Race(allNumbers[0], allNumbers[1]);
        return race.GetWinPossibilities();
    }

    public record Race(long Duration, long Distance)
    {
        public int GetWinPossibilities()
        {
            int count = 0;
            for (long i = 1; i < Duration - 1; i++)
            {
                if ((Duration - i) * i > Distance)
                {
                    count++;
                }
                else if (count > 0)
                {
                    break;
                }
            }

            return count;
        }
    }
}
