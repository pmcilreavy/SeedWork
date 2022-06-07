using Microsoft.EntityFrameworkCore;

namespace Todo.Web;

public static class DbContextBuilderExtensions
{
    public static void ConfigureDbContext(
        this DbContextOptionsBuilder options,
        bool enableSensitiveDataLogging,
        string databaseConnectionString,
        int databaseCommandTimeout,
        string migrationsAssembly)
    {
        if (enableSensitiveDataLogging) options.EnableSensitiveDataLogging();

        options.UseSqlite(databaseConnectionString,
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(migrationsAssembly);
                sqlOptions.CommandTimeout(databaseCommandTimeout == 0 ? 60 : databaseCommandTimeout);
            });
    }
}