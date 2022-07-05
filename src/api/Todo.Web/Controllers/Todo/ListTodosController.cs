using Microsoft.AspNetCore.Mvc;
using Todo.Application.Todo.Queries.ListTodos;

namespace Todo.Web.Controllers.Todo;

[ApiController]
[Route("api/todo/list")]
public class ListTodosController : ControllerBase
{
    private readonly ListTodosQueryHandler _listTodosQueryHandler;

    public ListTodosController(ListTodosQueryHandler listTodosQueryHandler) => _listTodosQueryHandler = listTodosQueryHandler;

    /*
     * GET: retrieve data (read-only) no state changes.
     * https://restfulapi.net/http-methods/#get
     */
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ListTodosDto>>> List(CancellationToken cancellationToken)
    {
        var results = await _listTodosQueryHandler.Handle(cancellationToken);

        return new OkObjectResult(results);
    }
}
