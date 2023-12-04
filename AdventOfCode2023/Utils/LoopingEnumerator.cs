using System.Collections;

namespace AdventOfCode2023.Utils;

public class LoopingEnumerator<T> : IEnumerator<T>
{
    private readonly IReadOnlyCollection<T> _enumerable;
    private IEnumerator<T> _enumerator;

    public LoopingEnumerator(IReadOnlyCollection<T> enumerable)
    {
        _enumerable = enumerable;
        _enumerator = enumerable.GetEnumerator();
    }

    public bool MoveNext()
    {
        if (_enumerator.MoveNext())
        {
            return true;
        }

        Reset();
        return _enumerator.MoveNext();
    }

    public void Reset()
    {
        _enumerator = _enumerable.GetEnumerator();
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }

    public T Current => _enumerator.Current;
}