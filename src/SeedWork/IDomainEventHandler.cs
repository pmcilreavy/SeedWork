using Microsoft.Extensions.DependencyInjection;
using SeedWork.CommandDispatcher;

namespace SeedWork;

public abstract class DomainEventHandlerBase
{
    public abstract Task Handle(object notification, IServiceProvider services, CancellationToken cancellation);
}

public abstract class DomainEventHandlerWrapper<TDomainEvent> : DomainEventHandlerBase where TDomainEvent : IDomainEvent
{
    public abstract Task Handle(TDomainEvent notification, IServiceProvider services, CancellationToken cancellationToken);
}

public class DomainEventHandlerWrapperImpl<TDomainEvent> : DomainEventHandlerWrapper<TDomainEvent>
    where TDomainEvent : IDomainEvent
{

    public override Task Handle(object notification, IServiceProvider services, CancellationToken cancellation)
    {
        return Handle((TDomainEvent)notification, services, cancellation);
    }

    public override Task Handle(TDomainEvent notification, IServiceProvider services, CancellationToken cancellationToken)
    {
        var x = services.GetRequiredService<IDomainEventHandler<TDomainEvent>>();

        return x.Handle((TDomainEvent)notification, cancellationToken);
    }
}

public interface IDomainEventHandler<in TDomainEvent> 
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent notification, CancellationToken cancellationToken = default);
}