using Microsoft.AspNetCore.Mvc;
using Todo.Application.Todo.Queries.ListTodos;

namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<IReadOnlyCollection<ListTodosDto>>> Get(CancellationToken cancellationToken) => throw new InvalidOperationException("Argh!");
}
