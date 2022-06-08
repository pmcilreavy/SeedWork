using Xunit;

namespace Todo.Tests.Unit;

public class CanaryTests
{
    [Fact]
    public void GivenXunitIsSetupCorrectly_WhenExecuted_ThenThisTestWillAlwaysSucceed()
    {
        Assert.True(true);
    }
}
