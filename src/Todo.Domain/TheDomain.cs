using System.Reflection;

namespace Todo.Domain;

public static class TheDomain
{
    public static Assembly Assembly => typeof(TheDomain).Assembly;
}