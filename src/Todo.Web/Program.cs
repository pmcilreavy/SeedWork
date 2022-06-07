using Microsoft.Extensions.DependencyInjection.Extensions;
using SeedWork;
using System.Reflection;
using SeedWork.Command;
using SeedWork.DomainEvent;
using Todo.Domain;
using Todo.Infrastructure;
using Todo.Web;

var builder = WebApplication.CreateBuilder(args);
var executable = Assembly.GetExecutingAssembly().Location;
var path = Path.GetDirectoryName(executable);

AppDomain.CurrentDomain.SetData("DataDirectory", path);

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

builder.Services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.TryAddScoped<IWriteRepository, TodoContext>();

builder.Services.AddDbContext<TodoContext>(
    options =>
    {
        options.ConfigureDbContext(
            false,
            "Data Source=|DataDirectory|Database.sqlite;",
            30,
            TheInfrastructure.Assembly.FullName!);
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var x = builder.Services
//    .OrderBy(o => o.ServiceType.Name)
//    .Select(o => o.ServiceType.FullName)
//    .Where(o => o.Contains("Seed") || o.Contains("Todo")).ToList();
//x.ForEach(s => Debug.WriteLine(s!));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
