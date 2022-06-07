using SeedWork;
using Todo.Domain.Aggregates.Todo.DomainEvents.TodoCreated;

namespace Todo.Domain.Aggregates.Todo;

public class Todo : AggregateRoot
{
    public Todo(
        Guid id,
        string title) : base(id)
    {
        Title = title;

        AddDomainEvent(new TodoCreatedEvent(id));
    }

    public string Title { get; private set; }

    public void SetTitle(string title)
    {
        Title = title;
    }
}