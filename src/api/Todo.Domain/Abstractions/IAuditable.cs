namespace Todo.Domain.Abstractions;

public interface IAuditable
{
    DateTimeOffset CreatedOn { get; }
    string CreatedBy { get; }

    DateTimeOffset ModifiedOn { get; }
    string ModifiedBy { get; }

    void RecordCreation(DateTimeOffset createdOn, string createdBy);

    void RecordModification(DateTimeOffset modifiedOn, string modifiedBy);
}
