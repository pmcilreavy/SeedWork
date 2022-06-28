using Todo.Application.Todo.Commands.CreateTodo;
using Todo.Application.Todo.Queries.GetTodo;
using Todo.Application.Todo.Queries.ListTodos;

namespace Todo.Tests.TestInfrastructure;

public static class TodoApiHelper
{
    public static async Task<(Guid Id, HttpResponseMessage HttpResponse, string Body)> CreateTodo(this HttpClient client, CreateTodoDto request)
    {
        var result = await client.PostAsync<Guid>("/api/todo", request);

        return (result.ResponseObject, result.HttpResponse, result.Body);
    }

    public static async Task<(IReadOnlyCollection<ListTodosDto>? Results, HttpResponseMessage HttpResponse, string Body)> ListTodos(this HttpClient client)
    {
        var result = await client.GetAsync<IReadOnlyCollection<ListTodosDto>>("/api/todo/list");

        return (result.ResponseObject, result.HttpResponse, result.Body);
    }

    public static async Task<(GetTodoDto? Todo, HttpResponseMessage HttpResponse, string Body)> GetTodo(this HttpClient client, Guid id)
    {
        var result = await client.GetAsync<GetTodoDto>($"/api/todo/{id}");

        return (result.ResponseObject, result.HttpResponse, result.Body);
    }
}
