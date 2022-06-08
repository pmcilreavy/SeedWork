using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using SeedWork.DomainEvent;
using Todo.Domain.Exceptions;

namespace Todo.Infrastructure;

public class TodoContext : DbContext, IWriteRepository
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public TodoContext(DbContextOptions<TodoContext> options, IDomainEventDispatcher domainEventDispatcher) :
        base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    public DbSet<Domain.Aggregates.Todo.Todo> Todos => Set<Domain.Aggregates.Todo.Todo>();

    public async Task SaveAsync(CancellationToken cancellationToken = default)
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

    public new T Add<T>(T aggregate) where T : AggregateRoot
    {
        return Set<T>().Add(aggregate).Entity;
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
