using Todo.Domain.Abstractions;

namespace Todo.Application.Abstractions;

public interface IWriteRepository<T>
    : IUnitOfWork
    where T : AggregateRoot

{
    public T Add(T aggregate);

    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
