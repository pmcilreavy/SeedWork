namespace Todo.Domain.Abstractions.DomainEvent;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent notification, CancellationToken cancellationToken = default);
}
