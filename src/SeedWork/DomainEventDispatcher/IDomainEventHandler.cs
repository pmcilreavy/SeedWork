﻿namespace SeedWork.DomainEventDispatcher;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent notification, CancellationToken cancellationToken = default);
}