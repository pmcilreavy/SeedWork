using Microsoft.AspNetCore.Mvc;
using Todo.Application.Todo.Commands.CreateTodo;

namespace Todo.Web.Controllers.Todo;

[ApiController]
[Route("api/todo")]
public class CreateTodoController : ControllerBase
{
    private readonly CreateTodoCommandHandler _createTodoCommandHandler;

    public CreateTodoController(CreateTodoCommandHandler createTodoCommandHandler) => _createTodoCommandHandler = createTodoCommandHandler;

    /*
     * POST: Create a new entity when the user is not expected to know/create an id.
     *       Not guaranteed to be idempotent.
     *       https://restfulapi.net/http-methods/#post
     */
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoDto dto, CancellationToken cancellationToken)
    {
        var createdId = await _createTodoCommandHandler.Handle(new CreateTodoCommand(dto.Title, dto.Description), cancellationToken);

        return Created($"{Request.Path}/{createdId}", createdId);
    }
}
