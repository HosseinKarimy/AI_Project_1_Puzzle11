using AI_Project_1_Puzzle11;

var node1 = new Node(new int?[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, null },11);
var node2 = new Node(new int?[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 , null }, 11);

Console.WriteLine(node1.GetHashCode());
Console.WriteLine(node2.GetHashCode());

Console.WriteLine(node1 == node2);

HashSet<Node> explored = new();

