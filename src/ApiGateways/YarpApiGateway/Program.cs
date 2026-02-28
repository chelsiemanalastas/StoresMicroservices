var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapReverseProxy();

app.Run();
