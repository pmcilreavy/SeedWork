using System.Data;
using Microsoft.Data.SqlClient;
using Todo.Domain.Abstractions.Query;

namespace Todo.Infrastructure.Persistance;

public class SqlServerConnectionProvider : IDbConnectionProvider
{
    private readonly string _connectionString;

    public SqlServerConnectionProvider(string connectionString) => _connectionString = connectionString;

    public IDbConnection Create() => new SqlConnection(_connectionString);
}