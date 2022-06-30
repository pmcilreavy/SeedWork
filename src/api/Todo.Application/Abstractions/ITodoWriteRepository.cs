namespace Todo.Application.Abstractions;

public interface ITodoWriteRepository : IWriteRepository<Domain.Aggregates.Todo.Todo>
{
}
