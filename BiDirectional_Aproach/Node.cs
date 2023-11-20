using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiDirectional_Approach;

public class Node
{
    public readonly string puzzle;
    public readonly int spaceIndex;
    public readonly Node? parent;

    public Node(string puzzle, int spaceIndex, Node? parent)
    {
        this.puzzle = puzzle;
        this.spaceIndex = spaceIndex;
        this.parent = parent;
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
             new Node(Swap(11,6),6, this)
            };

        throw new Exception("SpaceIndex out of range");
    }

    private string Swap(int first, int second)
    {
        char[] chars = puzzle.ToCharArray();
        (chars[second], chars[first]) = (chars[first], chars[second]);
        return new string(chars);
    }

    public void Print()
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

