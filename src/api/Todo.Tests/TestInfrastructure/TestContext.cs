using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Todo.Domain.Abstractions;
using Todo.Infrastructure.Persistance;
using Todo.Web;
using Xunit;

namespace Todo.Tests.TestInfrastructure;

public class TestContext : WebApplicationFactory<Program>, IAsyncLifetime
{
    public Task InitializeAsync() => Task.CompletedTask;

    Task IAsyncLifetime.DisposeAsync() => DisposeAsync().AsTask();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseEnvironment(Environments.Development);

        builder.ConfigureAppConfiguration((_, configurationBuilder) =>
        {
            configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json", false, true);
        });

        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });

        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var db = serviceProvider.GetRequiredService<TodoContext>();

            db.Database.SetConnectionString(configuration["DbConnectionString"]);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var ctx = serviceProvider.GetRequiredService<IWriteRepository>();

            ctx.Add(TestSeedData.TodoOne);

            ctx.SaveAsync(CancellationToken.None).GetAwaiter().GetResult();
        });
    }
}
