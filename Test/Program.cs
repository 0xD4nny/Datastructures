using Datastructures;
using System.Diagnostics;

namespace Test;
class Program
{
    private static readonly List<int> _numbers = new List<int>();

    static void Main()
    {
        Random _random = new Random();

        for (int i = 0; i < 100_000; i++)
            _numbers.Add(_random.Next(0, 101));
        
        RecursiveRbtTest(); // this run is to make the compiler warm.
        
        Stopwatch sw = Stopwatch.StartNew();

        //RecursiveRbt:
        sw.Start();
        RecursiveRbtTest();
        sw.Stop();
        long recursiveRbtTime = sw.ElapsedMilliseconds;
        sw.Reset();

        //IterativeRbt:
        sw.Start();
        IterativeRbtTest();
        sw.Stop();
        long iterativeRbtTime = sw.ElapsedMilliseconds;
        sw.Reset();

        // SortedDictionary:
        sw.Start();
        SortedDictionaryTest();
        sw.Stop();
        long dotNetSortedDict = sw.ElapsedMilliseconds;

        //Result:
        Console.WriteLine($"1. Recursive Rbt Test Time: {recursiveRbtTime} ms");
        Console.WriteLine($"2. Iterative Rbt Test Time: {iterativeRbtTime} ms");
        Console.WriteLine($"3. .NET SortedDictionary Test Time: {dotNetSortedDict}ms");
        Console.WriteLine("All Tree - Tests successfull executed.");

    }

    #region Tree tests
    public static void BinaryTreeTest()
    {
        BinaryTree<(int, int)> binaryTree = new BinaryTree<(int, int)>();

        for (int i = 0; i < _numbers.Count; i++)
            binaryTree.Insert((_numbers[i], i));

        for (int i = 0; i < _numbers.Count; i++)
        {
            //Console.WriteLine("_____Binary Tree_____");
            //Console.WriteLine($"Count: {binaryTree.Count}");
            //Console.WriteLine($"Max value: {binaryTree.Max()}");
            //Console.WriteLine($"Min value: {binaryTree.Min()}");
            //Console.WriteLine($"Calculate Hight {binaryTree.Height}");
            //binaryTree.PrintTree();

            //Console.Write("\nLevelOrderTraversal:\t");
            //foreach ((int, int) number in binaryTree)
            //    Console.Write(number.Item1 + " ");

            //Console.Write("\nPreOrderTraversal:\t");
            //foreach ((int, int) number in binaryTree.PreOrderTraversal())
            //    Console.Write(number.Item1 + " ");

            //Console.Write("\nInOrderTraversal:\t");
            //foreach ((int, int) number in binaryTree.InOrderTraversal())
            //    Console.Write(number.Item1 + " ");

            //Console.Write("\nPostOrderTraversal:\t");
            //foreach ((int, int) number in binaryTree.PostOrderTraversal())
            //    Console.Write(number.Item1 + " ");

            //Console.WriteLine();
            //Console.WriteLine("Press any key to remove next.");
            //Console.ReadKey();
            //Console.Clear();

            binaryTree.Remove((_numbers[i], i));
        }

    }
    private static void BinarySearchTreeTest()
    {
        BinarySearchTree<(int, int)> binarySearchTree = new BinarySearchTree<(int, int)>();

        for (int i = 0; i < _numbers.Count; i++)
            binarySearchTree.Insert((_numbers[i], i));

        for (int i = 0; i < _numbers.Count; i++)
        {
            //Console.WriteLine("_____Binary Search Tree_____");
            //Console.WriteLine($"Count: {binarySearchTree.Count}");
            //Console.WriteLine($"Max value: {binarySearchTree.FindMax()}");
            //Console.WriteLine($"Min value: {binarySearchTree.FindMin()}");
            //Console.WriteLine($"Calculate Hight {binarySearchTree.Height}");
            //binarySearchTree.PrintTree();

            //Console.Write("\nLevelOrderTraversal:\t");
            //foreach ((int, int) number in binarySearchTree)
            //    Console.Write(number.Item1 + " ");

            //Console.Write("\nPreOrderTraversal:\t");
            //foreach ((int, int) number in binarySearchTree.PreOrderTraversal())
            //    Console.Write(number.Item1 + " ");

            //Console.Write("\nInOrderTraversal:\t");
            //foreach ((int, int) number in binarySearchTree.InOrderTraversal())
            //    Console.Write(number.Item1 + " ");

            //Console.Write("\nPostOrderTraversal:\t");
            //foreach ((int, int) number in binarySearchTree.PostOrderTraversal())
            //    Console.Write(number.Item1 + " ");

            //Console.WriteLine();
            //Console.WriteLine("Press any key to remove next.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine($"Removed node: {binarySearchTree.RemoveMin().Item1}");
            binarySearchTree.RemoveMin();
        }
    }
    private static void RecursiveRbtTest()
    {
        RecursiveRBT<int, int> recursiveRbt = new RecursiveRBT<int, int>();

        for (int i = 0; i < _numbers.Count; i++)
            recursiveRbt.Add(i, i);


        for (int i = 0; i < _numbers.Count; i++)
        {
            //Console.WriteLine("_____Red Black Tree_____");
            //Console.WriteLine($"Count: {redBlackTree.Count,8}");
            //Console.WriteLine($"Hight: {redBlackTree.Height,8}");
            //Console.WriteLine($"MaxValue: {redBlackTree.Max().Value,5}");
            //Console.WriteLine($"MinValue: {redBlackTree.Min().Value,5}");

            //Console.WriteLine();
            //redBlackTree.PrintTree();

            //Console.Write("\nLevelOrderTraversal:\t");
            //foreach (var kvp in redBlackTree)
            //    Console.Write(kvp.Value + " ");

            //Console.Write("\nPreOrderTraversal:\t");
            //foreach (var kvp in redBlackTree.PreOrder())
            //    Console.Write(kvp.Value + " ");

            //Console.Write("\nInOrderTraversal:\t");
            //foreach (var kvp in redBlackTree.InOrderTraversal())
            //    Console.Write(kvp.Value + " ");

            //Console.Write("\nPostOrderTraversal:\t");
            //foreach (var kvp in redBlackTree.PostOrderTraversal())
            //    Console.Write(kvp.Value + " ");

            //Console.WriteLine("\n\nPress any key to remove next.");
            //Console.ReadKey();
            //Console.SetCursorPosition(0,0);
            //Console.CursorVisible = false;
            //Thread.Sleep(10);
            //Console.WriteLine($"Removed node: {redBlackTree.RemoveMin().Value.Value}");
            recursiveRbt.RemoveMin();
        }
    }
    private static void IterativeRbtTest()
    {
        IterativeRBT<int, int> iterativeRbt = new IterativeRBT<int, int>();

        for (int i = 0; i < _numbers.Count; i++)
            iterativeRbt.Add(i, i);


        for (int i = 0; i < _numbers.Count; i++)
        {
            //Console.WriteLine("_____Red Black Tree_____");
            //Console.WriteLine($"Count: {redBlackTree.Count,8}");
            //Console.WriteLine($"Hight: {redBlackTree.Height,8}");
            //Console.WriteLine($"MaxValue: {redBlackTree.Max().Value,5}");
            //Console.WriteLine($"MinValue: {redBlackTree.Min().Value,5}");

            //Console.WriteLine();
            //redBlackTree.PrintTree();

            //Console.Write("\nLevelOrderTraversal:\t");
            //foreach (var kvp in redBlackTree)
            //    Console.Write(kvp.Value + " ");

            //Console.Write("\nPreOrderTraversal:\t");
            //foreach (var kvp in redBlackTree.PreOrder())
            //    Console.Write(kvp.Value + " ");

            //Console.Write("\nInOrderTraversal:\t");
            //foreach (var kvp in redBlackTree.InOrderTraversal())
            //    Console.Write(kvp.Value + " ");

            //Console.Write("\nPostOrderTraversal:\t");
            //foreach (var kvp in redBlackTree.PostOrderTraversal())
            //    Console.Write(kvp.Value + " ");

            //Console.WriteLine("\n\nPress any key to remove next.");
            //Console.ReadKey();
            //Console.Clear();
            //Console.SetCursorPosition(0, 0);
            //Console.CursorVisible = false;
            //Thread.Sleep(10);
            //Console.WriteLine($"Removed node: {redBlackTree.RemoveMin().Value.Value}");
            iterativeRbt.RemoveMin();
        }
    }
    private static void SortedDictionaryTest()
    {
        SortedDictionary<int, int> sortedDict = new SortedDictionary<int, int>();

        for (int i = 0; i < _numbers.Count; i++)
            sortedDict.Add(i, i);

        for (int i = _numbers.Count; i > 0; i--)
            sortedDict.Remove(i);

    }
    #endregion

