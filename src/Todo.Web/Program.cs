using Microsoft.Extensions.DependencyInjection.Extensions;
using SeedWork;
using SeedWork.Command;
using SeedWork.DomainEvent;
using Todo.Domain;
using Todo.Infrastructure;
using Todo.Web;

public partial class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
    }
}
