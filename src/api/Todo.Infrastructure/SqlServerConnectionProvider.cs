using System.Data;
using Microsoft.Data.SqlClient;
using SeedWork.Query;

namespace Todo.Infrastructure;

public class SqlServerConnectionProvider : IDbConnectionProvider
{
    private readonly string _connectionString;

    public SqlServerConnectionProvider(string connectionString) => _connectionString = connectionString;

    public IDbConnection Create() => new SqlConnection(_connectionString);
}