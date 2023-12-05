using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day05 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        var seeds = lines[0].Split(new[] {':', ' '}, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToList();

        List<PlantationMap> plantationMaps = ParseMaps(lines.Skip(2)).ToList();
        const string source = "seed";
        const string destination = "location";
        return seeds.Select(x => GetValue(plantationMaps, x, source, destination)).Min();
    }

    private long GetValue(List<PlantationMap> plantationMaps, long sourceValue, string source, string destination)
    {
        var plantationMap = plantationMaps.First(x => x.SourceName == source);
        long destinationValue = plantationMap.GetValue(sourceValue);
        if (plantationMap.DestinationName == destination)
        {
            return destinationValue;
        }

        return GetValue(plantationMaps, destinationValue, plantationMap.DestinationName, destination);
    }

    private IEnumerable<PlantationMap> ParseMaps(IEnumerable<string> lines)
    {
        var enumerator = lines.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var header = enumerator.Current;
            var tokens = header.Split(new[] {'-', ' '});
            var sourceName = tokens[0];
            var destinationName = tokens[2];
            List<PlantationRange> ranges = new List<PlantationRange>();
            while (enumerator.MoveNext())
            {
                if (string.IsNullOrEmpty(enumerator.Current))
                {
                    break;
                }

                var rangeTokens = enumerator.Current.Split(' ');
                ranges.Add(new PlantationRange(long.Parse(rangeTokens[1]), long.Parse(rangeTokens[0]), long.Parse(rangeTokens[2])));
            }

            ranges.Sort((a, b) => a.SourceInterval.Start.CompareTo(b.SourceInterval.Start));
            yield return new PlantationMap(sourceName, destinationName, ranges);
        }
    }

    public long ExecutePart2(string[] lines)
    {
        var rawSeeds = lines[0].Split(new[] {':', ' '}, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToList();
        var seedRanges = rawSeeds.Batch(2).Select(x => LongInterval.FromToExcluding(x[0], x[0] + x[1])).ToList();
        List<PlantationMap> plantationMaps = ParseMaps(lines.Skip(2)).ToList();
        const string source = "seed";
        const string destination = "location";
        return seedRanges.SelectMany(x => GetIntervals(plantationMaps, x, source, destination)).Select(x => x.Start).Min();
    }

    private IEnumerable<LongInterval> GetIntervals(List<PlantationMap> plantationMaps, LongInterval sourceValue, string source, string destination)
    {
        var plantationMap = plantationMaps.First(x => x.SourceName == source);
        IEnumerable<LongInterval> destinationIntervals = plantationMap.GetIntervals(sourceValue);
        if (plantationMap.DestinationName == destination)
        {
            return destinationIntervals;
        }

        return destinationIntervals.SelectMany(x => GetIntervals(plantationMaps, x, plantationMap.DestinationName, destination));
    }

    public class PlantationMap
    {
        public readonly string SourceName;
        public readonly string DestinationName;
        public readonly IReadOnlyList<PlantationRange> ConversionRanges;

        public PlantationMap(string sourceName, string destinationName, IReadOnlyList<PlantationRange> conversionRanges)
        {
            SourceName = sourceName;
            DestinationName = destinationName;
            ConversionRanges = conversionRanges;
        }

        public long GetValue(long sourceValue)
        {
            foreach (var conversionRange in ConversionRanges)
            {
                if (conversionRange.TryConvertValue(sourceValue, out var destinationValue))
                {
                    return destinationValue;
                }
            }

            return sourceValue;
        }

        public IEnumerable<LongInterval> GetIntervals(LongInterval sourceInterval)
        {
            LongInterval? remaining = sourceInterval;
            foreach (var conversionRange in ConversionRanges)
            {
                LongInterval? converted = conversionRange.ConvertRange(remaining.Value, out remaining);
                if (converted.HasValue)
                {
                    yield return converted.Value;
                }

                if (remaining == null)
                {
                    yield break;
                }
            }

            yield return remaining.Value;
        }
    }

    public class PlantationRange
    {
        public readonly LongInterval SourceInterval;
        public readonly long DestinationStart;

        public PlantationRange(long sourceStart, long destinationStart, long length)
        {
            SourceInterval = LongInterval.FromLengthExcluding(sourceStart, length);
            DestinationStart = destinationStart;
        }

        public bool TryConvertValue(long value, out long destinationValue)
        {
            if (!SourceInterval.IsInside(value))
            {
                destinationValue = 0;
                return false;
            }

            destinationValue = value - SourceInterval.Start + DestinationStart;
            return true;
        }


        public LongInterval? ConvertRange(LongInterval range, out LongInterval? remainingRange)
        {
            if (range.End < SourceInterval.Start)
            {
                remainingRange = null;
                return range;
            }

            if (range.Start > SourceInterval.End)
            {
                remainingRange = range;
                return null;
            }

            if (range.End > SourceInterval.End)
            {
                remainingRange = LongInterval.FromToExcluding(SourceInterval.End + 1, range.End);
            }
            else
            {
                remainingRange = null;
            }

            long start = Math.Max(SourceInterval.Start, range.Start);
            long end = Math.Min(SourceInterval.End, range.End);

            return LongInterval.FromToExcluding(start - SourceInterval.Start + DestinationStart,
                end - SourceInterval.Start + DestinationStart);
        }
    }
}
