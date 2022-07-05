namespace Todo.Tests.TestInfrastructure;

public record Result<T>(HttpResponseMessage HttpResponse, T? ResponseObject, string Body);
