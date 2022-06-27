using Todo.Domain.Abstractions.DomainEvent;

namespace Todo.Application.Abstractions.DomainEvent;

public abstract class DomainEventHandlerWrapper<TDomainEvent> : DomainEventHandlerBase where TDomainEvent : IDomainEvent
{
    public abstract Task Handle(TDomainEvent notification,
                                IServiceProvider services,
                                CancellationToken cancellationToken = default);
}
