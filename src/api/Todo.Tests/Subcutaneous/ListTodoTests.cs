using Todo.Domain.Aggregates.Todo.Commands.CreateTodo;
using Todo.Tests.TestInfrastructure;
using Xunit;

namespace Todo.Tests.Subcutaneous;

[Trait("Category", "Subcutaneous")]
[Collection(nameof(TestFixtureCollection))]
public class ListTodoTests
{
    private readonly TestContext _context;

    public ListTodoTests(TestContext context) => _context = context;

    [Fact]
    public async Task GivenListTodosRequest_WhenValid_ThenTodosAreReturned()
    {
        var (oneId, _, _) = await _context.CreateClient().CreateTodo(new CreateTodoDto("One Title", "One Description."));
        var (twoId, _, _) = await _context.CreateClient().CreateTodo(new CreateTodoDto("Two Title", "Two Description."));

        var (results, _, _) = await _context.CreateClient().ListTodos();

        var actualCount = results?.Where(o => o.Id == oneId || o.Id == twoId).Count() ?? 0;

        Assert.Equal(2, actualCount);
    }
}
