using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Todo.Tests.TestInfrastructure;
using Xunit;

namespace Todo.Tests.Subcutaneous;

[Trait("Category", "Subcutaneous")]
[Collection(nameof(TestFixtureCollection))]
public class ProblemJsonTests
{
    private readonly TestContext _context;

    public ProblemJsonTests(TestContext context) => _context = context;

    [Fact]
    public async Task GivenFailingRequest_WhenCalled_ThenProblemJsonIsReturned()
    {
        var result = await _context.CreateClient().GetTodo(Guid.NewGuid());

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(result.Body, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

        Assert.Equal((int)HttpStatusCode.InternalServerError, problemDetails.Status);
        Assert.Equal("Sequence contains no elements", problemDetails.Detail);
        Assert.Equal("Internal Server Error", problemDetails.Title);
    }
}
