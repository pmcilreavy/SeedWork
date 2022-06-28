using Microsoft.AspNetCore.Mvc;
using Todo.Application.Todo.Queries.GetTodo;

namespace Todo.Web.Controllers.Todo;

[ApiController]
[Route("api/todo/{id}")]
public class GetTodoController : ControllerBase
{
    private readonly GetTodoQueryHandler _getTodoQueryHandler;

    public GetTodoController(GetTodoQueryHandler getTodoQueryHandler) => _getTodoQueryHandler = getTodoQueryHandler;

    /*
     * GET: retrieve data (read-only) no state changes.
     * https://restfulapi.net/http-methods/#get
     */
    [HttpGet]
    public async Task<ActionResult<GetTodoDto>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var results = await _getTodoQueryHandler.Handle(id, cancellationToken);

        return new OkObjectResult(results);
    }
}
