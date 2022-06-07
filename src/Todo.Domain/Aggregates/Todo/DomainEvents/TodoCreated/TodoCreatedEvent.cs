using SeedWork.DomainEvent;

namespace Todo.Domain.Aggregates.Todo.DomainEvents.TodoCreated;

public record TodoCreatedEvent(Guid TodoId) : IDomainEvent;