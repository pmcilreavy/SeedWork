using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Aggregates.Todo.Commands.CreateTodo;

namespace Todo.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly CreateTodoCommandHandler _createTodoCommandHandler;

    public TodoController(CreateTodoCommandHandler createTodoCommandHandler)
    {
        _createTodoCommandHandler = createTodoCommandHandler;
    }

    /*
     * POST: Create a new entity when the user is not expected to know/create an id.
     *       Not guaranteed to be idempotent.
     */
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoDto dto, CancellationToken cancellationToken)
    {
        var createdId = await _createTodoCommandHandler.Handle(new CreateTodoCommand(dto.Title), cancellationToken);

        return Created($"{Request.Path}/{createdId}", createdId);
    }
}