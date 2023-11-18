using AI_Project_1_Puzzle11;
using System.Diagnostics;
using System.Xml.Linq;

Queue<Node> FromRootFrontier = new();
Queue<Node> FromGoalFrontier = new();

//var rootPuzzle = "566274131 AB";
var rootPuzzle = "5263478 3453";
var goal = new Node(CalculateGoal(rootPuzzle), 11, null);
var root = new Node(rootPuzzle, rootPuzzle.IndexOf(' '), null);
Node commonNode = null;
FromRootFrontier.Enqueue(root);
FromGoalFrontier.Enqueue(goal);

HashSet<Node> frontierAndExplored_FromRootPath = new();
HashSet<Node> frontierAndExplored_FromGoalPath = new();


Stopwatch sw = new();
sw.Start();


var tokenSource = new CancellationTokenSource();
var FromRootThread = new Thread(() => GraphSearch(FromRootFrontier, frontierAndExplored_FromRootPath, frontierAndExplored_FromGoalPath));
var FromGoalThread = new Thread(() => GraphSearch(FromGoalFrontier, frontierAndExplored_FromGoalPath, frontierAndExplored_FromRootPath));
FromRootThread.Start();
FromGoalThread.Start();


void GraphSearch(Queue<Node> frontier, HashSet<Node> frontierAndExplored, HashSet<Node> other)
{

    while (frontier.Count > 0)
    {
        var node = frontier.Dequeue();
        if (other.Contains(node))
        {
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            commonNode = node;

            tokenSource.Cancel();
            if (tokenSource.IsCancellationRequested)
                break;
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
}

FromRootThread.Join();
FromGoalThread.Join();
tokenSource.Dispose();

Console.WriteLine(commonNode?.puzzle);
var nodeFromGoal = frontierAndExplored_FromGoalPath.First(x=> x.GetHashCode() == commonNode!.GetHashCode());
var nodeFromRoot = frontierAndExplored_FromRootPath.First(x => x.GetHashCode() == commonNode!.GetHashCode());

int level = 0;
PrintResult(nodeFromRoot, ref level);
PrintResult2(nodeFromGoal, ref level);

return 1;

//Console.WriteLine("RunTime: " + elapsedTime);
//Console.WriteLine($"frontier count = {frontier.Count}");
//Console.WriteLine($"explored count = {frontierAndExplored.Count - frontier.Count}");



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

static string CalculateGoal(string root)
{
    char[] chars = root.ToCharArray();
    Array.Sort(chars);
    chars = chars.Where((c, i) => i != 0).ToArray();
    chars = chars.Append(' ').ToArray();
    return new string(chars);
}
