namespace Todo.Domain.Abstractions;

public interface IUnitOfWork
{
    public Task SaveAsync(CancellationToken cancellationToken = default);
}
