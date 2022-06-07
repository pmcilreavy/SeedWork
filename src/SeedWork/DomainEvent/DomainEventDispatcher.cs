namespace SeedWork.DomainEvent;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken)
        where TDomainEvent : IDomainEvent
    {
        var handlerType = typeof(DomainEventHandlerWrapperImpl<>).MakeGenericType(domainEvent.GetType());

        var handler = (DomainEventHandlerBase) Activator.CreateInstance(handlerType)!;

        await handler.Handle(domainEvent, _serviceProvider, cancellationToken);
    }
}