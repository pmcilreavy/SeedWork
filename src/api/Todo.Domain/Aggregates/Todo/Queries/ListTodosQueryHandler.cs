using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SeedWork.Query;

namespace Todo.Domain.Aggregates.Todo.Queries;

public record ListTodoDto(Guid Id, string Title, string Description);


public class ListTodosQueryHandler : IQueryHandler<ListTodoDto[]>
{
    private readonly IDbConnectionProvider _connectionProvider;

    public ListTodosQueryHandler(IDbConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public string GetSql() => "select id, title, description from dbo.todo order by ModifiedOn desc;";

    public async Task<ListTodoDto[]> Handle(CancellationToken cancellation = default)
    {
        using var c = _connectionProvider.Create();
        var r= await c.QueryAsync<ListTodoDto>(GetSql());

        return r.ToArray();
    }
}
