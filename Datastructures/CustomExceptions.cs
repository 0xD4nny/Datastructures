namespace Datastructures;

internal class DuplicateItemException : Exception
{
    internal DuplicateItemException(string message) : base(message) { }
}
internal class ListIsFullException : Exception
{
    internal ListIsFullException(string message) : base(message) { }
}
internal class ItemIsNotInTheListException : Exception
{
    internal ItemIsNotInTheListException(string message) : base(message) { }
}
internal class UnreachableException : Exception
{
    internal UnreachableException() : base("This code should never be reached!") { }
    internal UnreachableException(string message) : base(message) { }
}
