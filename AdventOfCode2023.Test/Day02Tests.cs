namespace AdventOfCode2023.Test;

[TestFixture]
public class Day02Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        ""
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(-10, new Day02().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(-20, new Day02().ExecutePart2(_sampleLines));
    }
}
