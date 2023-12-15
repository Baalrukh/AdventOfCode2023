using System.Diagnostics;
using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day14 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        // var map = Map2D<char>.Parse(lines,  c => c, () => '#');
        // TiltNorth(map);
        // return map.EnumeratePositions(c => c == 'O').Sum(p => map.Height - p.Y);

        int width = lines[0].Length;
        int height = lines.Length;
        List<int>[] blocs = Enumerable.Range(0, width).Select(x => new List<int>()).ToArray();
        List<int>[] rocks = Enumerable.Range(0, width).Select(x => new List<int>()).ToArray();

        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < width; x++)
            {
                if (line[x] == '#')
                {
                    blocs[x].Add(y);
                }
                else if (line[x] == 'O')
                {
                    rocks[x].Add(y);
                }
            }
        }

        TiltNorth(blocs, rocks);
        var lines2 = Enumerable.Range(0, height).Select(_ => Enumerable.Repeat('.', width).ToArray()).ToArray();
        for (int x = 0; x < width; x++)
        {
            var blocList = blocs[x];
            var rockList = rocks[x];

            for (int i = 0; i < blocList.Count; i++)
            {
                lines2[blocList[i]][x] = '#';
            }

            for (int i = 0; i < rockList.Count; i++)
            {
                lines2[rockList[i]][x] = 'O';
            }
        }

        foreach (var chars in lines2)
        {
            Console.WriteLine(new string(chars));
        }


        return rocks.SelectMany(x => x).Sum(y => height - y);
    }

    private void TiltNorth(List<int>[] blocs, List<int>[] rocks)
    {
        for (int x = 0; x < blocs.Length; x++)
        {
            var blockList = blocs[x];
            var rockList = rocks[x];

            int blockIndex = 0;
            int y = 0;

            int nextBlockY = blockList.Count == 0 ? int.MaxValue : blockList[0];


            var rockEnum = rockList.GetEnumerator();

            int maxY;
            var blockEnum = blockList.GetEnumerator();
            if (!blockEnum.MoveNext())
            {
                maxY = int.MaxValue;
            }
            else
            {
                maxY = blockEnum.Current;
            }

            while (rockEnum.MoveNext())
            {
                while (rockEnum.Current > maxY)
                {
                    y = maxY + 1;
                    if (!blockEnum.MoveNext())
                    {
                        maxY = int.MaxValue;
                    }
                    else
                    {
                        maxY = blockEnum.Current;
                    }
                }


            }


            for (int i = 0; i < rockList.Count; i++)
            {
                while (y == nextBlockY)
                {
                    y++;
                    blockIndex++;
                    nextBlockY = blockIndex >= blockList.Count ? int.MaxValue : blockList[blockIndex];
                }

                if (rockList[i] < nextBlockY)
                {
                    rockList[i] = y;
                }
                else
                {

                }
                y++;
            }
        }
    }

    public long ExecutePart2(string[] lines)
    {
        var map = Map2D<char>.Parse(lines,  c => c, () => '#');
        var stopwatch = Stopwatch.StartNew();
        Dictionary<string, int> encounteredSteps = new();
        for (long i = 0; i < 1_000_000; i++)
        {
            DoTiltCycle(map);
            var text = map.ToString();
        }
Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");

        return -2;
    }

    public static void TiltNorth(Map2D<char> map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                if (map[x, y] == 'O')
                {
                    // MoveNorth(x, y, map);
                    MoveDirection(new IntVector2(x, y), map, new IntVector2(0, -1));
                }
            }
        }
    }

    public static void TiltWest(Map2D<char> map)
    {
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (map[x, y] == 'O')
                {
                    MoveDirection(new IntVector2(x, y), map, new IntVector2(-1, 0));
                }
            }
        }
    }

    public static void TiltSouth(Map2D<char> map)
    {
        for (int y = map.Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < map.Width; x++)
            {
                if (map[x, y] == 'O')
                {
                    MoveDirection(new IntVector2(x, y), map, new IntVector2(0, 1));
                }
            }
        }
    }

    public static void TiltEast(Map2D<char> map)
    {
        for (int x = map.Width - 1; x >= 0; x--)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (map[x, y] == 'O')
                {
                    MoveDirection(new IntVector2(x, y), map, new IntVector2(1, 0));
                }
            }
        }
    }

    public static void MoveDirection(IntVector2 position, Map2D<char> map, IntVector2 direction)
    {
        IntVector2 dest = position;
        while (map.IsInside(dest))
        {
            if (map[dest + direction] != '.')
            {
                if (dest != position)
                {
                    map[dest] = 'O';
                    map[position] = '.';
                }

                return;
            }

            dest += direction;
        }
    }

    public static void MoveNorth(int x, int y, Map2D<char> map)
    {
        int destY = y;
        while (destY >= 0)
        {
            if (map[x, destY - 1] != '.')
            {
                if (destY != y)
                {
                    map[x, destY] = 'O';
                    map[x, y] = '.';
                }

                return;
            }

            destY--;
        }
    }

    public static void DoTiltCycle(Map2D<char> map)
    {
        TiltNorth(map);
        TiltWest(map);
        TiltSouth(map);
        TiltEast(map);
    }
}
