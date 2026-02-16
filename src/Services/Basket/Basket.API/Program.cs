var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var assembly = typeof(Program).Assembly;
var connString = builder.Configuration.GetConnectionString("Database")!;

services.AddCarter();
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
services.AddMarten(options =>
{
    options.Connection(connString);
    options.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();

services.AddScoped<IBasketRepository, BasketRepository>();
services.Decorate<IBasketRepository, CachedBasketRepository>();
services.AddExceptionHandler<CustomExceptionHandler>();

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
