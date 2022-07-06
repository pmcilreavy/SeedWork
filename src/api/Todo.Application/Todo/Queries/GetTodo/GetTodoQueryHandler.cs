using Dapper;
using Slapper;
using Todo.Domain.Abstractions.Query;

namespace Todo.Application.Todo.Queries.GetTodo;

public class GetTodoQueryHandler : IQueryHandler<Guid, GetTodoDto>
{
    private readonly IDbConnectionProvider _connectionProvider;

    public GetTodoQueryHandler(IDbConnectionProvider connectionProvider) => _connectionProvider = connectionProvider;

    public string GetSql() =>
        @"select t.[Id], t.[Title], t.[Description], ts.Id Steps_Id, ts.[Name] Steps_Name, ts.[Order] Steps_Order
from [dbo].[Todo] t
left outer join [dbo].[TodoStep] ts on ts.TodoId = t.Id
where t.id = @id;";

    public async Task<GetTodoDto> Handle(Guid id, CancellationToken cancellation = default)
    {
        using var c = _connectionProvider.Create();
        var r = await c.QueryAsync(
                                   GetSql(),
                                   new { id });

        AutoMapper.Configuration.AddIdentifiers(typeof(GetTodoDto), new[] { "Id" });
        AutoMapper.Configuration.AddIdentifiers(typeof(TodoStepDto), new[] { "Id" });

        var todo = AutoMapper.MapDynamic<GetTodoDto>(r, false).Single();

        return todo;
    }
}
