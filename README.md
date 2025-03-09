## Custom Data Structures in <img src="https://img.shields.io/badge/-C%23-239120?style=plastic&logo=csharp&logoColor=black"> <img src="https://img.shields.io/badge/-.NET-512BD4?style=plastic&logo=dotnet&logoColor=black">
### Overview
This project is a collection of custom data structures implemented in C#, created as part of my exploration into memory management, data organization, and algorithm efficiency.
This project started as an experiment with linked lists and fixed size arrays but quickly grew into a deeper exploration of read/write operations, performance trade-offs, and tree balancing.
As my understanding grew, I implemented more complex structures, including Binary Trees, Binary Search Trees, and a Red-Black Tree (RBT).
The included diagram illustrates key comparisons and optimizations explored throughout my learning journey.

![](https://github.com/0xD4nny/Datastructures/releases/download/v1.0.0/Datastructures.png)

# Implemented Data Structures

## 📃 List Structures
- [CustomLinkedList.cs](https://github.com/0xD4nny/Datastructures/blob/main/Datastructures/CustomLinkedList.cs) – A linked list with efficient insert and delete operations.
- [CustomList.cs](https://github.com/0xD4nny/Datastructures/blob/main/Datastructures/CustomList.cs) – A fixed size list optimized for memory efficiency and iteration thread safety.

## 🌳 Tree Structures
- [BinaryTree.cs](https://github.com/0xD4nny/Datastructures/blob/main/Datastructures/BinaryTree.cs) – A foundational hierarchical structure.
- [BinarySearchTree.cs](https://github.com/0xD4nny/Datastructures/blob/main/Datastructures/BinarySearchTree.cs) A BST optimized for fast searching and sorting.
- [RedBlackTree.cs](https://github.com/0xD4nny/Datastructures/blob/main/Datastructures/RedBlackTree.cs) – A self-balancing BST ensuring O(log n) efficiency.

## ⚠️ Additional Features
- [CustomException.cs](https://github.com/0xD4nny/Datastructures/blob/main/Datastructures/CustomExceptions.cs) – For better error handling.

## ✅ Key Takeaways
- Memory allocation and its impact on performance.
- Read and write operations in dynamic and tree-based structures.
- Tree balancing techniques for efficient searching and insertion.
- Analyzing .NET collections to understand best practices.
- Thread safety, including a custom thread-safe enumerator for iteration.
- Debugging strategies to handle edge cases efficiently.
  
## 🚀 Upcoming Enhancements:
- Add unit tests to validate functionality (**In Progress**)
- Optimize memory usage for better efficiency (**Planned for Q1 2025**)
- Explore additional structures such as AVL Trees (**Planned for ???**)
- Expand documentation with real-world examples (**Ongoing**)
- Benchmark performance against .NET collections (**Scheduled for Q1 2025**)

## 📖 License
- This project is open-source and freely available for educational and development purposes.
- Hopefully, this will serve as a useful learning resource for others exploring data structures in C#


# 🚀 Last Benchmark 09.03.2025
- This benchmark compares the performance of a my custom Red-Black-Tree implementation with SortedDictionary from .NET. 
- The tests were performed with BenchmarkDotNet.
- Ad New RBT with Iterative Methods instead of Recursives.
```
| Method                   | N      | Mean            | Error           | StdDev          | Median          | Gen0     | Gen1     | Allocated |
|------------------------- |------- |----------------:|----------------:|----------------:|----------------:|---------:|---------:|----------:|
| RecursiveRbtAdd          | 1000   |    113,899.0 ns |     2,160.13 ns |     1,914.90 ns |    113,554.6 ns |  11.4746 |        - |   48032 B |
| IterativeRbtAdd          | 1000   |     74,137.3 ns |     1,366.83 ns |     1,278.54 ns |     74,312.0 ns |  13.3057 |   0.2441 |   56032 B |
| SortedDictionaryAdd      | 1000   |     94,321.1 ns |     1,557.68 ns |     1,300.74 ns |     94,445.4 ns |  11.4746 |        - |   48112 B |
| RecursiveRbtContains     | 1000   |     46,015.3 ns |       583.20 ns |       455.32 ns |     45,880.5 ns |        - |        - |         - |
| IterativeRbtContains     | 1000   |     46,399.0 ns |       849.03 ns |     1,244.50 ns |     46,250.3 ns |        - |        - |         - |
| SortedDictionaryContains | 1000   |     60,951.7 ns |       652.23 ns |       801.00 ns |     60,786.1 ns |        - |        - |         - |
| RecursiveRbtRemove       | 1000   |        993.1 ns |        30.52 ns |        90.00 ns |        989.5 ns |        - |        - |         - |
| IterativeRbtRemove       | 1000   |      1,800.7 ns |        80.87 ns |       234.61 ns |      1,683.9 ns |        - |        - |         - |
| SortedDictionaryRemove   | 1000   |      5,758.2 ns |       109.57 ns |       223.82 ns |      5,739.0 ns |        - |        - |         - |
| RecursiveRbtAdd          | 100000 | 33,001,022.8 ns | 1,029,692.56 ns | 3,003,661.76 ns | 32,100,833.3 ns | 777.7778 | 666.6667 | 4800076 B |
| IterativeRbtAdd          | 100000 | 23,629,492.1 ns |   440,133.45 ns |   411,701.10 ns | 23,623,303.1 ns | 906.2500 | 750.0000 | 5600044 B |
| SortedDictionaryAdd      | 100000 | 24,666,955.8 ns |   488,959.26 ns |   543,476.92 ns | 24,555,548.4 ns | 781.2500 | 687.5000 | 4800124 B |
| RecursiveRbtContains     | 100000 | 32,262,498.4 ns | 2,353,487.62 ns | 6,752,597.65 ns | 28,783,804.7 ns |        - |        - |      12 B |
| IterativeRbtContains     | 100000 | 24,046,557.8 ns |   369,573.10 ns |   288,538.37 ns | 24,010,450.0 ns |        - |        - |      12 B |
| SortedDictionaryContains | 100000 | 25,099,944.0 ns |   233,544.62 ns |   182,336.27 ns | 25,145,332.8 ns |        - |        - |      12 B |
| RecursiveRbtRemove       | 100000 |     85,552.2 ns |       238.84 ns |       186.47 ns |     85,594.8 ns |        - |        - |         - |
| IterativeRbtRemove       | 100000 |    167,806.5 ns |     1,278.65 ns |     1,133.49 ns |    167,619.3 ns |        - |        - |         - |
| SortedDictionaryRemove   | 100000 |    573,186.9 ns |    11,363.38 ns |    14,775.60 ns |    573,042.8 ns |        - |        - |         - |
```

## Visualization of the RedBlackTree
- Every tree class includes a `PrintTree()` method for visualizing its structure, primarily for debugging and analysis. 
- Below is an example of a Red-Black Tree with 30 nodes.

```
_____Red Black Tree_____
Count:       30
Hight:        6
MaxValue:    43
MinValue:    14

└── 31(B)
    ├── 23(R)
    │   ├── 19(B)
    │   │   ├── 17(B)
    │   │   │   ├── 15(R)
    │   │   │   │   ├── 14(B)
    │   │   │   │   └── 16(B)
    │   │   │   └── 18(B)
    │   │   └── 21(B)
    │   │       ├── 20(B)
    │   │       └── 22(B)
    │   └── 27(B)
    │       ├── 25(B)
    │       │   ├── 24(B)
    │       │   └── 26(B)
    │       └── 29(B)
    │           ├── 28(B)
    │           └── 30(B)
    └── 39(B)
        ├── 35(R)
        │   ├── 33(B)
        │   │   ├── 32(B)
        │   │   └── 34(B)
        │   └── 37(B)
        │       ├── 36(B)
        │       └── 38(B)
        └── 41(B)
            ├── 40(B)
            └── 43(B)
                └── 42(R)

LevelOrderTraversal: 31 23 39 19 27 35 41 17 21 25 29 33 37 40 43 15 18 20 22 24 26 28 30 32 34 36 38 42 14 16

PreOrderTraversal:   31 23 19 17 15 14 16 18 21 20 22 27 25 24 26 29 28 30 39 35 33 32 34 37 36 38 41 40 43 42

InOrderTraversal:    14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43

PostOrderTraversal:  14 16 15 18 17 20 22 21 19 24 26 25 28 30 29 27 23 32 34 33 36 38 37 35 40 42 43 41 39 31

Press any key to remove next.
```
