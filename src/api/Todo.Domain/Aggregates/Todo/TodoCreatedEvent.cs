using SeedWork.DomainEvent;

namespace Todo.Application.Todo.DomainEvents.TodoCreated;

public record TodoCreatedEvent(Guid TodoId) : IDomainEvent;
