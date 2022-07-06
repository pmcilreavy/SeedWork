namespace Todo.Application.Todo.Queries.GetTodo;

public record GetTodoDto
{
    protected GetTodoDto()
    {
    }

    public GetTodoDto(Guid id, string title, string description, TodoStepDto[] steps)
    {
        Id = id;
        Title = title;
        Description = description;
        Steps = steps;
    }

    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IReadOnlyCollection<TodoStepDto> Steps { get; init; } = new List<TodoStepDto>();
}
