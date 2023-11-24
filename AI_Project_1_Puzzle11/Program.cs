using AStar_Approach;
using BFS_Approach;
using BiDirectional_Approach;

//var rootPuzzle = "566274131 AB";
//var rootPuzzle = "88862146 787";
//var rootPuzzle = "64 85B12793A"; //Accept
//var rootPuzzle = "6B 85312794A"; //Accept
//var rootPuzzle = "678395142 AB"; //Accept
//var rootPuzzle = " 11111111111";
//var rootPuzzle = "123456789AB ";
//var rootPuzzle = "1A386 5B4972";
var rootPuzzle = "2 54B9683A17";   //34
//var rootPuzzle = "9 1532786A4B";


//var bfs = new BFS(rootPuzzle, CalculateGoal(rootPuzzle));
//var BFS_result = bfs.GraphSearch();
//Console.WriteLine(BFS_result.status);
//PrintResult(BFS_result.steps);



var bd = new BiDirectional(rootPuzzle, CalculateGoal(rootPuzzle));
var result = bd.BFS();
Console.WriteLine(result.status);
PrintResult(result.steps);


AStar_Approach.Node.PosArray = FillPosArray(CalculateGoal(rootPuzzle));
var AS = new AStar(rootPuzzle, CalculateGoal(rootPuzzle));
var AStar_result = AS.GraphSearch();
Console.WriteLine(AStar_result.status);
PrintResult(AStar_result.steps);
//AS.TreeSearch();



static string CalculateGoal(string root)
{
    char[] chars = root.ToCharArray();
    Array.Sort(chars);
    chars = chars.Where((c, i) => i != 0).ToArray();
    chars = chars.Append(' ').ToArray();
    return new string(chars);
}

static List<int>[] FillPosArray(string goal)
{
    var array = new List<int>[12];
    for (int i = 0; i < 12; i++)
    {
        (array[goal[i].ToInt()] ??= new List<int>()).Add(i);
    }
    return array;
}

static void PrintPuzzle(string puzzle)
{
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            Console.Write($"{puzzle[i * 4 + j]} ");
        }
        Console.WriteLine();
    }
}

static void PrintResult(List<string> result)
{
    int level = 0;
    foreach (var puzzle in result)
    {
        Console.WriteLine("____________________________________");
        Console.WriteLine("level " + level++ + ":");
        PrintPuzzle(puzzle);
    }
}