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

        options.ConfigureWarnings(builder => builder.Throw());
        options.EnableDetailedErrors();
        options.EnableServiceProviderCaching();
        options.EnableThreadSafetyChecks();

        options.UseSqlServer(databaseConnectionString,
            sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                sqlOptions.MigrationsAssembly(migrationsAssembly);
                sqlOptions.CommandTimeout(databaseCommandTimeout == 0 ? 60 : databaseCommandTimeout);
            });
    }
}