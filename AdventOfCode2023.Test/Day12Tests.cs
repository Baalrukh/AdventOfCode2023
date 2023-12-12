namespace AdventOfCode2023.Test;

[TestFixture]
public class Day12Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "???.### 1,1,3",
        ".??..??...?##. 1,1,3",
        "?#?#?#?#?#?#?#? 1,3,1,6",
        "????.#...#... 4,1,1",
        "????.######..#####. 1,6,5",
        "?###???????? 3,2,1",
    };

    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(21, new Day12().ExecutePart1(_sampleLines));
    }

    [Test]
    public void TestGetArrangementCount()
    {
        Assert.AreEqual(1, new Day12.PossiblePatternsSolver().GetArrangementCount(_sampleLines[0]));
        Assert.AreEqual(4, new Day12.PossiblePatternsSolver().GetArrangementCount(_sampleLines[1]));
    }

    [Test]
    public void TestMatches()
    {
        Assert.IsTrue(Day12.BruteForceSolver.Matches("#.#.###", new[] {1, 1, 3}));
        Assert.IsFalse(Day12.BruteForceSolver.Matches("###.###", new[] {1, 1, 3}));
        Assert.IsFalse(Day12.BruteForceSolver.Matches("#.##.###", new[] {1, 1, 3}));
    }

    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(525152, new Day12().ExecutePart2(_sampleLines));
    }

    [Test]
    public void TestRepeatLine()
    {
        CollectionAssert.AreEqual(new[] {".#?.#?.#?.#?.#", "1,1,1,1,1"}, Day12.RepeatLine(".# 1"));
    }

    [Test]
    public void TestGetArrangementCount_part2()
    {
        Assert.AreEqual(1, Day12.PossiblePatternsSolver.GetArrangementsCount(Day12.RepeatLine(_sampleLines[0])));
        Assert.AreEqual(4, Day12.PossiblePatternsSolver.GetArrangementsCount(Day12.RepeatLine(_sampleLines[1])));
    }
}
