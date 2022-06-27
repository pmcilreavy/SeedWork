namespace SeedWork;

public interface IReadRepository
{
    IQueryable<T> GetTable<T>() where T : AggregateRoot;
}