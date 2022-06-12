using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Aggregates.Todo.Commands.CreateTodo;
using Todo.Domain.Aggregates.Todo.Commands.ListTodos;

namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly CreateTodoCommandHandler _createTodoCommandHandler;
    private readonly ListTodosCommandHandler _listTodosCommandHandler;

    public TodoController(CreateTodoCommandHandler createTodoCommandHandler, ListTodosCommandHandler listTodosCommandHandler)
    {
        _createTodoCommandHandler = createTodoCommandHandler;
        _listTodosCommandHandler = listTodosCommandHandler;
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
        var results = await _listTodosCommandHandler.Handle(new ListTodosCommand(), cancellationToken);

        return new OkObjectResult(results);
    }
}
