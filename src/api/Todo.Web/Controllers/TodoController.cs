using Microsoft.AspNetCore.Mvc;
using Todo.Application.Todo.Commands.CreateTodo;
using Todo.Application.Todo.Queries.ListTodos;

namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly CreateTodoCommandHandler _createTodoCommandHandler;
    private readonly ListTodosQueryHandler _listTodosQueryHandler;

    public TodoController(CreateTodoCommandHandler createTodoCommandHandler, ListTodosQueryHandler listTodosQueryHandler)
    {
        _createTodoCommandHandler = createTodoCommandHandler;
        _listTodosQueryHandler = listTodosQueryHandler;
    }

    /*
     * POST: Create a new entity when the user is not expected to know/create an id.
     *       Not guaranteed to be idempotent.
     *       https://restfulapi.net/http-methods/#post
     */
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoDto dto, CancellationToken cancellationToken)
    {
        var createdId = await _createTodoCommandHandler.Handle(new CreateTodoCommand(dto.Title, dto.Description), cancellationToken);

        return Created($"{Request.Path}/{createdId}", createdId);
    }

    /*
     * https://restfulapi.net/http-methods/#get
     */
    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IReadOnlyCollection<ListTodosDto>>> List(CancellationToken cancellationToken)
    {
        var results = await _listTodosQueryHandler.Handle(cancellationToken);

        return new OkObjectResult(results);
    }
}
