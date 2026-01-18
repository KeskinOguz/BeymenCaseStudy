using BeymenCaseStudy.WebAPI.Middlewares;
using CommonModule;
using CommonModule.Data;
using CommonModule.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NotificationModule.Application;
using NotificationModule.Infrastructure.Data;
using NotificationModule.Infrastructure.Repositories;
using OrderModule.Infrastructure.Data;
using OrderModule.Infrastructure.Repositories;
using StockModule.Application;
using StockModule.Infrastructure.Data;
using StockModule.Infrastructure.Repositories;
using StockModule.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
 });

builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddHostedService<OrderCreatedStockHandler>();
builder.Services.AddHostedService<OrderCreatedNotificationHandler>();

var orderConnectionString = builder.Configuration.GetConnectionString("OrderDb");
var stockConnectionString = builder.Configuration.GetConnectionString("StockDb");
var notificationConnectionString = builder.Configuration.GetConnectionString("NotificationDb");

builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseNpgsql($"{orderConnectionString}"));
builder.Services.AddDbContext<StockDbContext>(opt => opt.UseNpgsql($"{stockConnectionString}"));
builder.Services.AddDbContext<NotificationDbContext>(opt => opt.UseNpgsql($"{notificationConnectionString}"));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IModuleSeeder, StockDataSeeder>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

}

using (var scope = app.Services.CreateScope())
{
    var orderDb = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    await orderDb.Database.MigrateAsync();

    var stockDb = scope.ServiceProvider.GetRequiredService<StockDbContext>();
    await stockDb.Database.MigrateAsync();

    var notificationdb = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
    await notificationdb.Database.MigrateAsync();

    var seeders = scope.ServiceProvider.GetServices<IModuleSeeder>();

    foreach (var seeder in seeders)
    {
        await seeder.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
