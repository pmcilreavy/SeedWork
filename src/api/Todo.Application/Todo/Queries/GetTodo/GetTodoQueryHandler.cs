using Dapper;
using Todo.Domain.Abstractions.Query;

namespace Todo.Application.Todo.Queries.GetTodo;

public class GetTodoQueryHandler : IQueryHandler<Guid, GetTodoDto>
{
    private readonly IDbConnectionProvider _connectionProvider;

    public GetTodoQueryHandler(IDbConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

    public string GetSql() => "select id, title, description from dbo.todo where id = @id;";

    public async Task<GetTodoDto> Handle(Guid id, CancellationToken cancellation = default)
    {
        using var c = _connectionProvider.Create();
        var r = await c.QuerySingleAsync<GetTodoDto>(GetSql(), new { id });

        return r;
    }
}
