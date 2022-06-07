﻿namespace SeedWork.QueryDispatcher;

public interface IQueryExecutor<in TQuery, TQueryResult>
{
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation = default);
}