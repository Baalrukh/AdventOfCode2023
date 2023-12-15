using AdventOfCode2023.Utils;

namespace AdventOfCode2023.Test;

[TestFixture]
public class Day14Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "O....#....",
        "O.OO#....#",
        ".....##...",
        "OO.#O....O",
        ".O.....O#.",
        "O.#..O.#.#",
        "..O..#O..O",
        ".......O..",
        "#....###..",
        "#OO..#....",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(136, new Day14().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestTiltNorth()
    {
        string[] expected = new[]
        {
            "OOOO.#.O..",
            "OO..#....#",
            "OO..O##..O",
            "O..#.OO...",
            "........#.",
            "..#....#.#",
            "..O..#.O.O",
            "..O.......",
            "#....###..",
            "#....#....",
        };

        var map = ParseSampleMap();
        Day14.TiltNorth(map);
        Assert.AreEqual(string.Join("\n", expected) + "\n", map.ToString());
    }

    private static Map2D<char> ParseSampleMap()
    {
        return Map2D<char>.Parse(_sampleLines,  c => c, () => '#');
    }

    [Test]
    public void TestTryMoveNorth()
    {
        var map = ParseSampleMap();
        var expectedNoMove = map.ToString();
        Day14.MoveNorth(0, 1, map);
        Assert.AreEqual(expectedNoMove, map.ToString());

        Day14.MoveNorth(1, 3, map);
        Assert.AreEqual('.', map[1, 3]);
        Assert.AreEqual('O', map[1, 0]);
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(64, new Day14().ExecutePart2(_sampleLines));
    }

    [Test]
    public void TestCycle()
    {
        string[] expected = new[]
        {
            ".....#....",
            "....#...O#",
            "...OO##...",
            ".OO#......",
            ".....OOO#.",
            ".O#...O#.#",
            "....O#....",
            "......OOOO",
            "#...O###..",
            "#..OO#....",
        };

        var map = ParseSampleMap();
        Day14.DoTiltCycle(map);
        Assert.AreEqual(string.Join("\n", expected) + "\n", map.ToString());
    }
}
