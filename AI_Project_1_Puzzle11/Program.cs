using BFS_Approach;
using BiDirectional_Approach;

var rootPuzzle = "566274131 AB";
//var rootPuzzle = "15562146 777";

var bfs = new BFS(rootPuzzle, CalculateGoal(rootPuzzle));

bfs.GraphSearch();

var bd = new BiDirectional(rootPuzzle, CalculateGoal(rootPuzzle));
bd.BFS();



static string CalculateGoal(string root)
{
    char[] chars = root.ToCharArray();
    Array.Sort(chars);
    chars = chars.Where((c, i) => i != 0).ToArray();
    chars = chars.Append(' ').ToArray();
    return new string(chars);
}