using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Abstractions;
using Todo.Domain.Abstractions;
using Todo.Domain.Abstractions.DomainEvent;
using Todo.Domain.Exceptions;

namespace Todo.Infrastructure.Persistance;

public class TodoContext : DbContext, ITodoWriteRepository
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public TodoContext(DbContextOptions<TodoContext> options, IDomainEventDispatcher domainEventDispatcher) :
        base(options) =>
        _domainEventDispatcher = domainEventDispatcher;

    public Domain.Aggregates.Todo.Todo Add(Domain.Aggregates.Todo.Todo aggregate) => Set<Domain.Aggregates.Todo.Todo>().Add(aggregate).Entity;

    public Task<Domain.Aggregates.Todo.Todo> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        Set<Domain.Aggregates.Todo.Todo>().AsNoTracking().SingleAsync(o => o.Id == id, cancellationToken);

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        var domainEntities = ChangeTracker.Entries<AggregateRoot>()
                                          .Where(x => x.Entity.DomainEvents.Any())
                                          .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _domainEventDispatcher.Dispatch(domainEvent, cancellationToken);
        }

        //_auditService?.SetAuditData(this);
        SetCreationAndModificationFieldsForAuditableEntities(this);

        try
        {
            await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new ConcurrencyException("This record has been modified.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void SetCreationAndModificationFieldsForAuditableEntities(DbContext context)
    {
        var addedOrEditedEntries = context.ChangeTracker
                                          .Entries()
                                          .Where(e => e.State is EntityState.Added or EntityState.Modified)
                                          .ToList();

        foreach (var entry in addedOrEditedEntries)
        {
            if (entry.Entity is AggregateRoot aggregateRoot)
            {
                var now = DateTimeOffset.UtcNow;
                aggregateRoot.RecordCreation(now, "ME");
                aggregateRoot.RecordModification(now, "ME");
            }
        }
    }
}
