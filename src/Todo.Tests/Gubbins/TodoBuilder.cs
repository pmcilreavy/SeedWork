namespace Todo.Tests.Gubbins;

public class TodoBuilder
{
    private string _description = "";
    private Guid _id = Guid.Empty;
    private string _title = "";

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

    public Domain.Aggregates.Todo.Todo Build()
    {
        return new Domain.Aggregates.Todo.Todo(_id, _title, _description);
    }
}
