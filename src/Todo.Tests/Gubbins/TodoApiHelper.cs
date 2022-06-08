using Todo.Web.Controllers;

namespace Todo.Tests.Gubbins;

public static class TodoApiHelper
{
    public static async Task<Guid> CreateTodo(this HttpClient client, CreateTodoDto request)
    {
        var response = await client.PostAsync<Guid>("/todo", request);

        return response;
    }
}
