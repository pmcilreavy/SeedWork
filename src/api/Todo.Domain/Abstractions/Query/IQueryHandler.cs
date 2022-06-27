namespace SeedWork.Query;

public interface IQueryHandler<in TQuery, TQueryResult>
{
    string GetSql();

    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation = default);
}

public interface IQueryHandler<TQueryResult>
{
    string GetSql();

    Task<TQueryResult> Handle(CancellationToken cancellation = default);
}