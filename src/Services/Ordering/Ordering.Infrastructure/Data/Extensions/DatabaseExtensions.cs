using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomersAsync(context);
        await SeedProductsAsync(context);
        await SeedOrdersAndItemsAsync(context);
    }

    private static async Task SeedOrdersAndItemsAsync(ApplicationDbContext context)
    {
        if (context.Orders.Any())
            return;

        await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        if (context.Products.Any())
            return;

        await context.Products.AddRangeAsync(InitialData.Products);
        await context.SaveChangesAsync();
    }

    private static async Task SeedCustomersAsync(ApplicationDbContext context)
    {
        if (context.Customers.Any())
            return;

        await context.Customers.AddRangeAsync(InitialData.Customers);
        await context.SaveChangesAsync();
    }

}
