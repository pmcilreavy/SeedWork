﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Abstractions;
using Todo.Domain.Abstractions.DomainEvent;
using Todo.Domain.Exceptions;

namespace Todo.Infrastructure.Persistance;

public class TodoContext : DbContext, IWriteRepository, IReadRepository
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public TodoContext(DbContextOptions<TodoContext> options, IDomainEventDispatcher domainEventDispatcher) :
        base(options) =>
        _domainEventDispatcher = domainEventDispatcher;

    public DbSet<Domain.Aggregates.Todo.Todo> Todos => Set<Domain.Aggregates.Todo.Todo>();

    public IQueryable<T> GetTable<T>() where T : AggregateRoot => Set<T>().AsNoTracking().AsQueryable();

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

    public new T Add<T>(T aggregate) where T : AggregateRoot => Set<T>().Add(aggregate).Entity;

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