using System.Diagnostics;
using System.Text;

namespace BFS_Approach;

public class BFS
{

    readonly Queue<Node> frontier = new();
    readonly Node root;
    readonly Node goal;

    public BFS(string root, string goal)
    {
        this.root = new Node(root, root.IndexOf(' '), null);
        this.goal = new Node(goal, 11, null);
        frontier.Enqueue(this.root);
    }

    public (string? status, List<string>? steps) GraphSearch()
    {
        Stopwatch sw = new();
        sw.Start();

        HashSet<Node> frontierAndExplored = new();

        while (frontier.Count > 0)
        {
            var node = frontier.Dequeue();
            if (node == goal)
            {
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                var status = new StringBuilder();
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                status.AppendLine("RunTime: " + elapsedTime);
                status.AppendLine($"frontier count = {frontier.Count}");
                status.AppendLine($"explored count = {frontierAndExplored.Count - frontier.Count}");
                return (status.ToString(), CalculatePath(node));
            }
            frontierAndExplored.Add(node);

            foreach (Node n in node.GetActions())
            {
                if (frontierAndExplored.Add(n))
                {
                    frontier.Enqueue(n);
                }
            }
        }
        return (null, null);
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
