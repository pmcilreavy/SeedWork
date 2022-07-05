using Todo.Domain.Aggregates.Todo;

namespace Todo.Tests.TestInfrastructure;

public class TodoBuilder
{
    private string _description = "";
    private Guid _id = Guid.Empty;
    private string _title = "";
    private IList<TodoStep> _steps = new List<TodoStep>();

    public TodoBuilder WithId(Guid id)
    {
        _id = id;

        return this;
    }

    public TodoBuilder WithTitle(string title)
    {
        _title = title;

        return this;
    }

    public TodoBuilder WithDescription(string description)
    {
        _description = description;

        return this;
    }

    public TodoBuilder WithSteps(IList<TodoStep> steps)
    {
        _steps = steps;

        return this;
    }

    public Domain.Aggregates.Todo.Todo Build() => new(_id, _title, _description, _steps.ToArray());

    public static TodoBuilder Default() => new();
}
