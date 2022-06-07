using System.Diagnostics.CodeAnalysis;
using SeedWork.DomainEventDispatcher;

namespace SeedWork;

public abstract class AggregateRoot : IAggregateRoot, IAuditable
{
    private readonly List<IDomainEvent> _domainEvents = new();

    private int? _hashCode;

    // ReSharper disable once UnusedMember.Global
    protected AggregateRoot()
    {
    }

    protected AggregateRoot(Guid id)
    {
        if (id == default) throw new ArgumentException("An id must be supplied", nameof(id));

        Id = id;
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    public Guid Id { get; }
    public DateTimeOffset CreatedOn { get; private set; }
    public string CreatedBy { get; private set; } = null!;
    public DateTimeOffset ModifiedOn { get; private set; }
    public string ModifiedBy { get; private set; } = null!;

    public void RecordModification(DateTimeOffset modifiedOn, string modifiedBy)
    {
        ModifiedOn = modifiedOn;
        ModifiedBy = modifiedBy;
    }

    public void RecordCreation(DateTimeOffset createdOn, string createdBy)
    {
        CreatedOn = createdOn;
        CreatedBy = createdBy;
    }

    private bool IsTransient()
    {
        return Id == default;
    }

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        var ar = (AggregateRoot) obj;
        if (ar.IsTransient() || IsTransient()) return false;

        return ar.Id == Id;
    }

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        if (IsTransient()) return base.GetHashCode();

        _hashCode ??=
            Id.GetHashCode() ^ 31; // (https://ericlippert.com/2011/02/28/guidelines-and-rules-for-gethashcode/)

        return _hashCode.Value;
    }
}