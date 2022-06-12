using Xunit;

namespace Todo.Tests.Unit;

[Trait("Category", "Unit")]
public class CanaryTests
{
    [Fact]
    public void GivenXunitIsSetupCorrectly_WhenExecuted_ThenThisTestWillAlwaysSucceed() => Assert.True(true);
}
