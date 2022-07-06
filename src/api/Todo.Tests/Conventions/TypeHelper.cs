using Todo.Application;
using Todo.Domain;
using Todo.Domain.Abstractions;
using Todo.Infrastructure;
using Todo.Web;

namespace Todo.Tests.Conventions;

public static class TypeHelper
{
    public static IEnumerable<Type> AllExportedTypes() =>
        TheApplication.Assembly.GetExportedTypes()
                      .Union(TheDomain.Assembly.GetExportedTypes())
                      .Union(TheInfrastructure.Assembly.GetExportedTypes())
                      .Union(TheWeb.Assembly.GetExportedTypes());

    public static Type[] AllAggregates() =>
        AllExportedTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => typeof(AggregateRoot).IsAssignableFrom(t))
            .Where(t => t != typeof(AggregateRoot))
            .OrderBy(t => t.Namespace)
            .ThenBy(t => t.Name)
            .ToArray();
}
