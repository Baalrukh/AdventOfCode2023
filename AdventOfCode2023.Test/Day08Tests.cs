namespace AdventOfCode2023.Test;

[TestFixture]
public class Day08Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "RL",
        "",
        "AAA = (BBB, CCC)",
        "BBB = (DDD, EEE)",
        "CCC = (ZZZ, GGG)",
        "DDD = (DDD, DDD)",
        "EEE = (EEE, EEE)",
        "GGG = (GGG, GGG)",
        "ZZZ = (ZZZ, ZZZ)",
    };

    private static readonly string[] _sampleLines2 = new[]
    {
        "LLR",
        "",
        "AAA = (BBB, BBB)",
        "BBB = (AAA, ZZZ)",
        "ZZZ = (ZZZ, ZZZ)",
    };

    private static readonly string[] _sampleLinesPart2 = new[]
    {
        "LR",
        "",
        "11A = (11B, XXX)",
        "11B = (XXX, 11Z)",
        "11Z = (11B, XXX)",
        "22A = (22B, XXX)",
        "22B = (22C, 22C)",
        "22C = (22Z, 22Z)",
        "22Z = (22B, 22B)",
        "XXX = (XXX, XXX)",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(2, new Day08().ExecutePart1(_sampleLines));
        Assert.AreEqual(6, new Day08().ExecutePart1(_sampleLines2));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(6, new Day08().ExecutePart2(_sampleLinesPart2));
    }
}
