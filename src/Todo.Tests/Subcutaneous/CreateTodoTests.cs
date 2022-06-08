using System;
using System.Threading.Tasks;
using Todo.Tests.Gubbins;
using Todo.Web.Controllers;
using Xunit;

namespace Todo.Tests.Subcutaneous;

[Collection(nameof(TestFixtureCollection))]
public class CreateTodoTests
{
    private readonly TestContext _context;

    public CreateTodoTests(TestContext context)
    {
        _context = context;
    }

    [Fact]
    public async Task GivenRequest_WhenValid_ThenTodoIsCreated()
    {
        var dto = new CreateTodoDto("Yeet!", "Double Yeet!");
        var newTodoId = await _context.CreateClient().CreateTodo(dto);

        Assert.NotStrictEqual(newTodoId, Guid.Empty);
    }
}