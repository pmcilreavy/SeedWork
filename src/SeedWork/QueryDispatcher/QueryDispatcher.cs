using Microsoft.Extensions.DependencyInjection;

namespace SeedWork.QueryDispatcher;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default)
    {
        var handler = _serviceProvider.GetRequiredService<IQueryExecutor<TQuery, TQueryResult>>();
        return handler.Handle(query, cancellation);
    }
}