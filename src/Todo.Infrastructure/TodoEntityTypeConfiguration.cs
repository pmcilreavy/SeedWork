using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todo.Infrastructure;

public class TodoEntityTypeConfiguration : DefaultEntityTypeConfiguration<Domain.Aggregates.Todo.Todo>
{
    public override void Configure(EntityTypeBuilder<Domain.Aggregates.Todo.Todo> configuration)
    {
        base.Configure(configuration);

        configuration.Property(a => a.Title).HasMaxLength(250).IsRequired();
        configuration.Property(a => a.Description).HasMaxLength(500).IsRequired();
        
        configuration.ToTable(nameof(Todo), "dbo");
    }
}