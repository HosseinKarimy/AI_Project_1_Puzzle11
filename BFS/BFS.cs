using System.Diagnostics;

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

    public void TreeSearch()
    {
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
                frontier.Enqueue(n);
            }
        }

        Console.WriteLine("Not Found");
    }

    public void GraphSearch()
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
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("RunTime: " + elapsedTime);
                Console.WriteLine($"frontier count = {frontier.Count}");
                Console.WriteLine($"explored count = {frontierAndExplored.Count - frontier.Count}");
                int level = 0;
                PrintResult(node, ref level);
                return;
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

        Console.WriteLine("Not Found");
    }


    private void PrintResult(Node node, ref int level)
    {
        if (node.parent is null)
        {
            Console.WriteLine("level " + level++ + ":");
            node.Print();
            Console.WriteLine("____________________________________");
            return;
        }
        PrintResult(node.parent, ref level);
        Console.WriteLine("level " + level++ + ":");
        node.Print();
        Console.WriteLine("____________________________________");
    }


}
