using Todo.Domain.Aggregates.Todo.Commands.CreateTodo;
using Todo.Tests.TestInfrastructure;
using Xunit;

namespace Todo.Tests.Subcutaneous;

[Trait("Category", "Subcutaneous")]
[Collection(nameof(TestFixtureCollection))]
public class CreateTodoTests
{
    private readonly TestContext _context;

    public CreateTodoTests(TestContext context) => _context = context;

    [Fact]
    public async Task GivenCreateTodoRequest_WhenValid_ThenTodoIsCreated()
    {
        var dto = new CreateTodoDto("Remember Remember", "The 5th of November.");
        var (newTodoId, _, _) = await _context.CreateClient().CreateTodo(dto);

        Assert.NotEqual(Guid.Empty, newTodoId);
    }
}
