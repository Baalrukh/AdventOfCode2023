using System.Diagnostics;

namespace AdventOfCode2023.Test;

[TestFixture]
public class Day01Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "1abc2",
        "pqr3stu8vwx",
        "a1b2c3d4e5f",
        "treb7uchet",
    };

    private static readonly string[] _sampleLinesPart2 = new[]
    {
        "two1nine",
        "eightwothree",
        "abcone2threexyz",
        "xtwone3four",
        "4nineeightseven2",
        "zoneight234",
        "7pqrstsixteen",
    };

    [Test]
    public void TestBaseSample()
    {
        Assert.AreEqual(142, new Day01().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestConsecutiveDigits()
    {
        Assert.AreEqual(23, new Day01().ExecutePart1(new [] {"fgd21fg83ef"}));
    }

    [Test]
    public void TestAdvancedSample()
    {
        Assert.AreEqual(281, new Day01().ExecutePart2(_sampleLinesPart2));
    }
}