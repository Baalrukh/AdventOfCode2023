namespace AdventOfCode2023;

public class Day07 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        return Execute(lines, false);
    }

    private long Execute(string[] lines, bool useJokers)
    {
        var orderedEnumerable = lines.Select(x => ParseLine(x, useJokers)).OrderBy(x => x.hand).ToList();
        return orderedEnumerable.Select(x => x.bid)
            .Aggregate((num: 1, sum: 0L), (res, bid) => (num: res.num + 1, sum: res.sum + res.num * bid)).sum;
    }

    private (Hand hand, long bid) ParseLine(string line, bool useJokers)
    {
        var tokens = line.Split(' ');
        return (Hand.Parse(tokens[0], useJokers), long.Parse(tokens[1]));
    }

    public long ExecutePart2(string[] lines)
    {
        return Execute(lines, true);
    }

    public enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
    }

    public class Hand : IComparable<Hand>
    {
        private readonly string _originalHand;
        private readonly string _sortingHand;
        public HandType HandType { get; }

        private Hand(string hand, string sortingHand)
        {
            _originalHand = hand;
            _sortingHand = sortingHand;
            HandType = GetHandType(sortingHand);
        }

        public static Hand Parse(string text, bool useJokers = false)
        {
            var sortingText = text.Replace('T', (char)('9' + 1))
                .Replace('J', useJokers ? '0' : (char)('9' + 2))
                .Replace('Q', (char)('9' + 3))
                .Replace('K', (char)('9' + 4))
                .Replace('A', (char)('9' + 5));
            return new Hand(text, sortingText);
        }

        private static HandType GetHandType(string hand)
        {
            var orderedGroups = hand.GroupBy(x => x).Where(x => x.Key != '0').Select(x => x.Count()).OrderByDescending(x => x).ToList();
            var jokerCount = 5 - orderedGroups.Sum();
            if (jokerCount == 5)
            {
                orderedGroups.Add(5);
            }
            else
            {
                orderedGroups[0] += jokerCount;
            }
            var pattern = string.Join("", orderedGroups);

            switch (pattern)
            {
                case "5": return HandType.FiveOfAKind;
                case "41": return HandType.FourOfAKind;
                case "32": return HandType.FullHouse;
                case "311": return HandType.ThreeOfAKind;
                case "221": return HandType.TwoPair;
                case "2111": return HandType.OnePair;
                default: return HandType.HighCard;
            }
        }

        public int CompareTo(Hand? other)
        {
            int compare = HandType.CompareTo(other.HandType);
            if (compare != 0)
            {
                return compare;
            }

            return String.Compare(_sortingHand, other._sortingHand, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return _originalHand;
        }
    }
}
