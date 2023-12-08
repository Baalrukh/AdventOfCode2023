namespace AdventOfCode2023.Test;

[TestFixture]
public class Day07Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "32T3K 765",
        "T55J5 684",
        "KK677 28",
        "KTJJT 220",
        "QQQJA 483",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(6440, new Day07().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(5905, new Day07().ExecutePart2(_sampleLines));
    }

    [Test]
    public void TestGetHandType()
    {
        Assert.AreEqual(Day07.HandType.HighCard, Day07.Hand.Parse("23456").HandType);
        Assert.AreEqual(Day07.HandType.OnePair, Day07.Hand.Parse("A23A4").HandType);
        Assert.AreEqual(Day07.HandType.TwoPair, Day07.Hand.Parse("23432").HandType);
        Assert.AreEqual(Day07.HandType.ThreeOfAKind, Day07.Hand.Parse("TTT98").HandType);
        Assert.AreEqual(Day07.HandType.FullHouse, Day07.Hand.Parse("23332").HandType);
        Assert.AreEqual(Day07.HandType.FourOfAKind, Day07.Hand.Parse("AA8AA").HandType);
        Assert.AreEqual(Day07.HandType.FiveOfAKind, Day07.Hand.Parse("AAAAA").HandType);
    }

    [Test]
    public void TestCompareToReturnsByHandType()
    {
        var high = Day07.Hand.Parse("23456");
        var one = Day07.Hand.Parse("A23A4");
        var two = Day07.Hand.Parse("23432");
        var three = Day07.Hand.Parse("TTT98");
        var full = Day07.Hand.Parse("23332");
        var four = Day07.Hand.Parse("AA8AA");
        var five = Day07.Hand.Parse("AAAAA");

        CollectionAssert.AreEqual(new [] {high, one, two, three, full, four, five}, new [] {five, four, full, three, two, one, high}.OrderBy(x => x));
    }

    [Test]
    public void TestCompareToComparesCardValueIfSameType()
    {
        var a = Day07.Hand.Parse("33332");
        var b = Day07.Hand.Parse("2AAAA");
        var c = Day07.Hand.Parse("77888");
        var d = Day07.Hand.Parse("77788");
        Assert.AreEqual(a, new [] {a, b}.Max());
        Assert.AreEqual(a, new [] {b, a}.Max());
        Assert.AreEqual(c, new [] {c, d}.Max());
        Assert.AreEqual(c, new [] {d, c}.Max());
    }
}
