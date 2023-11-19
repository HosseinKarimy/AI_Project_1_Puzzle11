using AI_Project_1_Puzzle11;
using System.Diagnostics;

PriorityQueue<Node, int> frontier = new();

var rootPuzzle = "566274131 AB";
//var rootPuzzle = "15562146 777";
var realGoal = CalculateGoal(rootPuzzle);
var MappedRoot = MapPuzzle(rootPuzzle, realGoal);

var root = new Node(MappedRoot, MappedRoot.IndexOf(' '), null);
var goal = new Node(CalculateGoal(MappedRoot), 11, null);

frontier.Enqueue(root, root.Manhattan);


Stopwatch sw = new();
sw.Start();

GraphSearch();
//TreeSearch();



void TreeSearch()
{
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
            frontier.Enqueue(n, n.Manhattan);
        }
    }
}

void GraphSearch()
{
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
                frontier.Enqueue(n, n.Manhattan);
            }
        }

    }
}

return 1;


void PrintResult(Node node, ref int level)
{
    if (node.parent is null)
    {
        Console.WriteLine("level " + level++ + ":");
        Node.Print(UnMapPuzzle(node.puzzle, realGoal!));
        Console.WriteLine("____________________________________");
        return;
    }
    PrintResult(node.parent, ref level);
    Console.WriteLine("level " + level++ + ":");
    Node.Print(UnMapPuzzle(node.puzzle, realGoal!));
    Console.WriteLine("____________________________________");
}

static string CalculateGoal(string root)
{
    char[] chars = root.ToCharArray();
    Array.Sort(chars);
    chars = chars.Where((c, i) => i != 0).ToArray();
    chars = chars.Append(' ').ToArray();
    return new string(chars);
}


string MapPuzzle(string source, string pattern)
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

string UnMapPuzzle(string source, string pattern)
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

