using System.Diagnostics.CodeAnalysis;
using Todo.Domain.Abstractions.DomainEvent;

namespace Todo.Domain.Abstractions;

public abstract class AggregateRoot : IAggregateRoot, IAuditable
{
    private readonly List<IDomainEvent> _domainEvents = new();

    private int? _hashCode;

    protected AggregateRoot(Guid id)
    {
        GuardAgainst.ArgumentBeingEmpty(id, "An Id must be supplied");

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

    private bool IsTransient() => Id == default;

    public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        var ar = (AggregateRoot)obj;
        if (ar.IsTransient() || IsTransient())
        {
            return false;
        }

        return ar.Id == Id;
    }

    public static bool operator ==(AggregateRoot left, AggregateRoot right)
    {
        if (left is null)
        {
            if (right is null)
            {
                return true;
            }

            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(AggregateRoot left, AggregateRoot right) => !(left == right);

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        if (IsTransient())
        {
            return base.GetHashCode();
        }

        _hashCode ??=
            Id.GetHashCode() ^ 31; // (https://ericlippert.com/2011/02/28/guidelines-and-rules-for-gethashcode/)

        return _hashCode.Value;
    }
}
