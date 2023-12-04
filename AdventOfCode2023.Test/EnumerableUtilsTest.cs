using AdventOfCode2023.Utils;

namespace AdventOfCode2023.Test;

[TestFixture]
public class EnumerableUtilsTest
{

    [Test]
    public void TestBatchNormal()
    {
        CollectionAssert.AreEqual(
            new List<List<int>>()
                {new List<int>() {1, 2, 3}, new List<int>() {4, 5, 6}, new List<int>() {7, 8, 9}},
            new[] {1, 2, 3, 4, 5, 6, 7, 8, 9}.Batch(3));
    }

    [Test]
    public void TestBatchNotEnoughElementsForLast()
    {
        CollectionAssert.AreEqual(
            new List<List<int>>()
                {new List<int>() {1, 2, 3}, new List<int>() {4, 5, 6}, new List<int>() {7, 8,}},
            new[] {1, 2, 3, 4, 5, 6, 7, 8,}.Batch(3));
    }

    [Test]
    public void TestTakeByColumn()
    {
        CollectionAssert.AreEqual(
            new List<int>() {1, 4, 7},
            new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}.TakeByColumn(3, 3));
    }

    [Test]
    public void TestTakeByColumnNotEnoughElements()
    {
        CollectionAssert.AreEqual(
            new List<int>() {1, 5},
            new[] {1, 2, 3, 4, 5, 6, 7, 8}.TakeByColumn(3, 4));
    }


    [Test]
    public void TestBatchCollection_EmptyLines()
    {
        CollectionAssert.AreEqual(new[]
        {
            new [] {"a", "b"}, new []{"c"}
        }, new[] {"a", "b", "", "c"}.Batch(x => x.Length == 0, true));
    }

    [Test]
    public void TestBatchCollection_StartChar()
    {
        CollectionAssert.AreEqual(new[]
        {
            new [] {"$ a"}, new[] {"$ b", ""}, new []{"$ c"}
        }, new[] {"$ a", "$ b", "", "$ c"}.Batch(x => x.StartsWith('$'),false));
    }

    [Test]
    public void TestLoopingEnumerator()
    {
        int[] values = { 1, 2, 3 };
        var loopingEnumerable = new LoopingEnumerator<int>(values);
        
        int GetNext(LoopingEnumerator<int> enumerator)
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }
        Assert.AreEqual(1, GetNext(loopingEnumerable));
        Assert.AreEqual(2, GetNext(loopingEnumerable));
        Assert.AreEqual(3, GetNext(loopingEnumerable));
        Assert.AreEqual(1, GetNext(loopingEnumerable));
        Assert.AreEqual(2, GetNext(loopingEnumerable));
        Assert.AreEqual(3, GetNext(loopingEnumerable));
        Assert.AreEqual(1, GetNext(loopingEnumerable));
    }
}