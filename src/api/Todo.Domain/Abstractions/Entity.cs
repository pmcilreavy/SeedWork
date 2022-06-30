namespace Todo.Domain.Abstractions;

public abstract class Entity : IEntity
{
    protected Entity(int id) => Id = id;

    public int Id { get; }
}
