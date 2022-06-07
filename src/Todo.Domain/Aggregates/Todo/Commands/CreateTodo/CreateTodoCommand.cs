using SeedWork.CommandDispatcher;

namespace Todo.Domain.Aggregates.Todo.Commands.CreateTodo;

public record CreateTodoCommand(string Title) : IDomainCommand<Guid>;