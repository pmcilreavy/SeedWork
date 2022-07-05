using Todo.Application;
using Todo.Domain;
using Todo.Domain.Abstractions;
using Todo.Infrastructure;
using Todo.Web;

namespace Todo.Tests.Conventions;

public static class TypeHelper
{
    public static IEnumerable<Type> AllExportedTypes()
    {
        return typeof(TheApplication).Assembly.GetExportedTypes()
                                     .Union(typeof(TheDomain).Assembly.GetExportedTypes())
                                     .Union(typeof(TheInfrastructure).Assembly.GetExportedTypes())
                                     .Union(typeof(TheWeb).Assembly.GetExportedTypes());
    }

    public static Type[] AllAggregates()
    {
        return AllExportedTypes()
               .Where(t => !t.IsAbstract && !t.IsInterface)
               .Where(t => typeof(AggregateRoot).IsAssignableFrom(t))
               .Where(t => t != typeof(AggregateRoot))
               .OrderBy(t => t.Namespace)
               .ThenBy(t => t.Name)
               .ToArray();
    }
}