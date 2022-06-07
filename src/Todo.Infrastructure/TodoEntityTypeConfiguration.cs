using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Aggregates;

namespace Todo.Infrastructure;

public class TodoEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Todo.Todo>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Todo.Todo> configuration)
    {
        configuration.HasKey(a => a.Id);
        configuration.Property(a => a.Id).ValueGeneratedOnAdd();

        configuration.Property(a => a.Title).HasMaxLength(250).IsRequired();

        configuration.Property(a => a.CreatedBy);
        configuration.Property(a => a.CreatedOn);
        configuration.Property(a => a.ModifiedBy);
        configuration.Property(a => a.ModifiedOn);

        configuration.ToTable(nameof(Todo), "dbo");

    }
}