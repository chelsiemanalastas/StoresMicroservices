using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.Create(
            CustomerId.Of(Guid.Parse("3f1c2a8e-7b4e-4b8b-9c6d-1a2f3e4d5c01")),
            "Chelsie",
            "chelsie@test.com"),

        Customer.Create(
            CustomerId.Of(Guid.Parse("7a9d5c3b-2e8f-4f1a-8b7c-2d3e4f5a6b02")),
            "Matilda",
            "matilda@test.com"),

        Customer.Create(
            CustomerId.Of(Guid.Parse("c4e8f1a2-9b6d-4c7e-8a5f-3b2d1e6f7c03")),
            "Admin",
            "admin@test.com"),
    };

    public static IEnumerable<Product> Products =>
    new List<Product>
    {
        Product.Create(
            ProductId.Of(Guid.Parse("a1f3c2e4-5b6d-4c7a-8e9f-0a1b2c3d4e01")),
            "MacBook Pro M2",
            1500.50M),

        Product.Create(
            ProductId.Of(Guid.Parse("b2e4d3c5-6a7b-4d8c-9f0e-1b2c3d4e5f02")),
            "MacBook Air M3",
            1200.75M),

        Product.Create(
            ProductId.Of(Guid.Parse("c3d5e4f6-7b8c-4e9d-0a1f-2c3d4e5f6a03")),
            "Dell XPS 15",
            1400.00M),

        Product.Create(
            ProductId.Of(Guid.Parse("d4e6f5a7-8c9d-4f0e-1b2a-3d4e5f6a7b04")),
            "HP Spectre x360",
            1350.20M),

        Product.Create(
            ProductId.Of(Guid.Parse("e5f7a6b8-9d0e-4a1f-2c3b-4e5f6a7b8c05")),
            "Lenovo ThinkPad X1 Carbon",
            1600.00M),

        Product.Create(
            ProductId.Of(Guid.Parse("f6a8b7c9-0e1f-4b2a-3d4c-5f6a7b8c9d06")),
            "Asus ROG Zephyrus G14",
            1789.99M),

        Product.Create(
            ProductId.Of(Guid.Parse("07b9c8d0-1f2a-4c3b-4e5d-6a7b8c9d0e07")),
            "iPhone 15 Pro",
            999.99M),

        Product.Create(
            ProductId.Of(Guid.Parse("18c0d9e1-2a3b-4d4c-5f6e-7b8c9d0e1f08")),
            "Samsung Galaxy S24 Ultra",
            1199.75M),

        Product.Create(
            ProductId.Of(Guid.Parse("29d1e0f2-3b4c-4e5d-6a7f-8c9d0e1f2a09")),
            "iPad Pro 12.9",
            1100.00M),

        Product.Create(
            ProductId.Of(Guid.Parse("3ae2f103-4c5d-4f6e-7b8a-9d0e1f2a3b10")),
            "Sony WH-1000XM5 Headphones",
            399.89M),
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Chelsie", "Tur", "chelsie@test.com", "117522676", "Australia", "Sydney", "2000");
            var address2 = Address.Of("Matilda", "Tur", "matilda@test.com", "117522677", "Australia", "Sydney", "2000");
            var address3 = Address.Of("Admin", "Admin", "admin@test.com", "117522675", "Singapore", "Singapore", "169933");

            var payment1 = Payment.Of("Chelsie", "1234-5678-9012-3456", "12/30", "777", 1);
            var payment2 = Payment.Of("Matilda", "1234-5678-9012-3456", "12/30", "777", 2);
            var payment3 = Payment.Of("Admin", "9876-5432-1098-765", "07-27", "345", 2);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()), 
                CustomerId.Of(new Guid("3f1c2a8e-7b4e-4b8b-9c6d-1a2f3e4d5c01")), 
                OrderName.Of("Order 1_12345678909876543"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1,
                OrderStatus.Draft);
            order1.Add(ProductId.Of(new Guid("a1f3c2e4-5b6d-4c7a-8e9f-0a1b2c3d4e01")), 1, 1500.50M);
            order1.Add(ProductId.Of(new Guid("18c0d9e1-2a3b-4d4c-5f6e-7b8c9d0e1f08")), 2, 1199.75M);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("7a9d5c3b-2e8f-4f1a-8b7c-2d3e4f5a6b02")),
                OrderName.Of("Order 2_12345678909876543"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2,
                OrderStatus.Draft);
            order2.Add(ProductId.Of(new Guid("b2e4d3c5-6a7b-4d8c-9f0e-1b2c3d4e5f02")), 1, 1200.75M);
            order2.Add(ProductId.Of(new Guid("07b9c8d0-1f2a-4c3b-4e5d-6a7b8c9d0e07")), 2, 999.99M);
            order2.Add(ProductId.Of(new Guid("29d1e0f2-3b4c-4e5d-6a7f-8c9d0e1f2a09")), 3, 1100.00M);

            return new List<Order> { order1, order2 };
        }
    }

}
