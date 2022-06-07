namespace SeedWork.DomainEvent;

public interface IDomainEventDispatcher
{
    Task Dispatch<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken)
        where TDomainEvent : IDomainEvent;
}