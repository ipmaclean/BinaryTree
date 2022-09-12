namespace BinaryTree
{
    public class Node
    {
        public long Value { get; private set; }
        public Node? Parent { get; private set; }

        public Node(long value, Node? parent)
        {
            Value = value;
            Parent = parent;
        }
    }
}
