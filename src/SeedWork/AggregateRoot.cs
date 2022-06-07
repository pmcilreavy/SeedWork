using System.Diagnostics.CodeAnalysis;

namespace SeedWork
{
    public abstract class AggregateRoot : IAggregateRoot, IAuditable
    {
        // ReSharper disable once UnusedMember.Global
        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid id)
        {
            if (id == default)
            {
                throw new ArgumentException("An id must be supplied", nameof(id));
            }

            Id = id;
        }

        private bool IsTransient() => Id == default;

        public Guid Id { get; private set; }
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

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

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
            if (obj.GetType() != this.GetType()) return false;

            var ar = (AggregateRoot)obj;
            if (ar.IsTransient() || IsTransient())
            {
                return false;
            }

            return ar.Id == Id;
        }

        private int? _hashCode;

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            _hashCode ??= Id.GetHashCode() ^ 31; // (https://ericlippert.com/2011/02/28/guidelines-and-rules-for-gethashcode/)

            return _hashCode.Value;

        }
    }
}