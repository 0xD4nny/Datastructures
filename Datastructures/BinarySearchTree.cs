namespace Datastructures;

public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>
{
    public new void Insert(T value)
    {
        _root = InsertRecursive(_root, value);
        _count++;
    }
    private Node InsertRecursive(Node? node, T value)
    {
        if (node is null)
            return new Node(value);

        if (value.CompareTo(node.Value) < 0)
            node.Left = InsertRecursive(node.Left, value);
        else
            node.Right = InsertRecursive(node.Right, value);

        return node;
    }

    public new void Remove(T value)
    {
        _root = RemoveRecursive(_root, value);
    }
    public T? RemoveMin()
    {
        if (_root == null) return default!;

        Node min = GetMinNode(_root);

        _root = RemoveRecursive(_root, min.Value);
        _count--;

        return min.Value;
    }
    private Node? RemoveRecursive(Node? node, T value)
    {
        if (node is null)
            return null;

        if (value.CompareTo(node.Value) < 0)
        {
            node.Left = RemoveRecursive(node.Left, value);
        }
        else if (value.CompareTo(node.Value) > 0)
        {
            node.Right = RemoveRecursive(node.Right, value);
        }
        else
        {
            if (node.Left is null)
                return node.Right;
            if (node.Right is null)
                return node.Left;

            Node successor = GetMinNode(node.Right);
            node.Value = successor.Value;
            node.Right = RemoveRecursive(node.Right, successor.Value);
        }

        return node;
    }

    public T? FindMin()
    {
        if (_root is null)
            return default;

        return GetMinNode(_root).Value;
    }
    private Node GetMinNode(Node node)
    {
        while (node.Left is not null)
            node = node.Left;

        return node;
    }
    public T? FindMax()
    {
        if (_root is null)
            return default;

        return GetMaxNode(_root).Value;
    }
    private Node GetMaxNode(Node node)
    {
        while (node.Right is not null)
            node = node.Right;

        return node;
    }

}