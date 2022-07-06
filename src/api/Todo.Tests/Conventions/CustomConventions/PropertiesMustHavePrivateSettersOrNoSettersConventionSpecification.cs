using System.Reflection;
using Conventional.Conventions;

namespace Todo.Tests.Conventions.CustomConventions;

internal class PropertiesMustHavePrivateSettersOrNoSettersConventionSpecification : PropertyConventionSpecification
{
    protected override string FailureMessage => "All properties must either have private setters or no setters";

    protected override PropertyInfo[] GetNonConformingProperties(Type type)
    {
        var thing = GetDeclaredProperties(type)
                    .Where(subject => subject.CanWrite && subject.GetSetMethod(true)!.IsPrivate == false)
                    .ToArray();

        return thing;
    }

    private static PropertyInfo[] GetDeclaredProperties(Type type) =>
        type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
}
