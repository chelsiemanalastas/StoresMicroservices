using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var assembly = typeof(Program).Assembly;
var postgresConnString = builder.Configuration.GetConnectionString("Database")!;
var redisConnString = builder.Configuration.GetConnectionString("Redis")!;

// Application services
services.AddCarter();
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Data services
services.AddMarten(options =>
{
    options.Connection(postgresConnString);
    options.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();

services.AddScoped<IBasketRepository, BasketRepository>();
services.Decorate<IBasketRepository, CachedBasketRepository>();

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnString;
});

// Grpc services
services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

// Cross-cutting services
services.AddExceptionHandler<CustomExceptionHandler>();
services.AddHealthChecks()
    .AddNpgSql(postgresConnString)
    .AddRedis(redisConnString);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
