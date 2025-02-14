# Custom Data Structures in C
### Overview
This project is a collection of custom data structures implemented in C#, created as part of my exploration into memory management, data organization, and algorithm efficiency.
This project started as an experiment with linked lists and dynamic arrays but quickly grew into a deeper exploration of read/write operations, performance trade-offs, and tree balancing.
As my understanding grew, I implemented more complex structures, including Binary Trees, Binary Search Trees, and a Red-Black Tree (RBT).
The included diagram illustrates key comparisons and optimizations explored throughout my learning journey.

<img src="https://github.com/0xD4nny/Datastructures/releases/download/v1.0.0/DatastructuresNew.png" style="max-width:100%; height:auto;">

# Implemented Data Structures

## 📃 List Structures
- [CustomLinkedList.cs](./path/to/CustomLinkedList.cs) – A linked list with efficient insert and delete operations.
- [CustomList.cs](./path/to/CustomList.cs) – A dynamically resizing list optimized for memory efficiency and iteration safety.

## 🌳 Tree Structures
- [BinaryTree.cs](./path/to/BinaryTree.cs) – A foundational hierarchical structure.
- [BinarySearchTree.cs](./path/to/BinarySearchTree.cs) – A BST optimized for fast searching and sorting.
- [RedBlackTree.cs](./path/to/RedBlackTree.cs) – A self-balancing BST ensuring O(log n) efficiency.

## ⚠️ Additional Features
- [CustomException.cs](./path/to/CustomException.cs) – For better error handling.

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


# 🚀 Last Benchmark 14.02.25
- This benchmark compares the performance of a my custom Red-Black-Tree implementation with SortedDictionary from .NET. 
- The tests were performed with BenchmarkDotNet.
```
| Method            | N    | Mean         | Error       | StdDev      | Median       | Gen0    | Gen1   | Allocated |
|------------------ |----- |-------------:|------------:|------------:|-------------:|--------:|-------:|----------:|
| RBT_Insert        | 100  |   5,797.4 ns |   112.23 ns |   246.34 ns |   5,710.5 ns |  1.1520 |      - |    4832 B |
| SortedDict_Insert | 100  |   5,423.1 ns |   134.63 ns |   390.60 ns |   5,301.2 ns |  1.1673 |      - |    4912 B |
| RBT_Search        | 100  |   1,191.0 ns |    18.74 ns |    17.53 ns |   1,193.2 ns |       - |      - |         - |
| SortedDict_Search | 100  |   1,887.9 ns |    37.69 ns |    99.29 ns |   1,841.5 ns |       - |      - |         - |
| RBT_Delete        | 100  |     115.0 ns |     3.25 ns |     9.22 ns |     112.6 ns |       - |      - |         - |
| SortedDict_Delete | 100  |     651.3 ns |    12.87 ns |    16.74 ns |     643.9 ns |       - |      - |         - |
| RBT_Insert        | 1000 | 174,296.3 ns | 1,810.60 ns | 2,085.09 ns | 173,741.1 ns | 11.4746 |      - |   48032 B |
| SortedDict_Insert | 1000 | 170,632.0 ns |   792.98 ns |   662.17 ns | 170,729.4 ns | 11.4746 | 0.2441 |   48112 B |
| RBT_Search        | 1000 |  47,946.9 ns |   958.38 ns |   896.47 ns |  47,425.9 ns |       - |      - |         - |
| SortedDict_Search | 1000 |  70,567.3 ns | 1,228.47 ns | 3,192.95 ns |  69,425.5 ns |       - |      - |         - |
| RBT_Delete        | 1000 |   1,022.8 ns |    20.85 ns |    57.08 ns |   1,007.2 ns |       - |      - |         - |
| SortedDict_Delete | 1000 |   6,855.8 ns |   198.99 ns |   586.74 ns |   6,560.7 ns |       - |      - |         - |
```

## Visualization of the RedBlackTree
- Every tree class includes a `PrintTree()` method for visualizing its structure, primarily for debugging and analysis. 
- Below is an example of a Red-Black Tree with 31 nodes.

```
_____Red Black Tree_____
Count:       31
Hight:        5
MaxValue:    30
MinValue:     0

└── 15
    ├── 7
    │   ├── 3
    │   │   ├── 1
    │   │   │   ├── 0
    │   │   │   └── 2
    │   │   └── 5
    │   │       ├── 4
    │   │       └── 6
    │   └── 11
    │       ├── 9
    │       │   ├── 8
    │       │   └── 10
    │       └── 13
    │           ├── 12
    │           └── 14
    └── 23
        ├── 19
        │   ├── 17
        │   │   ├── 16
        │   │   └── 18
        │   └── 21
        │       ├── 20
        │       └── 22
        └── 27
            ├── 25
            │   ├── 24
            │   └── 26
            └── 29
                ├── 28
                └── 30

LevelOrderTraversal: 15 7 23 3 11 19 27 1 5 9 13 17 21 25 29 0 2 4 6 8 10 12 14 16 18 20 22 24 26 28 30;
                     
PreOrderTraversal:   15 7 3 1 0 2 5 4 6 11 9 8 10 13 12 14 23 19 17 16 18 21 20 22 27 25 24 26 29 28 30;
                     
InOrderTraversal:    0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30;
                     
PostOrderTraversal:  0 2 1 4 6 5 3 8 10 9 12 14 13 11 7 16 18 17 20 22 21 19 24 26 25 28 30 29 27 23 15;
```