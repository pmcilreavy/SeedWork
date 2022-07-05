using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Todo.Application;
using Todo.Application.Abstractions;
using Todo.Application.Abstractions.DomainEvent;
using Todo.Domain.Abstractions.Command;
using Todo.Domain.Abstractions.DomainEvent;
using Todo.Domain.Abstractions.Query;
using Todo.Infrastructure;
using Todo.Infrastructure.Persistance;

namespace Todo.Web;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                     .MinimumLevel.Debug()
                     .Enrich.FromLogContext()
                     .WriteTo.Console()
                     .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            // https://andrewlock.net/using-scrutor-to-automatically-register-your-services-with-the-asp-net-core-di-container/
            builder.Services.Scan(scan => scan
                                          .FromAssemblies(TheApplication.Assembly)
                                          .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                                          .AsImplementedInterfaces()
                                          .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                                          .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<>)))
                                          .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>))));

            builder.Services.AddDbContext<TodoContext>(options =>
            {
                options.ConfigureDbContext(
                                           false,
                                           builder.Configuration["DbConnectionString"],
                                           30,
                                           TheInfrastructure.Assembly.FullName!);
            });

            builder.Services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            builder.Services.TryAddScoped<ITodoWriteRepository, TodoContext>();
            builder.Services.TryAddScoped<IDbConnectionProvider>(provider => new SqlServerConnectionProvider(builder.Configuration["DbConnectionString"]));

            builder.Services.AddProblemDetails();
            builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin()));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSerilogRequestLogging();
            app.UseProblemDetails();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
