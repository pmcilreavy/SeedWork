using Todo.Domain.Abstractions;

namespace Todo.Domain.Aggregates.Todo;

public class Todo : AggregateRoot
{
    // ReSharper disable once UnusedMember.Global
    protected Todo() { }

    public Todo(
        Guid id,
        string title,
        string description,
        TodoStep[] steps) : base(id)
    {
        GuardAgainst.ArgumentBeingNullOrWhitespace(title);
        GuardAgainst.ArgumentBeingNull(steps);

        Title = title;
        Description = description;

        AddDomainEvent(new TodoCreatedEvent(id));
    }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    private IList<TodoStep> _steps = new List<TodoStep>();
    public IReadOnlyCollection<TodoStep> Steps => _steps.OrderBy(o => o.Order).ToArray();

    public void SetTitle(string title)
    {
        GuardAgainst.ArgumentBeingNullOrWhitespace(title);

        Title = title;
    }

    public void SetDescription(string description) => Description = description;

    public void AddStep(string title)
    {
        _steps.Add(new TodoStep(title, _steps.Max(o => o.Order) + 1));
    }
}