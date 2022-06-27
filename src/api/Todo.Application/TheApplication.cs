using System.Reflection;

namespace Todo.Application;

public static class TheApplication
{
    public static Assembly Assembly => typeof(TheApplication).Assembly;
}
