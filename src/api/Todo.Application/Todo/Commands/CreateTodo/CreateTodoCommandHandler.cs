using SeedWork;
using SeedWork.Command;

namespace Todo.Application.Todo.Commands.CreateTodo;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Guid>
{
    private readonly IWriteRepository _writer;

    public CreateTodoCommandHandler(IWriteRepository writer) => _writer = writer;

    public async Task<Guid> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var newTodo = new Domain.Aggregates.Todo.Todo(Guid.NewGuid(), command.Title, command.Description);

        _writer.Add(newTodo);

        await _writer.SaveAsync(cancellationToken);

        return newTodo.Id;
    }
}
