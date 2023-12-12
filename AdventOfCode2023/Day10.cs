using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day10 : Exercise
{
    public static IntVector2 NORTH = new IntVector2(0, -1);
    public static IntVector2 EAST = new IntVector2(1, 0);
    public static IntVector2 SOUTH = new IntVector2(0, 1);
    public static IntVector2 WEST = new IntVector2(-1, 0);
    
    private static IntVector2[] DirectionVectors = new[]
    {
        NORTH, EAST, SOUTH, WEST
    };

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static Dictionary<char, Pipe?> Pipes = new()
    {
        { '7', new Pipe('7', Direction.East, Direction.South) },
        { 'J', new Pipe('J', Direction.South, Direction.West) },
        { 'L', new Pipe('L', Direction.West, Direction.North) },
        { 'F', new Pipe('F', Direction.North, Direction.East) },
        { '-', new Pipe('-', Direction.East, Direction.East) },
        { '|', new Pipe('|', Direction.North, Direction.North) },
        { '.', null }
    };
    
    public long ExecutePart1(string[] lines)
    {
        Map2D<char> map = Map2D<char>.Parse(lines, c => c, () => '.');
        IntVector2 startPosition = FindStartPosition(lines);
        var startDirection = Enum.GetValues<Direction>()
                         .First(x => Pipes[map[startPosition + DirectionVectors[(int)x]]]?.CanEnter(x) ?? false);

        PipePosition position = new PipePosition(startPosition + DirectionVectors[(int)startDirection], startDirection, 0);
        while (position.Position != startPosition)
        {
            position = Pipes[map[position.Position]].Advance(position);
        } 
        return (position.StepCount + 1) / 2;
    }

    public static IntVector2 FindStartPosition(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int index = line.IndexOf('S');
            if (index != -1)
            {
                return new IntVector2(index, i);
            }
        }

        throw new Exception();
    }

    public long ExecutePart2(string[] lines)
    {
        Map2D<char> map = Map2D<char>.Parse(lines, c => c, () => '.');
        IntVector2 startPosition = FindStartPosition(lines);
        var startDirection = Enum.GetValues<Direction>()
                                 .First(x => Pipes[map[startPosition + DirectionVectors[(int)x]]]?.CanEnter(x) ?? false);

        List<IntVector2> allPipePositions = new List<IntVector2>(); 
        PipePosition position = new PipePosition(startPosition + DirectionVectors[(int)startDirection], startDirection, 0);
        allPipePositions.Add(position.Position);
        while (position.Position != startPosition)
        {
            position = Pipes[map[position.Position]].Advance(position);
            allPipePositions.Add(position.Position);
        }

        LoopPipe(map, startPosition, startDirection, position);
        List<List<IntVector2>> orderedPositions =
            allPipePositions.GroupBy(p => p.Y).Select(g => g.OrderBy(p => p.X).ToList())
                            .OrderBy(l => l[0].Y)
                            .ToList();

        
        int insideCount = 0;
        foreach (List<IntVector2> linePipes in orderedPositions)
        {
            foreach (var interval in EnumeratePipeIntervals(map, linePipes))
            {
                insideCount += interval.Length - 1;
            }
        }

        return insideCount;
    }

    public static IEnumerable<Interval> EnumeratePipeIntervals(Map2D<char> map, List<IntVector2> linePipes)
    {
        bool isInside = false;
        int x = 0;
        char endPipeChar;
        IEnumerator<IntVector2> enumerator = linePipes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            char c = map[enumerator.Current];
            switch (c)
            {
                case '|':
                    if (isInside)
                    {
                        yield return Interval.FromToExcluding(x, enumerator.Current.X);
                        isInside = false;
                    }
                    else
                    {
                        isInside = true;
                        x = enumerator.Current.X;
                    }

                    break;
                case 'F':
                    if (isInside)
                    {
                        yield return Interval.FromToExcluding(x, enumerator.Current.X);
                    }

                    endPipeChar = AdvanceUntilHorizontalPipeEnd(map, enumerator);
                    x = enumerator.Current.X;
                    if (endPipeChar == 'J') // crosses
                    {
                        isInside = !isInside;
                    }

                    break;

                case 'L':
                    if (isInside)
                    {
                        yield return Interval.FromToExcluding(x, enumerator.Current.X);
                    }

                    endPipeChar = AdvanceUntilHorizontalPipeEnd(map, enumerator);
                    x = enumerator.Current.X;
                    if (endPipeChar == '7') // crosses
                    {
                        isInside = !isInside;
                    }

                    break;
            }
        }
    }

    private static char AdvanceUntilHorizontalPipeEnd(Map2D<char> map, IEnumerator<IntVector2> enumerator)
    {
        while (enumerator.MoveNext())
        {
            var c = map[enumerator.Current];
            if (c != '-')
            {
                return c;
            }
        }

        throw new Exception();
    }

    private static void LoopPipe(Map2D<char> map, IntVector2 startPosition, Direction startDirection, PipePosition endPosition)
    {
        map[startPosition] = GetStartPipeChar(startDirection, endPosition.Direction);
    }   
    private static char GetStartPipeChar(Direction startDirection, Direction endDirection)
    {
        return 'F'; // TODO do not hardcode start pipe
        // List<Direction> directions = new() { startDirection, endDirection };
        // directions.Sort();
        // char c = '.';
        // switch (directions[0])
        // {
        //     case Direction.North:
        //         switch (directions[1])
        //         {
        //             case Direction.East: return 'L';
        //             case Direction.South: return '|';
        //             case Direction.West: return 'J';
        //             default: throw new Exception();
        //         }
        //     case Direction.East:
        //         switch (directions[1])
        //         {
        //             case Direction.South: return 'F';
        //             case Direction.West: return '-';
        //             default: throw new Exception();
        //         }
        //     case Direction.South:
        //         return '7';
        // }
        //
        // throw new Exception();
    }

    public struct PipePosition
    {
        public readonly IntVector2 Position;
        public readonly Direction Direction;
        public readonly int StepCount;

        public PipePosition(IntVector2 position, Direction direction, int stepCount)
        {
            Position = position;
            Direction = direction;
            StepCount = stepCount;
        }

        public bool Equals(PipePosition other)
        {
            return Position.Equals(other.Position) && Direction == other.Direction && StepCount == other.StepCount;
        }

        public override bool Equals(object? obj)
        {
            return obj is PipePosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, (int)Direction, StepCount);
        }

        public override string ToString()
        {
            return $"{Position}, {Direction}, {StepCount}";
        }
    }

    private static Direction GetOppositeDirection(Direction direction)
    {
        return (Direction)(((int)direction + 2) % 4);
    }
    
    public class Pipe
    {
        public readonly char Symbol;
        public readonly Direction Entrance;
        public readonly Direction Exit;

        public Pipe(char symbol, Direction entrance, Direction exit)
        {
            Symbol = symbol;
            Entrance = entrance;
            Exit = exit;
        }

        public bool CanEnter(Direction direction)
        {
            return (direction == Entrance) || (direction == GetOppositeDirection(Exit));
        }

        public PipePosition Advance(PipePosition position)
        {
            if (position.Direction == Entrance)
            {
                return new PipePosition(position.Position + DirectionVectors[(int)Exit], Exit,
                                        position.StepCount + 1);
            }

            Direction oppositeDirection = GetOppositeDirection(Entrance);
            return new PipePosition(position.Position + DirectionVectors[(int)oppositeDirection], oppositeDirection,
                                    position.StepCount + 1);
        }
    }
}