    #region Custom Data-Structures Tests
    private static void LinkListTest()
    {
        CustomLinkedList<(int, int)> linkedList = new CustomLinkedList<(int, int)>();

        for (int i = 0; i < 50; i++)
            linkedList.AddFirst((_numbers[i], i));

        Console.WriteLine("UNSORTED Processes: " + "\n");
        foreach ((int, int) numbers in linkedList)
            Console.WriteLine($"{numbers.Item1}");

        linkedList.SortAsc();

        Console.WriteLine("\n" + "SORTED(ASC) by Processes: " + "\n");
        foreach ((int, int) numbers in linkedList)
            Console.WriteLine($"Process {numbers.Item1}");

        linkedList.SortDesc();

        Console.WriteLine("\n" + "SORTED(DESC) by Processes: " + "\n");
        foreach ((int, int) numbers in linkedList)
            Console.WriteLine($"Process {numbers.Item1}");

        Console.WriteLine("\n" + "Reverse Enumerator: " + "\n");
        foreach ((int, int) numbers in linkedList.GetReverseEnumerable())
            Console.WriteLine($"Process {numbers.Item1}");

    }
    private static void ListTest()
    {
        ConcurrentList<string> list = ["Sup", "Bro", "Yoyo"];

        // Racecondition - test (Solved)
        ThreadPool.QueueUserWorkItem(delegate
        {
            while (true)
            {
                list.Remove("Yoyo");
                list.Add("Yoyo");
            }
        });

        int count = 1, nullcount = 0;

        while (count < 100_000)
            foreach (string str in list)
            {
                if (str == null)
                    nullcount++;

                count++;

                Console.WriteLine($"{str,-15}loopcount: {count} nullcount: {nullcount}");
            }

    }
    #endregion

}
