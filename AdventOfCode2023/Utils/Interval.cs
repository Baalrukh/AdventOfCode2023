namespace AdventOfCode2023.Utils;

public readonly struct Interval
{
    public readonly int Start;
    private readonly int _endExcluded;
    public int End => _endExcluded - 1;

    private Interval(int start, int endExcluded)
    {
        Start = start;
        _endExcluded = endExcluded;
    }

    public static Interval FromToIncluded(int fromIncluded, int toIncluded)
    {
        return new Interval(fromIncluded, toIncluded + 1);
    }

    public int Length => _endExcluded - Start;

    public bool IsInside(int value)
    {
        return (Start <= value) && (value < _endExcluded);
    }

    public bool Intersects(Interval other)
    {
        return (other.Start < _endExcluded) && (other._endExcluded > Start);
    }

    public bool IsInside(Interval other)
    {
        return (other.Start >= Start) && (other._endExcluded <= _endExcluded);
    }

    public bool IsEmpty => _endExcluded == Start;
    public static Interval Empty => new Interval(0, 0);

    public override string ToString()
    {
        return IsEmpty ? "[]" : $"[{Start}/{_endExcluded - 1}]";
    }

    public bool IsWithin(int min, int max)
    {
        return (Start >= min) && (_endExcluded < max);
    }
}