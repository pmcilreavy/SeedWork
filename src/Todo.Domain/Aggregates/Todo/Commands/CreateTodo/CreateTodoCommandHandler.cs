using SeedWork;
using SeedWork.CommandDispatcher;

namespace Todo.Domain.Aggregates.Todo.Commands.CreateTodo;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Guid>
{
    private readonly IWriteRepository _writer;

    public CreateTodoCommandHandler(IWriteRepository writer)
    {
        _writer = writer;
    }

    public async Task<Guid> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var newTodo = new Todo(Guid.NewGuid(), command.Title);

        _writer.Add(newTodo);

        await _writer.SaveAsync(cancellationToken);

        return newTodo.Id;
    }
}