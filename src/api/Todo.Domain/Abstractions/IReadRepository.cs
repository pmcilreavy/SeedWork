namespace Todo.Domain.Abstractions;

public interface IReadRepository
{
    IQueryable<T> GetTable<T>() where T : AggregateRoot;
}
