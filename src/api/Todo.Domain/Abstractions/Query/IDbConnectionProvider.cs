using System.Data;

namespace Todo.Domain.Abstractions.Query;

public interface IDbConnectionProvider
{
    IDbConnection Create();
}