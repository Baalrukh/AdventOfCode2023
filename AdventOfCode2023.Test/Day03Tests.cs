namespace AdventOfCode2023.Test;

[TestFixture]
public class Day03Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598..",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(4361, new Day03().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(467835, new Day03().ExecutePart2(_sampleLines));
    }
}
