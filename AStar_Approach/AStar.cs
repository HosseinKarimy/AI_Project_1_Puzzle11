using System.Diagnostics;

namespace AStar_Approach;

public class AStar
{
    readonly Node root;
    readonly string pattern;
    readonly Node goal = new("123456789AB ", 11, null);

    public AStar(string root, string goal)
    {
        pattern = goal;
        var mappedRoot = MapPuzzle(root, pattern);
        this.root = new Node(mappedRoot, mappedRoot.IndexOf(' '), null);
    }

    public void GraphSearch()
    {
        HashSet<Node> frontierSet = new();
        HashSet<Node> Explored = new();
        PriorityQueue<Node, int> frontier = new();

        frontier.Enqueue(root, root.f);
        frontierSet.Add(root);

        Stopwatch sw = new();
        sw.Start();

        while (frontier.Count > 0)
        {
            var node = frontier.Dequeue();
            _ = frontierSet.TryGetValue(node, out Node? nodeInFrontier);
            frontierSet.Remove(node);
            if (nodeInFrontier!.f < node.f)
                node = nodeInFrontier;

            if (node == goal)
            {
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("RunTime: " + elapsedTime);
                Console.WriteLine($"frontier count = {frontier.Count}");
                Console.WriteLine($"explored count = {Explored.Count}");
                int level = 0;
                PrintResult(node, ref level);
                return;
            }
            Explored.Add(node);

            foreach (Node n in node.GetActions())
            {
                if (frontierSet.Contains(n))
                {
                    _ = frontierSet.TryGetValue(n, out Node? valueInFrontier);
                    if (valueInFrontier!.f <= n.f)
                        continue;
                    else
                    {
                        frontierSet.Remove(valueInFrontier);
                        frontierSet.Add(n);
                    }
                } else if (!Explored.Contains(n))
                {
                    frontier.Enqueue(n,n.f);
                    frontierSet.Add(n);
                }

            }
        }

        Console.WriteLine("not Found");
    }

    public void TreeSearch()
    {
        PriorityQueue<Node, int> frontier = new();

        frontier.Enqueue(root, root.f);

        Stopwatch sw = new();
        sw.Start();

        while (frontier.Count > 0)
        {
            var node = frontier.Dequeue();
            if (node == goal)
            {
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("RunTime: " + elapsedTime);
                Console.WriteLine($"frontier count = {frontier.Count}");
                int level = 0;
                PrintResult(node, ref level);
                return;
            }

            foreach (Node n in node.GetActions())
            {
                    frontier.Enqueue(n, n.f);
            }
        }

        Console.WriteLine("not Found");

    }


    private void PrintResult(Node node, ref int level)
    {
        if (node.parent is null)
        {
            Console.WriteLine("level " + level++ + ":");
            Node.Print(UnMapPuzzle(node.puzzle, pattern!));
            Console.WriteLine("____________________________________");
            return;
        }
        PrintResult(node.parent, ref level);
        Console.WriteLine("level " + level++ + ":");
        Node.Print(UnMapPuzzle(node.puzzle, pattern!));
        Console.WriteLine("____________________________________");
    }

    private static string MapPuzzle(string source, string pattern)
    {
        string mapped = string.Empty;
        foreach (char c in source)
        {
            if (c == ' ')
                mapped += ' ';
            else
            {
                var index = pattern.IndexOf(c);
                pattern = pattern.ReplaceCharAtIndex(index, ' ');
                mapped += index.ToChar();
            }
        }
        return mapped;
    }

    static string UnMapPuzzle(string source, string pattern)
    {
        string UnMapped = string.Empty;
        foreach (char c in source)
        {
            if (c == ' ')
                UnMapped += ' ';
            else
            {
                char ch = pattern[c.ToInt()];
                UnMapped += ch;
            }
        }
        return UnMapped;
    }

}