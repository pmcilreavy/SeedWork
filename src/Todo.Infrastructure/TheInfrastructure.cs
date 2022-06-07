using System.Reflection;

namespace Todo.Infrastructure;

public static class TheInfrastructure
{
    public static Assembly Assembly => typeof(TheInfrastructure).GetTypeInfo().Assembly;
}