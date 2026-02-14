var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var assembly = typeof(Program).Assembly;
var connString = builder.Configuration.GetConnectionString("Database");

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
services.AddMarten(options =>
{
    options.Connection(connString!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    services.InitializeMartenWith<CatalogInitialData>();

services.AddValidatorsFromAssembly(assembly);
services.AddCarter();
services.AddExceptionHandler<CustomExceptionHandler>();

services.AddHealthChecks()
    .AddNpgSql(connString!);

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
