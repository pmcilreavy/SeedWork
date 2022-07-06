namespace Todo.Application.Todo.Queries.GetTodo;

public record TodoStepDto
{
    protected TodoStepDto()
    {
    }

    public TodoStepDto(int id, string name, int order)
    {
        Id = id;
        Name = name;
        Order = order;
    }

    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public int Order { get; init; }
}
