using Todo.Domain.Abstractions.DomainEvent;

namespace Todo.Domain.Aggregates.Todo;

public record TodoCreatedEvent(Guid TodoId) : IDomainEvent;
