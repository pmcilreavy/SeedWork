using System.Reflection;

namespace Todo.Web;

public static class TheWeb
{
    public static Assembly Assembly => typeof(TheWeb).GetTypeInfo().Assembly;
}