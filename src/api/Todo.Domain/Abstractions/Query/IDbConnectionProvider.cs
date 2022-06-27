using System.Data;

namespace SeedWork.Query;

public interface IDbConnectionProvider
{
    IDbConnection Create();
}