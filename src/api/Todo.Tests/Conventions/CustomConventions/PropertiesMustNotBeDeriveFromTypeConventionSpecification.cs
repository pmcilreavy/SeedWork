using Conventional;
using Conventional.Conventions;

namespace Todo.Tests.Conventions.CustomConventions;

internal class PropertiesMustNotBeDeriveFromTypeConventionSpecification : ConventionSpecification
{
    private readonly Type _propertyType;
    private readonly string _reason;

    public PropertiesMustNotBeDeriveFromTypeConventionSpecification(Type propertyType, string reason)
    {
        _propertyType = propertyType;
        _reason = reason;
    }

    protected override string FailureMessage => "Has a property derived from prohibited type {0}:";

    public override ConventionResult IsSatisfiedBy(Type type)
    {
        if (_propertyType.IsGenericTypeDefinition)
        {
            return type.GetProperties()
                       .Any(p => p.PropertyType.IsGenericType &&
                                 _propertyType.IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()))
                ? NotSatisfied(type)
                : ConventionResult.Satisfied(type.FullName);
        }

        return type.GetProperties().Any(p =>
        {
            var genericsAreAssignable = p.PropertyType.GetGenericArguments().Any(o => _propertyType.IsAssignableFrom(o));
            var elementTypeIsAssignable = p.PropertyType.GetElementType() != null && _propertyType.IsAssignableFrom(p.PropertyType.GetElementType()!);

            return genericsAreAssignable ||
                   elementTypeIsAssignable ||
                   _propertyType.IsAssignableFrom(p.PropertyType);
        })
            ? NotSatisfied(type)
            : ConventionResult.Satisfied(type.FullName);
    }

    private ConventionResult NotSatisfied(Type type)
    {
        var format = FailureMessage + Environment.NewLine + _reason;
        var message = string.Format(format, _propertyType.FullName);
        return ConventionResult.NotSatisfied(type.FullName, message);
    }
}

// Slightly different to the method in Conventional
