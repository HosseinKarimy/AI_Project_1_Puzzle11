using System.Diagnostics;

namespace BiDirectional_Approach
{
    public class BiDirectional
    {
        readonly Node root;
        readonly Node goal;
        readonly Queue<Node> FromRootFrontier = new();
        readonly Queue<Node> FromGoalFrontier = new();
        private Node? commonNode;

        public BiDirectional(string root, string goal)
        {
            this.root = new Node(root, root.IndexOf(' '), null);
            this.goal = new Node(goal, 11, null);
        }

        public void BFS()
        {

            FromRootFrontier.Enqueue(root);
            FromGoalFrontier.Enqueue(goal);

            HashSet<Node> frontierAndExplored_FromRootPath = new();
            HashSet<Node> frontierAndExplored_FromGoalPath = new();

            Stopwatch sw = new();
            sw.Start();

            var tokenSource = new CancellationTokenSource();
            var thread1 = new AutoResetEvent(true);
            var thread2 = new AutoResetEvent(true);
            var FromRootThread = new Thread(() => GraphSearch(FromRootFrontier, frontierAndExplored_FromRootPath, frontierAndExplored_FromGoalPath, tokenSource ,thread1 ,thread2 ));
            var FromGoalThread = new Thread(() => GraphSearch(FromGoalFrontier, frontierAndExplored_FromGoalPath, frontierAndExplored_FromRootPath, tokenSource , thread2,thread1));
            FromRootThread.Start();
            FromGoalThread.Start();

            FromRootThread.Join();
            FromGoalThread.Join();
            tokenSource.Dispose();

            if (commonNode is null)
            {
                Console.WriteLine("Not Found");
                return;
            }

            var nodeFromGoal = frontierAndExplored_FromGoalPath.First(x => x.GetHashCode() == commonNode!.GetHashCode());
            var nodeFromRoot = frontierAndExplored_FromRootPath.First(x => x.GetHashCode() == commonNode!.GetHashCode());

            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime: " + elapsedTime);
            Console.WriteLine($"From Root frontier count = {FromRootFrontier.Count}");
            Console.WriteLine($"From Root explored count = {frontierAndExplored_FromRootPath.Count - FromRootFrontier.Count}");
            Console.WriteLine($"From Goal frontier count = {FromGoalFrontier.Count}");
            Console.WriteLine($"From Goal explored count = {frontierAndExplored_FromGoalPath.Count - FromGoalFrontier.Count}");
            Console.WriteLine("______________________________________________________________________");

            int level = 0;
            PrintResult(nodeFromRoot, ref level);
            PrintResult2(nodeFromGoal.parent!, ref level);

            return;

        }

        void GraphSearch(Queue<Node> frontier, HashSet<Node> frontierAndExplored, HashSet<Node> other, CancellationTokenSource tokenSource, AutoResetEvent myEvent , AutoResetEvent otherEvent)
        {
            while (frontier.Count > 0)
            {
                if (tokenSource.IsCancellationRequested)
                    break;
                var node = frontier.Dequeue();
                myEvent.WaitOne();
                if (other.Contains(node))
                {
                    commonNode = node;
                    otherEvent.Set();
                    tokenSource.Cancel();
                }
                frontierAndExplored.Add(node);
               
                foreach (Node n in node.GetActions())
                {
                    if (frontierAndExplored.Add(n))
                    {
                        frontier.Enqueue(n);
                    }
                }
                otherEvent.Set();
            }
        }

        void PrintResult(Node node, ref int level)
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

        void PrintResult2(Node node, ref int level)
        {
            if (node.parent is null)
            {
                Console.WriteLine("level " + level++ + ":");
                node.Print();
                Console.WriteLine("____________________________________");
                return;
            }
            Console.WriteLine("level " + level++ + ":");
            node.Print();
            Console.WriteLine("____________________________________");
            PrintResult2(node.parent, ref level);
        }

    }
}
