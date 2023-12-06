namespace AdventOfCode2023.Test;

[TestFixture]
public class Day06Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "Time:      7  15   30",
        "Distance:  9  40  200"
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(288, new Day06().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(71503, new Day06().ExecutePart2(_sampleLines));
    }

    [Test]
    public void TestGetWinPossibilities()
    {
        Assert.AreEqual(4, new Day06.Race(7, 9).GetWinPossibilities());
        Assert.AreEqual(8, new Day06.Race(15, 40).GetWinPossibilities());
        Assert.AreEqual(9, new Day06.Race(30, 200).GetWinPossibilities());
    }
}
