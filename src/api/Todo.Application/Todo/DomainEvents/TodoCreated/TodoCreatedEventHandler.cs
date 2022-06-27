using System.Diagnostics;
using Todo.Domain.Abstractions.DomainEvent;
using Todo.Domain.Aggregates.Todo;

namespace Todo.Application.Todo.DomainEvents.TodoCreated;

public class TodoCreatedEventHandler : IDomainEventHandler<TodoCreatedEvent>
{
    public Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Yeet!");
        return Task.CompletedTask;
    }
}
