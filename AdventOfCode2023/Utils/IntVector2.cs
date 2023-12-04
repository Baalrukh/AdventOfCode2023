namespace AdventOfCode2023.Utils;

public struct IntVector2
{
    public readonly int X;
    public readonly int Y;

    public IntVector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y);

    public bool Equals(IntVector2 other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is IntVector2 other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (X * 397) ^ Y;
        }
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.X + b.X, a.Y + b.Y);
    }

    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.X - b.X, a.Y - b.Y);
    }

    public static IntVector2 operator *(IntVector2 a, int amount)
    {
        return new IntVector2(a.X * amount, a.Y * amount);
    }

    public static IntVector2 operator *(int amount, IntVector2 a)
    {
        return new IntVector2(a.X * amount, a.Y * amount);
    }

    public static IntVector2 operator /(IntVector2 a, int amount)
    {
        return new IntVector2(a.X / amount, a.Y / amount);
    }

    public static bool operator ==(IntVector2 a, IntVector2 b)
    {
        return (a.X == b.X) && (a.Y == b.Y);
    }

    public static bool operator !=(IntVector2 a, IntVector2 b)
    {
        return (a.X != b.X) || (a.Y != b.Y);
    }
}