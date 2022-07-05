using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Aggregates.Todo;

namespace Todo.Infrastructure.Persistance.EntityConfiguration;

public class TodoStepEntityTypeConfiguration : DefaultEntityTypeConfiguration<Domain.Aggregates.Todo.TodoStep>
{
    public override void Configure(EntityTypeBuilder<Domain.Aggregates.Todo.TodoStep> configuration)
    {
        base.Configure(configuration);

        configuration.Property(a => a.Name).IsRequired();
        configuration.Property(a => a.Order).IsRequired();

        configuration.ToTable(nameof(TodoStep), "dbo");
    }


}