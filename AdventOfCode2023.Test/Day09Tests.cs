namespace AdventOfCode2023.Test;

[TestFixture]
public class Day09Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "0 3 6 9 12 15",
        "1 3 6 10 15 21",
        "10 13 16 21 30 45",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(114, new Day09().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(2, new Day09().ExecutePart2(_sampleLines));
    }

    [Test]
    public void TestGetExtrapolation()
    {
        Assert.AreEqual(18, new Day09().GetExtrapolation(_sampleLines[0]));
        Assert.AreEqual(28, new Day09().GetExtrapolation(_sampleLines[1]));
        Assert.AreEqual(68, new Day09().GetExtrapolation(_sampleLines[2]));
    }
    
    [Test]
    public void TestGetBackwardExtrapolation()
    {
        Assert.AreEqual(-3, new Day09().GetBackwardExtrapolation(_sampleLines[0]));
        Assert.AreEqual(0, new Day09().GetBackwardExtrapolation(_sampleLines[1]));
        Assert.AreEqual(5, new Day09().GetBackwardExtrapolation(_sampleLines[2]));
    }
}
