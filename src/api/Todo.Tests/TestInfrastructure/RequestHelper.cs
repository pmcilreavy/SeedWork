using System.Text.Json;

namespace Todo.Tests.TestInfrastructure;

public static class RequestHelper
{
    public static async Task<Result<T>> PostAsync<T>(this HttpClient client, string uri, object request)
    {
        var httpResponse = await client.PostAsync(uri, new JsonContent(request));

        var content = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseObject = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = false,
            })!;

            return new Result<T>(httpResponse, responseObject, content);
        }

        return new Result<T>(httpResponse, default, content);
    }

    public static async Task<Result<T>> GetAsync<T>(this HttpClient client, string uri)
    {
        var httpResponse = await client.GetAsync(uri);

        var content = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            var responseObject = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = false,
            })!;

            return new Result<T>(httpResponse, responseObject, content);
        }

        return new Result<T>(httpResponse, default, content);
    }
}
