using System.Collections;

namespace Datastructures;

public class BinaryTree<T> : IEnumerable<T>
{
    protected class Node(T value)
    {
        public T Value = value;
        public Node? Left, Right;
    }

    protected Node? _root;
    protected int _count = 0;

    public int Count => _count;
    public int Height => CalculateHeightRecursive(_root);


    #region Public Methods
    public void Insert(T value)
    {
        if (_root is null)
        {
            _root = new Node(value);
            _count++;
            return;
        }

        foreach (Node node in LevelOrderTraversal())
        {
            if (node.Left is null)
            {
                node.Left = new Node(value);
                _count++;
                return;
            }

            if (node.Right is null)
            {
                node.Right = new Node(value);
                _count++;
                return;
            }
        }
    }
    public void Remove(T value)
    {
        if (_root is null)
            return;

        Node? nodeToDelete = null;
        Node? lastNode = null;
        Node? parentOfLastNode = null;

        foreach (Node node in LevelOrderTraversal())
        {
            if (node.Value.Equals(value))
                nodeToDelete = node;

            if (node.Left is not null)
            {
                parentOfLastNode = node;
                lastNode = node.Left;
            }

            if (node.Right is not null)
            {
                parentOfLastNode = node;
                lastNode = node.Right;
            }
        }

        if (nodeToDelete is null)
            return;

        if (lastNode is not null)
        {
            nodeToDelete.Value = lastNode.Value;

            if (parentOfLastNode.Left == lastNode)
            {
                _count--;
                parentOfLastNode.Left = null;
            }
            else if (parentOfLastNode.Right == lastNode)
            {
                _count--;
                parentOfLastNode.Right = null;
            }
        }
        else
        {
            _count--;
            _root = null;
        }

    }
    public void Clear()
    {
        _root = null;
    }

    public virtual T? FindMin() => _count != 0 ? this.Min() : default;
    public virtual T? FindMax() => _count != 0 ? this.Max() : default;
    public T? Find(Func<T, bool> predicate) => this.FirstOrDefault(predicate);
    public IEnumerable<T> FindAll(Func<T, bool> predicate) => this.Where(predicate);
    #endregion

    #region Help Methods
    private int CalculateHeightRecursive(Node? node)
    {
        if (node is null)
            return 0;

        int leftHeight = CalculateHeightRecursive(node.Left);
        int rightHeight = CalculateHeightRecursive(node.Right);

        return 1 + Math.Max(leftHeight, rightHeight);
    }
    #endregion

    #region Traversal-IEnumerables //Traversals as IEnumerators for a nice foreach experiance.
    /// <summary>
    /// Enumerates nodes level by level: top → bottom, left → right (Breadth-First Search).
    /// This method is the default traversal used in foreach loops.
    /// </summary>
    public IEnumerator<T> GetEnumerator() => LevelOrderTraversal().Select(node => node.Value).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    private IEnumerable<Node> LevelOrderTraversal()
    {
        if (_root is null)
            yield break;

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            yield return current;

            if (current.Left is not null)
                queue.Enqueue(current.Left);

            if (current.Right is not null)
                queue.Enqueue(current.Right);
        }
    }

    /// <summary>
    /// Traverses the tree in pre-order (root → left → right).
    /// Nodes are enumerated starting with the root, then recursively the left and right subtrees.
    /// </summary>
    public IEnumerable<T> PreOrderTraversal()
    {
        if (_root is null)
            yield break;

        Stack<Node> stack = new Stack<Node>();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            yield return current.Value;

            if (current.Right is not null)
                stack.Push(current.Right);

            if (current.Left is not null)
                stack.Push(current.Left);
        }
    }

    /// <summary>
    /// Traverses the tree in in-order (left → root → right).
    /// Nodes are enumerated in ascending order if the tree is a Binary Search Tree (BST).
    /// </summary>
    public IEnumerable<T> InOrderTraversal() => InOrderTraversal(_root);
    private IEnumerable<T> InOrderTraversal(Node? node = null)
    {
        if (node is null)
            yield break;

        foreach (var leftNode in InOrderTraversal(node.Left))
            yield return leftNode;

        yield return node.Value;

        foreach (var rightNode in InOrderTraversal(node.Right))
            yield return rightNode;
    }

    /// <summary>
    /// Traverses the tree in post-order (left → right → root).
    /// Nodes are enumerated after recursively traversing the left and right subtrees.
    /// </summary>
    public IEnumerable<T> PostOrderTraversal() => PostOrderTraversal(_root);
    private IEnumerable<T> PostOrderTraversal(Node? node = null)
    {
        if (node is null)
            yield break;

        foreach (var leftNode in PostOrderTraversal(node.Left))
            yield return leftNode;

        foreach (var rightNode in PostOrderTraversal(node.Right))
            yield return rightNode;

        yield return node.Value;
    }
    #endregion

    #region Print Methods to visualize the Tree 
    public void PrintTree()
    {
        if (_root is null)
        {
            Console.WriteLine("Tree is empty.");
            return;
        }

        PrintSubtree(_root, "", true);
    }
    private void PrintSubtree(Node? node, string indent, bool isLast)
    {
        if (node == null)
            return;

        Console.Write(indent);
        Console.Write(isLast ? "└── " : "├── ");
        Console.WriteLine(node.Value);

        indent += isLast ? "    " : "│   ";

        PrintSubtree(node.Left, indent, node.Right == null);
        PrintSubtree(node.Right, indent, true);
    }
    #endregion

}
