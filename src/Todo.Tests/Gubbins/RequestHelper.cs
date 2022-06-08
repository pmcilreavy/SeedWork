using System.Text.Json;

namespace Todo.Tests.Gubbins;

public static class RequestHelper
{
    public static async Task<T> PostAsync<T>(this HttpClient client, string uri, object request)
    {
        var httpResponse = await client.PostAsync(uri, new JsonContent(request));

        httpResponse.EnsureSuccessStatusCode();

        await using var s = await httpResponse.Content.ReadAsStreamAsync();

        var response = (T)(await JsonSerializer.DeserializeAsync(s, typeof(T)))!;

        return response;
    }
}
