using Todo.Domain.Abstractions;

namespace Todo.Domain.Aggregates.Todo;

public class TodoStep : Entity
{
    protected TodoStep() { }

    public TodoStep(string name, uint order)
    {
        GuardAgainst.ArgumentBeingNullOrWhitespace(name);

        Name = name;
    }

    public string Name { get; private set; } = null!;
    public uint Order { get; private set; } = default;
}