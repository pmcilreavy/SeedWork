using SeedWork;
using Todo.Application.Todo.DomainEvents.TodoCreated;

namespace Todo.Domain.Aggregates.Todo;

public class Todo : AggregateRoot
{
    public Todo(
        Guid id,
        string title,
        string description) : base(id)
    {
        Title = title;
        Description = description;

        AddDomainEvent(new TodoCreatedEvent(id));
    }

    public string Title { get; private set; }
    public string Description { get; private set; }

    public void SetTitle(string title) => Title = title;

    public void SetDescription(string description) => Description = description;
}
