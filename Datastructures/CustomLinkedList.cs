using System.Collections;

namespace Datastructures;

public class CustomLinkedList<T> : IEnumerable<T> where T : IComparable<T>
{
    private class Node(T value)
    {
        public T Value { get; set; } = value;
        public Node? Next { get; set; }
        public Node? Previous { get; set; }
    }

    private int _count;
    private Node? _head;

    public int Count => _count;
    public T? First => _head is null ? default : _head.Value;
    public T? Last
    {
        get
        {
            Node? current = _head;
            while (current is not null && current.Next is not null)
            {
                current = current.Next;
            }
            return current!.Value;
        }
    }

    #region Public Methods
    public void AddFirst(T item)
    {
        Node newNode = new Node(item);

        if (_head is null)
        {
            newNode.Next = null;
            newNode.Previous = null;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
        }

        _head = newNode;
        _count++;
    }
    public void AddLast(T item)
    {
        Node newItem = new Node(item);

        if (_head is null)
            _head = newItem;
        else
        {
            Node current = _head;
            while (current.Next is not null)
                current = current.Next;

            current.Next = newItem;
            newItem.Previous = current;
        }

        _count++;
    }
    public bool RemoveFirst(T item)
    {
        if (_head is null)
            return false;

        Node current = _head;

        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                if (current.Previous is null)
                {
                    _head = current.Next;
                    if (_head is not null) _head.Previous = null;
                }
                else
                {
                    current.Previous.Next = current.Next;
                    if (current.Next is not null) current.Next.Previous = current.Previous;
                }
                _count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }
    public bool RemoveLast(T item)
    {
        if (_head is null) return false;

        Node current = _head;

        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                if (_head.Next is null)
                {
                    _head = null;
                }
                else
                {
                    current = _head;
                    while (current.Next.Next is not null)
                    {
                        current = current.Next;
                    }
                    current.Next = null;
                }

                _count--;
                return true;
            }

            current = current.Next;
        }
        return false;
    }
    public bool Remove(T item)
    {
        if (_head is null) return false;

        bool wasRemoved = false;
        Node current = _head;
        Node? prevBlock = null;

        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                if (prevBlock is null)
                {
                    _head = _head.Next;
                    if (_head is not null)
                        _head.Previous = null;
                }
                else
                {
                    prevBlock.Next = current.Next;
                    if (current.Next is not null)
                        current.Previous = prevBlock;
                }

                _count--;
                wasRemoved = true;

                if (prevBlock is null)
                    current = _head;
                else
                    current = prevBlock.Next;
            }
            else
            {
                prevBlock = current;
                current = current.Next;
            }
        }
        return wasRemoved;
    }
    public void SortAsc()
    {
        T[] array = new T[_count];
        int index = 0;

        foreach (T item in this)
        {
            array[index++] = item;
            RemoveFirst(item);
        }

        array = MyOwnSort(array);

        foreach (T item in array)
            AddLast(item);
    }
    public void SortDesc()
    {
        T[] array = new T[_count];
        int index = 0;

        foreach (T item in this)
        {
            array[index++] = item;
            RemoveFirst(item);
        }

        MyOwnSort(array);

        foreach (T item in array)
            AddFirst(item);

    }
    #endregion

    #region Help Methods
    private T[] MyOwnSort(T[] arr)
    {
        bool traded = true;
        while (traded)
        {
            traded = false;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i].CompareTo(arr[i + 1]) > 0)
                {
                    T item = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = item;
                    traded = true;
                }
            }
        }

        return arr;
    }
    #endregion

    #region IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }
    public struct Enumerator : IEnumerator<T>, IEnumerator
    {
        private readonly CustomLinkedList<T> list;
        private Node? block;
        private T current;
        private bool hasStarted;

        public Enumerator(CustomLinkedList<T> list)
        {
            this.list = list;
            block = list._head;
            current = default!;
            hasStarted = false;
        }

        public readonly T Current => current;
        readonly object? IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (!hasStarted)
                hasStarted = true;
            else
                block = block?.Next;

            if (block == null)
                return false;

            current = block.Value;
            return true;
        }

        void IEnumerator.Reset()
        {
            hasStarted = false;
            block = list._head;
        }

        public readonly void Dispose() { }
    }

    public ReverseEnumerable GetReverseEnumerable()
    {
        return new ReverseEnumerable(this);
    }
    public readonly struct ReverseEnumerable : IEnumerable<T>
    {
        private readonly CustomLinkedList<T> list;

        internal ReverseEnumerable(CustomLinkedList<T> list)
        {
            this.list = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ReverseEnumerator(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public struct ReverseEnumerator : IEnumerator<T>, IEnumerator
    {
        private readonly CustomLinkedList<T> list;
        private Node? node;
        private T current;
        private bool hasStarted;

        internal ReverseEnumerator(CustomLinkedList<T> list)
        {
            this.list = list;
            node = new Node(list.Last); // Start am Ende der Liste
            current = default!;
            hasStarted = false;
        }

        public T Current => current;

        object? IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (!hasStarted)
                hasStarted = true;

            else if (node != null)
                node = node.Previous;


            if (node == null)
                return false;

            current = node.Value;
            return true;
        }

        void IEnumerator.Reset()
        {
            hasStarted = false;
            node = new Node(list.Last);
        }

        public void Dispose()
        {

        }
    }
    #endregion

}