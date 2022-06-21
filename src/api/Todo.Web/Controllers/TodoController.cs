using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Aggregates.Todo.Commands.CreateTodo;
using Todo.Domain.Aggregates.Todo.Queries;

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
     */
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoDto dto, CancellationToken cancellationToken)
    {
        var createdId = await _createTodoCommandHandler.Handle(new CreateTodoCommand(dto.Title, dto.Description), cancellationToken);

        return Created($"{Request.Path}/{createdId}", createdId);
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IReadOnlyCollection<ListTodoDto>>> List(CancellationToken cancellationToken)
    {
        var results = await _listTodosQueryHandler.Handle(cancellationToken);

        return new OkObjectResult(results);
    }
}
