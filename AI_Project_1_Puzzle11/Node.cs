namespace AI_Project_1_Puzzle11
{
    public class Node
    {
        public readonly int?[] puzzle = new int?[12];
        public readonly int SpaceIndex;

        public Node(int?[] puzzle, int spaceIndex)
        {
            this.puzzle = puzzle;
            SpaceIndex = spaceIndex;
        }   

        public override int GetHashCode()
        {
            return HashCode.Combine(puzzle, SpaceIndex);
        }

        public static bool operator ==(Node? left, Node? right)
        {
            return left!.GetHashCode == right!.GetHashCode;
        }

        public static bool operator !=(Node? left, Node? right)
        {
            return left!.GetHashCode != right!.GetHashCode;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Node other)
            {
                if (other == null)
                    return false;
                if (SpaceIndex != other.SpaceIndex)
                    return false;
                for (int i = 0; i < 12; i++)
                {
                    if (puzzle[i] != other.puzzle[i])
                        return false;
                }
                return true;
            }
            else return false;
        }
    }
}
