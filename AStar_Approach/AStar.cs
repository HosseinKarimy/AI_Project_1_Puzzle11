using System.Diagnostics;
using System.Text;
using System.Xml.XPath;

namespace AStar_Approach;

public class AStar
{
    readonly Node root;
    readonly Node goal;

    public AStar(string root, string goal)
    {
        this.goal = new Node(goal, goal.IndexOf(' '), null); ;
        this.root = new Node(root, root.IndexOf(' '), null);
    }

    public List<string> GraphSearch()
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
                return CalculatePath(node);
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
        return new List<string>();
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


    public List<string> CalculatePath(Node? lastNode)
    {
        List<string> result = new();
        while (lastNode is not null)
        {
            result.Add(lastNode.puzzle);
            lastNode = lastNode.parent;
        }
        return result;
    }

    private void PrintResult(Node node, ref int level)
    {
        if (node.parent is null)
        {
            Console.WriteLine("level " + level++ + ":");
            Node.Print(node.puzzle);
            Console.WriteLine("____________________________________");
            return;
        }
        PrintResult(node.parent, ref level);
        Console.WriteLine("level " + level++ + ":");
        Node.Print(node.puzzle);
        Console.WriteLine("____________________________________");
    }

    public static void PrintResult(List<string> result)
    {
        result.Reverse();
        int level = 0;
        foreach (var puzzle in result)
        {
            Console.WriteLine("____________________________________");
            Console.WriteLine("level " + level++ + ":");
            Node.Print(puzzle);
        }
    }
}