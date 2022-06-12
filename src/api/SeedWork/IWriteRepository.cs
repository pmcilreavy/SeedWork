namespace SeedWork;

public interface IWriteRepository : IUnitOfWork
{
    public T Add<T>(T aggregate) where T : AggregateRoot;
}

public interface IReadRepository
{
    IQueryable<T> GetTable<T>() where T : AggregateRoot;
}
