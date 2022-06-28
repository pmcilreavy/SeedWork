using Todo.Tests.TestInfrastructure;
using Xunit;

namespace Todo.Tests.Subcutaneous;

[Trait("Category", "Subcutaneous")]
[Collection(nameof(TestFixtureCollection))]
public class GetTodoTests
{
    private readonly TestContext _context;

    public GetTodoTests(TestContext context) => _context = context;

    [Fact]
    public async Task GivenGetTodoRequest_WhenValid_ThenTodoIsReturned()
    {
        var (todo, _, _) = await _context.CreateClient().GetTodo(TestContext.TestSeedData.TodoOne.Id);

        Assert.NotNull(todo);
        Assert.Equal(TestContext.TestSeedData.TodoOne.Id, todo!.Id);
        Assert.Equal(TestContext.TestSeedData.TodoOne.Title, todo!.Title);
        Assert.Equal(TestContext.TestSeedData.TodoOne.Description, todo!.Description);
    }
}
