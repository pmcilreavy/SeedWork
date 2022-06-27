using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Abstractions;

namespace Todo.Infrastructure.Persistance.EntityConfiguration;

public abstract class DefaultEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> configuration)
    {
        var isAggregate = typeof(AggregateRoot).IsAssignableFrom(typeof(T));
        var isEntityType = typeof(Entity).IsAssignableFrom(typeof(T));
        var isAuditable = typeof(IAuditable).IsAssignableFrom(typeof(T));
        //var isReferenceLookup = typeof(IReferenceLookup).IsAssignableFrom(typeof(T));
        //var isEnumeration = typeof(Enumeration).IsAssignableFrom(typeof(T));

        if (isAggregate)
        {
            configuration.HasKey(nameof(AggregateRoot.Id));
            configuration.Property(nameof(AggregateRoot.Id)).ValueGeneratedNever();
            //configuration.Ignore(nameof(AggregateRoot.DomainEvents));
        }
        else if (isEntityType)
        {
            configuration.HasKey(nameof(Entity.Id));
            configuration.Property(nameof(Entity.Id)).ValueGeneratedOnAdd();
        }
        //else if (isReferenceLookup)
        //{
        //    configuration.HasKey(nameof(IReferenceLookup.Id));
        //    configuration.Property(nameof(IReferenceLookup.Id)).ValueGeneratedNever();
        //    configuration.Property(nameof(IReferenceLookup.Description)).HasMaxLength(200).IsRequired();
        //}
        //else if (isEnumeration)
        //{
        //    configuration.HasKey(nameof(Enumeration.Id));
        //    configuration.Property(nameof(Enumeration.Id)).ValueGeneratedNever();
        //    configuration.Property(nameof(Enumeration.Description)).HasMaxLength(200).IsRequired();
        //}

        if (isAuditable)
        {
            configuration.Property(nameof(IAuditable.CreatedBy));
            configuration.Property(nameof(IAuditable.CreatedOn));
            configuration.Property(nameof(IAuditable.ModifiedBy));
            configuration.Property(nameof(IAuditable.ModifiedOn));
        }
    }
}
