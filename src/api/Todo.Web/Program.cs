using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Todo.Application;
using Todo.Application.Abstractions.DomainEvent;
using Todo.Domain.Abstractions;
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
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Scan(scan => scan
                                      .FromAssemblies(TheApplication.Assembly)
                                      .AddClasses(classes =>
                                                      classes.AssignableTo(typeof(ICommandHandler<,>)))
                                      .AsImplementedInterfaces()
                                      .AsSelf()
                                      .WithTransientLifetime());

        builder.Services.Scan(scan => scan
                                      .FromAssemblies(TheApplication.Assembly)
                                      .AddClasses(classes =>
                                                      classes.AssignableTo(typeof(IDomainEventHandler<>)))
                                      .AsImplementedInterfaces()
                                      .AsSelf()
                                      .WithTransientLifetime());

        builder.Services.Scan(scan => scan
                                      .FromAssemblies(TheApplication.Assembly)
                                      .AddClasses(classes =>
                                                      classes.AssignableTo(typeof(IQueryHandler<>)))
                                      .AsImplementedInterfaces()
                                      .AsSelf()
                                      .WithTransientLifetime());

        builder.Services.Scan(scan => scan
                                      .FromAssemblies(TheApplication.Assembly)
                                      .AddClasses(classes =>
                                                      classes.AssignableTo(typeof(IQueryHandler<,>)))
                                      .AsImplementedInterfaces()
                                      .AsSelf()
                                      .WithTransientLifetime());

        builder.Services.AddDbContext<TodoContext>(
                                                   options =>
                                                   {
                                                       options.ConfigureDbContext(
                                                                                  false,
                                                                                  //"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Parnian\\documents\\visual studio 2017\\Projects\\Bank\\Bank\\Database.mdf;Integrated Security=True",
                                                                                  //"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Todos;Integrated Security=True",
                                                                                  builder.Configuration["DbConnectionString"],
                                                                                  30,
                                                                                  TheInfrastructure.Assembly.FullName!);
                                                   });

        builder.Services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        builder.Services.TryAddScoped<IWriteRepository, TodoContext>();
        builder.Services.TryAddScoped<IReadRepository, TodoContext>();
        builder.Services.TryAddScoped<IDbConnectionProvider>(provider => new SqlServerConnectionProvider(builder.Configuration["DbConnectionString"]));

        builder.Services.AddProblemDetails();
        builder.Services.AddCors(options => options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin()));
        builder.Services.AddControllers()
               .AddJsonOptions(o =>
               {
                   //o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                   o.JsonSerializerOptions.WriteIndented = true;
                   //o.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
               });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //var x = builder.Services
        //    .OrderBy(o => o.ServiceType.Name)
        //    .Select(o => o.ServiceType.FullName)
        //    .Where(o => o.Contains("Seed") || o.Contains("Todo")).ToList();
        //x.ForEach(s => Debug.WriteLine(s!));

        var app = builder.Build();
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
}
