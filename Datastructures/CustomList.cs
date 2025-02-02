using System.Collections;

namespace Datastructures;

public class ConcurrentList<T> : IEnumerable<T> where T : class
{
    private const int MAX_COUNT = 16; // Todo: Remove MaxCount

    private static T[]? _tList = new T[16];

    private int _indexer = 0;

    private static int _count;
    public static int Count
    {
        get
        {
            return _count;
        }
        set
        {
            if (value > MAX_COUNT) throw new ListIsFullException("You can't add items to the list, the list is full.");
            _count = value;
        }
    }

    public bool this[T ManualIndex] => FindTIndex(ManualIndex); // Todo: Remove that shit to.
    private bool FindTIndex(T ManualIndex)
    {
        if (_tList is null) return false;

        for (int i = 0; i < _tList.Length; i++)
        {
            if (_tList[i] == ManualIndex)
            {
                return true;
            }
        }
        return false;
    }

    public void Add(T addT)
    {
        bool isEmpty = false;

        if (addT == null) throw new ArgumentNullException(nameof(addT));
        if (Count >= MAX_COUNT) throw new ListIsFullException($"You can't add \"{addT}\" to the list, the list is already full.");

        try
        {
            for (int i = 0; i < _tList.Length; i++)
                if (_tList[i] == null)
                {
                    isEmpty = true;
                    break;
                }

            if (isEmpty)
            {
                if (!this[addT])
                {
                    _tList[_indexer] = addT;
                    Count++;
                    _indexer++;
                }
                else throw new DuplicateItemException($"The item \"{addT}\" is already in the List, you can't add duplicates.");
            }
        }
        catch (DuplicateItemException) { }

    }
    public void Remove(T removeT)
    {
        try
        {
            if (this[removeT])
            {
                for (int i = 0; i < _tList.Length; i++)
                {
                    if (_tList[i] == removeT)
                    {
                        _tList[i] = null;
                        Count--;
                        _indexer = i;
                    }
                }
            }
            else
            {
                throw new ItemIsNotInTheListException($"The item \"{removeT}\" is not in the list, you can't remove it.");
            }
        }
        catch (ItemIsNotInTheListException e)
        {
            Console.WriteLine(e);
        }
    }
    public void Clear()
    {
        _tList = new T[16];
    }

    #region IEnumerable
    public IEnumerator<T> GetEnumerator()
    {
        return new CustomListEnumerator<T>(_tList);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    class CustomListEnumerator<T> : IEnumerator<T> where T : class
    {
        private readonly T[] _list;
        private int _index = -1;
        private T? _newT;

        public CustomListEnumerator(T[] list)
        {
            _list = list;
        }

        object? IEnumerator.Current => _newT;

        public T? Current => _newT;

        public bool MoveNext()
        {
            while (++_index < _list.Length)
            {
                _newT = _list[_index];

                if (_newT is not null)
                    return true;
            }
            return false;
        }

        public void Reset()
        {
            _index = -1;
        }

        public void Dispose() { }
    }
    #endregion

}
