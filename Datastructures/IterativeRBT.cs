using System.Collections;

namespace Datastructures;

public class IterativeRBT<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
{
    private enum Color { Red, Black }
    private Node? _root;
    private int _count;

    private class Node
    {
        public TKey Key;
        public TValue Value;
        public Node? Left, Right, Parent;
        public Color NodeColor;

        public Node(TKey key, TValue value, Color color, Node? parent)
        {
            Key = key;
            Value = value;
            NodeColor = color;
            Parent = parent;
        }
    }

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
        Node? parent = null;
        Node? current = _root;

        while (current != null)
        {
            parent = current;
            int cmp = key.CompareTo(current.Key);
            if (cmp < 0)
                current = current.Left;
            else if (cmp > 0)
                current = current.Right;
            else
            {
                current.Value = value;
                return;
            }
        }

        Node newNode = new Node(key, value, Color.Red, parent);
        if (parent == null)
            _root = newNode;
        else if (key.CompareTo(parent.Key) < 0)
            parent.Left = newNode;
        else
            parent.Right = newNode;

        BalanceAfterInsert(newNode);
        _count++;
    }
    public KeyValuePair<TKey, TValue>? Remove(TKey key)
    {
        Node? node = FindNode(key);
        if (node == null) return default;

        if (node.Left != null && node.Right != null)
        {
            Node successor = GetMinNode(node.Right);
            node.Key = successor.Key;
            node.Value = successor.Value;
            node = successor;
        }

        Node? child = (node.Left != null) ? node.Left : node.Right;
        if (child != null)
        {
            ReplaceNode(node, child);
            if (node.NodeColor == Color.Black)
                BalanceAfterDelete(child);
        }
        else if (node.NodeColor == Color.Black)
        {
            BalanceAfterDelete(node);
            ReplaceNode(node, null);
        }

        _count--;
        return KeyValuePair.Create(node.Key, node.Value);
    }
    public KeyValuePair<TKey, TValue>? RemoveMin()
    {
        if (_root == null) return default!;

        Node min = GetMinNode(_root);

        KeyValuePair<TKey, TValue>? kvp = Remove(min.Key);
        _count--;

        if (_root != null)
            _root.NodeColor = Color.Black;

        return kvp;
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
    private Node? FindNode(TKey key)
    {
        Node? current = _root;
        while (current != null)
        {
            int cmp = key.CompareTo(current.Key);
            if (cmp < 0) current = current.Left;
            else if (cmp > 0) current = current.Right;
            else return current;
        }
        return null;
    }
    private int CalculateHeight(Node? node)
    {
        if (node is null)
            return 0;
        return 1 + Math.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
    }
    #endregion

    #region Tree balance
    private void ReplaceNode(Node oldNode, Node? newNode)
    {
        if (oldNode.Parent == null)
            _root = newNode;
        else if (oldNode == oldNode.Parent.Left)
            oldNode.Parent.Left = newNode;
        else
            oldNode.Parent.Right = newNode;

        if (newNode != null)
            newNode.Parent = oldNode.Parent;
    }
    private void BalanceAfterInsert(Node node)
    {
        while (node.Parent != null && node.Parent.NodeColor == Color.Red)
        {
            Node grandparent = node.Parent.Parent!;
            Node uncle = (node.Parent == grandparent.Left) ? grandparent.Right : grandparent.Left;

            if (uncle != null && uncle.NodeColor == Color.Red)
            {
                // 🔹 Case 1: Recoloring (Onkel ist rot)
                node.Parent.NodeColor = Color.Black;
                uncle.NodeColor = Color.Black;
                grandparent.NodeColor = Color.Red;
                node = grandparent;
            }
            else
            {
                if (node == node.Parent.Right && node.Parent == grandparent.Left)
                {
                    // 🔄 Case 2a: Left-Right (Zig-Zag) - ROTIERE LINKS
                    node = node.Parent;
                    RotateLeft(node);
                }
                else if (node == node.Parent.Left && node.Parent == grandparent.Right)
                {
                    // 🔄 Case 2b: Right-Left (Zig-Zag) - ROTIERE RECHTS
                    node = node.Parent;
                    RotateRight(node);
                }

                // 🔄 Case 3: Final Rotation (Links-Links oder Rechts-Rechts)
                node.Parent.NodeColor = Color.Black;
                grandparent.NodeColor = Color.Red;
                if (node == node.Parent.Left)
                    RotateRight(grandparent);
                else
                    RotateLeft(grandparent);
            }
        }
        _root!.NodeColor = Color.Black; // Root bleibt schwarz
    }
    private void BalanceAfterDelete(Node node)
    {
        while (node != _root && node.NodeColor == Color.Black)
        {
            Node sibling = (node == node.Parent!.Left) ? node.Parent.Right! : node.Parent.Left!;

            if (sibling.NodeColor == Color.Red)
            {
                sibling.NodeColor = Color.Black;
                node.Parent.NodeColor = Color.Red;
                if (node == node.Parent.Left)
                    RotateLeft(node.Parent);
                else
                    RotateRight(node.Parent);
                sibling = (node == node.Parent.Left) ? node.Parent.Right! : node.Parent.Left!;
            }

            if ((sibling.Left == null || sibling.Left.NodeColor == Color.Black) &&
                (sibling.Right == null || sibling.Right.NodeColor == Color.Black))
            {
                sibling.NodeColor = Color.Red;
                node = node.Parent;
                continue;
            }
            else
            {
                if (sibling == node.Parent.Right && (sibling.Right == null || sibling.Right.NodeColor == Color.Black))
                {
                    sibling.Left!.NodeColor = Color.Black;
                    sibling.NodeColor = Color.Red;
                    RotateRight(sibling);
                    sibling = node.Parent.Right!;
                }
                else if (sibling == node.Parent.Left && (sibling.Left == null || sibling.Left.NodeColor == Color.Black))
                {
                    sibling.Right!.NodeColor = Color.Black;
                    sibling.NodeColor = Color.Red;
                    RotateLeft(sibling);
                    sibling = node.Parent.Left!;
                }

                sibling.NodeColor = node.Parent.NodeColor;
                node.Parent.NodeColor = Color.Black;
                if (sibling == node.Parent.Right)
                    sibling.Right!.NodeColor = Color.Black;
                else
                    sibling.Left!.NodeColor = Color.Black;

                if (node == node.Parent.Left)
                    RotateLeft(node.Parent);
                else
                    RotateRight(node.Parent);

                node = _root!;
            }
        }
        node.NodeColor = Color.Black;
    }
    private void RotateLeft(Node node)
    {
        Node pivot = node.Right!;
        ReplaceNode(node, pivot);

        node.Right = pivot.Left;
        if (pivot.Left != null)
            pivot.Left.Parent = node;

        pivot.Left = node;
        pivot.Parent = node.Parent;
        node.Parent = pivot;

        // Falls die Rotation die Wurzel verändert, müssen wir das sicherstellen
        if (pivot.Parent == null)
            _root = pivot;
    }
    private void RotateRight(Node node)
    {
        Node pivot = node.Left!;
        ReplaceNode(node, pivot);

        node.Left = pivot.Right;
        if (pivot.Right != null)
            pivot.Right.Parent = node;

        pivot.Right = node;
        pivot.Parent = node.Parent;
        node.Parent = pivot;

        if (pivot.Parent == null)
            _root = pivot;
    }
    #endregion

    #region Traversal-IEnumerables //Traversals as IEnumerators for a nice foreach experiance.
    /// <summary>
    /// Enumerates nodes level by level: top → bottom, left → right (Breadth-First Search).
    /// This method is the default traversal used in foreach loops.
    /// </summary>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => LevelOrder().Select(node => new KeyValuePair<TKey, TValue>(node.Key, node.Value)).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    private IEnumerable<KeyValuePair<TKey, TValue>> LevelOrder()
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
    public IEnumerable<KeyValuePair<TKey, TValue>> PreOrder()
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

        PrintSubtree(_root, "", true);
    }
    private void PrintSubtree(Node? node, string indent, bool isLast)
    {
        if (node == null)
            return;

        Console.Write(indent);
        Console.Write(isLast ? "└── " : "├── ");

        Console.ForegroundColor = node.NodeColor == Color.Red ? ConsoleColor.Red : ConsoleColor.Gray;
        //Console.WriteLine(node.Key + (node.NodeColor == Color.Red ? "(R)" : "(B)"));
        Console.WriteLine(node.Key);
        Console.ResetColor();

        indent += isLast ? "    " : "│   ";
        PrintSubtree(node.Left, indent, node.Right == null);
        PrintSubtree(node.Right, indent, true);
    }
    #endregion

    #region Debug
    public bool IsValidRedBlackTree()
    {
        if (_root == null) return true;

        if (_root.NodeColor is Color.Red) return false;

        return ValidateNode(_root, 0, out int _);
    }
    private bool ValidateNode(Node? node, int blackCount, out int pathBlackCount)
    {
        if (node == null)
        {
            pathBlackCount = blackCount;
            return true;
        }

        if (node.NodeColor is Color.Red)
        {
            pathBlackCount = 0;
            if ((node.Left?.NodeColor is Color.Red) || (node.Right?.NodeColor is Color.Red))
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
    #endregion
}
