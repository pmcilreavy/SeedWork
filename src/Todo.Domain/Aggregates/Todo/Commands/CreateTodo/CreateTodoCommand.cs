using SeedWork.Command;

namespace Todo.Domain.Aggregates.Todo.Commands.CreateTodo;

public record CreateTodoCommand(string Title, string Description) : IDomainCommand<Guid>;