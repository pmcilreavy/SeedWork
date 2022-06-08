using Xunit;

namespace Todo.Tests.TestInfrastructure;

[CollectionDefinition(nameof(TestFixtureCollection))]
public class TestFixtureCollection : ICollectionFixture<TestContext>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition]
}
