namespace AdventOfCode2023.Test;

[TestFixture]
public class Day13Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "#.##..##.",
        "..#.##.#.",
        "##......#",
        "##......#",
        "..#.##.#.",
        "..##..##.",
        "#.#.##.#.",
        "",
        "#...##..#",
        "#....#..#",
        "..##..###",
        "#####.##.",
        "#####.##.",
        "..##..###",
        "#....#..#",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(405, new Day13().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(400, new Day13().ExecutePart2(_sampleLines));
    }
}
