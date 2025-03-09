using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Datastructures;

namespace RBTBenchmark;

[MemoryDiagnoser]
public class TreeBenchmark
{
    private List<int> _data;
    private RecursiveRBT<int, int> _recursiveRbt;
    private IterativeRBT<int, int> _iterativeRbt;
    private SortedDictionary<int, int> _sortedDict;

    [Params(1_000, 100_000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        _data = Enumerable.Range(1, N).OrderBy(_ => Guid.NewGuid()).ToList();
        
        _recursiveRbt = new RecursiveRBT<int, int>();
        _iterativeRbt = new IterativeRBT<int, int>();
        _sortedDict = new SortedDictionary<int, int>();

        foreach (int item in _data)
        {
            _recursiveRbt.Add(item, item);
            _iterativeRbt.Add(item, item);
            _sortedDict.Add(item, item);
        }

    }

    // --- Add ---
    [Benchmark]
    public void RecursiveRbtAdd()
    {
        RecursiveRBT<int, int> tree = new RecursiveRBT<int, int>();
        foreach (int item in _data)
            tree.Add(item, item);
    }
    [Benchmark]
    public void IterativeRbtAdd()
    {
        IterativeRBT<int, int> tree = new IterativeRBT<int, int>();
        foreach (int item in _data)
            tree.Add(item, item);
    }
    [Benchmark]
    public void SortedDictionaryAdd()
    {
        SortedDictionary<int, int> sortedDict = new SortedDictionary<int, int>();
        foreach (int item in _data)
            sortedDict.Add(item, item);
    }

    // --- Contains ---
    [Benchmark]
    public void RecursiveRbtContains()
    {
        foreach (int item in _data)
            _recursiveRbt.Contains(item);
    }
    [Benchmark]
    public void IterativeRbtContains()
    {
        foreach (int item in _data)
            _iterativeRbt.Contains(item);
    }
    [Benchmark]
    public void SortedDictionaryContains()
    {
        foreach (int item in _data)
            _sortedDict.ContainsKey(item);
    }

    // --- Remove ---
    [Benchmark]
    public void RecursiveRbtRemove()
    {
        foreach (int item in _data)
            _recursiveRbt.Remove(item);
    }
    [Benchmark]
    public void IterativeRbtRemove()
    {
        foreach (int item in _data)
            _iterativeRbt.Remove(item);
    }
    [Benchmark]
    public void SortedDictionaryRemove()
    {
        foreach (int item in _data)
            _sortedDict.Remove(item);
    }

}

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<TreeBenchmark>();
    }

}
