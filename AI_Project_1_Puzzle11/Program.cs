using AStar_Approach;
using BFS_Approach;
using BiDirectional_Approach;

//var rootPuzzle = "566274131 AB";
//var rootPuzzle = "15562146 777";
var rootPuzzle = "64 85B12793A";

//var bfs = new BFS(rootPuzzle, CalculateGoal(rootPuzzle));

//bfs.GraphSearch();

var bd = new BiDirectional(rootPuzzle, CalculateGoal(rootPuzzle));
bd.BFS();

var AS = new AStar(rootPuzzle, CalculateGoal(rootPuzzle));
AS.GraphSearch();
//AS.TreeSearch();



static string CalculateGoal(string root)
{
    char[] chars = root.ToCharArray();
    Array.Sort(chars);
    chars = chars.Where((c, i) => i != 0).ToArray();
    chars = chars.Append(' ').ToArray();
    return new string(chars);
}