namespace AdventOfCode2023.Utils;

public struct IntVector3
{
    public readonly int X;
    public readonly int Y;
    public readonly int Z;

    public IntVector3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public bool Equals(IntVector3 other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object? obj)
    {
        return obj is IntVector3 other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = X;
            hashCode = (hashCode * 397) ^ Y;
            hashCode = (hashCode * 397) ^ Z;
            return hashCode;
        }
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public int LengthSq => X * X + Y * Y + Z * Z;

    public int this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return X;
                case 1: return Y;
                case 2: return Z;
                default: throw new ArgumentException("Invalid Index " + index);
            }
        }
    }

    public static IntVector3 operator +(IntVector3 a, IntVector3 b)
    {
        return new IntVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    public static IntVector3 operator -(IntVector3 a, IntVector3 b)
    {
        return new IntVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    public static IntVector3 operator *(IntVector3 a, int amount)
    {
        return new IntVector3(a.X * amount, a.Y * amount, a.Z * amount);
    }

    public static IntVector3 operator *(int amount, IntVector3 a)
    {
        return new IntVector3(a.X * amount, a.Y * amount, a.Z * amount);
    }

    public static IntVector3 operator /(IntVector3 a, int amount)
    {
        return new IntVector3(a.X / amount, a.Y / amount, a.Z / amount);
    }

    public static IntVector3 operator -(IntVector3 a)
    {
        return new IntVector3(-a.X, -a.Y, -a.Z);
    }

    public static bool operator ==(IntVector3 a, IntVector3 b)
    {
        return (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z);
    }

    public static bool operator !=(IntVector3 a, IntVector3 b)
    {
        return (a.X != b.X) || (a.Y != b.Y) || (a.Z != b.Z);
    }
}