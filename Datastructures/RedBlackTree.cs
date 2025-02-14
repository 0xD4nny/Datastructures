using System.Collections;

namespace Datastructures;

/// <summary>
/// This structure is optimized for handling fewer than 100,000 objects.<br />
/// If you expect to store more, consider using a different data structure.<br />
/// The reason is that this implementation relies on efficient recursion,<br />
/// which is safe for up to 20 recursive stack frames.<br />
/// However, I cannot guarantee that deeper recursion will always be avoided.<br />
/// </summary>
public class RedBlackTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : struct, IComparable<TKey>
{
    private class Node(TKey key, TValue value, bool isRed)
    {
        public TKey Key = key;
        public TValue Value = value;
        public Node? Left, Right;
        public bool IsRed = isRed;
    }

    private Node? _root;
    private int _count;

    public int Count => _count;
    public int Height => CalculateHeight(_root);

    public TValue this[TKey key]
    {
        get
        {
            Node? node = FindNode(key);

            if (node == null)
                throw new KeyNotFoundException($"Key {key} not found in RedBlackTree.");

            return node.Value;
        }
        set
        {
            Node? node = FindNode(key);

            if (node == null)
                Add(key, value);
            else
                node.Value = value;
        }
    }


    #region Public Methods
    public void Add(TKey key, TValue value)
    {
        _root = InsertRecursive(_root, key, value);
        if (_root != null)
            _root.IsRed = false;
    }
    public void Remove(TKey key)
    {
        if (_root == null) return;

        _root = RemoveRecursive(_root, key);

        _count--;

        if (_root != null)
            _root.IsRed = false;
    }
    public KeyValuePair<TKey, TValue>? RemoveMin()
    {
        if (_root == null) return default!;

        Node min = GetMinNode(_root);

        _root = RemoveRecursive(_root, min.Key);
        _count--;

        if (_root != null)
            _root.IsRed = false;

        return KeyValuePair.Create(min.Key, min.Value);
    }

    public bool Contains(TKey key)
    {
        Node? current = _root;
        while (current != null)
        {
            int cmp = key.CompareTo(current.Key);
            if (cmp < 0)
                current = current.Left;
            else if (cmp > 0)
                current = current.Right;
            else
                return true;
        }
        return false;
    }
    public KeyValuePair<TKey, TValue> Max()
    {
        Node node = GetMaxNode(_root);
        return KeyValuePair.Create(node.Key, node.Value);
    }
    public KeyValuePair<TKey, TValue> Min()
    {
        Node node = GetMinNode(_root);
        return KeyValuePair.Create(node.Key, node.Value);
    }
    #endregion

    #region Help Methods
    private Node? FindNode(TKey key)
    {
        Node? current = _root;
        while (current != null)
        {
            int cmp = key.CompareTo(current.Key);
            if (cmp < 0)
                current = current.Left;
            else if (cmp > 0)
                current = current.Right;
            else
                return current;
        }
        return null;
    }
    private Node InsertRecursive(Node? node, TKey key, TValue value)
    {
        if (node == null)
        {
            _count++;
            return new Node(key, value, true);
        }

        int cmp = key.CompareTo(node.Key);
        if (cmp < 0)
            node.Left = InsertRecursive(node.Left, key, value);
        else if (cmp > 0)
            node.Right = InsertRecursive(node.Right, key, value);
        else
            node.Value = value;


        return Balance(node);
    }
    private Node? RemoveRecursive(Node? node, TKey key)
    {
        if (node == null)
            return null;

        if (key.CompareTo(node.Key) < 0)
        {
            if (node.Left != null && !IsRed(node.Left) && !IsRed(node.Left.Left))
                node = MoveRedLeft(node);

            node.Left = RemoveRecursive(node.Left, key);
        }
        else
        {
            if (IsRed(node.Left))
                node = RotateRight(node);

            if (key.CompareTo(node.Key) == 0 && node.Right == null)
                return node.Left;

            if (node.Right != null && !IsRed(node.Right) && !IsRed(node.Right.Left))
                node = MoveRedRight(node);

            if (key.CompareTo(node.Key) == 0)
            {
                Node min = GetMinNode(node.Right);
                node.Key = min.Key;
                node.Value = min.Value;
                node.Right = RemoveRecursive(node.Right, min.Key);
            }
            else
                node.Right = RemoveRecursive(node.Right, key);

        }

        return Balance(node);
    }
    private Node GetMinNode(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }
    private Node GetMaxNode(Node node)
    {
        while (node.Right != null)
            node = node.Right;
        return node;
    }
    private int CalculateHeight(Node? node)
    {
        if (node is null)
            return 0;

        int leftHeight = CalculateHeight(node.Left);
        int rightHeight = CalculateHeight(node.Right);

        return 1 + Math.Max(leftHeight, rightHeight);
    }
    #endregion

