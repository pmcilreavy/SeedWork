using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using SeedWork.Query;

namespace Todo.Infrastructure;

public static class TheInfrastructure
{
    public static Assembly Assembly => typeof(TheInfrastructure).Assembly;
}

public class SqlServerConnectionProvider : IDbConnectionProvider
{
    private readonly string _connectionString;

    public SqlServerConnectionProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Create() => new SqlConnection(_connectionString);
}
