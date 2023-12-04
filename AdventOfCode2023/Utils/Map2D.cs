using System.Text;

namespace AdventOfCode2023.Utils;

public class Map2D<T>
{
    private readonly T[,] _values;
    public readonly int Width;
    public readonly int Height;

    private Map2D(T[,] values, int width, int height)
    {
        _values = values;
        Width = width;
        Height = height;
    }

    public T this[IntVector2 position]
    {
        get => _values[position.X + 1, position.Y + 1];
        set => _values[position.X + 1, position.Y + 1] = value;
    }

    public T this[int x, int y]
    {
        get => _values[x + 1, y + 1];
        set => _values[x + 1, y + 1] = value;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder((Width + 1) * Height);
        for (int y = 1; y < Height + 1; y++)
        {
            for (int x = 1; x < Width + 1; x++)
            {
                stringBuilder.Append(_values[x, y]);
            }

            stringBuilder.Append('\n');
        }

        return stringBuilder.ToString();
    }

    public void CopyTo(Map2D<T> other, int startX, int startY)
    {
        int endX = Math.Min(Width, other.Width - startX);
        int endY = Math.Min(Height, other.Height - startY);

        for (int y = 0; y < endY; y++)
        {
            for (int x = 0; x < endX; x++)
            {
                other._values[x + startX + 1, y + startY + 1] = _values[x + 1, y + 1];
            }
        }
    }

    public IEnumerable<IntVector2> EnumeratePositions(Predicate<T> predicate)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (predicate(this[x, y]))
                {
                    yield return new IntVector2(x, y);
                }
            }
        }
    }

    public static Map2D<T> Create(int width, int height, Func<IntVector2, T> elementFactory, Func<T> borderValueFactory)
    {
        var map = new T[width + 2, height + 2];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x + 1, y + 1] = elementFactory(new IntVector2(x, y));
            }
        }

        for (int y = 0; y < height; y++)
        {
            map[0, y + 1] = borderValueFactory();
            map[width + 1, y + 1] = borderValueFactory();
        }

        for (int x = 0; x < width; x++)
        {
            map[x + 1, 0] = borderValueFactory();
            map[x + 1, height + 1] = borderValueFactory();
        }

        map[0, 0] = borderValueFactory();
        map[width + 1, 0] = borderValueFactory();
        map[width + 1, height + 1] = borderValueFactory();
        map[0, height + 1] = borderValueFactory();

        return new Map2D<T>(map, width, height);
    }

    public static Map2D<T> Parse(string[] lines, Func<char, T> elementFactory, Func<T> borderValueFactory)
    {
        int width = lines[0].Length;
        int height = lines.Length;

        var map = new T[width + 2, height + 2];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x + 1, y + 1] = elementFactory(lines[y][x]);
            }
        }

        for (int y = 0; y < height; y++)
        {
            map[0, y + 1] = borderValueFactory();
            map[width + 1, y + 1] = borderValueFactory();
        }

        for (int x = 0; x < width; x++)
        {
            map[x + 1, 0] = borderValueFactory();
            map[x + 1, height + 1] = borderValueFactory();
        }

        map[0, 0] = borderValueFactory();
        map[width + 1, 0] = borderValueFactory();
        map[width + 1, height + 1] = borderValueFactory();
        map[0, height + 1] = borderValueFactory();

        return new Map2D<T>(map, width, height);
    }

    public bool IsInside(IntVector2 position)
    {
        return (position.X >= 0)
               && (position.X < Width)
               && (position.Y >= 0)
               && (position.Y < Height);
    }
}