using AdventOfCode2023.Utils;

namespace AdventOfCode2023.Test;

[TestFixture]
public class IntervalTests
{
    [Test]
    public void TestIsEmpty()
    {
        Assert.IsFalse(Interval.FromToIncluded(1, 3).IsEmpty);
        Assert.IsTrue(Interval.FromToIncluded(1, 0).IsEmpty);
    }

    [Test]
    public void TestLength()
    {
        Assert.AreEqual(5, Interval.FromToIncluded(1, 5).Length);
    }
}