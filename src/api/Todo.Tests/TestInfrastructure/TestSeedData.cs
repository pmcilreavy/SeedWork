namespace Todo.Tests.TestInfrastructure;

public static class TestSeedData
{
    public static readonly Domain.Aggregates.Todo.Todo TodoOne = TodoBuilder.Default()
                                                                            .WithId(Guid.NewGuid())
                                                                            .WithTitle("Title One")
                                                                            .WithDescription("Description One")
                                                                            .Build();
}
