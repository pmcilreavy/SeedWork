namespace SeedWork;

public interface IAuditable
{
    DateTimeOffset CreatedOn { get; }
    string CreatedBy { get; }

    void RecordCreation(DateTimeOffset createdOn, string createdBy);

    DateTimeOffset ModifiedOn { get; }
    string ModifiedBy { get; }

    void RecordModification(DateTimeOffset modifiedOn, string modifiedBy);
}