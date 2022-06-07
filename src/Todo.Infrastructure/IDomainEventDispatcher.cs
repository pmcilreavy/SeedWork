using SeedWork;

namespace Todo.Infrastructure;

public interface IDomainEventDispatcher
{
    Task Dispatch<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken) where TDomainEvent : IDomainEvent;
}