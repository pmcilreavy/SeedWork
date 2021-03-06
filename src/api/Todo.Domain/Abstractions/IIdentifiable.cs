namespace Todo.Domain.Abstractions;

public interface IIdentifiable<out T> where T : IComparable<T>, new()
{
    public T Id { get; }
}
