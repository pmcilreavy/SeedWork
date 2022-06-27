namespace SeedWork;

public interface IUnitOfWork
{
    public Task SaveAsync(CancellationToken cancellationToken = default);
}
