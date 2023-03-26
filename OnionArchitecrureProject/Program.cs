using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer.Service.Implementation;
using System.Reflection;
using NLog;
using NLog.Web;
using WebAPI_Layer;
using WebAPI_Layer.Extensions;

var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    string connection = builder.Configuration.GetConnectionString("DefaultConnection");

    // Add services to the container.
    builder.Services.AddDbContext<AppDbContext>(con => con.UseSqlServer(connection))
        .AddUnitOfWork()
        .AddCustomRepository<Book, BookService>()
        .AddCustomRepository<User, UserService>()
        .AddCustomRepository<Order, OrderService>();

    var assembly = Assembly.GetAssembly(typeof(MappingProfile));
    builder.Services.AddAutoMapper(assembly);
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

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
}
catch (Exception e)
{
    logger.Error(e);
    throw (e);
}
finally
{
    NLog.LogManager.Shutdown();
}

