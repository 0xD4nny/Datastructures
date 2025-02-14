namespace RBTBenchmark;

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;
using Datastructures;

[MemoryDiagnoser]
public class TreeBenchmark
{
    private List<int> _data;
    private RedBlackTree<int, int> _rbt;
    private SortedDictionary<int, int> _dict;

    [Params(100, 1_000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        _data = Enumerable.Range(1, N).OrderBy(_ => Guid.NewGuid()).ToList();
        _rbt = new RedBlackTree<int, int>();
        _dict = new SortedDictionary<int, int>();

        foreach (int item in _data)
        {
            _rbt[item] = item;
            _dict[item] = item;
        }
    }


    [Benchmark]
    public void RBT_Insert()
    {
        RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
        foreach (int item in _data)
            tree[item] = item;
    }
    [Benchmark]
    public void SortedDict_Insert()
    {
        SortedDictionary<int, int> dict = new SortedDictionary<int, int>();
        foreach (int item in _data)
            dict[item] = item;
    }


    [Benchmark]
    public void RBT_Search()
    {
        foreach (int item in _data)
            _rbt.Contains(item);
    }
    [Benchmark]
    public void SortedDict_Search()
    {
        foreach (int item in _data)
            _dict.ContainsKey(item);
    }


    [Benchmark]
    public void RBT_Delete()
    {
        foreach (int item in _data)
            _rbt.Remove(item);
    }
    [Benchmark]
    public void SortedDict_Delete()
    {
        foreach (int item in _data)
            _dict.Remove(item);
    }

}

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<TreeBenchmark>();
    }
}
