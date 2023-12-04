namespace AdventOfCode2023.Utils;

public class IntervalGroup
{
    private readonly List<Interval> _intervals = new List<Interval>();
    public long LengthSum => _intervals.Sum(x => (long) x.Length);

    public IReadOnlyList<Interval> Intervals => _intervals;

    public IntervalGroup Add(Interval toAdd)
    {
        if (toAdd.IsEmpty)
        {
            return this;
        }

        if (_intervals.Count == 0)
        {
            _intervals.Add(toAdd);
            return this;
        }

        var startIndex = _intervals.FindIndex(x => x.End >= toAdd.Start);
        var endIndex = _intervals.FindLastIndex(x => x.Start <= toAdd.End);

        if (startIndex == -1) // after
        {
            _intervals.Add(toAdd);
        }
        else if (endIndex == -1) // before
        {
            _intervals.Insert(0, toAdd);
        }
        else
        {
            if (startIndex == endIndex)
            {
                var newInterval = Interval.FromToIncluded(Math.Min(_intervals[startIndex].Start, toAdd.Start),
                    Math.Max(_intervals[startIndex].End, toAdd.End));
                _intervals[startIndex] = newInterval;
            }
            else if (endIndex < startIndex)
            {
                _intervals.Insert(startIndex, toAdd);
            }
            else
            {
                var newInterval = Interval.FromToIncluded(Math.Min(_intervals[startIndex].Start, toAdd.Start),
                    Math.Max(_intervals[endIndex].End, toAdd.End));
                _intervals[startIndex] = newInterval;
                _intervals.RemoveRange(startIndex + 1, endIndex - startIndex);
            }
        }

        return this;
    }

    public override string ToString()
    {
        return string.Join("", _intervals);
    }

    public IntervalGroup Remove(Interval toRemove)
    {
        if (toRemove.IsEmpty)
        {
            return this;
        }

        if (_intervals.Count == 0)
        {
            return this;
        }

        int i = _intervals.Count - 1;
        while (i >= 0)
        {
            Interval current = _intervals[i];
            if (current.End < toRemove.Start)
            {
                return this;
            }

            if (current.Start > toRemove.End)
            {
                i--;
                continue;
            }

            if (current.End > toRemove.End)
            {

                if (current.Start >= toRemove.Start)
                {
                    _intervals[i] = Interval.FromToIncluded(toRemove.End + 1, current.End);
                }
                else
                {
                    _intervals.Insert(i + 1, Interval.FromToIncluded(toRemove.End + 1, current.End));
                    _intervals[i] = Interval.FromToIncluded(current.Start, toRemove.Start - 1);
                    return this;
                }
            }
            else if (toRemove.Start <= current.Start)
            {
                _intervals.RemoveAt(i);
            }
            else
            {
                _intervals[i] = Interval.FromToIncluded(current.Start, toRemove.Start - 1);
                return this;
            }

            i--;
        }

        return this;
    }
}