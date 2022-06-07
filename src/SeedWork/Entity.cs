namespace SeedWork;

public abstract class Entity : IEntity
{
    // ReSharper disable once UnusedMember.Global
    protected Entity() { }

    protected Entity(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }

}