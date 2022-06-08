using Microsoft.Extensions.DependencyInjection;

namespace SeedWork.DomainEvent;

public class DomainEventHandlerWrapperImpl<TDomainEvent> : DomainEventHandlerWrapper<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public override Task Handle(object notification,
                                IServiceProvider services,
                                CancellationToken cancellation = default)
    {
        return Handle((TDomainEvent)notification, services, cancellation);
    }

    public override Task Handle(TDomainEvent notification,
                                IServiceProvider services,
                                CancellationToken cancellationToken = default)
    {
        var x = services.GetRequiredService<IDomainEventHandler<TDomainEvent>>();

        return x.Handle(notification, cancellationToken);
    }
}
