using Conventional;
using Todo.Domain.Abstractions;
using Todo.Tests.Conventions.CustomConventions;
using Xunit;

namespace Todo.Tests.Conventions;

[Trait("Category", "Convention")]
public class AggregateRootConventionTests
{
    [Fact]
    public void GivenAnAggregate_ItMustHaveADefaultConstructor() =>
        TypeHelper.AllAggregates()
                  .MustConformTo(Convention.MustHaveADefaultConstructor)
                  .WithFailureAssertion(msg => Assert.True(false, msg));

    [Fact]
    public void GivenAnAggregate_AllPropertiesMustHavePrivateSetters() =>
        TypeHelper.AllAggregates()
                  .MustConformTo(new PropertiesMustHavePrivateSettersOrNoSettersConventionSpecification())
                  .WithFailureAssertion(msg => Assert.True(false, msg));

    [Fact]
    public void GivenAnAggregate_AllPropertiesMustBeAssignedDuringConstruction() =>
        TypeHelper.AllAggregates()
                  .MustConformTo(Convention.AllPropertiesMustBeAssignedDuringConstruction())
                  .WithFailureAssertion(msg => Assert.True(false, msg));

    [Fact]
    public void GivenAnAggregate_PropertiesMustNotBeDerivedFromAggregateRoot() =>
        TypeHelper.AllAggregates()
                  .MustConformTo(new PropertiesMustNotBeDeriveFromTypeConventionSpecification(typeof(AggregateRoot), "Aggregates must only reference other Aggregates by Id."))
                  .WithFailureAssertion(msg => Assert.True(false, msg));
}
