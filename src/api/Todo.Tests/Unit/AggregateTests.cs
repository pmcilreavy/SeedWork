using Todo.Domain.Abstractions;
using Todo.Domain.Aggregates.Todo;
using Xunit;

namespace Todo.Tests.Unit;

[Trait("Category", "Unit")]
public class AggregateTests
{
    [Fact]
    public void GivenTwoAggregatesWithTheSameId_WhenCompared_ThenTheyAreEqual()
    {
        var id = Guid.NewGuid();

        var one = new Domain.Aggregates.Todo.Todo(id, "the title 1", "the description 1", Array.Empty<TodoStep>());
        var two = new Domain.Aggregates.Todo.Todo(id, "the title 2", "the description 2", Array.Empty<TodoStep>());

        Assert.Equal(one, two);
        Assert.True(one.Equals(two));
        Assert.True(one == two);
        Assert.False(one != two);
    }

    [Fact]
    public void GivenTwoAggregatesWithTheDifferentId_WhenCompared_ThenTheyAreNotEqual()
    {
        var one = new Domain.Aggregates.Todo.Todo(Guid.NewGuid(), "the title 1", "the description 1", Array.Empty<TodoStep>());
        var two = new Domain.Aggregates.Todo.Todo(Guid.NewGuid(), "the title 2", "the description 2", Array.Empty<TodoStep>());

        Assert.NotEqual(one, two);
        Assert.False(one.Equals(two));
        Assert.False(one == two);
        Assert.True(one != two);
    }

    [Fact]
    public void GivenTwoAggregatesRightNull_WhenCompared_ThenTheyAreNotEqual()
    {
        var one = new Domain.Aggregates.Todo.Todo(Guid.NewGuid(), "the title 1", "the description 1", Array.Empty<TodoStep>());
        var two = default(Domain.Aggregates.Todo.Todo);

        Assert.NotEqual(one, two);
        Assert.False(one.Equals(two));
        Assert.False(one == two!);
        Assert.True(one != two!);
    }

    [Fact]
    public void GivenTwoAggregatesLeftNull_WhenCompared_ThenTheyAreNotEqual()
    {
        var one = default(Domain.Aggregates.Todo.Todo);
        var two = new Domain.Aggregates.Todo.Todo(Guid.NewGuid(), "the title 1", "the description 1", Array.Empty<TodoStep>());

        Assert.NotEqual(one, two);
        Assert.False(one! == two);
        Assert.True(one! != two);
    }

    [Fact]
    public void GivenAnEmptyGuid_WhenPassedToAggregateConstructor_ThenArgumentExceptionThrown()
    {
        var ex = Assert.Throws<ArgumentException>(() => { _ = new Domain.Aggregates.Todo.Todo(Guid.Empty, "the title 1", "the description 1", Array.Empty<TodoStep>()); });

        Assert.Equal(nameof(AggregateRoot.Id).ToLowerInvariant(), ex.ParamName);
    }
}
