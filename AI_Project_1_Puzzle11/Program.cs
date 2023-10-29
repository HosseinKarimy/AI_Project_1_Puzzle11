using AI_Project_1_Puzzle11;

Queue<Node> frontier = new();

var rootPuzzle = "566274131 AB";
var goal = new Node(CalculateGoal(rootPuzzle), 11, null);
var root = new Node(rootPuzzle, 9, null);
frontier.Enqueue(root);

GraphSearch();
//TreeSearch();

void TreeSearch()
{
    while (frontier.Count > 0)
    {
        var node = frontier.Dequeue();
        if (node == goal)
        {
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
}

void GraphSearch()
{
    HashSet<Node> frontierAndExplored = new();

    while (frontier.Count > 0)
    {
        var node = frontier.Dequeue();
        if (node == goal)
        {
            Console.WriteLine($"frontier count = {frontier.Count}");
            Console.WriteLine($"explored count = {frontierAndExplored.Count}");
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
}

return 1;


void PrintResult(Node node, ref int level)
{
    if (node.parent is null)
    {
        Console.WriteLine("level: " + level++);
        node.Print();
        Console.WriteLine("____________________________________");
        return;
    }
    PrintResult(node.parent, ref level);
    Console.WriteLine("level: " + level++);
    node.Print();
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