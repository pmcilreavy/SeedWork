using Xunit;

namespace Todo.Tests
{
    public class CanaryTest
    {
        [Fact]
        public void GivenXunitIsSetupCorrectly_WhenExecuted_ThenThisTestWillAlwaysSucceed()
        {
            Assert.True(true);
        }
    }
}