    #region Tree balance
    private Node Balance(Node node)
    {
        if (IsRed(node.Right) && !IsRed(node.Left))
            node = RotateLeft(node);

        if (IsRed(node.Left) && IsRed(node.Left.Left))
            node = RotateRight(node);

        if (IsRed(node.Left) && IsRed(node.Right))
            FlipColors(node);

        return node;
    }
    private Node MoveRedRight(Node node)
    {
        FlipColors(node);
        if (node.Left?.Left != null && IsRed(node.Left.Left))
        {
            node = RotateRight(node);
            FlipColors(node);
        }
        return node;
    }
    private Node MoveRedLeft(Node node)
    {
        FlipColors(node);
        if (node.Right?.Left != null && IsRed(node.Right.Left))
        {
            node.Right = RotateRight(node.Right);
            node = RotateLeft(node);
            FlipColors(node);
        }
        return node;
    }
    private Node RotateLeft(Node node)
    {
        Node x = node.Right!;
        node.Right = x.Left;
        x.Left = node;
        x.IsRed = node.IsRed;
        node.IsRed = true;
        return x;
    }
    private Node RotateRight(Node node)
    {
        Node x = node.Left!;
        node.Left = x.Right;
        x.Right = node;
        x.IsRed = node.IsRed;
        node.IsRed = true;
        return x;
    }
    private void FlipColors(Node node)
    {
        node.IsRed = !node.IsRed;
        if (node.Left != null)
            node.Left.IsRed = !node.Left.IsRed;
        if (node.Right != null)
            node.Right.IsRed = !node.Right.IsRed;
    }
    private bool IsRed(Node? node)
    {
        return node is not null && node.IsRed;
    }
    #endregion

    #region Traversal-IEnumerables //Traversals as IEnumerators for a nice foreach experiance.
    /// <summary>
    /// Enumerates nodes level by level: top → bottom, left → right (Breadth-First Search).
    /// This method is the default traversal used in foreach loops.
    /// </summary>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => LevelOrderTraversal().Select(node => new KeyValuePair<TKey, TValue>(node.Key, node.Value)).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    private IEnumerable<KeyValuePair<TKey, TValue>> LevelOrderTraversal()
    {
        if (_root is null)
            yield break;

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            yield return KeyValuePair.Create(current.Key, current.Value);

            if (current.Left is not null)
                queue.Enqueue(current.Left);

            if (current.Right is not null)
                queue.Enqueue(current.Right);
        }
    }

    /// <summary>
    /// Traverses the tree in pre-order(root → left → right).
    /// Nodes are enumerated starting with the root, then recursively the left and right subtrees.
    /// </summary>
    public IEnumerable<KeyValuePair<TKey, TValue>> PreOrderTraversal()
    {
        if (_root is null)
            yield break;

        Stack<Node> stack = new Stack<Node>();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            yield return KeyValuePair.Create(current.Key, current.Value);

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
    public IEnumerable<KeyValuePair<TKey, TValue>> InOrderTraversal() => InOrderTraversal(_root);
    private IEnumerable<KeyValuePair<TKey, TValue>> InOrderTraversal(Node? node = null)
    {
        if (node is null)
            yield break;

        foreach (var leftNode in InOrderTraversal(node.Left))
            yield return leftNode;

        yield return KeyValuePair.Create(node.Key, node.Value);

        foreach (var rightNode in InOrderTraversal(node.Right))
            yield return rightNode;
    }

    /// <summary>
    /// Traverses the tree in post-order (left → right → root).
    /// Nodes are enumerated after recursively traversing the left and right subtrees.
    /// </summary>
    public IEnumerable<KeyValuePair<TKey, TValue>> PostOrderTraversal() => PostOrderTraversal(_root);
    private IEnumerable<KeyValuePair<TKey, TValue>> PostOrderTraversal(Node? node = null)
    {
        if (node is null)
            yield break;

        foreach (var leftNode in PostOrderTraversal(node.Left))
            yield return leftNode;

        foreach (var rightNode in PostOrderTraversal(node.Right))
            yield return rightNode;

        yield return KeyValuePair.Create(node.Key, node.Value);
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

        Console.WriteLine($"Count: {_count}\nMax: {GetMaxNode(_root).Key}\nMin: {GetMinNode(_root).Key}\n");

        PrintSubtree(_root, "", true);
    }
    private void PrintSubtree(Node? node, string indent, bool isLast)
    {
        if (node == null)
            return;

        Console.Write(indent);
        Console.Write(isLast ? "└── " : "├── ");

        Console.ForegroundColor = node.IsRed ? ConsoleColor.Red : ConsoleColor.Gray;
        //Console.WriteLine(node.Key + (node.IsRed ? "(R)" : "(B)"));
        Console.WriteLine(node.Key);
        Console.ResetColor();

        indent += isLast ? "    " : "│   ";
        PrintSubtree(node.Left, indent, node.Right == null);
        PrintSubtree(node.Right, indent, true);
    }
    #endregion

    public bool IsValidRedBlackTree()
    {
        if (_root == null) return true;

        if (_root.IsRed) return false;

        return ValidateNode(_root, 0, out int _);
    }
    private bool ValidateNode(Node? node, int blackCount, out int pathBlackCount)
    {
        if (node == null)
        {
            pathBlackCount = blackCount;
            return true;
        }

        if (node.IsRed)
        {
            pathBlackCount = 0;
            if ((node.Left?.IsRed ?? false) || (node.Right?.IsRed ?? false))
                return false;
        }
        else
        {
            blackCount++;
        }

        bool leftValid = ValidateNode(node.Left, blackCount, out int leftBlack);
        bool rightValid = ValidateNode(node.Right, blackCount, out int rightBlack);

        pathBlackCount = leftBlack;
        return leftValid && rightValid && leftBlack == rightBlack;
    }


}