using System.Text;
using System.Text.Json;

namespace Todo.Tests.Gubbins;

public class JsonContent : StringContent
{
    public JsonContent(object @object) : base(
                                              JsonSerializer.Serialize(@object, new JsonSerializerOptions { WriteIndented = true }),
                                              Encoding.UTF8,
                                              "application/json")
    {
        //
    }
}
