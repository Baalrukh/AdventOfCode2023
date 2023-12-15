namespace AdventOfCode2023.Test;

[TestFixture]
public class Day15Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(1320, new Day15().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestComputeHash()
    {
        Assert.AreEqual(30, Day15.ComputeHash("rn=1"));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(-20, new Day15().ExecutePart2(_sampleLines));
    }
}
