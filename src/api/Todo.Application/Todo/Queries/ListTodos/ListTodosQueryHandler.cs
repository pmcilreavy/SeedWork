using Dapper;
using Todo.Domain.Abstractions.Query;

namespace Todo.Application.Todo.Queries.ListTodos;

public class ListTodosQueryHandler : IQueryHandler<ListTodosDto[]>
{
    private readonly IDbConnectionProvider _connectionProvider;

    public ListTodosQueryHandler(IDbConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

    public string GetSql() => "select id, title, description from dbo.todo order by ModifiedOn desc;";

    public async Task<ListTodosDto[]> Handle(CancellationToken cancellation = default)
    {
        using var c = _connectionProvider.Create();
        var r = await c.QueryAsync<ListTodosDto>(GetSql());

        return r.ToArray();
    }
}
