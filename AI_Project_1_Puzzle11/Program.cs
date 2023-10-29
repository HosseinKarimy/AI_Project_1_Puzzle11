using AI_Project_1_Puzzle11;

Queue<Node> frontier = new();
var goal = new Node("123456789AB ",11, null);

var root = new Node("123456798 AB",9, null);
frontier.Enqueue(root);

GraphSearch();
//TreeSearch();

//void TreeSearch()
//{
//    while (frontier.Count > 0)
//    {
//        var node = frontier.Dequeue();
//        if (node.Value == goal)
//        {
//            Console.WriteLine($"frontier count = {frontier.Count}");
//            PrintResult(node);
//            return;
//        }

//        var parentValue = node.Value;
//        double newValue;

//        // *5 
//        newValue = parentValue * 5;
//        frontier.Enqueue(new Node(newValue, Operator.MultipleOn5, node));

//        // sqrt
//        newValue = Math.Sqrt(parentValue);
//        frontier.Enqueue(new Node(newValue, Operator.Sqrt, node));

//        // floor
//        newValue = Math.Floor(parentValue);
//        if (newValue != parentValue)
//            frontier.Enqueue(new Node(newValue, Operator.Floor, node));
//    }
//}

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


void PrintResult(Node node,ref int level)
{
    if (node.parent is null)
    {
        Console.WriteLine("start = " + node.puzzle);
        return;
    }
    PrintResult(node.parent, ref level);
    Console.WriteLine("level: " + ++level);
    node.Print();
    Console.WriteLine("____________________________________");
}