﻿using System.Diagnostics;
using SeedWork.DomainEventDispatcher;

namespace Todo.Domain.Aggregates.Todo.DomainEvents.TodoCreated;

public class TodoCreatedEventHandler : IDomainEventHandler<TodoCreatedEvent>
{
    public Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Yeet!");
        return Task.CompletedTask;
    }
}