using Todo.Domain.Abstractions;

namespace Todo.Domain.Aggregates.Todo;

public class Todo : AggregateRoot
{
    protected Todo() { }

    public Todo(
        Guid id,
        string title,
        string description) : base(id)
    {
        Title = title;
        Description = description;

        AddDomainEvent(new TodoCreatedEvent(id));
    }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    //public IEnumerable<X> DescriptiXon { get;  set; } = null!;

    //public void SetTitle(string title) => Title = title;

    //public void SetDescription(string description) => Description = description;
}
