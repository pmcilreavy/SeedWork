using Todo.Domain.Abstractions.Command;

namespace Todo.Application.Todo.Commands.CreateTodo;

public record CreateTodoCommand(string Title, string Description) : IDomainCommand<Guid>;
