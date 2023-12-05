namespace AdventOfCode2023.Utils;

public readonly struct LongInterval
{
    public readonly long Start;
    private readonly long _endExcluded;
    public long End => _endExcluded - 1;

    public long EndExcluded => _endExcluded;

    private LongInterval(long start, long endExcluded)
    {
        Start = start;
        _endExcluded = endExcluded;
    }

    public static LongInterval FromToIncluded(long fromIncluded, long toIncluded)
    {
        return new LongInterval(fromIncluded, toIncluded + 1);
    }

    public static LongInterval FromToExcluding(long fromIncluded, long toExcluded)
    {
        return new LongInterval(fromIncluded, toExcluded);
    }

    public static LongInterval FromLengthExcluding(long fromIncluded, long length)
    {
        return new LongInterval(fromIncluded, fromIncluded + length);
    }

    public long Length => _endExcluded - Start;

    public bool IsInside(long value)
    {
        return (Start <= value) && (value < _endExcluded);
    }

    public bool Intersects(LongInterval other)
    {
        return (other.Start < _endExcluded) && (other._endExcluded > Start);
    }

    public bool IsInside(LongInterval other)
    {
        return (other.Start >= Start) && (other._endExcluded <= _endExcluded);
    }

    public bool IsEmpty => _endExcluded == Start;
    public static LongInterval Empty => new LongInterval(0, 0);

    public override string ToString()
    {
        return IsEmpty ? "[]" : $"[{Start}/{_endExcluded - 1}]";
    }

    public bool IsWithin(long min, long max)
    {
        return (Start >= min) && (_endExcluded < max);
    }
}