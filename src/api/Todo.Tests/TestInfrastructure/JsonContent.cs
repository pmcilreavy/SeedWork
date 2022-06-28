using System.Text;
using System.Text.Json;

namespace Todo.Tests.TestInfrastructure;

public class JsonContent : StringContent
{
    public JsonContent(object @object) : base(
                                              JsonSerializer.Serialize(@object, new JsonSerializerOptions
                                              {
                                                  WriteIndented = true,
                                                  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                                  PropertyNameCaseInsensitive = false
                                              }),
                                              Encoding.UTF8,
                                              "application/json")
    {
        //
    }
}
