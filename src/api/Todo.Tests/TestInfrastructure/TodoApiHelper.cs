using Todo.Application.Todo.Commands.CreateTodo;
using Todo.Application.Todo.Queries.ListTodos;

namespace Todo.Tests.TestInfrastructure;

public static class TodoApiHelper
{
    public static async Task<(Guid Id, HttpResponseMessage HttpResponse, string Body)> CreateTodo(this HttpClient client, CreateTodoDto request)
    {
        var result = await client.PostAsync<Guid>("/api/todo/create", request);

        return (result.ResponseObject, result.HttpResponse, result.Body);
    }

    public static async Task<(IReadOnlyCollection<ListTodosDto>? Results, HttpResponseMessage HttpResponse, string Body)> ListTodos(this HttpClient client)
    {
        var result = await client.GetAsync<IReadOnlyCollection<ListTodosDto>>("/api/todo/list");

        return (result.ResponseObject, result.HttpResponse, result.Body);
    }
}
