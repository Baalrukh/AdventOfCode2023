namespace AdventOfCode2023;

public class Day02 : Exercise
{
    private class GameCubes
    {
        public int Red;
        public int Green;
        public int Blue;

        public bool IsPossible(int maxRed, int maxGreen, int maxBlue)
        {
            return (Red <= maxRed) && (Green <= maxGreen) && (Blue <= maxBlue);
        }
    }
    
    public long ExecutePart1(string[] lines)
    {
        return lines.Select((x, i) => IsPossible(x) ? (i + 1) : 0).Sum();
    }

    private bool IsPossible(string line)
    {
        IEnumerable<GameCubes> drawnCubes = ParseLineCubeDraw(line);
        return drawnCubes.All(x => x.IsPossible(12, 13, 14));
    }

    private static IEnumerable<GameCubes> ParseLineCubeDraw(string line)
    {
        string[] tokens = line.Split(new[] { ':', ';' });
        IEnumerable<GameCubes> drawnCubes = ParseCubes(tokens.Skip(1));
        return drawnCubes;
    }

    private int GetPower(string line)
    {
        IEnumerable<GameCubes> drawnCubes = ParseLineCubeDraw(line);
        int maxRed = drawnCubes.Select(x => x.Red).Max();
        int maxGreen = drawnCubes.Select(x => x.Green).Max();
        int maxBlue = drawnCubes.Select(x => x.Blue).Max();
        return maxRed * maxGreen * maxBlue;
    }

    public long ExecutePart2(string[] lines)
    {
        return lines.Sum(GetPower);
    }

    private static IEnumerable<GameCubes> ParseCubes(IEnumerable<string> tokens)
    {
        return tokens.Select(x => ParseGroup(x));

        GameCubes ParseGroup(string text)
        {
            string[] tokens = text.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            GameCubes cubes = new GameCubes();
            for (int i = 0; i < tokens.Length; i += 2)
            {
                int count = int.Parse(tokens[i]);
                switch (tokens[i + 1])
                {
                    case "red":
                        cubes.Red = count;
                        break;
                    case "green":
                        cubes.Green = count;
                        break;
                    case "blue":
                        cubes.Blue = count;
                        break;
                }
            }

            return cubes;
        }
    }
}
