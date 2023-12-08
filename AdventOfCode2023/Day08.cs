using System.Diagnostics;
using AdventOfCode2023.Utils;

namespace AdventOfCode2023;

public class Day08 : Exercise
{
    public long ExecutePart1(string[] lines)
    {
        var nodes = lines.Skip(2).Select(x =>
                x.Split(new[] { '=', ' ', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(x => x[0], x => new Node(x[0], x[1], x[2]));
        var loopingEnumerator = new LoopingEnumerator<char>(lines[0].ToArray());

        int steps = 0;
        string currentNode = "AAA";
        while (currentNode != "ZZZ")
        {
            steps++;
            loopingEnumerator.MoveNext();
            currentNode = nodes[currentNode].GetNext(loopingEnumerator.Current);
        }
        
        
        return steps;
    }

    public long ExecutePart2(string[] lines)
    {
        var nodes = lines.Skip(2).Select(x =>
                x.Split(new[] { '=', ' ', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => new Node(x[0], x[1], x[2]))
            .ToDictionary(x => x.Name, x => x);
        var loopingEnumerator = new LoopingEnumerator<char>(lines[0].ToArray());

        var currentNodes = nodes.Values.Where(x => x.IsStart).ToArray();
        List<long> periods = new();

        foreach (Node node in currentNodes)
        {
            int steps = 0;
            Dictionary<Node, long> seenNodes = new ();

            Node currentNode = node;
            long lastSeen = 0;
            while (true)
            {
                loopingEnumerator.MoveNext();
                currentNode = nodes[currentNode.GetNext(loopingEnumerator.Current)];
                steps++;
                
                if (currentNode.IsEnd)
                {
                    if (!seenNodes.TryAdd(currentNode, steps))
                    {
                        break;
                    }
                }
            }

            long firstSeenTime = seenNodes[currentNode];
            long period = steps - firstSeenTime;
            periods.Add(period);
        }

        return periods.Aggregate(1L, (res, p) => res * p / (GCD(res, p)));
    }

    private static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
    
    public class Node
    {
        public readonly string Name;
        public readonly string Left;
        public readonly string Right;

        public Node(string name, string left, string right)
        {
            Left = left;
            Right = right;
            Name = name;
        }

        public string GetNext(char c)
        {
            return c == 'L' ? Left : Right;
        }

        public bool IsStart => Name.EndsWith('A');
        public bool IsEnd => Name.EndsWith('Z');
    }

}
