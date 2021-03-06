namespace Todo.Application.Abstractions.DomainEvent;

public abstract class DomainEventHandlerBase
{
    public abstract Task Handle(object notification,
                                IServiceProvider services,
                                CancellationToken cancellation = default);
}
