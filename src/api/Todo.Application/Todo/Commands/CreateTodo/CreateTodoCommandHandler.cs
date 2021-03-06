using Todo.Application.Abstractions;
using Todo.Domain.Abstractions.Command;
using Todo.Domain.Aggregates.Todo;

namespace Todo.Application.Todo.Commands.CreateTodo;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Guid>
{
    private readonly ITodoWriteRepository _writer;

    public CreateTodoCommandHandler(ITodoWriteRepository writer) => _writer = writer;

    public async Task<Guid> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var newTodo = new Domain.Aggregates.Todo.Todo(Guid.NewGuid(), command.Title, command.Description, Array.Empty<TodoStep>());

        _writer.Add(newTodo);

        await _writer.SaveAsync(cancellationToken);

        return newTodo.Id;
    }
}
