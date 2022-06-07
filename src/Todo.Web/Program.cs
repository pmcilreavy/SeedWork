using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SeedWork;
using SeedWork.CommandDispatcher;
using System.Reflection;
using Todo.Domain;
using Todo.Infrastructure;
using Todo.Web;

var builder = WebApplication.CreateBuilder(args);

var executable = Assembly.GetExecutingAssembly().Location;
var path = Path.GetDirectoryName(executable);
AppDomain.CurrentDomain.SetData("DataDirectory", path);

// Add services to the container.

builder.Services.Scan(scan => scan
    .FromAssemblies(TheDomain.Assembly)
    .AddClasses(classes =>
        classes.AssignableTo(typeof(ICommandHandler<,>)))
    .AsImplementedInterfaces()
    .AsSelf()
    .WithTransientLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblies(TheDomain.Assembly)
    .AddClasses(classes =>
        classes.AssignableTo(typeof(IDomainEventHandler<>)))
    .AsImplementedInterfaces()
    .AsSelf()
    .WithTransientLifetime());

//    .WithTransientLifetime());


builder.Services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.TryAddScoped<IWriteRepository, TodoContext>();

builder.Services.AddDbContext<TodoContext>(
    options =>
    {
        options.ConfigureDbContext(
            false,
            "Data Source=|DataDirectory|Database.sqlite;",
            60,
            TheInfrastructure.Assembly.FullName!);
    }, ServiceLifetime.Scoped);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var x = builder.Services
    .OrderBy(o => o.ServiceType.Name)
    .Select(o => o.ServiceType.FullName)
    .Where(o => o.Contains("Seed") || o.Contains("Todo")).ToList();

x.ForEach(s => Debug.WriteLine(s!));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();