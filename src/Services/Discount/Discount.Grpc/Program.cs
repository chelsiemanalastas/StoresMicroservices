using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var sqliteConnString = builder.Configuration.GetConnectionString("Database");

// Add services to the container.
services.AddGrpc();
services.AddDbContext<DiscountDbContext>(options => options.UseSqlite(sqliteConnString));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
