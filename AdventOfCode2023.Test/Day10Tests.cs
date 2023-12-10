using AdventOfCode2023.Utils;

namespace AdventOfCode2023.Test;

[TestFixture]
public class Day10Tests
{
    private static readonly string[] _sampleLines = new[]
    {
        "-L|F7",
        "7S-7|",
        "L|7||",
        "-L-J|",
        "L|-JF",
    };

    private static readonly string[] _sampleLines2 = new[]
    {
        "7-F7-",
        ".FJ|7",
        "SJLL7",
        "|F--J",
        "LJ.LJ",
    };

    private static readonly string[] _sampleLinePart2_1 = new[]
    {
        "..........",
        ".S------7.",
        ".|F----7|.",
        ".||....||.",
        ".||....||.",
        ".|L-7F-J|.",
        ".|..||..|.",
        ".L--JL--J.",
        ".........."
    };

    private static readonly string[] _sampleLinePart2_2 = new[]
    {
        "...........",
        ".S-------7.",
        ".|F-----7|.",
        ".||.....||.",
        ".||.....||.",
        ".|L-7OF-J|.",
        ".|..|O|..|.",
        ".L--JOL--J.",
        ".....O.....",
    };
        
    private static readonly string[] _sampleLinePart2_3 = new[]
    {
        ".F----7F7F7F7F-7....",
        ".|F--7||||||||FJ....",
        ".||.FJ||||||||L7....",
        "FJL7L7LJLJ||LJ.L-7..",
        "L--J.L7...LJS7F-7L7.",
        "....F-J..F7FJ|L7L7L7",
        "....L7.F7||L7|.L7L7|",
        ".....|FJLJ|FJ|F7|.LJ",
        "....FJL-7.||.||||...",
        "....L---J.LJ.LJLJ...",
    };
        
    [Test]
    public void TestPart1()
    {
        Assert.AreEqual(4, new Day10().ExecutePart1(_sampleLines));
        Assert.AreEqual(8, new Day10().ExecutePart1(_sampleLines2));
    }

    [Test]
    public void TestGetPosition()
    {
        Assert.AreEqual(new IntVector2(1, 1), Day10.FindStartPosition(_sampleLines));
        Assert.AreEqual(new IntVector2(0, 2), Day10.FindStartPosition(_sampleLines2));
    }

    [Test]
    public void TestPipeCanEnter()
    {
        Day10.Pipe pipe = new Day10.Pipe('G', Day10.Direction.East, Day10.Direction.South, null);
        Assert.IsTrue(pipe.CanEnter(Day10.Direction.East));    
        Assert.IsTrue(pipe.CanEnter(Day10.Direction.North));    
        Assert.IsFalse(pipe.CanEnter(Day10.Direction.West));    
        Assert.IsFalse(pipe.CanEnter(Day10.Direction.South));    
    }

    [Test]
    public void TestPipeAdvance()
    {
        Day10.Pipe pipe = new Day10.Pipe('G', Day10.Direction.East, Day10.Direction.South, null);
        Assert.AreEqual(new Day10.PipePosition(new IntVector2(8, 10), Day10.Direction.South, 7),
                        pipe.Advance(new Day10.PipePosition(new IntVector2(8, 9), Day10.Direction.East, 6)));
        Assert.AreEqual(new Day10.PipePosition(new IntVector2(7, 9), Day10.Direction.West, 7),
                        pipe.Advance(new Day10.PipePosition(new IntVector2(8, 9), Day10.Direction.North, 6)));

    }
    
    [Test]
    public void TestPart2()
    {
        Assert.AreEqual(4, new Day10().ExecutePart2(_sampleLinePart2_1));
        Assert.AreEqual(4, new Day10().ExecutePart2(_sampleLinePart2_2));
        Assert.AreEqual(8, new Day10().ExecutePart2(_sampleLinePart2_3));
    }
    [Test]
    public void TestPartTMP()
    {
        Assert.AreEqual(8, new Day10().ExecutePart2(_sampleLinePart2_3));
    }
}
