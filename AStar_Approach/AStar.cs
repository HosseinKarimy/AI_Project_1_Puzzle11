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
        this.goal = new Node(goal, goal.IndexOf(' '), null);
        this.root = new Node(root, root.IndexOf(' '), null);
    }

    public (string? status, List<string>? steps) GraphSearch()
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
                var status = new StringBuilder();
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                status.AppendLine("** AStar Graph Approach **");
                status.AppendLine("RunTime: " + elapsedTime);
                status.AppendLine($"frontier count = {frontier.Count}");
                status.AppendLine($"explored count = {Explored.Count}");
                return (status.ToString(), CalculatePath(node));
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

        return (null,null);
    }

    public static List<string> CalculatePath(Node? lastNode)
    {
        List<string> result = new();
        while (lastNode is not null)
        {
            result.Add(lastNode.puzzle);
            lastNode = lastNode.parent;
        }
        return result;
    }
}