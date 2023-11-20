namespace AStar_Approach;

public class Node
{
    public readonly string puzzle;
    public readonly int spaceIndex;
    public readonly int f;
    public readonly Node? parent;

    public Node(string puzzle, int spaceIndex, Node? parent)
    {
        this.puzzle = puzzle;
        this.spaceIndex = spaceIndex;
        this.parent = parent;
        f = CalculateManhattanDistance() + parent?.f + 1 ?? 0;
    }

    private int CalculateManhattanDistance()
    {
        int ManhattanDistance = 0;
        for (int i = 0; i < 12; i++)
        {
            if (puzzle[i] == ' ')
            {
                continue;
            }
            ManhattanDistance += FindManhattan(i.FindXY(), puzzle[i].ToInt().FindXY());
        }
        return ManhattanDistance;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(puzzle, spaceIndex);
    }

    public static bool operator ==(Node? left, Node? right)
    {
        if (left is null || right is null)
            return false;
        return left.GetHashCode() == right.GetHashCode();
    }

    public static bool operator !=(Node? left, Node? right)
    {
        if (left is null || right is null)
            return true;
        return left!.GetHashCode() != right!.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is Node other)
        {
            if (other == null)
                return false;
            if (spaceIndex != other.spaceIndex)
                return false;
            for (int i = 0; i < 12; i++)
            {
                if (puzzle[i] != other.puzzle[i])
                    return false;
            }
            return true;
        } else return false;
    }

    public List<Node> GetActions()
    {

        if (spaceIndex == 0)
            return new List<Node>() {
             new Node(Swap(0,1),1,this),
             new Node(Swap(0,4),4, this)
            };

        else if (spaceIndex == 1)
            return new List<Node>() {
             new Node(Swap(1,0),0, this),
             new Node(Swap(1,2),2, this),
             new Node(Swap(1,5),5, this),
            };

        else if (spaceIndex == 2)
            return new List<Node>() {
             new Node(Swap(2,1),1, this),
             new Node(Swap(2,3),3, this),
             new Node(Swap(2,6),6, this ),
            };

        else if (spaceIndex == 3)
            return new List<Node>() {
             new Node(Swap(3,2),2, this),
             new Node(Swap(3,7),7, this)
            };

        else if (spaceIndex == 4)
            return new List<Node>() {
             new Node(Swap(4,0),0, this),
             new Node(Swap(4,5),5, this),
             new Node(Swap(4,8),8, this),
            };

        else if (spaceIndex == 5)
            return new List<Node>() {
             new Node(Swap(5,1),1, this),
             new Node(Swap(5,4),4, this),
             new Node(Swap(5,6),6, this),
             new Node(Swap(5,9),9, this),
            };

        else if (spaceIndex == 6)
            return new List<Node>() {
             new Node(Swap(6,2),2, this),
             new Node(Swap(6,5),5, this),
             new Node(Swap(6,7),7, this),
             new Node(Swap(6,10),10, this),
            };

        else if (spaceIndex == 7)
            return new List<Node>() {
             new Node(Swap(7,3),3, this),
             new Node(Swap(7,6),6, this),
             new Node(Swap(7,11),11, this)
            };

        else if (spaceIndex == 8)
            return new List<Node>() {
             new Node(Swap(8,4),4, this),
             new Node(Swap(8,9),9, this)
            };

        else if (spaceIndex == 9)
            return new List<Node>() {
             new Node(Swap(9,5),5, this),
             new Node(Swap(9,8),8, this),
             new Node(Swap(9,10),10, this),
            };

        else if (spaceIndex == 10)
            return new List<Node>() {
             new Node(Swap(10,6),6, this),
             new Node(Swap(10,9),9, this),
             new Node(Swap(10,11),11, this),
            };

        else if (spaceIndex == 11)
            return new List<Node>() {
             new Node(Swap(11,7),7, this),
             new Node(Swap(11,10),10, this)
            };

        throw new Exception("SpaceIndex out of range");
    }

    private string Swap(int first, int second)
    {
        char[] chars = puzzle.ToCharArray();
        (chars[second], chars[first]) = (chars[first], chars[second]);
        return new string(chars);
    }

    private static int FindManhattan((int x, int y) CurrentPos, (int x, int y) CorrectPos)
    {
        return Math.Abs(CurrentPos.x - CorrectPos.x) + Math.Abs(CurrentPos.y - CorrectPos.y);
    }

    public static void Print(string puzzle)
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
}



public static class ExtensionMethods // Extensions
{
    public static char ToChar(this int number)
    {
        return (number + 1) switch
        {
            1 => '1',
            2 => '2',
            3 => '3',
            4 => '4',
            5 => '5',
            6 => '6',
            7 => '7',
            8 => '8',
            9 => '9',
            10 => 'A',
            11 => 'B',
            12 => 'C',
            _ => '0',// or any other default value
        };
    }

    public static int ToInt(this char ch)
    {
        return (ch) switch
        {
            '1' => 0,
            '2' => 1,
            '3' => 2,
            '4' => 3,
            '5' => 4,
            '6' => 5,
            '7' => 6,
            '8' => 7,
            '9' => 8,
            'A' => 9,
            'B' => 10,
            'C' => 11,
            _ => 0 // or any other default value
        };
    }

    public static (int, int) FindXY(this int index)
    {
        return (index / 4, index % 4);
    }

    public static string ReplaceCharAtIndex(this string input, int index, char newChar)
    {

        string before = input[..index];

        string after = input[(index + 1)..];

        return before + newChar + after;
    }
}

