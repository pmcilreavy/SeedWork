namespace Todo.Application.Abstractions;

public interface IUnitOfWork
{
    public Task SaveAsync(CancellationToken cancellationToken);
}
