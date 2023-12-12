using AdventOfCode2023.Utils;

namespace AdventOfCode2023.Test;

[TestFixture]
public class Day11Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "...#......",
        ".......#..",
        "#.........",
        "..........",
        "......#...",
        ".#........",
        ".........#",
        "..........",
        ".......#..",
        "#...#.....",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(374, new Day11().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestParseGalaxiesAndExpand()
    {
        CollectionAssert.AreEquivalent(new[]
            {
                new IntVector2(4, 0), new IntVector2(9, 1),
                new IntVector2(0, 2), new IntVector2(8, 5),
                new IntVector2(1, 6), new IntVector2(12, 7),
                new IntVector2(9, 10), new IntVector2(0, 11),
                new IntVector2(5, 11),

            }.ToList(),
            Day11.ParseGalaxiesAndExpand(_sampleLines, 1));
    }

    [Test]
    public void TestParseGalaxiesAndExpand2()
    {
        CollectionAssert.AreEquivalent(new[]
            {
                new IntVector2(12, 0), new IntVector2(25, 1),
                new IntVector2(0, 2), new IntVector2(24, 13),
                new IntVector2(1, 14), new IntVector2(36, 15),
                new IntVector2(25, 26), new IntVector2(0, 27),
                new IntVector2(13, 27),

            }.ToList(),
            Day11.ParseGalaxiesAndExpand(_sampleLines, 9).OrderBy(x => x.Y));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(1030, Day11.GetGalaxyDistancesSum(_sampleLines, 9));
        Assert.AreEqual(8410, Day11.GetGalaxyDistancesSum(_sampleLines, 99));
    }
}
